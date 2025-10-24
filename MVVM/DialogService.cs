using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestovoeNabiullinVladislav.MVVM
{
    public class DialogService : IDialogService
    {
        public void Show<T>() where T : Window, new()
        {
            var window = new T();
            window.Show();
        }

        public void Close<T>() where T : Window, new()
        {
            var window = new T();
            window.Close();
        }

    }
}
