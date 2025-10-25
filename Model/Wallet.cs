using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestovoeNabiullinVladislav
{
    public class Wallet : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private List<String> currency;
        private double startBalance;
        private double balance;
        private double amountOfIncome;
        private double amountOfExpenses;

        public int Id 
        { 
            get => id;
            set { id = value; OnPropertyChanged("ID");}
        }
        public string Name 
        {
            get => name;
            set { name = value; OnPropertyChanged("Name"); }
        }
        public List<string> Currency 
        { 
            get => currency;
            set { currency = value; OnPropertyChanged("Currency"); }
        }
        public double StartBalance 
        { 
            get => startBalance;
            set { startBalance = value; OnPropertyChanged("StartBalance"); }
        }
        public double Balance 
        { 
            get => balance = startBalance+amountOfIncome-AmountOfIncome;
            set { balance = value; OnPropertyChanged("Balance"); }
        }
        public double AmountOfIncome
        { 
            get => amountOfIncome;
            set { amountOfIncome = value; OnPropertyChanged("AmountOfIncome"); }
        }
        
        public double AmountOfExpenses
        { 
            get => amountOfExpenses;
            set { amountOfExpenses = value; OnPropertyChanged("AmountOfExpenses"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public Wallet()
        {
            currency = new List<string>()
            {
                "USD",
                "RUB",
                "EUR"
            };


        }
    }
}
