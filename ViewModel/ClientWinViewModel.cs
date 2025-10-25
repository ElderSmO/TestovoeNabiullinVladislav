using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestovoeNabiullinVladislav.Events;
using TestovoeNabiullinVladislav.FileDataBaseFolder;
using TestovoeNabiullinVladislav.View;

namespace TestovoeNabiullinVladislav.ViewModel
{
    public class ClientWinViewModel : INotifyPropertyChanged
    {
        private ComandsMVVM transferCommand;
        private Client thisClient;
        private Client selectedClient;
        private FileDataBase dataBase;
        private double transferAmount;

        public FileDataBase DataBase
        {
            get => dataBase;
            set { dataBase = value; OnPropertyChanged(); }
        }

        public Client SelectedClient
        {
            get => selectedClient;
            set
            {
                selectedClient = value;
                OnPropertyChanged();
            }
        }

        public Client ThisClient
        {
            get => thisClient;
            set
            {
                thisClient = value;
                OnPropertyChanged();
            }
        }

        public double TransferAmount
        {
            get => transferAmount;
            set
            {
                transferAmount = value;
                OnPropertyChanged();
            }
        }

        public ComandsMVVM TransferUserCommand
{
    get
    {
        return transferCommand ??
              (transferCommand = new ComandsMVVM(obj =>
              {
                  if (SelectedClient != null && TransferAmount > 0 && ThisClient != SelectedClient)
                  {
                      if (ThisClient.Wallet.Balance >= TransferAmount)
                      {
                          // Ваша текущая логика...
                          Client thisClientInDb = DataBase.ObservClients.First(a => a.Id == ThisClient.Id);
                          Client selectedClientInDb = DataBase.ObservClients.First(a => a.Id == SelectedClient.Id);

                          thisClientInDb.Wallet.Balance -= TransferAmount;
                          selectedClientInDb.Wallet.Balance += TransferAmount;

                          ThisClient = thisClientInDb;
                          SelectedClient = selectedClientInDb;

                          DataBase.RefreshCollection();
                          OnPropertyChanged(nameof(DataBase));


                          DataOperations.WriteData(DataBase);

                          MessageBox.Show($"Перевод выполнен!");
                          TransferAmount = 0;
                      }
                  }
              }));
    }
}

        public ClientWinViewModel(Client client)
        {
            dataBase = DataOperations.ReadData();
            ThisClient = client;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
