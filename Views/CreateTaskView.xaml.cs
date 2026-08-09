using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using TaskFlow.Services;


namespace TaskFlow.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateTaskView.xaml
    /// </summary>
    public partial class CreateTaskView : UserControl
    {
        private int _workspaceID;
        private TaskService _taskService;



        public CreateTaskView(int workspaceID)
        {
            InitializeComponent();

            _workspaceID = workspaceID;

            _taskService = new TaskService();
        }

        private void TitleBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Можно добавить валидацию заголовка или включение/отключение кнопки Create
            // Здесь оставим пустую реализацию чтобы устранить ошибку компиляции
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleBox.Text;
            string description = DescriptionBox.Text;
            string priority = PriorityBox.Text;

            if(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(priority))
            {
                MessageBox.Show("У задачи должно быть название и приоритет"); return;
            }

            _taskService.AddTask(
                title,
                description,
                priority,
                DateTime.Now,
                _workspaceID
            );

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new WorkspaceView(new WorkspaceService().GetWorkspace(_workspaceID));

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new MainView();
        }
    }
}
