using Controller_6.Danh_Muc;
using DataLayer_6;
using Entity_6.Danh_Muc;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Data;
using System.Data.SqlClient;
using Telerik.Blazor;
using Telerik.Blazor.Components;
using Telerik.Blazor.Components.Upload;
using Utility_6;

namespace Web_Net_6.Pages.Danh_Muc
{
    public partial class Don_Vi_Tinh
    {
        #region Grid
        private CDM_Don_Vi_Tinh_Controller _db = new CDM_Don_Vi_Tinh_Controller();
        private IList<CDM_Don_Vi_Tinh> p_arrDon_vi_Tinh = new List<CDM_Don_Vi_Tinh>();
        private bool p_blSelectEdit { get; set; }
        private CDM_Don_Vi_Tinh p_objDon_Vi_Tinh { get; set; }
        private CDM_Don_Vi_Tinh p_objDon_Vi_Tinh_Temp { get; set; }

        private TelerikContextMenu<MenuItem> p_ContextMenu_Don_Vi_Tinh { get; set; }
        private IList<MenuItem> p_MenuItems_Don_Vi_Tinh { get; set; }
        private class MenuItem
        {
            public string Text { get; set; }
            public string Icon { get; set; }
            public Action Action { get; set; }
        }
        private string p_strNotificationError { get; set; }
        protected override void OnInitialized()
        {
            p_MenuItems_Don_Vi_Tinh = new List<MenuItem>()
            {
                new MenuItem { Text ="Edit Item",Action=F_EditItem_Don_Vi_Tinh,Icon="edit"},
                new MenuItem { Text = "Delete Item",Action=F_Delete_Don_Vi_Tinh,Icon ="delete"}

            };
            F_LoadData_Don_Vi_Tinh();
            StateHasChanged();
        }
        private void F_LoadData_Don_Vi_Tinh()
        {
            p_arrDon_vi_Tinh = _db.List_DM_Don_Vi_Tinh();
        }
        private void F_OnContextMenu_Don_Vi_Tinh(GridRowClickEventArgs argsItem_Don_Vi_Tinh)
        {
            p_objDon_Vi_Tinh_Temp = argsItem_Don_Vi_Tinh.Item as CDM_Don_Vi_Tinh;
            if (argsItem_Don_Vi_Tinh.EventArgs is MouseEventArgs mouseEventArgs)
                _ = p_ContextMenu_Don_Vi_Tinh.ShowAsync(mouseEventArgs.ClientX, mouseEventArgs.ClientY);
        }
        private void F_OnItemClick(MenuItem item)
        {
            item.Action.Invoke();
        }
        private void F_AddItem_Don_Vi_Tinh()
        {
            p_strNotificationError = "";
            p_objDon_Vi_Tinh = new CDM_Don_Vi_Tinh();
            p_blSelectEdit = false;
        }
        private void F_EditItem_Don_Vi_Tinh()
        {
            p_strNotificationError = "";
            p_blSelectEdit = true;
            p_objDon_Vi_Tinh = p_objDon_Vi_Tinh_Temp;
        }
        private void F_Save_Don_Vi_Tinh()
        {
            if (p_blSelectEdit == true)
            {
                try
                {
                    p_objDon_Vi_Tinh.Last_Updated_By = "Long";
                    _db.F2001_Update_DM_Don_Vi_Tinh(p_objDon_Vi_Tinh);
                    F_ClearSelection_Don_Vi_Tinh();
                }
                catch (Exception ex)
                {
                    p_strNotificationError = ex.Message;
                }
            }
            else
            {
                try
                {
                    p_objDon_Vi_Tinh.Last_Updated_By = "Admin";
                    _db.F2001_Insert_DM_Don_Vi_Tinh(p_objDon_Vi_Tinh);
                    F_ClearSelection_Don_Vi_Tinh();
                }
                catch (Exception ex)
                {
                    p_strNotificationError = ex.Message;
                }
            }
            F_LoadData();
            OnInitialized();
        }
        private void F_ClearSelection_Don_Vi_Tinh()
        {
            p_objDon_Vi_Tinh = p_objDon_Vi_Tinh_Temp = null;
        }
        private async void F_Delete_Don_Vi_Tinh()
        {
            bool v_blConfirmedDelete_Don_Vi_Tinh = await F_Dialogs(1, "Are you sure", "Thông Báo");
            if (v_blConfirmedDelete_Don_Vi_Tinh == true)
            {
                p_objDon_Vi_Tinh = p_objDon_Vi_Tinh_Temp;
                p_objDon_Vi_Tinh.Last_Updated_By = "Long";
                _db.Delete_DM_Don_Vi_Tinh(p_objDon_Vi_Tinh.Auto_ID, p_objDon_Vi_Tinh.Last_Updated_By);
                p_objDon_Vi_Tinh = null;
                p_objDon_Vi_Tinh_Temp = null;
                OnInitialized();
            }
        }
        #endregion

        #region Upload File
        private string p_strpathUrl { get; set; }
        private async void F_UploadFile(InputFileChangeEventArgs args_UploadFile)
        {
            try
            {
                CExcel_Controller v_objExcel = new CExcel_Controller();
                string v_strWebRootPath = env.WebRootPath;
                var v_objUrl = await v_objExcel.SaveFile(args_UploadFile.File, v_strWebRootPath);
                p_strpathUrl = null;
                p_strpathUrl = v_objUrl;
            }
            catch (Exception ex)
            {
                bool v_blResDialogs = await F_Dialogs(0,ex.Message, "Warning!");
                IJS.InvokeVoidAsync("Clear_InputFile");
            }
        }
        private async void F_Import_Excel()
        {
            CExcel_Controller v_objexcel = new CExcel_Controller();
            SqlConnection v_conn = null;
            SqlTransaction v_trans = null;
            try
            {
                v_conn = CSqlHelper.CreateConnection(CConfig.g_strTKS_Thuc_Tap_Data_Conn_String);
                v_conn.Open();
                v_trans = v_conn.BeginTransaction();
                //lọc từ vùng chọn đến hết
                DataTable v_dt_List_Range_Value_To_End = v_objexcel.List_Range_Value_To_End("A1", "A", p_strpathUrl);
                string test = v_dt_List_Range_Value_To_End.Columns[0].ToString();
                foreach (DataRow v_row in v_dt_List_Range_Value_To_End.Rows)
                {
                    CDM_Don_Vi_Tinh v_objItem = new CDM_Don_Vi_Tinh();
                    v_objItem.Ten_Don_Vi_Tinh = CUtility.Convert_To_String(v_row[0]);
                    v_objItem.Last_Updated_By = "admin";
                    _db.F2001_Insert_DM_Don_Vi_Tinh(v_conn, v_trans, v_objItem);
                }
                v_trans.Commit();
                p_strpathUrl = "";
                IJS.InvokeVoidAsync("Clear_InputFile");
                bool v_blResDialogs = await F_Dialogs(0, "Import thành công", "Thông Báo");
                F_LoadData();
            }
            catch (Exception ex)
            {
                if (v_trans != null)
                    v_trans.Rollback();
                bool v_blResDialogs = await F_Dialogs(0, ex.Message, "Warning!");
            }
            finally
            {
                v_trans.Dispose();
                if (v_conn != null)
                    v_conn.Close();
            }
        }
        #endregion
        #region Dialogs
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }
        async Task<bool> F_Dialogs(int p_intSelectDialogs,string p_strContent, string p_strTitle)
        {
            bool v_blRes;
            switch (p_intSelectDialogs)
            {
                default: //Thông báo Alert  
                    await Dialogs.AlertAsync(p_strContent, p_strTitle);
                    return v_blRes = false;
                case 1://Thông báo Confirm cần sự xác nhận
                    v_blRes = await Dialogs.ConfirmAsync(p_strContent, p_strTitle);
                    return v_blRes;
            }
        }
        #endregion
        async void F_LoadData()
        {
            OnInitialized();
            await InvokeAsync(StateHasChanged);
        }
    }
}
