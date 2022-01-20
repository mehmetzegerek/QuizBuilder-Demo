using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ninject;
using QuizBuilder.Quizes.Business.Abstract;
using QuizBuilder.Quizes.Entities.Concrete;
using QuizBuilder.Quizes.WindowUI.Windows;

namespace QuizBuilder.Quizes.WindowUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for ManageView.xaml
    /// </summary>
    public partial class ManageView : UserControl
    {
        private MainWindow mnw = (MainWindow)Application.Current.MainWindow;
        private ISoruService _soruService;
        private ISinavKagitService _sinavKagitService;
        private Soru _selectedSoru = null;
        private SinavKagit _selectedKagit = null;
         

        public ManageView()
        {
            InitializeComponent();
            _soruService = mnw.kernel.Get<ISoruService>();
            _sinavKagitService = mnw.kernel.Get<ISinavKagitService>();
        }

        private void ManageView_OnLoaded(object sender, RoutedEventArgs e)
        {
            dgwItems.ItemsSource = _soruService.GetAll();
        }



        private void refreshList(object source)
        {
            dgwItems.ItemsSource = (System.Collections.IEnumerable)source;
        }

        private void BtnRefresh_OnClick(object sender, RoutedEventArgs e)
        {
            if (btnChange.IsChecked == true)
            {
                refreshList(_sinavKagitService.GetAll());
            }
            else
            {
                refreshList(_soruService.GetAll());

            }
        }

        private void DgwItems_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgwItems.SelectedItem is Soru && !(dgwItems.SelectedItems.Count > 1))
            {
                btnDelete.Visibility = Visibility.Visible;
                _selectedSoru = (Soru)dgwItems.SelectedItem;
                mnw.soruHolder = _selectedSoru;
                btnAction.Content = "Düzenle";
                

            }
            else if (dgwItems.SelectedItem is SinavKagit && !(dgwItems.SelectedItems.Count > 1))
            {
                btnDelete.Visibility = Visibility.Visible;
                _selectedKagit = (SinavKagit)dgwItems.SelectedItem;
                mnw.kagitHolder = _selectedKagit;
                btnAction.Content = "Düzenle";
            }
        }

        private void DgwItems_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.Visibility = Visibility.Hidden;
            btnAction.Content = "Ekle";
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (btnChange.IsChecked == false)
            {
                if (_selectedSoru != null)
                {
                    _soruService.Delete(_selectedSoru);
                    refreshList(_soruService.GetAll());
                }
            }
            else
            {
                if (_selectedKagit != null )
                {
                    _sinavKagitService.Delete(_selectedKagit);
                    refreshList(_sinavKagitService.GetAll());
                }
            }
            
        }

        private void BtnAction_OnClick(object sender, RoutedEventArgs e)
        {
            if (btnChange.IsChecked == true)
            {
               
                if ((string)((Button)sender).Content == "Ekle")
                {

                    SinavKagitPenceresi pencere = new SinavKagitPenceresi();
                    pencere.Owner = mnw;
                    pencere.ShowDialog();
                    
                   
                }
                else
                {
                    if (dgwItems.SelectedItem is SinavKagit)
                    {
                        _selectedKagit = (SinavKagit)dgwItems.SelectedItem;
                        SinavKagitPenceresi pencere =
                            new SinavKagitPenceresi(_selectedKagit.SinavAdi,(SinavKagit)dgwItems.SelectedItem);
                        pencere.Owner = mnw;
                        pencere.ShowDialog();

                    }
                }

                

            }
            else
            {
                Button btn = (Button)sender;
                if ((string)btn.Content == "Ekle")
                {
                    SoruPenceresi pencere = new SoruPenceresi();
                    pencere.Title = "Ekleme Penceresi";
                    pencere.Owner = mnw;
                    pencere.ShowDialog();
                }
                else
                {

                    SoruPenceresi pencere = new SoruPenceresi("Düzenleme Penceresi", _selectedSoru.Gorsel,
                        _selectedSoru.Klasik, _selectedSoru.Metin, _selectedSoru.SecA, _selectedSoru.SecB,
                        _selectedSoru.SecC, _selectedSoru.SecD);
                    pencere.Owner = mnw;
                    pencere.ShowDialog();
                }
            }
            
            
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            //ToggleButton tbtn = new ToggleButton();
            //if (tbtn.IsChecked == true)
            //{
            //    dgwItems.ItemsSource = _sinavKagitService.GetAll();
            //}
            //else
            //{
            //    dgwItems.ItemsSource = _soruService.GetAll();
            //}
        }

        private void BtnChange_OnChecked(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked == true)
            {
                dgwItems.ItemsSource = _sinavKagitService.GetAll();
            }
            else
            {
                dgwItems.ItemsSource = _soruService.GetAll();
            }
            

        }
    }
}
