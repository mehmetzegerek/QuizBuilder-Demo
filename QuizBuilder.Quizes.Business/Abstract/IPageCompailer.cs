using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.Business.Abstract
{
    public interface IPageCompailer
    {
        string GetPath(string path, string fileName);
        void Create20QuestionsPage(string path, string quizTitle, string logoPath, List<Soru> questionList);
    }
}
