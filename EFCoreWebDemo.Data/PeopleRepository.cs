using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreWebDemo.Data
{
    public class PeopleRepository
    {
        private readonly string _connectionString;

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAll()
        {
            using var ctx = new PeopleDataContext(_connectionString);
            return ctx.People.ToList();
        }

        public void Add(Person person)
        {
            using var ctx = new PeopleDataContext(_connectionString);
            ctx.People.Add(person);
            ctx.SaveChanges();
        }

        public Person GetById(int id)
        {
            using var ctx = new PeopleDataContext(_connectionString);
            return ctx.People.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Person person)
        {
            using var ctx = new PeopleDataContext(_connectionString);
            ctx.Entry(person).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            using var ctx = new PeopleDataContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"DELETE FROM People WHERE Id = {id}");
        }
    }
}
