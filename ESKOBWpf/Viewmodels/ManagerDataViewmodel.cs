using ESKOBWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ESKOBWpf.Viewmodels
{
    class ManagerDataViewmodel : ViewmodelBase
    {
        private Manager _manager;
        public Manager Manager { get { return _manager; } set { _manager = value; Changed(); } }

        public ICommand CancelCommand => new DelegateCommand(Cancel);
        public ICommand SaveCommand => new DelegateCommand(Save);
        public Action CloseAction { get; set; }

        public ManagerDataViewmodel(Manager manager)
        {
            Manager = manager;
        }

        public void Cancel(object parameter)
        {
            CloseAction();
        }

        public async void Save(object parameter)
        {
            if (Manager.Id == 0)
                await API.POST("/" + Manager.TenantReference + "/managers/create", Manager);
            else
                await API.POST("/" + Manager.TenantReference + "/managers/edit", Manager);
            CloseAction();
        }

    }
}
