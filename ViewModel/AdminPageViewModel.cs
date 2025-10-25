using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TestovoeNabiullinVladislav.Events;
using TestovoeNabiullinVladislav.FileDataBaseFolder;
using TestovoeNabiullinVladislav.View;

namespace TestovoeNabiullinVladislav.ViewModel
{
    internal class AdminPageViewModel : INotifyPropertyChanged
    {

        Client selectedCLient {  get; set; }
        private FileDataBase dataBase;
        public FileDataBase DataBase
        {
            get => dataBase;
            set { dataBase = value; OnPropertyChanged("DataBase"); }
        }

        public Client SelectedClient
        {
            get => selectedClient;
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }
        
        
        private ComandsMVVM addUserCommand;
        
        private ComandsMVVM deleteUserCommand;
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
        public ComandsMVVM DeleteUserCommand
        {
            get
            {
                return deleteUserCommand ??
                  (deleteUserCommand = new ComandsMVVM(obj =>
                  {
                      if (selectedClient!=null)
                      dataBase.ObservClients.Remove(selectedClient);
                  }));
            }
        }

        public AdminPageViewModel()
        {
            dataBase = new FileDataBase()
            {
                ObservClients = new ObservableCollection<Client>()
            };
            UserEvents.clientAddHandler += ClientAdded;
        }
        /// <summary>
        /// Получение нового клиента по событию
        /// </summary>
        /// <param name="client">Клиент</param>
        public void ClientAdded(Client client)
        {
            dataBase.ObservClients.Add(client);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

}