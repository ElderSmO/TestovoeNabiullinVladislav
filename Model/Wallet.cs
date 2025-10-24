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
