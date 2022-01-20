using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.Business.Abstract
{
    public interface IQuestionCompailer
    {
        string FormatWithNonePictureStylish(Soru soru,Int16 questionIndex);
        string FormatWithPictureStylish(Soru soru, Int16 questionIndex);
        string FormatWithNonePictureClassic(Soru soru, Int16 questionIndex);
        string FormatWithPictureClassic(Soru soru, Int16 questionIndex);
    }
}
