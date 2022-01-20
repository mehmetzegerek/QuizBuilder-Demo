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
    /// Interaction logic for SinavKagitPenceresi.xaml
    /// </summary>
    public partial class SinavKagitPenceresi : Window
    {
        private MainWindow mnw = (MainWindow)Application.Current.MainWindow;
        private ISinavKagitService _service;
        private IDersService _dersService;
        private bool _static = false;
        private SinavKagit _sinavKagit = null;
        private string _paperText = "";

        public SinavKagitPenceresi()
        {
            InitializeComponent();
        }

        public SinavKagitPenceresi(string paperText,SinavKagit sinavKagit)
        {
            InitializeComponent();
            _paperText = paperText;
            _sinavKagit = sinavKagit;
            _static = true;
        }

        private void SinavKagitPenceresi_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new WindowBlurEffect(this);
            _service = mnw.kernel.Get<ISinavKagitService>();
            _dersService = mnw.kernel.Get<IDersService>();

            SetCbxLessons();
            if (_static)
            {
                tbxQuizTitle.Text = _paperText;
                cbxLessons.SelectedValue = _sinavKagit.DersID;
            }

        }

        private void SetCbxLessons()
        {
            cbxLessons.ItemsSource = _dersService.GetAll();
            cbxLessons.DisplayMemberPath = "DersAdi";
            cbxLessons.SelectedValuePath = "DersID";
            cbxLessons.SelectedIndex = 0;
        }


        private void BtnAccept_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_static)
                {
                    _sinavKagit.DersID = cbxLessons.SelectedIndex + 1;
                    _sinavKagit.SinavAdi = tbxQuizTitle.Text;
                    _service.Update(_sinavKagit);
                    this.Close();
                }
                else
                {
                    _service.Add(new SinavKagit()
                    {
                        DersID = ((Ders)cbxLessons.SelectedItem).DersID,
                        SinavAdi = tbxQuizTitle.Text
                    });
                    this.Close();
                }
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }


        private void BtnManageLessons_OnClick(object sender, RoutedEventArgs e)
        {
            DersPenceresi pencere = new DersPenceresi();
            pencere.Owner = mnw;
            pencere.ShowDialog();
            SetCbxLessons();
            
        }


        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
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
