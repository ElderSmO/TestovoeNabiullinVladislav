using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestovoeNabiullinVladislav
{
    public class Transaction : INotifyPropertyChanged
    {
        private int id;
        private DateTime date;
        private double amount;
        private Type typeTransaction;

        public int Id 
        {
            get => id; 
            set { id = value; OnPropertyChanged("Id"); } 
        }

        public DateTime Date 
        { 
            get => date;
            set { date = value; OnPropertyChanged("Date"); }
        }
        public double Amount 
        { 
            get => amount;
            set { amount = value; OnPropertyChanged("Amount"); }
        }

        public Type TypeTransaction 
        { 
            get => typeTransaction;
            set { typeTransaction = value; OnPropertyChanged("TypeTransaction"); }
        }

        public enum Type
        {
            Expense,
            Income
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}