using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestovoeNabiullinVladislav
{
   public class Client : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private Wallet wallet;
        private ObservableCollection<Transaction> transactions;

        public ObservableCollection<Transaction> Transactions 
        {
            get => transactions;
            set
            {
                transactions = value;
                OnPropertyChanged("Transactions");
            }
        }

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }
       public string Name 
        { 
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
       public Wallet Wallet 
        {
            get => wallet;
            set
            {
                wallet = value;
                OnPropertyChanged("Wallet");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
