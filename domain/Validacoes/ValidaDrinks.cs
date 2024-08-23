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
        }
    }
}
