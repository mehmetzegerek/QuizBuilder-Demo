using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.Business.Abstract;
using QuizBuilder.Quizes.DataAccess.Abstract;
using QuizBuilder.Quizes.Entities.ComplexTypes;

namespace QuizBuilder.Quizes.Business.Concrete.Managers
{
    public class SinavSoruManager:ISinavSoruService
    {
        private ISinavSoruDal _sinavSoruDal;

        public SinavSoruManager(ISinavSoruDal sinavSoruDal)
        {
            _sinavSoruDal = sinavSoruDal;
        }

        public List<SinavSoru> GetAll()
        {
            return _sinavSoruDal.GetList();
        }

        public SinavSoru GetById(int id)
        {
            return _sinavSoruDal.Get(x=>x.ID == id);
        }

        public SinavSoru Add(SinavSoru sinavSoru)
        {
            return _sinavSoruDal.Add(sinavSoru);
        }

        public SinavSoru Update(SinavSoru sinavSoru)
        {
            return _sinavSoruDal.Update(sinavSoru);
        }

        public void Delete(SinavSoru sinavSoru)
        {
            _sinavSoruDal.Delete(sinavSoru);
        }
    }
}
