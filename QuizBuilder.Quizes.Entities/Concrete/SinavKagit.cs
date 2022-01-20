using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Core.Entities;

namespace QuizBuilder.Quizes.Entities.Concrete
{
    public class SinavKagit:IEntity
    {
        public int SinavID{ get; set; }
        public string SinavAdi{ get; set; }
        public int DersID{ get; set; }
    }
}
