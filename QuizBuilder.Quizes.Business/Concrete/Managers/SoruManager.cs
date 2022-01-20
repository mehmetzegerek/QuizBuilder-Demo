using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Core.Aspects.Postsharp.ValidationAspects;
using QuizBuilder.Quizes.Business.Abstract;
using QuizBuilder.Quizes.Business.ValidationRules.FluentValidation;
using QuizBuilder.Quizes.DataAccess.Abstract;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.Business.Concrete.Managers
{
    public class SoruManager:ISoruService
    {
        private ISoruDal _soruDal;

        public SoruManager(ISoruDal soruDal)
        {
            _soruDal = soruDal;
        }


        public List<Soru> GetAll()
        {
            return _soruDal.GetList();
        }

        public Soru GetById(int id)
        {
            return _soruDal.Get(x => x.SoruID == id);
        }

        [FluentValidationAspect(typeof(SoruValidator))]
        public Soru Add(Soru Soru)
        {
            return _soruDal.Add(Soru);
        }

        [FluentValidationAspect(typeof(SoruValidator))]
        public Soru Update(Soru Soru)
        {
            return _soruDal.Update(Soru);
        }

        public void Delete(Soru Soru)
        {
            _soruDal.Delete(Soru);
        }

        public List<Soru> GetSoruList(int questionID)
        {
           return _soruDal.GetSoruList(questionID);
        }
    }
}
