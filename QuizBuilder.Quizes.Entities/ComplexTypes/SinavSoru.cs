using QuizBuilder.Core.Entities;

namespace QuizBuilder.Quizes.Entities.ComplexTypes
{
    public class SinavSoru:IEntity
    {
        public int ID { get; set; }
        public int SinavID { get; set; }
        public int SoruID { get; set; }
    }
}
