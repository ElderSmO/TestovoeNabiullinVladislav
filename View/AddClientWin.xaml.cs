
using System.Windows;
using TestovoeNabiullinVladislav.ViewModel;

namespace TestovoeNabiullinVladislav.View
{
    public partial class AddClientWin : Window
    {
        public AddClientWin()
        {
            InitializeComponent();
            DataContext = new AddClientViewModel(this);
        }
    }
}
