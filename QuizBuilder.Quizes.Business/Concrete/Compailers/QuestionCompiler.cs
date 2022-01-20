using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.Business.Abstract;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.Business.Concrete.Compailers
{
    class QuestionCompiler:IQuestionCompailer
    {
        public string FormatWithNonePictureStylish(Soru soru, Int16 questionIndex)
        {
            string format = "S{0}) {1}\n" +
                            "\n" +
                            "A) {2}\n" +
                            "B) {3}\n" +
                            "C) {4}\n" +
                            "D) {5}\n";

            return string.Format(format, questionIndex, soru.Metin, soru.SecA, soru.SecB, soru.SecC, soru.SecD);
        }

        public string FormatWithPictureStylish(Soru soru, short questionIndex)
        {
            throw new NotImplementedException();
        }

        public string FormatWithNonePictureClassic(Soru soru, short questionIndex)
        {
            throw new NotImplementedException();
        }

        public string FormatWithPictureClassic(Soru soru, short questionIndex)
        {
            string format = "S{0}) {1}\n";
            return string.Format(format, questionIndex, soru.Metin);
        }
    }
}
