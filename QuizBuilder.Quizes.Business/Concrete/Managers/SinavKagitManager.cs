using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Core.Aspects.Postsharp.ValidationAspects;
using QuizBuilder.Quizes.Business.Abstract;
using QuizBuilder.Quizes.Business.ValidationRules.FluentValidation;
using QuizBuilder.Quizes.DataAccess.Abstract;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.Business.Concrete.Managers
{
    public class SinavKagitManager:ISinavKagitService
    {
        private ISinavKagitDal _sinavKagitDal;

        public SinavKagitManager(ISinavKagitDal sinavKagitDal)
        {
            _sinavKagitDal = sinavKagitDal;
        }


        public List<SinavKagit> GetAll()
        {
            return _sinavKagitDal.GetList();
        }

        public SinavKagit GetById(int id)
        {
            return _sinavKagitDal.Get(x => x.SinavID == id);
        }

        [FluentValidationAspect(typeof(SinavKagitValidator))]
        public SinavKagit Add(SinavKagit Sinav)
        {
            return _sinavKagitDal.Add(Sinav);
        }

        [FluentValidationAspect(typeof(SinavKagitValidator))]
        public SinavKagit Update(SinavKagit Sinav)
        {
            return _sinavKagitDal.Update(Sinav);
        }

        public void Delete(SinavKagit Sinav)
        {
            _sinavKagitDal.Delete(Sinav);
        }
    }
}
