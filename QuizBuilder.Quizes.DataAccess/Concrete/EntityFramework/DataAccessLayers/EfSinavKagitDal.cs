using QuizBuilder.Core.DataAccess.EntityFramework;
using QuizBuilder.Quizes.DataAccess.Abstract;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework.DataAccessLayers
{
    public class EfSinavKagitDal:EfEntityRepositoryBase<SinavKagit,SinavHavuzuContext>,ISinavKagitDal
    {
    }
}
