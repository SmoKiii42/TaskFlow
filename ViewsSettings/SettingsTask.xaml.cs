using System.Windows;
using System.Windows.Controls;
using TaskFlow.Core;
using TaskFlow.Views;
using TaskFlow.ViewsSettings;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow.ViewsSettings
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class SettingsTask : UserControl
    {
        private ServisUser _userService = new ServisUser();
        public SettingsTask()
        {
            InitializeComponent();
        }


        private void ChengeEmail_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new CheckEmail();
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

        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            {
                if (MessageBox.Show(
        "Вы действительно хотите удалить аккаунт? Это действие нельзя отменить.",
        "Удаление аккаунта",
        MessageBoxButton.YesNo,
        MessageBoxImage.Warning) != MessageBoxResult.Yes)
                {
                    return;
                }

                _userService.DeleteUser(CurrentSession.CurrentUser.Id);

                CurrentSession.CurrentUser = null;

                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.MainFrame.Content = new RegisterView();

            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new ChangePassword();
        }

        private void BackButtom_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new MainView();
        }
    }
}

