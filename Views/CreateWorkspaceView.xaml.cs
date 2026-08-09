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
using TaskFlow.Services;

namespace TaskFlow.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateWorkspaceView.xaml
    /// </summary>
    public partial class CreateWorkspaceView : UserControl
    {
        public CreateWorkspaceView()
        {
            InitializeComponent();
        }
        private WorkspaceService _workspaceService = new WorkspaceService();

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string name = WorkspaceNameBox.Text;
            string description = WorkspaceDescriptionBox.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("У задачи должно быть название и описание"); return;
            }

            _workspaceService.CreateWorkspace(
                name,
                description,
                CurrentSession.CurrentUser.Id);

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new MainView();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new MainView();
        }
    }
}
