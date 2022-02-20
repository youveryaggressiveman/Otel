using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Otel.ViewModel
{
    /// <summary>
    /// МетодЮ который реализует интерфейс INotifyPropertyChanged
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
