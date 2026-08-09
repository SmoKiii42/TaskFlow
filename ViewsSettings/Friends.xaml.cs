using System.Windows;
using System.Windows.Controls;
using TaskFlow.Core;
using TaskFlow.Models;
using TaskFlow.Services;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow.ViewsSettings
{
    public partial class Friends : UserControl
    {
        private readonly ServisUser _userService = new ServisUser();
        private readonly FriendsService _friendsService = new FriendsService();
        private readonly WorkspaceInviteService _workspaceInviteService = new WorkspaceInviteService();
        private readonly WorkspaceMemberService _workspaceMemberService = new WorkspaceMemberService();

        public Friends()
        {
            InitializeComponent();
            LoadFriends();
            LoadRequests();
            LoadWorkspaceInvites();
        }

        private void SendFriend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FriendIdBox.Text))
            {
                MessageBox.Show("Enter user ID");
                return;
            }

            if (!int.TryParse(FriendIdBox.Text, out int friendId))
            {
                MessageBox.Show("ID must be number");
                return;
            }

            if (friendId == CurrentSession.CurrentUser.Id)
            {
                MessageBox.Show("You can't add yourself");
                return;
            }

            User? user = _userService.GetUserById(friendId);

            if (user == null)
            {
                MessageBox.Show("User not found");
                return;
            }

            if (_friendsService.RequestExists(CurrentSession.CurrentUser.Id, friendId))
            {
                MessageBox.Show("Request already sent");
                return;
            }

            _friendsService.SendRequest(CurrentSession.CurrentUser.Id, friendId);
            MessageBox.Show("Friend request sent!");
            FriendIdBox.Clear();
        }

        private void AcceptFriend_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            FriendRequest? request = button.DataContext as FriendRequest;

            if (request == null) return;

            _friendsService.AcceptRequest(request.Id);
            MessageBox.Show("Friend added!");
            LoadFriends();
            LoadRequests();
        }

        private void DeclineFriend_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            FriendRequest? request = button.DataContext as FriendRequest;

            if (request == null)
                return;

            _friendsService.DeclineRequest(request.Id);
            LoadRequests();
            MessageBox.Show("Friend request declined");
        }

        private void InviteWorkspace_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User? friend = button.DataContext as User;

            if (friend == null)
                return;

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new InviteWorkSpace(friend);
        }

        private void AcceptWorkspaceInvite_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            WorkspaceInvite? invite = button.DataContext as WorkspaceInvite;

            if (invite == null)
                return;

            _workspaceMemberService.AddMember(invite.WorkspaceId, CurrentSession.CurrentUser.Id, "Member");
            _workspaceInviteService.AcceptInvite(invite.Id);
            MessageBox.Show("You joined workspace!");
            LoadWorkspaceInvites();
        }

        private void DeclineWorkspaceInvite_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            WorkspaceInvite? invite = button.DataContext as WorkspaceInvite;

            if (invite == null)
                return;

            _workspaceInviteService.DeclineInvite(invite.Id);
            MessageBox.Show("Invitation declined");
            LoadWorkspaceInvites();
        }

        private void LoadFriends()
        {
            FriendsList.ItemsSource = _friendsService.GetFriends(CurrentSession.CurrentUser.Id);
        }

        private void LoadRequests()
        {
            RequestsList.ItemsSource = _friendsService.GetIncomingRequests(CurrentSession.CurrentUser.Id);
        }

        private void LoadWorkspaceInvites()
        {
            WorkspaceInviteList.ItemsSource = _workspaceInviteService.GetInvites(CurrentSession.CurrentUser.Id);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new SettingsTask();
        }

        private void DeleteFriend_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User? friend = button.DataContext as User;
            if (friend == null) return;

            _friendsService.RemoveFriend(CurrentSession.CurrentUser.Id, friend.Id);
            MessageBox.Show("Friend deleted!");
            LoadFriends();
        }

        private void FriendIdBox_TextChanged(object sender, TextChangedEventArgs e) { }
    }
}