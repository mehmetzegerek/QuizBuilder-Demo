using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using QuizBuilder.Core.DataAccess;
using QuizBuilder.Core.DataAccess.EntityFramework;
using QuizBuilder.Core.DataAccess.NHibernate;
using QuizBuilder.Quizes.Business.Abstract;
using QuizBuilder.Quizes.Business.Concrete.Compailers;
using QuizBuilder.Quizes.Business.Concrete.Managers;
using QuizBuilder.Quizes.DataAccess.Abstract;
using QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework;
using QuizBuilder.Quizes.DataAccess.Concrete.EntityFramework.DataAccessLayers;
using QuizBuilder.Quizes.DataAccess.Concrete.NHibernate.Helpers;

namespace QuizBuilder.Quizes.Business.DependencyResolvers.Ninject
{
    public class BusinessModule:NinjectModule
    {
        public override void Load()
        {

            Bind<IProductService>().To<ProductManager>().InSingletonScope();
            Bind<IProductDal>().To<EfProductDal>();
            
            Bind<ISoruDal>().To<EfSoruDal>();
            Bind<ISoruService>().To<SoruManager>().InSingletonScope().WithConstructorArgument("soruDal",new EfSoruDal());

            Bind<ISinavKagitDal>().To<EfSinavKagitDal>();
            Bind<ISinavKagitService>().To<SinavKagitManager>().InSingletonScope()
                .WithConstructorArgument("sinavKagitDal", new EfSinavKagitDal());

            Bind<IDersDal>().To<EfDersDal>();
            Bind<IDersService>().To<DersManager>().InSingletonScope()
                .WithConstructorArgument("dersDal", new EfDersDal());

            Bind<ISinavSoruDal>().To<EfSinavSoruDal>();
            Bind<ISinavSoruService>().To<SinavSoruManager>().InSingletonScope();

            Bind<IPageCompailer>().To<PageCompailer>();
            Bind<IQuestionCompailer>().To<QuestionCompiler>();

            //

            Bind(typeof(IQueryableRepository<>)).To(typeof(EfQueryableRepository<>));
            Bind<DbContext>().To<SinavHavuzuContext>();
            Bind<NHibernateHelper>().To<SqlServerHelper>();

        }
    }
}
