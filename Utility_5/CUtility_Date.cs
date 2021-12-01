using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility_5
{
    public class CUtility_Date
    {
        /// <summary>
        /// Convert ngày về đầu ngày.
        /// VD: 03/01/2017 14:22:11 thì sẽ chuyển thành 03/01/2017 00:00:00
        /// </summary>
        /// <param name="p_dtmDate"></param>
        /// <returns></returns>
        public static DateTime Convert_To_Dau_Ngay(DateTime p_dtmDate)
        {
            DateTime v_dtmRes = p_dtmDate;
            v_dtmRes = CUtility.Convert_String_To_Datetime(p_dtmDate.ToString("dd/MM/yyyy") + " 00:00:00", "dd/MM/yyyy HH:mm:ss");
            return v_dtmRes;
        }

        /// <summary>
        /// Convert ngày về cuối ngày.
        /// VD: 03/01/2017 14:22:11 thì sẽ chuyển thành 03/01/2017 23:59:59
        /// </summary>
        /// <param name="p_dtmDate"></param>
        /// <returns></returns>
        public static DateTime Convert_To_Cuoi_Ngay(DateTime p_dtmDate)
        {
            DateTime v_dtmRes = p_dtmDate;
            v_dtmRes = CUtility.Convert_String_To_Datetime(p_dtmDate.ToString("dd/MM/yyyy") + " 23:59:59", "dd/MM/yyyy HH:mm:ss");
            return v_dtmRes;
        }

        /// <summary>
        /// Convert ngày về đầu tháng
        /// VD: 13/05/2017 14:22:11 thì sẽ chuyển thành 01/05/2017 00:00:00
        /// </summary>
        /// <param name="p_dtmData"></param>
        /// <returns></returns>
        public static DateTime Convert_To_Dau_Thang(DateTime p_dtmData)
        {
            return CUtility.Convert_String_To_Datetime("01/" + p_dtmData.Month.ToString() + "/" + p_dtmData.Year.ToString() + " 00:00:00", "dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Convert ngày về cuối tháng
        /// VD: 13/05/2017 14:22:11 thì sẽ chuyển thành 31/05/2017 23:59:59
        /// </summary>
        /// <param name="p_dtmData"></param>
        /// <returns></returns>
        public static DateTime Convert_To_Cuoi_Thang(DateTime p_dtmData)
        {
            return Convert_To_Cuoi_Ngay(Convert_To_Dau_Thang(p_dtmData.AddMonths(1)).AddDays(-1));
        }

        /// <summary>
        /// Convert ngày về đầu tuần
        /// VD: 13/05/2017 14:22:11 thì sẽ chuyển thành 08/05/2017 00:00:00 do 08/05 là thứ 2
        /// </summary>
        /// <param name="p_dtmData"></param>
        /// <returns></returns>
        public static DateTime Convert_To_Dau_Tuan(DateTime p_dtmData)
        {
            DateTime v_dtmRes = p_dtmData;

            while (v_dtmRes.DayOfWeek != DayOfWeek.Monday)
                v_dtmRes = v_dtmRes.AddDays(-1);

            return Convert_To_Dau_Ngay(v_dtmRes);
        }

        /// <summary>
        /// Convert ngày về cuối tuần
        /// VD: 13/05/2017 14:22:11 thì sẽ chuyển thành 14/05/2017 23:59:59 do 14/05 là chủ nhật
        /// </summary>
        /// <param name="p_dtmData"></param>
        /// <returns></returns>
        public static DateTime Convert_To_Cuoi_Tuan(DateTime p_dtmData)
        {
            DateTime v_dtmRes = p_dtmData;

            while (v_dtmRes.DayOfWeek != DayOfWeek.Sunday)
                v_dtmRes = v_dtmRes.AddDays(1);

            return Convert_To_Cuoi_Ngay(v_dtmRes);
        }

        /// <summary>
        /// Convert ngày về đầu năm
        /// VD: 13/05/2017 14:22:11 thì sẽ chuyển thành 01/01/2017 00:00:00
        /// </summary>
        /// <param name="p_dtmData"></param>
        /// <returns></returns>
        public static DateTime Convert_To_Dau_Nam(DateTime p_dtmData)
        {
            return CUtility.Convert_String_To_Datetime("01/01/" + p_dtmData.Year.ToString() + " 00:00:00", "dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Convert ngày về cuối năm
        /// VD: 13/05/2017 14:22:11 thì sẽ chuyển thành 31/12/2017 23:59:59
        /// </summary>
        /// <param name="p_dtmData"></param>
        /// <returns></returns>
        public static DateTime Convert_To_Cuoi_Nam(DateTime p_dtmData)
        {
            return CUtility.Convert_String_To_Datetime("31/12/" + p_dtmData.Year.ToString() + " 23:59:59", "dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Lấy ngày tương lai của 1 ngày chỉ định loại trừ chủ nhật ra.
        /// </summary>
        /// <param name="p_dtmNow"></param>
        /// <param name="p_iDay"></param>
        /// <returns></returns>
        public static DateTime Add_Day_Ngoai_Tru_Chu_Nhat(DateTime p_dtmNow, int p_iDay)
        {
            int v_iCount = 0;
            int v_iSub = 1;
            if (p_iDay < 0)
                v_iSub = -1;
            DateTime v_dtRes = p_dtmNow;

            while (v_iCount < Math.Abs(p_iDay))
            {
                v_iCount++;
                v_dtRes = v_dtRes.AddDays(v_iSub);

                while (v_dtRes.DayOfWeek == DayOfWeek.Sunday)
                    v_dtRes = v_dtRes.AddDays(v_iSub);
            }

            return v_dtRes;
        }

        /// <summary>
        /// Dùng lấy dòng mô tả về thời điểm trước đó
        /// VD: 2 phút trước, 5 phút trước, 2h trước...
        /// </summary>
        /// <param name="p_dtmData"></param>
        /// <param name="p_dtmSource"></param>
        /// <returns></returns>
        public static string Get_Mo_Ta_Thoi_Gian(DateTime p_dtmData)
        {
            TimeSpan v_ts = DateTime.Now - p_dtmData;
            if (v_ts.TotalSeconds < 60)
                return Math.Round(v_ts.TotalSeconds, 0).ToString("######") + " giây trước";

            if (v_ts.TotalMinutes < 60)
                return Math.Round(v_ts.TotalMinutes, 0).ToString("######") + " phút trước";

            if (v_ts.TotalHours < 24)
                return Math.Round(v_ts.TotalHours, 0).ToString("######") + " giờ trước";

            return Math.Round(v_ts.TotalDays, 0).ToString("######") + " ngày trước";
        }

        public static DateTime Add_Day_Include_Saturday(DateTime p_dtmNow, int p_iDay)
        {
            int v_iCount = 0;
            int v_iSub = 1;
            if (p_iDay < 0)
                v_iSub = -1;
            DateTime v_dtRes = p_dtmNow;

            while (v_iCount < Math.Abs(p_iDay))
            {
                v_iCount++;
                v_dtRes = v_dtRes.AddDays(v_iSub);

                while (v_dtRes.DayOfWeek == DayOfWeek.Sunday)
                    v_dtRes = v_dtRes.AddDays(v_iSub);
            }

            return v_dtRes;
        }

        /// <summary>
        /// Lấy ngày đầu tuần
        /// </summary>
        /// <param name="p_dtmData"></param>
        /// <returns></returns>
        public static DateTime Lay_Ngay_Dau_Tuan(DateTime p_dtmData)
        {
            DateTime v_dtmRes = p_dtmData;

            while (v_dtmRes.DayOfWeek != DayOfWeek.Monday)
                v_dtmRes = v_dtmRes.AddDays(-1);

            return v_dtmRes;
        }

        /// <summary>
        /// Lấy ngày đầu tháng
        /// </summary>
        /// <param name="p_dtmData"></param>
        /// <returns></returns>
        public static DateTime Lay_Ngay_Dau_Thang(DateTime p_dtmData)
        {
            return CUtility.Convert_String_To_Datetime("01/" + p_dtmData.Month.ToString("00") + "/" + p_dtmData.Year.ToString() + " 00:00:00", "dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Convert ngày về cuối tháng
        /// VD: 13/05/2017 14:22:11 thì sẽ chuyển thành 31/05/2017 23:59:59
        /// </summary>
        /// <param name="p_dtmData"></param>
        /// <returns></returns>
        public static DateTime Lay_Ngay_Cuoi_Thang(DateTime p_dtmData)
        {
            return Convert_To_Cuoi_Ngay(Lay_Ngay_Dau_Thang(p_dtmData.AddMonths(1)).AddDays(-1));
        }

        /// <summary>
        /// Lấy ngày đầu nam
        /// </summary>
        /// <param name="p_dtmData"></param>
        /// <returns></returns>
        public static DateTime Lay_Ngay_Dau_Nam(DateTime p_dtmData)
        {
            DateTime v_dtmRes = p_dtmData;

            while (v_dtmRes.DayOfYear != 1)
                v_dtmRes = v_dtmRes.AddDays(-1);

            return v_dtmRes;
        }
    }
}
