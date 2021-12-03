using DataLayer_6;
using Entity_6.Danh_Muc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility_6;

namespace Controller_6.Danh_Muc
{
    public class CDM_Don_Vi_Tinh_Controller
    {
        public IList<CDM_Don_Vi_Tinh> List_DM_Don_Vi_Tinh()
        {
            IList<CDM_Don_Vi_Tinh> v_arrRes = new List<CDM_Don_Vi_Tinh>();
            DataTable v_dt = new DataTable();

            try
            {
                CSqlHelper.FillDataTable(CConfig.g_strTKS_Thuc_Tap_Data_Conn_String, v_dt, "sp_sel_List_DM_Don_Vi_Tinh");

                foreach (DataRow v_row in v_dt.Rows)
                {
                    CDM_Don_Vi_Tinh v_objRes = CUtility.MapItemFromRow<CDM_Don_Vi_Tinh>(v_row);
                    v_arrRes.Add(v_objRes);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                v_dt.Dispose();
            }

            return v_arrRes;
        }

        public CDM_Don_Vi_Tinh Get_DM_Don_Vi_Tinh_By_ID(int p_iID)
        {
            CDM_Don_Vi_Tinh v_objRes = null;
            DataTable v_dt = new DataTable();

            try
            {
                CSqlHelper.FillDataTable(CConfig.g_strTKS_Thuc_Tap_Data_Conn_String, v_dt, "sp_sel_Get_DM_Don_Vi_Tinh_By_ID", p_iID);

                if (v_dt.Rows.Count > 0)
                {
                    v_objRes = CUtility.MapItemFromRow<CDM_Don_Vi_Tinh>(v_dt.Rows[0]);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                v_dt.Dispose();
            }

            return v_objRes;
        }

        public int Insert_DM_Don_Vi_Tinh(CDM_Don_Vi_Tinh p_objData)
        {
            int v_iRes = CConst.INT_VALUE_NULL;

            try
            {
                v_iRes = Convert.ToInt32(CSqlHelper.ExecuteScalar(CConfig.g_strTKS_Thuc_Tap_Data_Conn_String, "sp_ins_DM_Don_Vi_Tinh",
                    p_objData.Ten_Don_Vi_Tinh, p_objData.Last_Updated_By));
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return v_iRes;
        }

        public void Update_DM_Don_Vi_Tinh(CDM_Don_Vi_Tinh p_objData)
        {
            try
            {
                CSqlHelper.ExecuteNonquery(CConfig.g_strTKS_Thuc_Tap_Data_Conn_String, "sp_upd_DM_Don_Vi_Tinh", p_objData.Auto_ID,
                    p_objData.Ten_Don_Vi_Tinh, p_objData.Last_Updated_By);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete_DM_Don_Vi_Tinh(int p_iAuto_ID, string p_strLast_Updated_By)
        {
            try
            {
                CSqlHelper.ExecuteNonquery(CConfig.g_strTKS_Thuc_Tap_Data_Conn_String, "sp_del_DM_Don_Vi_Tinh", p_iAuto_ID, p_strLast_Updated_By);
            }

            catch (Exception)
            {
                throw;
            }
        }

        public void Delete_DM_Don_Vi_Tinh_Select(IList<CDM_Don_Vi_Tinh> p_arrDon_Vi_Tinh, string p_strLast_Updated_User)
        {
            try
            {
                foreach (CDM_Don_Vi_Tinh v_obj in p_arrDon_Vi_Tinh)
                    CSqlHelper.ExecuteNonquery(CConfig.g_strTKS_Thuc_Tap_Data_Conn_String, "sp_del_DM_Don_Vi_Tinh", v_obj.Auto_ID, p_strLast_Updated_User);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CDM_Don_Vi_Tinh Get_DM_Don_Vi_Tinh_By_Ten_Don_Vi_Tinh(string p_iten)
        {
            CDM_Don_Vi_Tinh v_objRes = null;
            DataTable v_dt = new DataTable();

            try
            {
                CSqlHelper.FillDataTable(CConfig.g_strTKS_Thuc_Tap_Data_Conn_String, v_dt, "sp_sel_Get_DM_Don_Vi_Tinh_By_Ten_Don_Vi_Tinh", p_iten);

                if (v_dt.Rows.Count > 0)
                {
                    v_objRes = CUtility.MapItemFromRow<CDM_Don_Vi_Tinh>(v_dt.Rows[0]);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                v_dt.Dispose();
            }

            return v_objRes;
        }

        public int F2001_Insert_DM_Don_Vi_Tinh(CDM_Don_Vi_Tinh p_objData)
        {
            int v_iRes = CConst.INT_VALUE_NULL;

            try
            {
                v_iRes = Convert.ToInt32(CSqlHelper.ExecuteScalar(CConfig.g_strTKS_Thuc_Tap_Data_Conn_String, "F2001_sp_ins_Don_Vi_Tinh",
                    p_objData.Ten_Don_Vi_Tinh, p_objData.Last_Updated_By));
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return v_iRes;
        }

        public int F2001_Insert_DM_Don_Vi_Tinh(SqlConnection conn, SqlTransaction v_trans, CDM_Don_Vi_Tinh p_objData)
        {
            int v_iRes = CConst.INT_VALUE_NULL;

            try
            {
                v_iRes = Convert.ToInt32(CSqlHelper.ExecuteScalar(CConfig.g_strTKS_Thuc_Tap_Data_Conn_String, "F2001_sp_ins_Don_Vi_Tinh",
                    p_objData.Ten_Don_Vi_Tinh, p_objData.Last_Updated_By));
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return v_iRes;
        }

        public void F2001_Update_DM_Don_Vi_Tinh(CDM_Don_Vi_Tinh p_objData)
        {
            try
            {
                CSqlHelper.ExecuteNonquery(CConfig.g_strTKS_Thuc_Tap_Data_Conn_String, "F2001_sp_upd_Don_Vi_Tinh", p_objData.Auto_ID,
                    p_objData.Ten_Don_Vi_Tinh, p_objData.Last_Updated_By);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
