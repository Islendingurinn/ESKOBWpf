using ESKOBWpf.Model;
using ESKOBWpf.Viewmodels;
using System;
using System.Windows;

namespace ESKOBWpf.View
{
    public partial class TenantData : Window
    {
        public TenantData(Tenant tenant)
        {
            InitializeComponent();
            TenantDataViewmodel vm = new TenantDataViewmodel(tenant);
            DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(Close);
        }
    }
}
