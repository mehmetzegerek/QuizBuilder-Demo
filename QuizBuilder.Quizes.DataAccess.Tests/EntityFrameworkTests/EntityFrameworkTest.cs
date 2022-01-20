using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework;
using QuizBuilder.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QuizBuilder.Quizes.Business.Concrete.Compailers;
using QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework.DataAccessLayers;
using QuizBuilder.Quizes.Entities.ComplexTypes;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.DataAccess.Tests.EntityFrameworkTests
{
    [TestClass]
    public class EntityFrameworkTest
    {
        // Question tests
        [TestMethod]
        public void get_all_returns_all_questions()
        {
            EfSoruDal soruDal = new EfSoruDal();
            var result = soruDal.GetList();

            Shuffle(result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void get_all_with_parameters_returns_filtered_questions()
        {
            EfSoruDal soruDal = new EfSoruDal();
            var result = soruDal.GetList(x=>x.Metin.Contains("es"));

            Assert.AreEqual(2, result.Count);
        }

        private void Shuffle<T>(IList<T> list)
        {
            Random _random = new Random();
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

        [TestMethod]
        public void set_image_sets_image()
        {
            EfSoruDal soruDal = new EfSoruDal();
            var entity = soruDal.Get(x => x.SoruID == 2);
            byte[] imageDataBytes = File.ReadAllBytes(@"C:\Users\Mehmet\Desktop\right-arrow.png");

            MemoryStream stream = new MemoryStream(imageDataBytes);
            Image img = Image.FromStream(stream); // byte arrayden görsele geçme

            entity.Gorsel = imageDataBytes;
            soruDal.Update(entity);
        }

        // product tests

        [TestMethod]
        public void get_all_returns_all_products()
        {
            EfProductDal productDal = new EfProductDal();
            var result = productDal.GetList();

            Assert.AreEqual(79, result.Count);
        }

        
        [TestMethod]
        public void get_all_with_parameters_returns_filtered_products()
        {
            EfProductDal productDal = new EfProductDal();
            var result = productDal.GetList(x=>x.ProductId > 70);

            Assert.AreEqual(9, result.Count);
        }

        // sinavKagit tests
        [TestMethod]
        public void get_all_returns_all_quizes()
        {
            EfSinavKagitDal sinavKagit = new EfSinavKagitDal();
            var result = sinavKagit.GetList();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void add_quiz_appened_quiz()
        {
            EfSinavKagitDal sinavKagit = new EfSinavKagitDal();
            sinavKagit.Add(new SinavKagit() { DersID = 1, SinavAdi = "Test Sınavı" });

        }

        // Ders tests
        [TestMethod]
        public void get_all_returns_all_lessons()
        {
            EfDersDal dersDal = new EfDersDal();
            var result = dersDal.GetList();

            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public void list_off_questions_test()
        {
            EfSoruDal dal = new EfSoruDal();
            var result = dal.GetSoruList(1002);
            Assert.AreEqual(2,result.Count);
        }

        [TestMethod]
        public void add_sinavSoru_appened()
        {
            EfSinavSoruDal dal = new EfSinavSoruDal();
            var result = dal.Add(new SinavSoru { SinavID = 1, SoruID = 8016 });
        }

        [TestMethod]
        public void question_Page_Tests()
        {
            EfSoruDal dal = new EfSoruDal();
            PageCompailer compailer = new PageCompailer();
            compailer.Create20QuestionsPage(compailer.GetPath(@"C:\Users\Mehmet\Desktop\sorular","Tests1"),
                "Ankara üniversitesi cart curt sınavı", @"C:\Users\Mehmet\Desktop\uniLogo.png",dal.GetList());
        }


    }
}
