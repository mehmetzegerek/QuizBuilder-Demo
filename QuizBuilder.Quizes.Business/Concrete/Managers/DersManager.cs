using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.Business.Abstract;
using QuizBuilder.Quizes.DataAccess.Abstract;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.Business.Concrete.Managers
{
    public class DersManager:IDersService
    {
        private IDersDal _dersDal;

        public DersManager(IDersDal dersDal)
        {
            _dersDal = dersDal;
        }

        public List<Ders> GetAll()
        {
          return _dersDal.GetList();
        }

        public Ders GetById(int id)
        {
            return _dersDal.Get(x=>x.DersID == id);
        }

        public Ders Add(Ders ders)
        {
            return _dersDal.Add(ders);
        }

        public Ders Update(Ders ders)
        {
            return _dersDal.Update(ders);
        }

        public void Delete(Ders ders)
        {
            _dersDal.Delete(ders);
        }
    }
}
