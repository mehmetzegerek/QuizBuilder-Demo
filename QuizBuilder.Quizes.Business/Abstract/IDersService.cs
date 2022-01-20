using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.Business.Abstract
{
    public interface IDersService
    {
        List<Ders> GetAll();
        Ders GetById(int id);
        Ders Add(Ders ders);
        Ders Update(Ders ders);
        void Delete(Ders ders);
    }
}
