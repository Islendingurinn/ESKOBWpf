using ESKOBWpf.Model;
using System;
using System.Windows.Input;

namespace ESKOBWpf.Viewmodels
{
    class TenantDataViewmodel : ViewmodelBase
    {
        private Tenant _tenant;
        public Tenant Tenant { get { return _tenant; } set { _tenant = value; Changed(); } }

        public ICommand CancelCommand => new DelegateCommand(Cancel);
        public ICommand SaveCommand => new DelegateCommand(Save);
        public Action CloseAction { get; set; }

        public TenantDataViewmodel(Tenant tenant)
        {
            Tenant = tenant;
        }

        public void Cancel(object parameter)
        {
            CloseAction();
        }

        public async void Save(object parameter)
        {
            if (Tenant.Id == 0)
                await API.POST("/tenants", Tenant);
            else
                await API.PUT("/tenants/" + Tenant.Id, Tenant);
            CloseAction();
        }

    }
}
