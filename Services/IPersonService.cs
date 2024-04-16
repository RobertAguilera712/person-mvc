using persons_mvc.Models;

namespace persons_mvc.Services
{
    public interface IPersonService
    {
        public IEnumerable<Person> GetAll();
        public Person Get(int id);
        public Person Insert(Person person);
        public Person Update(Person person);
        public Person Delete(int id);
    }
}