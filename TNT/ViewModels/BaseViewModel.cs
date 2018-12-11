
using PropertyChanged;
using System.ComponentModel;

namespace TNT
{
    [AddINotifyPropertyChangedInterface]
   public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
