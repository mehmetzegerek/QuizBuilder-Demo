using QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework.Mappings;
using QuizBuilder.Quizes.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.Entities.ComplexTypes;

namespace QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework
{
    public class SinavHavuzuContext:DbContext
    {
        public SinavHavuzuContext()
        {
            Database.SetInitializer<SinavHavuzuContext>(null);
        }
        public DbSet<Soru> Sorus { get; set; }
        public DbSet<SinavKagit> Kagits { get; set; }
        public DbSet<Ders> Derses { get; set; }
        public DbSet<SinavSoru> SinavSorus { get; set; }
        //

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SoruMap());
            modelBuilder.Configurations.Add(new SinavKagitMap());
            modelBuilder.Configurations.Add(new DersMap());
            modelBuilder.Configurations.Add(new SinavSoruMap());
            //
        }
    }
}
