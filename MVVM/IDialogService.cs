using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestovoeNabiullinVladislav.MVVM
{
    public interface IDialogService
    {
        void Show<T>() where T : Window, new();
        void Close<T>() where T: Window, new();
    }

   
}
