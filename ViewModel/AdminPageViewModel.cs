using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
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
        private DateTime sortSelectedDate;

        public DateTime SortSelectedDate
        {
            get => sortSelectedDate;
            set { sortSelectedDate = value; OnPropertyChanged("SortSelectedDate"); }
        }

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
                OnPropertyChanged();
                
            }
        }

        private ComandsMVVM addUserCommand;
        private ComandsMVVM deleteUserCommand;
        private ComandsMVVM openUserCommand;
        private ComandsMVVM expensesUserCommand;
        private ComandsMVVM groupTransactionsUserCommand;

        public ComandsMVVM GroupTransactionsUserCommand
        {
            get
            {
                return groupTransactionsUserCommand ??
                  (groupTransactionsUserCommand = new ComandsMVVM(obj =>
                  {
                      var result = DataBase.ObservClients.SelectMany(client => client.Transactions.Where(t => t.Date.Month == SortSelectedDate.Month &&
                              t.Date.Year == SortSelectedDate.Year)
                   .Select(t => new
                   {
                       ClientName = client.Name,
                       WalletName = client.Wallet.Name,
                       Transaction = t
                   }))
               .GroupBy(x => x.Transaction.TypeTransaction)
               .Select(g => new
               {
                   Type = g.Key,
                   TotalAmount = g.Sum(x => x.Transaction.Amount),
                   TransactionCount = g.Count(),
                   Transactions = g.OrderBy(x => x.Transaction.Date).ToList()
               })
               .OrderByDescending(g => g.TotalAmount)
               .Select(g => $"{g.Type}:\n" +
                          $"  Общая сумма: {g.TotalAmount}\n" +
                          $"  Количество: {g.TransactionCount}\n" +
                          $"  Транзакции:\n" +
                          string.Join("", g.Transactions.Select(t =>
                              $"    • {t.ClientName} ({t.WalletName}): {t.Transaction.Amount} - {t.Transaction.Date:dd.MM.yyyy}\n")))
               .ToList();

                      MessageBox.Show(result.Any()
                          ? $"Транзакции за {SortSelectedDate:MMMM yyyy}:\n\n{string.Join("\n", result)}"
                          : $"Нет транзакций за {SortSelectedDate:MMMM yyyy}");
                  }));
            }
        }

        public ComandsMVVM ExpensesUserCommand //Самые большие траты кошелька
        {
            get
            {
                return expensesUserCommand ??
                  (expensesUserCommand = new ComandsMVVM(obj =>
                  {
                      var result = DataBase.ObservClients
                   .Select(client => new
                   {
                       Client = client,
                       TopExpenses = client.Transactions
                           .Where(t => t.Date.Month == SortSelectedDate.Month &&
                                      t.Date.Year == SortSelectedDate.Year &&
                                      t.TypeTransaction == Transaction.Type.Expense)
                           .OrderByDescending(t => t.Amount)
                           .Take(3)
                           .ToList()
                   })
                   .Where(x => x.TopExpenses.Any())
                   .Select(x => $"{x.Client.Name} ({x.Client.Wallet.Name}):\n" +
                              string.Join("\n", x.TopExpenses.Select((t, i) =>
                                  $"  {i + 1}. {t.Amount} - {t.Date:dd.MM.yyyy}")))
                   .ToList();

                      MessageBox.Show(result.Any()
                          ? $"Топ-3 трат за {SortSelectedDate:MMMM yyyy}:\n\n{string.Join("\n\n", result)}"
                          : $"Нет трат за {SortSelectedDate:MMMM yyyy}");
                  
            }));
            }
        }
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
        public ComandsMVVM OpenUserCommand //Открытие карточки клиента
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
            SortSelectedDate = DateTime.Now;
            dataBase = DataOperations.ReadData() ?? new FileDataBase()
            {
                ObservClients = new ObservableCollection<Client>(),
            };
            UserEvents.clientAddHandler += ClientAdded;
            UserEvents.ClientPayHandler += TransferMoney;
        }

        void TransferMoney(double amount, int id_Client, int id_SelectedClient)
        {
                Client thisClientInDb = DataBase.ObservClients.First(a => a.Id == id_Client);
                Client selectedClientInDb = DataBase.ObservClients.First(a => a.Id == id_SelectedClient);
            if (thisClientInDb.Wallet.Currency == selectedClientInDb.Wallet.Currency)
            {
                if (thisClientInDb.Wallet.Balance >= amount)
                {
                    thisClientInDb.Wallet.Balance -= amount;
                    selectedClientInDb.Wallet.Balance += amount;

                    thisClientInDb.Wallet.AmountOfExpenses += amount;
                    selectedClientInDb.Wallet.AmountOfIncome += amount;

                    thisClientInDb.Transactions.Add(new Transaction()
                    {
                        Amount = amount,
                        Date = DateTime.Now,
                        Id = thisClientInDb.Transactions.Count + 1,
                        TypeTransaction = Transaction.Type.Expense,
                    });
                    selectedClientInDb.Transactions.Add(new Transaction()
                    {
                        Amount = amount,
                        Date = DateTime.Now,
                        Id = thisClientInDb.Transactions.Count + 1,
                        TypeTransaction = Transaction.Type.Income,
                    });

                    //DataBase.RefreshCollection();
                    //    OnPropertyChanged(nameof(DataBase));
                    DataOperations.WriteData(DataBase);
                }
                else MessageBox.Show("Не хватает денежных средств");
            }
            else MessageBox.Show("У выбранного клиента другая валюта, перевод не возможен!");

            
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