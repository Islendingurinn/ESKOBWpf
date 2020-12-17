using ESKOBWpf.Controller;
using System.Windows;

namespace ESKOBWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Logic();
        }
    }
}
