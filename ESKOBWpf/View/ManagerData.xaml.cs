using ESKOBWpf.Model;
using ESKOBWpf.Viewmodels;
using System;
using System.Windows;

namespace ESKOBWpf.View
{
    /// <summary>
    /// Interaction logic for ManagerData.xaml
    /// </summary>
    public partial class ManagerData : Window
    {
        public ManagerData(Manager manager)
        {
            InitializeComponent();
            ManagerDataViewmodel vm = new ManagerDataViewmodel(manager);
            DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(Close);
        }
    }
}
