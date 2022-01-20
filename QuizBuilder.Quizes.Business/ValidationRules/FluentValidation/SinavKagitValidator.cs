using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using QuizBuilder.Quizes.Entities.Concrete;

namespace QuizBuilder.Quizes.Business.ValidationRules.FluentValidation
{
    public class SinavKagitValidator:AbstractValidator<SinavKagit>
    {
        public SinavKagitValidator()
        {
            RuleFor(x => x.SinavAdi).NotEmpty().WithMessage("Sınav adı boş geçilemez !");
            RuleFor(x => x.SinavAdi).MaximumLength(70).WithMessage("Sınav adı maksimum 70 karakterden oluşmalıdır !");
            RuleFor(x => x.SinavAdi).MinimumLength(20).WithMessage("Sınav adı minimum 20 karakterden oluşmalıdır !");

            RuleFor(x => x.DersID).NotEmpty().WithMessage("Sınavın ilgili dersi boş bırakılamaz !");
        }
    }
}
