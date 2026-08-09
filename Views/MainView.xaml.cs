using System.Windows;
using System.Windows.Controls;
using TaskFlow.Core;
using TaskFlow.Services;
using TaskFlow.Новая_папка1.TaskFlow;


namespace TaskFlow.Views
{
    public partial class MainView : UserControl
    {
        private readonly WorkspaceMemberService _memberService =
    new WorkspaceMemberService();
        public MainView()
        {

            InitializeComponent();

            LoadWorkspaces();

        }
        private WorkspaceService _workspaceService = new WorkspaceService();


        private void CreateWorkspaceButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new CreateWorkspaceView();
        }

        private void DeleteSpace_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Workspace workspace = (Workspace)button.DataContext;

            if (MessageBox.Show(
        $"Удалить рабочее пространство \"{workspace.Name}\"?",
        "Подтверждение",
        MessageBoxButton.YesNo,
        MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            _workspaceService.DeleteWorkspace(workspace.Id);

            LoadWorkspaces();
        }

        private void LoadWorkspaces()
        {
            int userId = CurrentSession.CurrentUser.Id;

            WorkspaceList.ItemsSource =
                _workspaceService.GetWorkspacesForUser(userId);
        }

        private void WorkspaceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Workspace? workspace = WorkspaceList.SelectedItem as Workspace;
            if (workspace == null) return;

            CurrentSession.CurrentWorkspace = workspace;

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow?.MainFrame.Content = new WorkspaceView(workspace);
        }

        private void OpenSpace_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            Workspace? workspace = button?.DataContext as Workspace;

            if (workspace == null)
                return;


            bool access = _memberService.HasAccess(
                workspace.Id,
                CurrentSession.CurrentUser.Id);


            if (!access)
            {
                MessageBox.Show(
                    "You don't have access to this workspace.");
                return;
            }


            MainWindow mainWindow =
                (MainWindow)Application.Current.MainWindow;


            mainWindow.MainFrame.Content =
                new WorkspaceView(workspace);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = ( MainWindow )Application.Current.MainWindow;
            mainWindow.ToggleMenu();    
        }

    }
}
