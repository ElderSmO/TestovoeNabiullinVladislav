
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using TestovoeNabiullinVladislav.Events;
using TestovoeNabiullinVladislav.FileDataBaseFolder;

namespace TestovoeNabiullinVladislav.ViewModel
{
    public class AddClientViewModel : INotifyPropertyChanged
    {
        private FileDataBase dataBase {  get; set; }
        Window thisWindow {  get; set; }
        private ComandsMVVM addCommand;
        private ComandsMVVM closeCommand;
        private Client client;
        private ObservableCollection<string> currencyList;
        
        private int selectedCurrencyId;



        public int SelectedCurrencyId
        {
            get => selectedCurrencyId;
            set
            {
                selectedCurrencyId = value;
                OnPropertyChanged("SelectedCurrencyId");
            }
        }
        public ObservableCollection<string> CurrencyList
        {
            get => currencyList;
            set
            {
                currencyList = value;
                OnPropertyChanged("CurrencyList");
            }
        }

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
                      NewClient.Wallet.Name += " Wallet";
                      
                      dataBase = DataOperations.ReadData();
                      if (dataBase != null)
                      {
                          NewClient.Id = dataBase.ObservClients.Count + 1;
                      }
                      else NewClient.Id = 1;
                      NewClient.Wallet.Currency = CurrencyList[SelectedCurrencyId];
                      NewClient.Wallet.Balance = NewClient.Wallet.StartBalance;
                          UserEvents.OnClientAdded(NewClient);
                      thisWindow.Close();
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
                Wallet = new Wallet
                {
                    Name = "NoName",
                    StartBalance = 1000,
                },
                Transactions = new ObservableCollection<Transaction>()
            };
            CurrencyList = new ObservableCollection<string>
                {
                 "USD",
                 "RUB",
                 "EUR"
                };
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}