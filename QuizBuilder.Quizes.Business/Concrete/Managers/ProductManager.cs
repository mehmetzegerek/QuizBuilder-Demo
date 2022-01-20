using QuizBuilder.Core.CrossCuttingConcerns.Valdiation.FluentValidation;
using QuizBuilder.Quizes.Business.Abstract;
using QuizBuilder.Quizes.Business.ValidationRules.FluentValidation;
using QuizBuilder.Quizes.DataAccess.Abstract;
using QuizBuilder.Quizes.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Core.Aspects.Postsharp;
using QuizBuilder.Core.Aspects.Postsharp.TransactionAspects;
using QuizBuilder.Core.Aspects.Postsharp.ValidationAspects;

namespace QuizBuilder.Quizes.Business.Concrete.Managers
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        public Product Add(Product product)
        {
            return _productDal.Add(product);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetList();
        }

        public Product GetById(int id)
        {
            return _productDal.Get(p => p.ProductId == id);
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        public Product Update(Product product)
        { 
            return _productDal.Update(product);
        }

        [TransactionScopeAspect]
        public void TransactionalOperation(Product product1, Product product2)
        {
            _productDal.Add(product1);
            //business codes
            _productDal.Update(product2);
        }
    }
}
