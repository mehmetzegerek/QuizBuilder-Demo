using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Ninject;
using QuizBuilder.Quizes.Business.Abstract;
using QuizBuilder.Quizes.Entities.Concrete;
using QuizBuilder.Quizes.WindowUI.Effects;
using Image = System.Drawing.Image;

namespace QuizBuilder.Quizes.WindowUI.Windows
{
    /// <summary>
    /// Interaction logic for SoruPenceresi.xaml
    /// </summary>
    public partial class SoruPenceresi : Window
    {
        private MainWindow mnw = (MainWindow)Application.Current.MainWindow;
        private ISoruService _service;
        private bool _state = false;

        private byte[] imgBytes = null;
        private bool _classic = false;
        private string _questionTitle = "";
        private string _optA = "";
        private string _optB = "";
        private string _optC = "";
        private string _optD = "";

        public string Title { get; set; }
        public SoruPenceresi()
        {
            InitializeComponent();
            

        }

        public SoruPenceresi(string Title,byte[] ImgBytes,bool Classic,string QuestionTitle,string OptA,string OptB,string OptC,string OptD)
        {
            InitializeComponent();
            this.Title = Title; 
            imgBytes = ImgBytes;
            _classic = Classic;
            _questionTitle = QuestionTitle;
            _optA = OptA;
            _optB = OptB;
            _optC = OptC;
            _optD = OptD;
            _state = true;


        }

        

        private void SoruPenceresi_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new WindowBlurEffect(this);
            lblTitle.Content = Title;
            SetVeriables(_state);

            _service = mnw.kernel.Get<ISoruService>();
        }

        private void SetVeriables(bool state)
        {
            if (state)
            {
                lblTitle.Content = this.Title;
                imgQuestionImage.Source = ByteToImage(imgBytes);
                setClassic(_classic);
                tbxQuestionText.Text = _questionTitle;
                tbxSecA.Text = _optA;
                tbxSecB.Text = _optB;
                tbxSecC.Text = _optC;
                tbxSecD.Text = _optD;


            }
        }

        private void setClassic(bool classic)
        {
            if (classic)
            {
                rdBtnYes.IsChecked = true;
            }
            else
            {
                rdBtnNo.IsChecked = true;
            }
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnUploadImage_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Multiselect = false;
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                imgBytes = File.ReadAllBytes(dialog.FileName);
                
                
                imgQuestionImage.Source = new BitmapImage(new Uri(dialog.FileName));
            }

        }

        private ImageSource ByteToImage(byte[] imageData)
        {
            ImageSource imgSrc = null;
            if (imageData != null)
            {
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(imageData);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();
                imgSrc = biImg as ImageSource;


            }
            return imgSrc;

        }


        private void BtnAccept_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_state)
            {
                try
                {
                    _service.Add(new Soru()
                    {
                        Gorsel = imgBytes,
                        Klasik = _classic,
                        Metin = tbxQuestionText.Text,
                        SecA = tbxSecA.Text,
                        SecB = tbxSecB.Text,
                        SecC = tbxSecC.Text,
                        SecD = tbxSecD.Text
                    });

                    this.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            else
            {
                mnw.soruHolder.Gorsel = imgBytes;
                mnw.soruHolder.Klasik = _classic;
                mnw.soruHolder.Metin = tbxQuestionText.Text;
                mnw.soruHolder.SecA = tbxSecA.Text;
                mnw.soruHolder.SecB = tbxSecB.Text;
                mnw.soruHolder.SecC = tbxSecC.Text;
                mnw.soruHolder.SecD = tbxSecD.Text;

                _service.Update(mnw.soruHolder);
                mnw.soruHolder = null;
                this.Close();
            }
           
           
        }

        private void RdBtnNo_OnChecked(object sender, RoutedEventArgs e)
        {
            RadioButton rbtn = (RadioButton)sender;
            if (rbtn.Name == "rdBtnYes")
            {
                List<TextBox> boxes = new List<TextBox>() { tbxSecA, tbxSecB, tbxSecC, tbxSecD };
                _classic = true;
                foreach (var tbx in boxes)
                {
                    tbx.Text = null;
                    tbx.IsEnabled = false;
                }
            }
            else
            {
                List<TextBox> boxes = new List<TextBox>() { tbxSecA, tbxSecB, tbxSecC, tbxSecD };
                if (!boxes.Contains(null))
                {
                    foreach (var tbx in boxes)
                    {
                        tbx.IsEnabled = true;
                    }
                }
                

                _classic = false;
            }
        }
    }
}
