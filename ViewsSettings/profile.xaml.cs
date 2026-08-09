using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using TaskFlow.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TaskFlow.Core;
using TaskFlow.Models;
using TaskFlow.Views;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow.ViewsSettings
{
    /// <summary>
    /// Логика взаимодействия для profile.xaml
    /// </summary>
    public partial class profile : UserControl
    {
        private ServisUser _userService = new ServisUser();
        private readonly AppDbContext _context;
        public profile()
        {
            InitializeComponent();

            _context = new AppDbContext();
            DataContext = CurrentSession.CurrentUser;


            LoadAvatar();
        }


        private void SelectAvatar()
        {
            ProfileImage.Source = null;

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Images|*.png;*.jpg;*.jpeg";

            if (dialog.ShowDialog() != true)
                return;

            string path = dialog.FileName;

            string extension = Path.GetExtension(path);

            string folder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Images",
                "Profiles"
            );

            Directory.CreateDirectory(folder);

            string newFileName =
                $"{CurrentSession.CurrentUser.Id}{extension}";

            
            string destination = Path.Combine(folder, newFileName);

            if(File.Exists(destination))
            {
                File.Delete(destination);
            }

            File.Copy(path, destination);

            string relativePath = Path.Combine("Images", "Profiles", newFileName);
            CurrentSession.CurrentUser.AvatarPath = relativePath;

            _context.Users.Update(CurrentSession.CurrentUser);
            _context.SaveChanges();
            LoadAvatar();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectAvatar();
        }

        private void ProfileImage_Click(object sender, MouseButtonEventArgs e)
        {
            SelectAvatar();
        }

        private void LoadAvatar()
        {
            if (string.IsNullOrWhiteSpace(CurrentSession.CurrentUser.AvatarPath))
                return;

            if (!File.Exists(CurrentSession.CurrentUser.AvatarPath))
                return;

            BitmapImage bitmap = new BitmapImage();

            using (FileStream stream = new FileStream(CurrentSession.CurrentUser.AvatarPath, FileMode.Open, FileAccess.Read))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                bitmap.Freeze();
            }

            ProfileImage.Source = bitmap;
        }

        private void DeletAcc_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
    "Вы действительно хотите удалить аккаунт? Это действие нельзя отменить.",
    "Удаление аккаунта",
    MessageBoxButton.YesNo,
    MessageBoxImage.Warning) != MessageBoxResult.Yes)
            {
                return;
            }

            _userService.DeleteUser(CurrentSession.CurrentUser.Id);

            CurrentSession.CurrentUser = null;

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new RegisterView();

        }

        private void EditProfile_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new EditProfile();
        }

        private void BackToMainV_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = new MainView();
        }
    }
}
