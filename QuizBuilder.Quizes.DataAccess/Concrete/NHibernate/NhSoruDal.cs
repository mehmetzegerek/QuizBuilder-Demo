using QuizBuilder.Core.DataAccess.NHibernate;
using QuizBuilder.Quizes.DataAccess.Abstract;
using QuizBuilder.Quizes.Entities.Concrete;
using System;
using System.Collections.Generic;


namespace QuizBuilder.Quizes.DataAccess.Concrete.NHibernate
{
    public class NhSoruDal : NHEntityRepositoryBase<Soru> , ISoruDal
    {
        public NhSoruDal(NHibernateHelper nHibernateHelper):base(nHibernateHelper)
        {

        }

        public List<Soru> GetSoruList(int questionID)
        {
            throw new NotImplementedException();
        }
    }
}
