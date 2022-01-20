using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.Business.Abstract
{
    public interface ISoruService
    {
        List<Soru> GetAll();
        Soru GetById(int id);
        Soru Add(Soru Soru);
        Soru Update(Soru Soru);
        void Delete(Soru Soru);

        List<Soru> GetSoruList(int questionID);

    }
}
