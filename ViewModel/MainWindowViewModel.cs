using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using TestovoeNabiullinVladislav.View;

namespace TestovoeNabiullinVladislav.ViewModel
{ 
        internal class MainWindowViewModel : INotifyPropertyChanged
        {

            AdminPage adminPage;

            private readonly NavigationService navigationService;

            public MainWindowViewModel()
            {
                navigationService = new NavigationService();
                adminPage = new AdminPage();
            }

            public Frame MainFrame => navigationService.Frame;

            private ComandsMVVM openAdminPanelCommand;
            public ComandsMVVM OpenAdminPanelCommand
            {
                get
                {
                    return openAdminPanelCommand ??
                      (openAdminPanelCommand = new ComandsMVVM(obj =>
                      {
                          navigationService.NavigateTo(adminPage);
                      }));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }

        public class NavigationService : INotifyPropertyChanged
        {
            private Frame frame;

            public Frame Frame
            {
                get => frame;
                set
                {
                    frame = value;
                    OnPropertyChanged();
                }
            }

            public NavigationService()
            {
                Frame = new Frame();
            }

            public void NavigateTo(Page page)
            {
                Frame.Navigate(page);
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
}