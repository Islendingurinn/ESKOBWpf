using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ESKOBWpf.Viewmodels
{
    public abstract class ViewmodelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// The PropertyChanged invokation is encapsualted in this reusable method
        /// in an abstract base class
        /// </summary>
        /// <param name="Property"></param>
        protected void Changed([CallerMemberName] string Property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
