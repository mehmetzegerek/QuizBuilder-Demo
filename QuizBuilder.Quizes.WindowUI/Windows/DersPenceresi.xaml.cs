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
using System.Windows.Shapes;
using Ninject;
using QuizBuilder.Quizes.Business.Abstract;
using QuizBuilder.Quizes.Entities.Concrete;
using QuizBuilder.Quizes.WindowUI.Effects;

namespace QuizBuilder.Quizes.WindowUI.Windows
{
    /// <summary>
    /// Interaction logic for DersPenceresi.xaml
    /// </summary>
    public partial class DersPenceresi : Window
    {
        private MainWindow mnw = (MainWindow)Application.Current.MainWindow;
        private IDersService _dersService;
        private bool _state = true;
        private Ders _currentDers = null;
        public DersPenceresi()
        {
            InitializeComponent();
        }

        private void DersPenceresi_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new WindowBlurEffect(this);
            _dersService = mnw.kernel.Get<IDersService>();
            dgwLessons.ItemsSource = _dersService.GetAll();
            dgwLessons.Columns[0].Visibility = Visibility.Hidden;
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAction_OnClick(object sender, RoutedEventArgs e)
        {
            if ((string)((Button)sender).Content == "Ekle")
            {
                brdContent.Visibility = Visibility.Visible;
                
            }
            else if(dgwLessons.SelectedItem is Ders && (string)((Button)sender).Content == "Düzenle")
            {
                tbxPageTitle.Text = ((Ders)dgwLessons.SelectedItem).DersAdi;
                brdContent.Visibility = Visibility.Visible;
                
            }
        }

        private void dgwLessons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            brdContent.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
            btnAction.Content = "Ekle";
            tbxPageTitle.Text = null;
            _state = true;
        }

        private void DgwLessons_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgwLessons.SelectedItem is Ders)
            {
                btnDelete.Visibility = Visibility.Visible;
                btnAction.Content = "Düzenle";
                _currentDers = (Ders)dgwLessons.SelectedItem;
                _state = false;
            }
        }

        private void BtnAccept_OnClick(object sender, RoutedEventArgs e)
        {
            if (_state)
            {
                if (tbxPageTitle.Text != "")
                {
                    _dersService.Add(new Ders() { DersAdi = tbxPageTitle.Text });
                    tbxPageTitle.Text = null;
                    dgwLessons.ItemsSource = _dersService.GetAll();
                    dgwLessons.Columns[0].Visibility = Visibility.Hidden;

                }
            }
            else
            {
                if (tbxPageTitle.Text != "")
                {
                    _currentDers.DersAdi = tbxPageTitle.Text;
                    _dersService.Update(_currentDers);
                    dgwLessons.ItemsSource = _dersService.GetAll();
                    dgwLessons.Columns[0].Visibility = Visibility.Hidden;

                }
            }
            
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (_currentDers != null && !(dgwLessons.SelectedItems.Count > 1))
            {
                _dersService.Delete(_currentDers);
                dgwLessons.ItemsSource = _dersService.GetAll();
            }
        }


        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
