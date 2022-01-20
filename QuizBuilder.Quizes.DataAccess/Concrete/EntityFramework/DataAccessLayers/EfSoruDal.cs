using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using NHibernate.Hql.Ast.ANTLR.Tree;
using QuizBuilder.Core.DataAccess.EntityFramework;
using QuizBuilder.Quizes.DataAccess.Abstract;
using QuizBuilder.Quizes.Entities.ComplexTypes;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework.DataAccessLayers
{
    public class EfSoruDal : EfEntityRepositoryBase<Soru,SinavHavuzuContext> ,ISoruDal
    {
        public List<Soru> GetSoruList(int questionID)
        {
            List<Soru> sorular = new List<Soru>();
            using (SinavHavuzuContext context = new SinavHavuzuContext())
            {
                var result = from p in context.SinavSorus
                    join c in context.Sorus on p.SoruID equals c.SoruID where p.SinavID == questionID
                    select new SoruDetails
                    {
                        SinavID = p.SinavID,
                        SoruID = c.SoruID
                    };

                foreach (SoruDetails details in result.ToList())
                {
                    sorular.Add(this.Get(x=>x.SoruID == details.SoruID));
                }
                return sorular;
            }
            
            
        }
    }
}
