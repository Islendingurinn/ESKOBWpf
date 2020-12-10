using ESKOBWpf.Model;
using ESKOBWpf.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                vm.CloseAction = new Action(this.Close);
            
        }
    }
}
