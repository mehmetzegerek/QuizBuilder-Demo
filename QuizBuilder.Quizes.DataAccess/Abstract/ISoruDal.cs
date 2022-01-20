using QuizBuilder.Core.DataAccess;
using QuizBuilder.Quizes.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuizBuilder.Quizes.DataAccess.Abstract
{
    public interface ISoruDal:IEntityRepository<Soru>
    {
        List<Soru> GetSoruList(int questionID);
    }
}
