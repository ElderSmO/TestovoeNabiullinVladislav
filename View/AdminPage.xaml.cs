using System.Windows.Controls;
using TestovoeNabiullinVladislav.ViewModel;

namespace TestovoeNabiullinVladislav.View
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            DataContext = new AdminPageViewModel();
        }
    }
}
