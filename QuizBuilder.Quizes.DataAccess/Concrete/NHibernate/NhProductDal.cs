using QuizBuilder.Core.DataAccess.NHibernate;
using QuizBuilder.Quizes.DataAccess.Abstract;
using QuizBuilder.Quizes.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBuilder.Quizes.DataAccess.Concrete.NHibernate
{
    public class NhProductDal:NHEntityRepositoryBase<Product>,IProductDal
    {
        public NhProductDal(NHibernateHelper nHibernateHelper):base(nHibernateHelper)
        {

        }
    }
}
