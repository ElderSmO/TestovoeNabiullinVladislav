
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestovoeNabiullinVladislav.FileDataBaseFolder
{
    public class FileDataBase : INotifyPropertyChanged
    {
        ObservableCollection<Client> observClients;

        public ObservableCollection<Client> ObservClients
        {
            get => observClients;
            set { observClients = value; OnPropertyChanged("ObservClients"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
