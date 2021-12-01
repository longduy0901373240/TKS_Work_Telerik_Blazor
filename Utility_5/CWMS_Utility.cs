using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility_5
{
    public class CWMS_Utility
    {
        // Tách chuỗi Location Plan
        public static int GetLocation_Plan_ID(string p_strLocation_Plan_Text)
        {
            int v_iRes = 0;
            p_strLocation_Plan_Text = p_strLocation_Plan_Text.Trim();
            switch (p_strLocation_Plan_Text.ToUpper())
            {
                case "GOOD":
                    v_iRes = 1;
                    break;
                case "BEST":
                    v_iRes = 2;
                    break;
                case "TRANSIT":
                    v_iRes = 3;
                    break;
            }

            if (v_iRes == 0)
            {
                string v_strNum = p_strLocation_Plan_Text.Split('-')[0];
                switch (v_strNum.ToUpper())
                {
                    case "1":
                        v_iRes = 1;
                        break;
                    case "2":
                        v_iRes = 2;
                        break;
                    case "3":
                        v_iRes = 3;
                        break;
                }
            }

            return v_iRes;
        }

        public static bool Check_Valid_Date(string p_strDate)
        {
            try
            {
                if (p_strDate.Length == 10)
                {
                    DateTime v_dtm = CUtility.Convert_String_To_Datetime(p_strDate, "dd/MM/yyyy");
                    return true;
                }

                if (p_strDate.Length == 16)
                {
                    DateTime v_dtm = CUtility.Convert_String_To_Datetime(p_strDate, "dd/MM/yyyy HH:mm");
                    return true;
                }

                if (p_strDate.Length == 19)
                {
                    DateTime v_dtm = CUtility.Convert_String_To_Datetime(p_strDate, "dd/MM/yyyy HH:mm:ss");
                    return true;
                }
            }

            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public static DateTime Get_Vietnam_Datetime_From_String(string p_strDate)
        {
            try
            {
                if (p_strDate.Length == 10)
                    return CUtility.Convert_String_To_Datetime(p_strDate, "dd/MM/yyyy");

                if (p_strDate.Length == 16)
                    return CUtility.Convert_String_To_Datetime(p_strDate, "dd/MM/yyyy HH:mm");

                if (p_strDate.Length == 19)
                    return CUtility.Convert_String_To_Datetime(p_strDate, "dd/MM/yyyy HH:mm:ss");
            }

            catch (Exception)
            {
                throw new Exception("Ngày không đúng định dạng.");
            }

            return CConst.DTM_VALUE_NULL;
        }

        public static IDictionary<string, int> Get_Dic_SKU_Dac_Biet_Daikin()
        {
            IDictionary<string, int> v_DicSKU_Continue = new Dictionary<string, int>();

            v_DicSKU_Continue.Add("SUACHUA", 1);
            v_DicSKU_Continue.Add("BAOTRI", 1);
            v_DicSKU_Continue.Add("VAN CHUYEN", 1);
            v_DicSKU_Continue.Add("VATTUPHU", 1);
            //20210413 NgocHB bổ sung thêm vào DicSKU_Continue:
            v_DicSKU_Continue.Add("KIEMTRA", 1);
            v_DicSKU_Continue.Add("OTHERS", 1);
            //20210427 NgocHB bổ sung thêm vào DicSKU_Continue:
            v_DicSKU_Continue.Add("T&C", 1);
            v_DicSKU_Continue.Add("AIRNETMAINTENANCE", 1);
            v_DicSKU_Continue.Add("ANTICORROSION", 1);

            return v_DicSKU_Continue;
        }
    }
}
