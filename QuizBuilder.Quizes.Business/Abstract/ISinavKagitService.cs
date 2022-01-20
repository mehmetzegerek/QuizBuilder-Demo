using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.Business.Abstract
{
    public interface ISinavKagitService
    {
        List<SinavKagit> GetAll();
        SinavKagit GetById(int id);
        SinavKagit Add(SinavKagit Sinav);
        SinavKagit Update(SinavKagit Sinav);
        void Delete(SinavKagit Sinav);
    }
}
