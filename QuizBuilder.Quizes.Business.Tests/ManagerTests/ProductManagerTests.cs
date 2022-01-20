using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentValidation;
using Moq;
using QuizBuilder.Quizes.Business.Concrete.Managers;
using QuizBuilder.Quizes.DataAccess.Abstract;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.Business.Tests
{
    [TestClass]
    public class ProductManagerTests
    {
        //[ExpectedException(typeof(ValidationException))]
        [TestMethod]
        public void Porduct_Validation_Check()
        {
            //Mock<IProductDal> mock = new Mock<IProductDal>();
            //ProductManager productManager = new ProductManager(mock.Object);

            //productManager.Add(new Product());

            Mock<ISoruDal> mock2 = new Mock<ISoruDal>();
            SoruManager soruManager = new SoruManager(mock2.Object);

            soruManager.Add(new Soru{Klasik = true,Metin = "asddfghjkl"});
        }
        // sinavkagit tests

        [ExpectedException(typeof(ValidationException))]
        [TestMethod]
        public void SinavKagit_Validation_Check()
        {
            //Mock<IProductDal> mock = new Mock<IProductDal>();
            //ProductManager productManager = new ProductManager(mock.Object);

            //productManager.Add(new Product());

            Mock<ISinavKagitDal> mock2 = new Mock<ISinavKagitDal>();
            SinavKagitManager sinavKagitManager = new SinavKagitManager(mock2.Object);

            sinavKagitManager.Add(new SinavKagit(){SinavAdi = "asdakalskdalsfkalşsasgfasgasdasgasf"});
        }

    }
}
