using FluentNHibernate.Mapping;
using QuizBuilder.Quizes.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBuilder.Quizes.DataAccess.Concrete.NHibernate.Mappings
{
    public class SoruMap:ClassMap<Soru>
    {
        public SoruMap()
        {
            Table(@"Sorular");
            LazyLoad();

            Id(x => x.SoruID).Column("SoruID");

            Map(x => x.Metin).Column("Metin");
            Map(x => x.SecA).Column("SecA");
            Map(x => x.SecB).Column("SecB");
            Map(x => x.SecC).Column("SecC");
            Map(x => x.SecD).Column("SecD");
            Map(x => x.Gorsel).Column("Gorsel");
            Map(x => x.Klasik).Column("Klasik");

        }
    }
}
