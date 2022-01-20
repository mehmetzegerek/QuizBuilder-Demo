using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.Business.ValidationRules.FluentValidation
{
    public class SoruValidator:AbstractValidator<Soru>
    {
        public SoruValidator()
        {
            RuleFor(p => p.Metin).NotEmpty().WithMessage("Soru metni boş geçilemez");
            RuleFor(p => p.Metin).MinimumLength(10).WithMessage("Soru metni minimum 10 karakterden oluşmalıdır");

            //RuleFor(p => p.Klasik).GreaterThanOrEqualTo((byte)0).WithMessage("Klasik 0 dan az değer alamaz !");
            //RuleFor(p => p.Klasik).LessThanOrEqualTo((byte)1).WithMessage("Klasik 1 den büyük değer alamaz !");

            RuleFor(p => p.SecA).NotEmpty().When(x => x.Klasik == false)
                .WithMessage("Şıklı sorularda şıklar boş bırakılamaz !");
            RuleFor(p => p.SecB).NotEmpty().When(x => x.Klasik == false)
                .WithMessage("Şıklı sorularda şıklar boş bırakılamaz !");
            RuleFor(p => p.SecC).NotEmpty().When(x => x.Klasik == false)
                .WithMessage("Şıklı sorularda şıklar boş bırakılamaz !");
            RuleFor(p => p.SecD).NotEmpty().When(x => x.Klasik == false)
                .WithMessage("Şıklı sorularda şıklar boş bırakılamaz !");

            RuleFor(p => p.SecA).Empty().When(x => x.Klasik == true).WithMessage("Klasik sorularda şık girilemez !");
            RuleFor(p => p.SecB).Empty().When(x => x.Klasik == true).WithMessage("Klasik sorularda şık girilemez !");
            RuleFor(p => p.SecC).Empty().When(x => x.Klasik == true).WithMessage("Klasik sorularda şık girilemez !");
            RuleFor(p => p.SecD).Empty().When(x => x.Klasik == true).WithMessage("Klasik sorularda şık girilemez !");

        }
    }
}
