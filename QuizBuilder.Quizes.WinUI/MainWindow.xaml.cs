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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuizBuilder.Quizes.WinUI.Classes;

namespace QuizBuilder.Quizes.WinUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public int shit = 1;
        public MainWindow()
        {
            InitializeComponent();
            
            
        }



        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();

            
            
            
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            BackgroundControl.DataContext = new WindowBlurEffect(this);
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "btnClose")
            {
                this.Close();
            }
            else
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        
    }
}
