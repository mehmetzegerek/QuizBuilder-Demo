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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ninject;
using QuizBuilder.Quizes.Business.Abstract;
using QuizBuilder.Quizes.Business.Concrete.Managers;
using QuizBuilder.Quizes.Business.DependencyResolvers.Ninject;
using QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework;

namespace QuizBuilder.Quizes.WinUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for CreateView.xaml
    /// </summary>
    public partial class CreateView : UserControl
    {
        private ISoruService soruService;

        public CreateView()
        {
            InitializeComponent();
            IKernel kernel = new StandardKernel(new BusinessModule());
            soruService = kernel.Get<ISoruService>();
        }

        private async Task setSource()
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    QuestionView.ItemsSource = soruService.GetAll();

                });
            });


        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await setSource();
        }
    }
}
