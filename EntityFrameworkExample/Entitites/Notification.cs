using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EntityFrameworkExample.Entitites
{
    public class Notification : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(T value, ref T field, [CallerMemberName] string property = "")
        {
            if (!Equals(value, field))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
