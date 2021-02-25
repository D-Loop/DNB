using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DNB1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void fuulscreen_Click(object sender, RoutedEventArgs e)
        {
            if (MaterialDesignThemes.Wpf.PackIconKind.FullscreenExit != fspi.Kind)
            {
                Launch.WindowState = WindowState.Maximized;
                fspi.Kind = MaterialDesignThemes.Wpf.PackIconKind.FullscreenExit;
            }
            else
            {
                Launch.WindowState = WindowState.Normal;
                fspi.Kind = MaterialDesignThemes.Wpf.PackIconKind.Fullscreen;

            }
        }

        private void hide_Click(object sender, RoutedEventArgs e)
        {
            Launch.WindowState = WindowState.Minimized;
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
    }
}
