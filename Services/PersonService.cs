using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using persons_mvc.Models;

namespace persons_mvc.Services
{
    public class PersonService : IPersonService
    {
        private readonly MySqlConnection _dbConnection;

        public PersonService(MySqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Person> GetAll()
        {
            string query = "SELECT * FROM PersonView;";
            return _dbConnection.Query<Person>(query);
        }

        public Person Get(int id)
        {
            string query = "SELECT * FROM person WHERE id = @Id";
            return _dbConnection.QueryFirstOrDefault<Person>(query, new {Id = id});
        }

        public Person Insert(Person person)
        {
            var parameters = new DynamicParameters();
            parameters.AddDynamicParams(person);
            parameters.Add("p_inserted_id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _dbConnection.Execute("InsertPerson", parameters, commandType: CommandType.StoredProcedure);
            person.Id = parameters.Get<int>("p_inserted_id");
            return person;
        }

        public Person Update(Person person)
        {
            var parameters = new DynamicParameters();
            parameters.AddDynamicParams(person);

            _dbConnection.Execute("UpdatePerson", parameters, commandType: CommandType.StoredProcedure);
            return person;
        }

        public Person Delete(int id)
        {

            string deleteQuery = "UPDATE person set status = 'INACTIVO' WHERE id = @Id";
             _dbConnection.Execute(deleteQuery, new {Id = id});

            string query = "SELECT * FROM person WHERE id = @Id";
            var person =  _dbConnection.QueryFirstOrDefault<Person>(query, new {Id = id});

            return person;
        }
    }
}