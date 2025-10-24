
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using TestovoeNabiullinVladislav.Events;

namespace TestovoeNabiullinVladislav.ViewModel
{
    public class AddClientViewModel : INotifyPropertyChanged
    {
        Window thisWindow {  get; set; }
        private ComandsMVVM addCommand;
        private ComandsMVVM closeCommand;
        private Client client;

        public Client NewClient
        {
            get => client;
            set
            {
                client = value;
                OnPropertyChanged("NewClient");
            }
        }

        public ComandsMVVM AddUserCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new ComandsMVVM(obj =>
                  {
                      Debug.WriteLine(client.Name + " " + client.Wallet.Name);
                      UserEvents.OnClientAdded(NewClient);
                  }));
            }
        }

        public ComandsMVVM CloseWinCommand
        {
            get
            {
                return closeCommand ??
                  (closeCommand = new ComandsMVVM(obj =>
                  {
                      thisWindow.Close();
                  }));
            }
        }
        public AddClientViewModel(Window thisWin)
        {
            thisWindow = thisWin;
            client = new Client
            {
                Name = "User",
                Wallet = new Wallet { Name = "Wallet", StartBalance = 1000 }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}