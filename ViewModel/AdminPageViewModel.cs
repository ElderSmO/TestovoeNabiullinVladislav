using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using TestovoeNabiullinVladislav.Events;
using TestovoeNabiullinVladislav.FileDataBaseFolder;
using TestovoeNabiullinVladislav.View;

namespace TestovoeNabiullinVladislav.ViewModel
{
    internal class AdminPageViewModel : INotifyPropertyChanged
    {
        private FileDataBase dataBase;
        private Client selectedClient;

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

        private ComandsMVVM addUserCommand;
        private ComandsMVVM deleteUserCommand;
        private ComandsMVVM openUserCommand;

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
        public ComandsMVVM OpenUserCommand
        {
            get
            {
                return openUserCommand ??
                  (openUserCommand = new ComandsMVVM(obj =>
                  {
                      if (SelectedClient != null)
                      {
                          ClientWindow clientWindow = new ClientWindow(SelectedClient);
                          clientWindow.ShowDialog();
                      }
                      else
                      {
                          MessageBox.Show("Клиент не выбран");
                      }
                  }));
            }
        }

        public ComandsMVVM DeleteUserCommand
        {
            get
            {
                return deleteUserCommand ??
                  (deleteUserCommand = new ComandsMVVM(obj =>
                  {
                      if (SelectedClient != null) 
                      {
                          DataBase.ObservClients.Remove(SelectedClient);
                          SelectedClient = null;
                          DataOperations.WriteData(dataBase);
                      }
                  }));
            }
        }

        public AdminPageViewModel()
        {
            dataBase = DataOperations.ReadData() ?? new FileDataBase()
            {
                ObservClients = new ObservableCollection<Client>(),
            };
            UserEvents.clientAddHandler += ClientAdded;
            UserEvents.ClientPayHandler += UpdateData;
        }

        void UpdateData()
        {
            //DataOperations.ReadData();
            //DataOperations.WriteData(dataBase);
            DataBase.RefreshCollection();
            OnPropertyChanged(nameof(DataBase));
        }

        void ClientAdded(Client client)
        {
            dataBase.ObservClients.Add(client);
            DataOperations.WriteData(dataBase);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

}