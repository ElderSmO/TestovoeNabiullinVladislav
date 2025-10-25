
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        // Метод для принудительного обновления коллекции
        public void RefreshCollection()
        {
            var temp = ObservClients.ToList();
            ObservClients = new ObservableCollection<Client>(temp);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
