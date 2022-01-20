using FluentValidation;
using QuizBuilder.Quizes.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBuilder.Quizes.Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("CategoryID boş geçilemez");
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Ürün ismi boş geçilemez");
            RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Ürün fiyatı 0 dan büyük olmalıdır");
            RuleFor(p => p.QuantityPerUnit).NotEmpty().WithMessage("Stok miktarı boş geçilemez");

            RuleFor(p => p.ProductName).Length(5).WithMessage("Ürün ismi minimum 5 karakter olmalıdır");
            RuleFor(p => p.UnitPrice).GreaterThan(20).When(p => p.CategoryId == 1).WithMessage("CategoryID si 1 olan ürünün fiyatı 20 den büyük olmalıdır");
            //RuleFor(p => p.ProductName).Must(StartsWithA).WithMessage("Ürün ismi A ile başlamalıdır");
        }

        private bool StartsWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
