using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using TaskFlow.Core;
using TaskFlow.Models;
using TaskFlow.Views;
using TaskFlow.Новая_папка1.TaskFlow;


namespace TaskFlow.ViewsSettings
{
    /// <summary>
    /// Логика взаимодействия для CheckEmail.xaml
    /// </summary>
    public partial class CheckEmail : UserControl
    {
        private ServisUser _userService = new ServisUser();

        public CheckEmail()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new MainView();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string CurrentEmail = CurrentEmailBox.Text;
            string NewEmail = NewEmailBox.Text;

            
            if(CurrentSession.CurrentUser.Email != CurrentEmail)
            {
                MessageBox.Show("Current email is incorrect!");
                return;
            }

            if (string.IsNullOrWhiteSpace(NewEmail) || string.IsNullOrWhiteSpace(CurrentEmail))
            {
                MessageBox.Show("Email is empty!");
                return;
            }


            if ((!NewEmail.Contains("@")))
            {
                MessageBox.Show("Invalid email!");
                return;
            }

            if (!_userService.CorrectEmail(NewEmail))
            {
                MessageBox.Show("Email already exists!");
                return;
            }

            bool exists = _userService.EmailExists(NewEmail);

            if (exists)
            {
                MessageBox.Show("This email is already used!");
                return;
            }

            CurrentSession.CurrentUser.Email = NewEmail;
            _userService.UpdateUser(CurrentSession.CurrentUser);
            MessageBox.Show("Email changed successfully!");
        }

    }
}
