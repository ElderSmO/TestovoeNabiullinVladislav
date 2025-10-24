using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestovoeNabiullinVladislav.View;

namespace TestovoeNabiullinVladislav.ViewModel
{
    internal class AdminPageViewModel : INotifyPropertyChanged
    {
       
       public Client SelectedClient
        {
            get => selectedClient;
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }
        public ObservableCollection<Client> clientsTable { get; set; }
        public AdminPageViewModel()
        {

        }

        private ComandsMVVM addUserCommand;
        private Client selectedClient;

        public ComandsMVVM AddUserCommand
        {
            get
            {
                return addUserCommand ??
                  (addUserCommand = new ComandsMVVM(obj =>
                  {
                      AddClientWin addClientWin = new AddClientWin();
                      addClientWin.ShowDialog();
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

}