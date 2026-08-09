using System.Windows;
using System.Windows.Controls;
using TaskFlow.Core;
using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private ServisUser _userService = new ServisUser();
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            string password = passbtn.Password;
            string email = emailtxt.Text;


            User? user = _userService.Login(email, password);
            if (user != null)
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

                MainView view = new MainView();

                mainWindow.MainFrame.Content = view;
            }
            else
            {
                MessageBox.Show("Неверный Email или пароль");
            }

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = ( MainWindow )Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new RegisterView();
        }
    }
}
