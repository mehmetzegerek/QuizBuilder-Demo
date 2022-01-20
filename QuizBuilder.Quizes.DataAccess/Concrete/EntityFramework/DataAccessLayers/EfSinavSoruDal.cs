using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Core.DataAccess.EntityFramework;
using QuizBuilder.Quizes.DataAccess.Abstract;
using QuizBuilder.Quizes.Entities.ComplexTypes;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework.DataAccessLayers
{
    public class EfSinavSoruDal:EfEntityRepositoryBase<SinavSoru,SinavHavuzuContext>,ISinavSoruDal
    {
        public List<Soru> GetSoruList(int id)
        {
            using (SinavHavuzuContext context = new SinavHavuzuContext())
            {
                var result = from s in context.SinavSorus
                    join p in context.Sorus on s.SoruID equals p.SoruID
                    select new Soru()
                    {
                        SoruID = p.SoruID,
                        Klasik = p.Klasik,
                        Metin = p.Metin,
                        Gorsel = p.Gorsel,
                        SecA = p.SecA,
                        SecB = p.SecB,
                        SecC = p.SecC,
                        SecD = p.SecD
                    };
                return result.ToList();
            }
        }
    }
}
