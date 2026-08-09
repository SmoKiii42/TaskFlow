using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using TaskFlow.Core;
using TaskFlow.Models;
using TaskFlow.Services;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow.ViewsSettings
{
    /// <summary>
    /// Логика взаимодействия для InviteWorkSpace.xaml
    /// </summary>
    public partial class InviteWorkSpace : UserControl
    {
        private readonly WorkspaceInviteService _inviteService =
    new WorkspaceInviteService();
        private readonly User _friend;
        private readonly WorkspaceService _workspaceService = new WorkspaceService();

        public InviteWorkSpace(User friend)
        {
            InitializeComponent();

            _friend = friend;

            DataContext = _friend;

            WorkspaceList.ItemsSource = _workspaceService.GetWorkspacesByOwner(CurrentSession.CurrentUser.Id);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new Friends();
        }

        private void SendInvite_Click(object sender, RoutedEventArgs e)
        {
            Workspace? workspace = WorkspaceList.SelectedItem as Workspace;

            if (workspace == null)
            {
                MessageBox.Show("Select workspace.");
                return;
            }

            _inviteService.SendInvite(
                workspace.Id,
                CurrentSession.CurrentUser.Id,
                _friend.Id);

            MessageBox.Show("Invitation sent!");

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new Friends();   
        }
    }
}


