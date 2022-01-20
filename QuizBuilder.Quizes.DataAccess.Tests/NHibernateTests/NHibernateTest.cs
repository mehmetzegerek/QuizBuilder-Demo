using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework;
using QuizBuilder.Core.DataAccess.EntityFramework;
using System;
using QuizBuilder.Quizes.DataAccess.Concrete.NHibernate;
using QuizBuilder.Quizes.DataAccess.Concrete.NHibernate.Helpers;

namespace QuizBuilder.Quizes.DataAccess.Tests.NHibernateTests
{
    [TestClass]
    public class NHibernateTest
    {
        [TestMethod]
        public void get_all_returns_all_questions()
        {
            NhSoruDal soruDal = new NhSoruDal(new SqlServerHelper());
            var result = soruDal.GetList();

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void get_all_with_parameters_returns_filtered_questions()
        {
            NhSoruDal soruDal = new NhSoruDal(new SqlServerHelper());

            var result = soruDal.GetList(x=>x.Metin.Contains("es"));

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void get_all_returns_all_products()
        {
            NhProductDal soruDal = new NhProductDal(new SqlServerHelper());
            var result = soruDal.GetList();

            Assert.AreEqual(79, result.Count);
        }

        [TestMethod]
        public void get_all_with_parameters_returns_filtered_products()
        {
            NhProductDal soruDal = new NhProductDal(new SqlServerHelper());


            var result = soruDal.GetList(x => x.ProductId > 70);

            Assert.AreEqual(9, result.Count);
        }
    }
}
