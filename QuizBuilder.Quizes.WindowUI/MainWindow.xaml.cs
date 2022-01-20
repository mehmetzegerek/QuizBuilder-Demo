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
using Ninject;
using QuizBuilder.Quizes.Business.DependencyResolvers.Ninject;
using QuizBuilder.Quizes.Entities.Concrete;
using QuizBuilder.Quizes.WindowUI.Core;
using QuizBuilder.Quizes.WindowUI.Effects;
using QuizBuilder.Quizes.WindowUI.MVVM.ViewModel;

namespace QuizBuilder.Quizes.WindowUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IKernel kernel = new StandardKernel(new BusinessModule());
        public Soru soruHolder = null;
        public SinavKagit kagitHolder = null;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            BackgroundControl.DataContext = new WindowBlurEffect(this);
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
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
                WindowState = WindowState.Minimized;
            }
        }

       
    }
}
