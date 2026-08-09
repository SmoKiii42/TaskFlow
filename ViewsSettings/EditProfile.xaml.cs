using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskFlow.Core;
using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Views;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow.ViewsSettings
{
    /// <summary>
    /// Логика взаимодействия для EditProfile.xaml
    /// </summary>
    public partial class EditProfile : UserControl
    {
        private readonly AppDbContext _context;
        private ServisUser _userService = new ServisUser();
        public EditProfile()
        {
            InitializeComponent();
            _context = new AppDbContext();
            User user = CurrentSession.CurrentUser;

            FirstNameBox.Text = user.FirstName;
            LastNameBox.Text = user.LastName;
            DescriptionBox.Text = user.Description;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new profile();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameBox.Text))
            {
                MessageBox.Show("name is empty!");
                return;
            }

            User user = CurrentSession.CurrentUser;

            user.FirstName = FirstNameBox.Text;
            user.LastName = LastNameBox.Text;
            user.Description = DescriptionBox.Text;

            _userService.UpdateUser(user);

            MessageBox.Show("Profile updated!");

        }
    }
}
