using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework.Mappings
{
    public class DersMap:EntityTypeConfiguration<Ders>
    {
        public DersMap()
        {
            ToTable(@"Dersler", @"dbo");
            HasKey(x => x.DersID);

            Property(x => x.DersID).HasColumnName("DersID");
            Property(x => x.DersAdi).HasColumnName("DersAdi");

        }
    }
}
