using FluentValidation;
using domain.Entities;
using domain.Interface;
namespace domain.Validacoes


{
    public class ValidaDrinks : AbstractValidator<Drinks>
    { private readonly IDrinkRepositorio _drinkRepositorio;
        public ValidaDrinks(IDrinkRepositorio drinkRepositorio)
        {
            _drinkRepositorio = drinkRepositorio;
            CascadeMode cascade = CascadeMode.Stop;
            RuleFor(Drinks => Drinks.Nome)
                .NotNull()
                .WithMessage("Qual nome da bebida?")
                .NotEmpty()
                .WithMessage("Qual nome da bebida?");

            RuleFor(Drinks => Drinks.Preco)
            .NotNull()
            .WithMessage("Qual o valor da mercadoria?")
            .NotEmpty()
            .WithMessage("Qual o valor da mercadoria ?");

            RuleFor(Drinks => Drinks.Composicao)
            .IsInEnum()
            .WithMessage("Não existe")
            .NotEmpty()
            .WithMessage("Não existe drink sem composição");


        }
    }
}
