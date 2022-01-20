using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QuizBuilder.Quizes.WindowUI
{
    /// <summary>
    /// Interaction logic for LoadScreen.xaml
    /// </summary>
    public partial class LoadScreen : Window
    {
        MainWindow mw = new MainWindow();

        private DispatcherTimer timer = new DispatcherTimer();

        public LoadScreen()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //Application.Current.MainWindow = mw;
            this.Close();
            mw.Show();
            timer.Stop();
        }

        private void LoadScreen_OnLoaded(object sender, RoutedEventArgs e)
        {
            Thread animThread = new Thread(animTask);

            animThread.Name = "Animation Thread";
            animThread.Start();

            timer.Start();



        }


        private void animTask()
        {
            Dispatcher.Invoke(() =>
            {

                ThicknessAnimation zegmaAnimation = new ThicknessAnimation(lblZegma.Padding,
                    new Thickness(10, 0, 0, 0), new Duration(TimeSpan.FromMilliseconds(1000)));
                zegmaAnimation.EasingFunction = new QuarticEase();
                lblZegma.BeginAnimation(PaddingProperty, zegmaAnimation);



                ThicknessAnimation logoAnimation = new ThicknessAnimation(logoImage.Margin,
                    new Thickness(20, 0, 0, 0), new Duration(TimeSpan.FromMilliseconds(500)));
                logoAnimation.EasingFunction = new QuarticEase();
                logoImage.BeginAnimation(MarginProperty, logoAnimation);

                ThicknessAnimation examoozeAnimation = new ThicknessAnimation(lblProgramName.Padding,
                    new Thickness(90, 0, 0, 0), new Duration(TimeSpan.FromMilliseconds(500)));
                examoozeAnimation.EasingFunction = new QuarticEase();
                lblProgramName.BeginAnimation(PaddingProperty, examoozeAnimation);


            });

        }
    }
}
