using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MyBot.Supporter.V2
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();

            //Find MBR banner
            image.Source = new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images\\Logo.png")));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.ActualWidth - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.ActualHeight - this.ActualHeight) / 2;
            Topmost = true;
        }
    }
}
