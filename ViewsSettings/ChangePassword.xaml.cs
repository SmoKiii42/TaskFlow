using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using TaskFlow.Core;

namespace TaskFlow.ViewsSettings
{
    /// <summary>
    /// Логика взаимодействия для ChengePassword.xaml
    /// </summary>
    public partial class ChangePassword : UserControl
    {
        public ChangePassword()
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

            if(!BCrypt.Net.BCrypt.Verify(CurrentPasswordBox.Password, CurrentSession.CurrentUser.Password))
            {
                MessageBox.Show("Invalid password");return;
            }

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new ChangeRepeatPass();
        }
    }
}
