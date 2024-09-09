using domain.Entities;
using domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiMenuDrinks.Controllers

{
    [Route("api/[controller]")] // é um atributo que irá criar um caminho ate meu método do end  point,
    [ApiController]
    public class DrinksController :ControllerBase

    {
        private IDrinkRepositorio drinkRepositorio;
        public DrinksController(IDrinkRepositorio _drinkRepositorio) {

            _drinkRepositorio = drinkRepositorio;
        }
        [HttpGet]
        public IActionResult ObterTodos()
        {
            var drink= drinkRepositorio.GetAll();
            return Ok(drink);//serve para retornar sucesso na requisição
        }
        [HttpGet("{id}")]
        public IActionResult ObtenhaPeloId([FromRoute]int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var drink = drinkRepositorio.GetById(id);
            if (drink == null)
            {
                return NotFound("Esse drink não existe");
            }
                return Ok(drink);
            
        }
        [HttpPost]
        public IActionResult Criar([FromBody] Drinks drink)
        { if (drink == null)
            {
                return BadRequest();
            }
            drinkRepositorio.Insert(drink);
            return Created($"novaBomba/{drink}", drink);
        }
        [HttpPut("{id}")]
        public IActionResult Atualizar([FromRoute]int id, [FromBody] Drinks drinks)
        { if(id == 0)
            {
                return NotFound();
            }
        if(drinks == null)
            {
                return BadRequest();
            }

            drinks.Id= id;
            drinkRepositorio.Update(drinks);
            return Ok(drinks);
        }

    }
}
