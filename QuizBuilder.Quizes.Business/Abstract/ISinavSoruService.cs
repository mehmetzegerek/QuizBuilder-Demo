using QuizBuilder.Quizes.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBuilder.Quizes.Business.Abstract
{
    public interface ISinavSoruService
    {
        List<SinavSoru> GetAll();
        SinavSoru GetById(int id);
        SinavSoru Add(SinavSoru Soru);
        SinavSoru Update(SinavSoru Soru);
        void Delete(SinavSoru Soru);
    }
}
