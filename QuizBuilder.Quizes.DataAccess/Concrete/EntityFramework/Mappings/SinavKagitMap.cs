using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework.Mappings
{
    public class SinavKagitMap:EntityTypeConfiguration<SinavKagit>
    {
        public SinavKagitMap()
        {
            ToTable(@"Sinav", @"dbo");
            HasKey(x => x.SinavID);

            Property(x => x.SinavID).HasColumnName("SinavID");
            Property(x => x.DersID).HasColumnName("DersID");
            Property(x => x.SinavAdi).HasColumnName("SinavAdi");
        }
    }
}
