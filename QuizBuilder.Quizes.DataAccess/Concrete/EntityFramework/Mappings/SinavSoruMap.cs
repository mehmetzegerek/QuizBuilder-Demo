using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.Entities.ComplexTypes;

namespace QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework.Mappings
{
    public class SinavSoruMap:EntityTypeConfiguration<SinavSoru>
    {
        public SinavSoruMap()
        {
            ToTable(@"SinavSoru", @"dbo");
            HasKey(x => x.ID);

            Property(x => x.SoruID).HasColumnName("SoruID");
            Property(x => x.ID).HasColumnName("ID");
            Property(x => x.SinavID).HasColumnName("SınavID");
        }
    }
}
