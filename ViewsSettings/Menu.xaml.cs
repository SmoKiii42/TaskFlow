using System.Windows;
using System.Windows.Controls;
using TaskFlow;
using TaskFlow.Core;
using TaskFlow.Views;

namespace TaskFlow.ViewsSettings
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        
        public Menu()
        {
            InitializeComponent();
        }

        private void Profile_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new profile();

        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new SettingsTask();


        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
        "Вы действительно хотите выйти из аккаунта? Это действие нельзя отменить.",
        "Выход из аккаунта",
        MessageBoxButton.YesNo,
        MessageBoxImage.Warning) != MessageBoxResult.Yes)
            {
                return;
            }

            CurrentSession.CurrentUser = null;

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new LoginView();
            mainWindow.CloseMenu();
        }

        private void Friends_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new Friends();
        }
    }
}
