using domain.Enum;

namespace domain.Entities
{
    public class Drinks
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double  Preco { get; set; }
        public bool Existe {  get; set; }  
        public bool EhAlcoolica {  get; set; }
        public TipoBase? Composicao { get; set; }
    }
}
