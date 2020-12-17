using ESKOBWpf.Model;
using ESKOBWpf.View;
using ESKOBWpf.Viewmodels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ESKOBWpf.Controller
{
    class Logic : ViewmodelBase
    {
        private ObservableCollection<Tenant> _tenants = new ObservableCollection<Tenant>();
        public ObservableCollection<Tenant> Tenants { get { return _tenants; } set { _tenants = value; Changed(); } }


        private ObservableCollection<Manager> _managers = new ObservableCollection<Manager>();
        public ObservableCollection<Manager> ManagerList { get { return _managers; } set { _managers = value; Changed(); } }


        private Manager _selectedManager;
        public Manager SelectedManager
        {
            get
            {
                return _selectedManager;
            }
            set
            {
                _selectedManager = value;
                ShowManagerControls = true;
            }
        }
        public Tenant SelectedTenant { get; set; }

        public ICommand SelectTenantCommand => new DelegateCommand(SelectTenant);
        public ICommand CreateTenantCommand => new DelegateCommand(CreateTenant);
        public ICommand EditTenantCommand => new DelegateCommand(EditTenant);
        public ICommand DeleteTenantCommand => new DelegateCommand(DeleteTenant);
        public ICommand CreateManagerCommand => new DelegateCommand(CreateManager);
        public ICommand EditManagerCommand => new DelegateCommand(EditManager);
        public ICommand DeleteManagerCommand => new DelegateCommand(DeleteManager);
        public ICommand RefreshTenantsCommand => new DelegateCommand(UpdateData);
        public ICommand RefreshManagersCommand => new DelegateCommand(SelectTenant);


        private bool _tenantControls = false;
        public bool ShowTenantControls
        {
            get
            {
                return _tenantControls;
            }

            set
            {
                _tenantControls = value;
                Changed();
            }
        }

        private bool _managerControls = false;
        public bool ShowManagerControls
        {
            get
            {
                return _managerControls;
            }

            set
            {
                _managerControls = value;
                Changed();
            }
        }

        public Logic()
        {
            UpdateData(null);
        }

        private void SelectTenant(object parameter)
        {
            int id = -1;
            if (parameter == null) 
                id = SelectedTenant.Id;
            else 
                id = (int)parameter;

            Tenant tenant = Tenants.Where(t => t.Id == id).FirstOrDefault();
            var response = API.GET("/" + tenant.Reference + "/managers").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                List<Manager> managers = JsonConvert.DeserializeObject<List<Manager>>(result);
                ManagerList = new ObservableCollection<Manager>(managers);
            }

            SelectedTenant = tenant;
            ShowTenantControls = true;
            ShowManagerControls = false;
        }

        public void CreateTenant(object parameter)
        {
            Tenant t = new Tenant();
            var mData = new TenantData(t);
            mData.Show();
        }

        public void EditTenant(object parameter)
        {
            var mData = new TenantData(SelectedTenant);
            mData.Show();
        }

        public async void DeleteTenant(object parameter)
        {
            await API.DELETE("/tenants/" + SelectedTenant.Id);
            SelectedTenant = null;
            ShowTenantControls = false;
            ShowManagerControls = false;
            UpdateData(null);
        }

        public void CreateManager(object parameter)
        {
            Manager m = new Manager
            {
                TenantId = SelectedTenant.Id,
                TenantReference = SelectedTenant.Reference
            };

            var mData = new ManagerData(m);
            mData.Show();

        }

        public void EditManager(object parameter)
        {
            SelectedManager.TenantReference = SelectedTenant.Reference;
            var mData = new ManagerData(SelectedManager);
            mData.Show();
        }

        public async void DeleteManager(object parameter)
        {
            await API.DELETE("/" + SelectedTenant.Reference + "/managers/" + SelectedManager.Id);
            SelectTenant(SelectedTenant.Id);
        }

        public void UpdateData(object parameter)
        {
            SelectedTenant = null;
            ShowTenantControls = false;
            ShowManagerControls = false;

            var response = API.GET("/tenants").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                List<Tenant> tenants = JsonConvert.DeserializeObject<List<Tenant>>(result);
                Tenants = new ObservableCollection<Tenant>(tenants);
            }
        }
    }
}
