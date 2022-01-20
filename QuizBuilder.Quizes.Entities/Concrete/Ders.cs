using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Core.Entities;

namespace QuizBuilder.Quizes.Entities.Concrete
{
    public class Ders:IEntity
    {
        public int DersID{ get; set; }
        public string DersAdi{ get; set; }
    }
}
