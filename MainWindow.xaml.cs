using Microsoft.EntityFrameworkCore.Query.Internal;
using System.IO.Packaging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TaskFlow.Views;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow
{



    public partial class MainWindow : Window
    {

        private bool _isMenuOpen;

        public MainWindow()
        {
            InitializeComponent();

            ServisUser servisUser = new ServisUser();

            if (servisUser.UserExists())
            {
                MainFrame.Content = new LoginView();
            }
            else
            {
                MainFrame.Content = new RegisterView();
            }
        }



        private void OpenMenu() 
        {
            Overlay.Visibility = Visibility.Visible;
         
            TranslateTransform transform =(TranslateTransform)MenuPanel.RenderTransform;
            
            transform.X = 100;
            _isMenuOpen = true;
        }

        public void CloseMenu()
        {
            Overlay.Visibility = Visibility.Collapsed;

            TranslateTransform transform = (TranslateTransform)MenuPanel.RenderTransform;
            
            transform.X = -320;
            _isMenuOpen = false;
        }

        public void ToggleMenu()
        {
            if (_isMenuOpen)
            {
                CloseMenu();
                
            }
            else
            {
                OpenMenu();
                
            }
        }

        private void Overlay_MouseLeftButtonDown(object sender, System.Windows.RoutedEventArgs e)
        {
            CloseMenu();
        }

    }
}