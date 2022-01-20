using QuizBuilder.Core.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;



namespace QuizBuilder.Quizes.Entities.Concrete
{
    public class Soru:IEntity
    {
        public virtual int SoruID { get; set; }
        public virtual string Metin { get; set; }
        public virtual string SecA { get; set; }
        public virtual string SecB { get; set; }
        public virtual string SecC { get; set; }
        public virtual string SecD { get; set; }
        public virtual byte[] Gorsel { get; set; }
        public virtual bool Klasik{ get; set; }

        //public virtual Bitmap QuestionImage
        //{
        //    get
        //    {
        //        Bitmap bmp = null;
        //        if (Gorsel != null)
        //        {
        //            MemoryStream stream = new MemoryStream(Gorsel);
        //            Image img = Image.FromStream(stream);
        //            bmp = new Bitmap(img);
                    
        //        }

        //        return bmp;

        //    }
        //}
    }
}
