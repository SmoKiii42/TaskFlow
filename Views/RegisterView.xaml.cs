using System.Windows;
using System.Windows.Controls;
using TaskFlow.Core;
using TaskFlow.Models;
using TaskFlow.Views;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow.Views
{
    public partial class RegisterView : UserControl
    {
        private ServisUser _userService = new ServisUser();

        public RegisterView()
        {
            InitializeComponent();
        }


        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string password = passbtn.Password;
            string confirmPassword = pass2btn.Password;

            string name = firstNameBox.Text;            
            string lastName = lastNameBox.Text;
            string email = emailBox.Text;

            if (string.IsNullOrWhiteSpace(firstNameBox.Text))
            {
                MessageBox.Show("name is empty!");
                return;
            }

            if ((!_userService.CorrectEmail(email)) || (_userService.EmailExists(email)))
            {
                MessageBox.Show("Email already exists!");
                return;
            }

            if ((password != confirmPassword) || (string.IsNullOrWhiteSpace(password)) || (string.IsNullOrWhiteSpace(confirmPassword)))
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }


            User user = new User()
            {
                FirstName = name,
                LastName = lastName,
                Description = "",
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };


            _userService.AddUser(user);
            CurrentSession.CurrentUser = user;

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;


            mainWindow.MainFrame.Content = new MainView();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new LoginView();
        }
    }
}