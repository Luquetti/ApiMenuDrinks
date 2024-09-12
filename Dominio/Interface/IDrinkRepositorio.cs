using domain.Entities;

namespace domain.Interface
{
    public interface IDrinkRepositorio
    {
        public void Insert(Drinks model);
        public void Update(Drinks model); 
        public void Delete(int id );
        public Drinks GetById( int id );
        public List <Drinks> GetAll();

    }
}
