using Azure.Core.Pipeline;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TaskFlow.Core;
using TaskFlow.Models;
using TaskFlow.Services;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow.Views
{
    /// <summary>
    /// Логика взаимодействия для WorkspaceView.xaml
    /// </summary>
    public partial class WorkspaceView : UserControl
    {
        private readonly Workspace _workspace;

        private readonly TaskService _taskService = new TaskService();

        private readonly WorkspaceMemberService _memberService =
            new WorkspaceMemberService();
        public WorkspaceView(Workspace workspace)
        {
            if (workspace == null)
                throw new Exception("Workspace == NULL");

            InitializeComponent();

            _workspace = workspace;

            WorkspaceName.Text = _workspace.Name;
            WorkspaceDescription.Text = _workspace.Description;

            LoadTask();
            LoadMembers();
        }

        private void LoadMembers()
        {
            MembersList.ItemsSource =
                _memberService.GetMembers(_workspace.Id);
        }
        public void LoadTask()
        {
            TaskList.ItemsSource = _taskService.GetTaskItems(_workspace.Id);
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new CreateTaskView(_workspace.Id);
        }


        private void DeleteTask_Click(Object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            TaskItem task = (TaskItem)button.DataContext;

            if (MessageBox.Show($"Удалить задачу \"{task.Title}\"?", "Подтверждение",MessageBoxButton.YesNo,MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            _taskService.RemoveTask(task.Id);
            LoadTask();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new MainView();
        }


        private void RemoveMember_Click(object sender, RoutedEventArgs e)
        {

            Button button = (Button)sender;


            WorkspaceMember? member =
                button.DataContext as WorkspaceMember;



            if (member == null)
                return;



            int currentUserId =
                CurrentSession.CurrentUser.Id;

            // Проверяем кто удаляет

            WorkspaceMember? currentMember =
                _memberService.GetMember(
                    _workspace.Id,
                    currentUserId);



            if (currentMember == null)
            {
                MessageBox.Show("Access denied");
                return;
            }


            if (currentMember.Role != "Owner")
            {
                MessageBox.Show("Only owner can remove members.");
                return;
            }

            if (member.Role == "Owner")
            {
                MessageBox.Show("You can't remove workspace owner.");

                return;
            }


            if (MessageBox.Show(
                $"Remove {member.User.FirstName}?",
                "Confirm",
                MessageBoxButton.YesNo)
                != MessageBoxResult.Yes)
            {
                return;
            }
            _memberService.RemoveMember(_workspace.Id,member.UserId);

            LoadMembers();MessageBox.Show("Member removed.");

        }

    }
}
