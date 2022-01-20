using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ninject;
using QuizBuilder.Quizes.Business.Abstract;
using QuizBuilder.Quizes.Business.DependencyResolvers.Ninject;
using QuizBuilder.Quizes.Entities.Concrete;
using Application = System.Windows.Application;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using UserControl = System.Windows.Controls.UserControl;
using Visibility = System.Windows.Visibility;
using System.IO;
using System.Threading;
using QuizBuilder.Quizes.Entities.ComplexTypes;
using QuizBuilder.Quizes.WindowUI.MVVM.ViewModel;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;


namespace QuizBuilder.Quizes.WindowUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for CreateView.xaml
    /// </summary>
    public partial class CreateView : UserControl
    {
        private MainWindow mnw = (MainWindow)Application.Current.MainWindow;
        private ISoruService _service;
        private ISinavKagitService _sinavService;
        private ISinavSoruService _sinavSoruService;
        private List<Soru> _selectedSorus = new List<Soru>();
        private List<Soru> _randomizedList = new List<Soru>();
        private SinavKagit _currenSinavKagit;
        private bool _status = false;
        private List<Soru> result;
        private IPageCompailer _pageCompailer;
        private Random _random = new Random();


        public CreateView()
        {
            InitializeComponent();
        }

        private void CreateView_OnLoaded(object sender, RoutedEventArgs e)
        {
            _service = mnw.kernel.Get<ISoruService>();
            _sinavService = mnw.kernel.Get<ISinavKagitService>();
            _sinavSoruService = mnw.kernel.Get<ISinavSoruService>();
            _pageCompailer = mnw.kernel.Get<IPageCompailer>();
            questionDgw.ItemsSource = _sinavService.GetAll();
        }


        private void QuestionDgw_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (questionDgw.SelectedItem is SinavKagit)
            {
                lblCountInfo.Visibility = Visibility.Visible;
                _currenSinavKagit = (SinavKagit)questionDgw.SelectedItem;
                result = _service.GetSoruList(((SinavKagit)questionDgw.SelectedItem).SinavID);
                //questionDgw.ItemsSource = _service.GetSoruList(((SinavKagit)questionDgw.SelectedItem).SinavID);
                if (result.Count == 0)
                {
                    questionDgw.ItemsSource = _service.GetAll();
                    lblCountInfo.Content = "Lütfen 20 tane soru seçiniz";
                }
                else
                {
                    questionDgw.ItemsSource = result;
                    _status = true;
                    lblCountInfo.Content = "Kağıt içerisindeki sorular";
                    if (result.Count == 20)
                    { 
                        btnAccept.Visibility = Visibility.Visible;
                    }
                    
                }
            }
        }

        private void questionDgw_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (questionDgw.SelectedItem is Soru && _status == false)
            {
                lblCountInfo.Visibility = Visibility.Visible;
                lblCountInfo.Content = string.Format("Seçilen soru sayısı : {0}/20", questionDgw.SelectedItems.Count);
                if (questionDgw.SelectedItems.Count == 20)
                {
                    btnAccept.Visibility = Visibility.Visible;
                }
                
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content.ToString() == "Onayla")
            {
                if (questionDgw.ItemsSource.GetType() == typeof(List<Soru>) && _status == false)
                {
                    foreach (Soru soru in questionDgw.SelectedItems)
                    {
                        _selectedSorus.Add(soru);
                        _sinavSoruService.Add(new SinavSoru { SinavID = _currenSinavKagit.SinavID, SoruID = soru.SoruID });
                    }

                    btnAccept.Content = "Oluştur";
                }
                else if (questionDgw.ItemsSource.GetType() == typeof(List<Soru>) && _status == true)
                {
                    foreach (Soru soru in questionDgw.ItemsSource)
                    {
                        _selectedSorus.Add(soru);
                    }

                    btnAccept.Content = "Oluştur";
                }

                brdStep1.Visibility = Visibility.Hidden;
                brdStep2.Visibility = Visibility.Visible;
                btnAccept.Visibility = Visibility.Hidden;
                lblCountInfo.Content = "Dosya bilgileri";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(lblPathInfo.Content.ToString()) && !string.IsNullOrWhiteSpace(tbxFileName.Text) && _selectedSorus.Count==20)
                {
                    try
                    {
                        Shuffle(_selectedSorus);
                        MessageBoxResult result = MessageBox.Show(string.Format("'{0}' dosya yoluna\n'{1}' dosya adıyla\n'{2}' adlı sınavı oluştumayı onaylıyor musunuz ?", lblPathInfo.Content.ToString(), tbxFileName.Text, _currenSinavKagit.SinavAdi),
                            caption: "Bilgi", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            _pageCompailer.Create20QuestionsPage(_pageCompailer.GetPath(lblPathInfo.Content.ToString(), tbxFileName.Text+"(A)")
                                , _currenSinavKagit.SinavAdi + " (A)", @"..\..\Images\uniLogo.png", _selectedSorus);
                            Thread.Sleep(200);
                            Shuffle(_selectedSorus);
                            Thread.Sleep(50);
                            _pageCompailer.Create20QuestionsPage(_pageCompailer.GetPath(lblPathInfo.Content.ToString(), tbxFileName.Text + "(B)")
                                , _currenSinavKagit.SinavAdi + " (B)", @"..\..\Images\uniLogo.png", _selectedSorus);
                        }

                        MessageBox.Show("Oluşturma işlemi başarı ile tamamlandı !", "İşlem başarılı",
                            MessageBoxButton.OK);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                    
                    
                }
            }
            
        }

        private void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


        private void BtnOpenFolder_OnClick(object sender, RoutedEventArgs e)
        {
            using (var fdb = new FolderBrowserDialog())
            {
                DialogResult result = fdb.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fdb.SelectedPath))
                {
                    btnAccept.Visibility = Visibility.Visible;
                    lblPathInfo.Content = fdb.SelectedPath;
                    spnlFileName.Effect = null;
                    spnlFileName.IsEnabled = true;
                }
            }

        }



        
    }
}
