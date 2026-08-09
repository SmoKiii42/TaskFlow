using System.Windows;
using System.Windows.Controls;
using TaskFlow.Core;
using TaskFlow.Models;
using TaskFlow.Views;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow.ViewsSettings
{
    /// <summary>
    /// Логика взаимодействия для ChangeRepeatPass.xaml
    /// </summary>
    public partial class ChangeRepeatPass : UserControl
    {
        private ServisUser _userService = new ServisUser();
        public ChangeRepeatPass()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new SettingsTask();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string password = NewPasswordBox.Password;
            string confirmPassword = RepeatPasswordBox.Password;

            if (BCrypt.Net.BCrypt.Verify(password, CurrentSession.CurrentUser.Password)
                || (string.IsNullOrWhiteSpace(password)) || (string.IsNullOrWhiteSpace(confirmPassword)))
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            CurrentSession.CurrentUser.Password = BCrypt.Net.BCrypt.HashPassword(password);

            _userService.UpdateUser(CurrentSession.CurrentUser);

            MessageBox.Show("Password changed successfully");
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new MainView();
        }
    }
}
