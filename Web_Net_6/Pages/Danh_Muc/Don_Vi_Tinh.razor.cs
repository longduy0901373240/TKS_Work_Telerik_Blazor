using Controller_6.Danh_Muc;
using Entity_6.Danh_Muc;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Telerik.Blazor;
using Telerik.Blazor.Components;

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
        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }
        protected override void OnInitialized()
        {
            p_MenuItems_Don_Vi_Tinh = new List<MenuItem>()
            {
                new MenuItem { Text = "Delete Item",Action=F_Delete_Don_Vi_Tinh,Icon ="delete"},
                new MenuItem { Text ="Edit Item",Action=F_EditItem_Don_Vi_Tinh,Icon="edit"}
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
            p_objDon_Vi_Tinh = new CDM_Don_Vi_Tinh();
            p_blSelectEdit = false;
        }
        private void F_EditItem_Don_Vi_Tinh()
        {
            p_blSelectEdit = true;
            p_objDon_Vi_Tinh = p_objDon_Vi_Tinh_Temp;
        }
        private void F_Save_Don_Vi_Tinh()
        {
            if (p_blSelectEdit == true)
            {
                p_objDon_Vi_Tinh.Last_Updated_By = "Long";
                _db.F2001_Update_DM_Don_Vi_Tinh(p_objDon_Vi_Tinh);
            }
            else
            {
                p_objDon_Vi_Tinh.Last_Updated_By = "Admin";
                _db.F2001_Insert_DM_Don_Vi_Tinh(p_objDon_Vi_Tinh);
            }
            F_ClearSelection_Don_Vi_Tinh();
            OnInitialized();
        }
        private void F_ClearSelection_Don_Vi_Tinh()
        {
            p_objDon_Vi_Tinh = p_objDon_Vi_Tinh_Temp = null;
        }
        private async void F_Delete_Don_Vi_Tinh()
        {
            bool v_blConfirmedDelete_Don_Vi_Tinh = await Dialogs.ConfirmAsync("Are you sure", "Thông Báo");
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
    }
}
