using Microsoft.AspNetCore.Components.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility_5
{
    public class CExcel_Controller
    {
        const int maxFileSize = 10485760;
        ///<summary>
        ///Check excel file
        ///</summary>
        ///<param name="p_strFileName">đuôi file</param>
        ///<returns></returns>
        private bool Check_Excel_File_Type(string p_strFileName)
        {
            if (p_strFileName != ".xls" && p_strFileName != ".xlsx")
                return false;
            return true;
        }
        ///<summary>
        ///Save file input and return url file save
        ///</summary>
        ///<param name="p_file">File select when input</param>
        ///<param name="v_strWebRootPath">đường dẫn đến wwwroot</param>
        public async Task<string> SaveFile(IBrowserFile p_file, string v_strWebRootPath)
        {
            try
            {
                //cắt đuôi file
                string v_strTextExl = Path.GetExtension(p_file.Name).ToLower();
                //kiểm tra file
                if (!Check_Excel_File_Type(v_strTextExl))
                    throw new Exception("Không phải định dạng excel nên không đọc được.");
                // đường dẫn đến thư mục trong wwwroot
                var relativePath = Path.Combine("FileManagement\\Upload");
                var dirToSave = Path.Combine(v_strWebRootPath, relativePath);
                var di = new DirectoryInfo(dirToSave);
                // Create folder theo Path di nếu không có folder
                if (!di.Exists)
                    di.Create();
                string v_strnewFileName = $"{DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss")}-{p_file.Name}";
                var filePath = Path.Combine(dirToSave, v_strnewFileName);
                using (var stream = p_file.OpenReadStream(maxFileSize))
                {
                    using (var mstream = new MemoryStream())
                    {
                        await stream.CopyToAsync(mstream);
                        await File.WriteAllBytesAsync(filePath, mstream.ToArray());
                    }
                }
                var url = Path.Combine(v_strWebRootPath, relativePath, v_strnewFileName);
                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        ///<summary>
        ///Get File save in wwwroot
        ///</summary>
        ///<param name="p_strPath_File">link folder save file</param>
        public FileInfo Get_File(string p_strPath_File)
        {
            try
            {
                FileInfo v_fileInfo = new FileInfo(p_strPath_File);
                return v_fileInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///<summary>
        ///get data in the range Cell_From to Cell_End vd A1-B3
        ///</summary>
        ///<param name="p_fileInfo"> File iput in wwwroot </param>
        ///<param name="p_strCell_End"></param>
        ///<param name="p_strCell_From"></param>
        public DataTable List_Range_Value(string p_strCell_From, string p_strCell_End, FileInfo p_fileInfo)
        {
            return List_Range_Value(1, p_strCell_From, p_strCell_End, p_fileInfo);
        }
        ///<summary>
        ///get data in the range Cell_From to Cell_End vd A1-B3
        ///</summary>
        ///<param name="p_fileInfo"> File iput in wwwroot </param>
        ///<param name="p_intSheet_Index"></param>
        ///<param name="p_strCell_End"></param>
        ///<param name="p_strCell_From"></param>
        public DataTable List_Range_Value(int p_intSheet_Index, string p_strCell_From, string p_strCell_End, FileInfo p_fileInfo)
        {
            DataTable v_dt = new DataTable();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage v_excelPackage = new ExcelPackage(p_fileInfo))
            {
                ExcelWorksheet v_excelWorksheet = v_excelPackage.Workbook.Worksheets[$"Sheet{p_intSheet_Index}"];
                int v_intRowStart = v_excelWorksheet.Cells[p_strCell_From].End.Row;
                int v_intColumnStart = v_excelWorksheet.Cells[p_strCell_From].End.Column;
                int v_intRowEnd = v_excelWorksheet.Cells[p_strCell_End].End.Row;
                int v_intColumnEnd = v_excelWorksheet.Cells[p_strCell_End].End.Column;
                foreach (var cell in v_excelWorksheet.Cells[v_intRowStart, v_intColumnStart, v_intRowStart, v_intColumnEnd])
                {
                    string v_strColumnName = cell.Text.Trim();
                    v_dt.Columns.Add(v_strColumnName);
                }
                for (int i = v_intRowStart; i <= v_intRowEnd; i++)
                {
                    var row = v_excelWorksheet.Cells[i, v_intColumnStart, i, v_intColumnEnd];
                    DataRow v_Row = v_dt.NewRow();
                    int v_intColumn = 0;
                    for (int j = v_intColumnStart; j <= v_intColumnEnd; j++)
                    {
                        v_Row[v_intColumn] = row[i, j].Value;
                        v_intColumn++;
                    }
                    v_dt.Rows.Add(v_Row);
                }
            }
            return v_dt;
        }
        ///<summary>
        ///get data in the range Cell_From to Cell_End vd A1-B3
        ///</summary>
        ///<param name="p_fileInfo"> File iput in wwwroot </param>
        ///<param name="p_strCell_End"></param>
        ///<param name="p_strCell_From"></param>
        public DataTable List_Range_Value(string p_strCell_From, string p_strCell_End, string p_strPath)
        {
            return List_Range_Value(1, p_strCell_From, p_strCell_End, p_strPath);
        }
        ///<summary>
        ///get data in the range Cell_From to Cell_End vd A1-B3
        ///</summary>
        ///<param name="p_strPath"> File iput in wwwroot </param>
        ///<param name="p_intSheet_Index"></param>
        ///<param name="p_strCell_End"></param>
        ///<param name="p_strCell_From"></param>
        public DataTable List_Range_Value(int p_intSheet_Index, string p_strCell_From, string p_strCell_End, string p_strPath)
        {
            FileInfo v_fileInfo = Get_File(p_strPath);
            return List_Range_Value(p_intSheet_Index, p_strCell_From, p_strCell_End, v_fileInfo);
        }
        /// <summary>
        /// get data in the range Cell_From to Cell_End + Dimension.End.Row vd A1-B3
        /// </summary>
        /// <param name="p_strCell_End"></param>
        /// <param name="p_strCell_From"></param>
        /// <param name="p_strPath"></param>
        public DataTable List_Range_Value_To_End(string p_strCell_From, string p_strCell_End, string p_strPath)
        {
            return List_Range_Value_To_End(1, p_strCell_From, p_strCell_End, p_strPath);
        }
        /// <summary>
        /// get data in the range Cell_From to Cell_End + Dimension.End.Row vd A1-B3
        /// </summary>
        /// <param name="p_strCell_End"></param>
        /// <param name="p_strCell_From"></param>
        /// <param name="p_strPath"></param>
        public DataTable List_Range_Value_To_End(int p_intSheet_Index, string p_strCell_From, string p_strCell_End, string p_strPath)
        {
            FileInfo v_fileInfo = Get_File(p_strPath);
            return List_Range_Value_To_End(p_intSheet_Index, p_strCell_From, p_strCell_End, v_fileInfo);
        }
        /// <summary>
        /// get data in the range Cell_From to Cell_End + Dimension.End.Row vd A1-B3
        /// </summary>
        /// <param name="p_strCell_End"></param>
        /// <param name="p_strCell_From"></param>
        /// <param name="p_fileInfo"></param>
        public DataTable List_Range_Value_To_End(string p_strCell_From, string p_strCell_End, FileInfo p_fileInfo)
        {
            return List_Range_Value_To_End(1, p_strCell_From, p_strCell_End, p_fileInfo);
        }
        /// <summary>
        /// get data in the range Cell_From to Cell_End + Dimension.End.Row vd A1-B3
        /// </summary>
        /// <param name="p_strCell_End"></param>
        /// <param name="p_strCell_From"></param>
        /// <param name="p_fileInfo"></param>
        public DataTable List_Range_Value_To_End(int p_intSheet_Index, string p_strCell_From, string p_strCell_End, FileInfo p_fileInfo)
        {
            DataTable v_dt = new DataTable();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage v_excelPackage = new ExcelPackage(p_fileInfo))
            {
                ExcelWorksheet v_excelWorksheet = v_excelPackage.Workbook.Worksheets[$"Sheet{p_intSheet_Index}"];
                int v_intRowStart = v_excelWorksheet.Cells[p_strCell_From].End.Row;
                int v_intColumnStart = v_excelWorksheet.Cells[p_strCell_From].End.Column;
                int v_intRowEnd = v_excelWorksheet.Dimension.End.Row;
                p_strCell_End = p_strCell_End + v_intRowEnd.ToString();
                int v_intColumnEnd = v_excelWorksheet.Cells[p_strCell_End].End.Column;
                foreach (var cell in v_excelWorksheet.Cells[v_intRowStart, v_intColumnStart, v_intRowStart, v_intColumnEnd])
                {
                    string v_strColumnName = cell.Text.Trim();
                    v_dt.Columns.Add(v_strColumnName);
                }
                for (int i = v_intRowStart + 1; i <= v_intRowEnd; i++)
                {
                    var row = v_excelWorksheet.Cells[i, v_intColumnStart, i, v_intColumnEnd];
                    DataRow v_Row = v_dt.NewRow();
                    int v_intColumn = 0;
                    for (int j = v_intColumnStart; j <= v_intColumnEnd; j++)
                    {
                        v_Row[v_intColumn] = row[i, j].Value;
                        v_intColumn++;
                    }
                    v_dt.Rows.Add(v_Row);
                }
            }
            return v_dt;
        }
        /// <summary>
        /// determine the starting cell of the data range
        /// </summary>
        /// <param name="p_fileInfo"></param>
        public string Cell_From(FileInfo p_fileInfo)
        {
            return Cell_From(1, p_fileInfo);
        }
        /// <summary>
        /// determine the starting cell of the data range
        /// </summary>
        /// <param name="p_fileInfo"></param>
        /// <param name="p_intSheet_Index"></param>
        public string Cell_From(int p_intSheet_Index, FileInfo p_fileInfo)
        {
            string v_intCell_From;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage v_excelPackage = new ExcelPackage(p_fileInfo))
            {
                ExcelWorksheet v_excelWorksheet = v_excelPackage.Workbook.Worksheets[$"Sheet{p_intSheet_Index}"];
                v_intCell_From = v_excelWorksheet.Dimension.Start.Address.ToString();
            }
            return v_intCell_From;
        }
        /// <summary>
        /// determine the ending cell of the data range
        /// </summary>
        /// <param name="p_fileInfo"></param>
        public string Cell_End(FileInfo p_fileInfo)
        {
            return Cell_End(1, p_fileInfo);
        }
        /// <summary>
        /// determine the ending cell of the data range
        /// </summary>
        /// <param name="p_fileInfo"></param>
        /// <param name="p_intSheet_Index"></param>
        public string Cell_End(int p_intSheet_Index, FileInfo p_fileInfo)
        {
            string v_intCell_End;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage v_excelPackage = new ExcelPackage(p_fileInfo))
            {
                ExcelWorksheet v_excelWorksheet = v_excelPackage.Workbook.Worksheets[$"Sheet{p_intSheet_Index}"];
                v_intCell_End = v_excelWorksheet.Dimension.End.Address.ToString();
            }
            return v_intCell_End;
        }
        /// <summary>
        /// determine the starting Row of the data range
        /// </summary>
        /// <param name="p_fileInfo"></param>
        public int Row_From(FileInfo p_fileInfo)
        {
            return Row_From(1, p_fileInfo);
        }
        /// <summary>
        /// determine the starting Row of the data range
        /// </summary>
        /// <param name="p_fileInfo"></param>
        /// <param name="p_intSheet_Index"></param>
        public int Row_From(int p_intSheet_Index, FileInfo p_fileInfo)
        {
            int v_intRow_From;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage v_excelPackage = new ExcelPackage(p_fileInfo))
            {
                ExcelWorksheet v_excelWorksheet = v_excelPackage.Workbook.Worksheets[$"Sheet{p_intSheet_Index}"];
                v_intRow_From = v_excelWorksheet.Dimension.Start.Row;
            }
            return v_intRow_From;
        }
        /// <summary>
        /// determine the starting column of the data range
        /// </summary>
        /// <param name="p_fileInfo"></param>
        public int Column_From(FileInfo p_fileInfo)
        {
            return Column_From(1, p_fileInfo);
        }
        /// <summary>
        /// determine the starting column of the data range
        /// </summary>
        /// <param name="p_fileInfo"></param>
        /// <param name="p_intSheet_Index"></param>
        public int Column_From(int p_intSheet_Index, FileInfo p_fileInfo)
        {
            int v_intColumn_From;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage v_excelPackage = new ExcelPackage(p_fileInfo))
            {
                ExcelWorksheet v_excelWorksheet = v_excelPackage.Workbook.Worksheets[$"Sheet{p_intSheet_Index}"];
                v_intColumn_From = v_excelWorksheet.Dimension.Start.Column;
            }
            return v_intColumn_From;
        }
        /// <summary>
        /// determine the ending row of the data range
        /// </summary>
        /// <param name="p_fileInfo"></param>
        public int Row_End(FileInfo p_fileInfo)
        {
            return Row_End(1, p_fileInfo);
        }
        /// <summary>
        /// determine the ending row of the data range
        /// </summary>
        /// <param name="p_fileInfo"></param>
        /// <param name="p_intSheet_Index"></param>
        public int Row_End(int p_intSheet_Index, FileInfo p_fileInfo)
        {
            int v_intRow_End;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage v_excelPackage = new ExcelPackage(p_fileInfo))
            {
                ExcelWorksheet v_excelWorksheet = v_excelPackage.Workbook.Worksheets[$"Sheet{p_intSheet_Index}"];
                v_intRow_End = v_excelWorksheet.Dimension.End.Row;
            }
            return v_intRow_End;
        }
        /// <summary>
        /// determine the ending column of the data range
        /// </summary>
        /// <param name="p_fileInfo"></param>
        public int Column_End(FileInfo p_fileInfo)
        {
            return Column_End(1, p_fileInfo);
        }
        /// <summary>
        /// determine the ending column of the data range
        /// </summary>
        /// <param name="p_fileInfo"></param>
        /// <param name="p_intSheet_Index"></param>
        public int Column_End(int p_intSheet_Index, FileInfo p_fileInfo)
        {
            int v_intColumn_End;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage v_excelPackage = new ExcelPackage(p_fileInfo))
            {
                ExcelWorksheet v_excelWorksheet = v_excelPackage.Workbook.Worksheets[$"Sheet{p_intSheet_Index}"];
                v_intColumn_End = v_excelWorksheet.Dimension.End.Column;
            }
            return v_intColumn_End;
        }
        ///<summary>
        ///Export file excel from list data
        ///</summary>
        ///<param name="p_strCell_From"></param>
        ///<param name="p_arrData"></param>
        ///<param name="p_dicHeader_Field"></param>
        public byte[] Export_Excel<T>(string p_strCell_From, List<T> p_arrData, Dictionary<int, string[]> p_dicHeader_Field, Dictionary<int, string[]> p_dicBold_Text, Dictionary<int, string[]> p_dicBkd_Color)
        {
            return Export_Excel<T>(1, p_strCell_From, p_arrData, p_dicHeader_Field, p_dicBold_Text, p_dicBkd_Color);
        }
        ///<summary>
        ///Export file excel from list data
        ///</summary>
        ///<param name="p_intSheet_Index"></param>
        ///<param name="p_strCell_From"></param>
        ///<param name="p_arrData"></param>
        ///<param name="p_dicHeader_Field"></param>
        public byte[] Export_Excel<T>(int p_intSheet_Index, string p_strCell_From, List<T> p_arrData, Dictionary<int, string[]> p_dicHeader_Field, Dictionary<int, string[]> p_dicBold_Text, Dictionary<int, string[]> p_dicBkd_Color)
        {
            byte[] v_fileContents;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage v_excelPackage = new ExcelPackage())
            {
                var v_excelWorksheet = v_excelPackage.Workbook.Worksheets.Add($"Sheet{p_intSheet_Index}");
                v_excelWorksheet.DefaultRowHeight = 12;
                if (p_dicBold_Text != null)
                {
                    for (int i = 1; i <= p_dicBold_Text.Count; i++)
                    {
                        v_excelWorksheet = Bold_Text_List_Range_Value(p_dicBold_Text[i][0], p_dicBold_Text[i][1], v_excelWorksheet);
                    }
                }
                if (p_dicBkd_Color != null)
                {
                    for (int i = 1; i <= p_dicBkd_Color.Count; i++)
                    {
                        v_excelWorksheet = Background_Color_List_Range_Value(p_dicBkd_Color[i][0], p_dicBkd_Color[i][1], p_dicBkd_Color[i][2], v_excelWorksheet);
                    }
                }
                int v_intRowStart = v_excelWorksheet.Cells[p_strCell_From].End.Row;
                int v_intColumnStart = v_excelWorksheet.Cells[p_strCell_From].End.Column;
                int v_intRowEnd = p_arrData.Count() + 1;
                int v_intColummEnd = p_dicHeader_Field.Count() + v_intColumnStart;
                using (var range = v_excelWorksheet.Cells[1, v_intColumnStart, 1, v_intColummEnd])
                {
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                int v_intKey_Value = 1;
                for (int i = v_intColumnStart; i < v_intColummEnd; i++)
                {
                    v_excelWorksheet.Cells[v_intRowStart, i].Value = p_dicHeader_Field[v_intKey_Value][0].ToString();
                    v_intKey_Value++;
                }
                foreach (T item in p_arrData)
                {
                    v_intRowStart++;
                    v_intKey_Value = 1;
                    for (int i = v_intColumnStart; i < v_intColummEnd; i++)
                    {
                        v_excelWorksheet.Cells[v_intRowStart, i].Value = item.GetType().GetProperty(p_dicHeader_Field[v_intKey_Value][1]).GetValue(item).ToString();
                        v_intKey_Value++;
                    }
                }
                v_excelWorksheet.Cells[v_excelWorksheet.Dimension.Address].AutoFitColumns();
                v_fileContents = v_excelPackage.GetAsByteArray();
            }
            return v_fileContents;
        }
        ///<summary>
        ///get cell value
        ///</summary>
        ///<param name="p_fileInfo"></param>
        ///<param name="p_strCell"></param>
        public string Get_Cell_Value(string p_strCell, FileInfo p_fileInfo)
        {
            return Get_Cell_Value(1, p_strCell, p_fileInfo);
        }
        ///<summary>
        ///get cell value
        ///</summary>
        ///<param name="p_fileInfo"></param>
        ///<param name="p_strCell"></param>
        ///<param name="p_intSheet_Index"></param>
        public string Get_Cell_Value(int p_intSheet_Index, string p_strCell, FileInfo p_fileInfo)
        {
            string v_strCell_Value;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage v_excelPackage = new ExcelPackage(p_fileInfo))
            {
                ExcelWorksheet v_excelWorksheet = v_excelPackage.Workbook.Worksheets[$"Sheet{p_intSheet_Index}"];
                v_strCell_Value = v_excelWorksheet.Cells[p_strCell].Value.ToString();
            }
            return v_strCell_Value;
        }
        ///<summary>
        ///set cell value
        ///</summary>
        ///<param name="p_fileInfo"></param>
        ///<param name="p_strCell"></param>
        ///<param name="p_objValue_Cell"></param>
        public void Set_Cell_Value<T>(string p_strCell, T p_objValue_Cell, FileInfo p_fileInfo)
        {
            Set_Cell_Value<T>(1, p_strCell, p_objValue_Cell, p_fileInfo);
        }
        ///<summary>
        ///set cell value
        ///</summary>
        ///<param name="p_intSheet_Index"></param>
        ///<param name="p_fileInfo"></param>
        ///<param name="p_strCell"></param>
        ///<param name="p_objValue_Cell"></param>
        public void Set_Cell_Value<T>(int p_intSheet_Index, string p_strCell, T p_objValue_Cell, FileInfo p_fileInfo)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage v_excelPackage = new ExcelPackage(p_fileInfo))
            {
                ExcelWorksheet v_excelWorksheet = v_excelPackage.Workbook.Worksheets[$"Sheet{p_intSheet_Index}"];
                v_excelWorksheet.Cells[p_strCell].Value = p_objValue_Cell.ToString();
            }
        }
        ///<summary>
        ///</summary>
        public ExcelWorksheet Bold_Text_List_Range_Value(string p_strCell_From, string p_strCell_End, ExcelWorksheet p_excelWorksheet)
        {
            ExcelWorksheet v_excelWorksheet = p_excelWorksheet;
            using (var range = v_excelWorksheet.Cells[$"{p_strCell_From}:{p_strCell_End}"])
            {

                range.Style.Font.Bold = true;
            }
            return v_excelWorksheet;
        }
        ///<summary>
        ///</summary>
        public ExcelWorksheet Background_Color_List_Range_Value(string p_strCell_From, string p_strCell_End, string p_strColor, ExcelWorksheet p_excelWorksheet)
        {
            Color v_colFromHex = System.Drawing.ColorTranslator.FromHtml($"{p_strColor}");
            p_excelWorksheet.Cells[$"{p_strCell_From}:{p_strCell_End}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            p_excelWorksheet.Cells[$"{p_strCell_From}:{p_strCell_End}"].Style.Fill.BackgroundColor.SetColor(v_colFromHex);
            return p_excelWorksheet;
        }
    }
}
