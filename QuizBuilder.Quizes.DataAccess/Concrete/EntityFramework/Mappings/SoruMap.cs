using QuizBuilder.Quizes.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework.Mappings
{
    public class SoruMap:EntityTypeConfiguration<Soru>
    {
        public SoruMap()
        {
            ToTable(@"Sorular", @"dbo");
            HasKey(x => x.SoruID);

            Property(x => x.SoruID).HasColumnName("SoruID");
            Property(x => x.Metin).HasColumnName("Metin");
            Property(x => x.SecA).HasColumnName("SecA");
            Property(x => x.SecB).HasColumnName("SecB");
            Property(x => x.SecC).HasColumnName("SecC");
            Property(x => x.SecD).HasColumnName("SecD");
            Property(x => x.Gorsel).HasColumnName("Gorsel").HasColumnType("image");
            Property(x => x.Klasik).HasColumnName("Klasik");


        }
    }
}
