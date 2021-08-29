using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SqlLiteXam.Models;

namespace SqlLiteXam.Helper
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Person>().Wait();
        }

        //Read All
        public Task<List<Person>> GetAllPeopleAsync()
        {
            return _database.Table<Person>().ToListAsync();
        }

        //Read with LINQ
        public Task<List<Person>> GetAdultPeopleLINQ()
        {
            return _database.Table<Person>().Where(x => x.Age >= 18).ToListAsync();
        }

        //Read with Query
        public Task<List<Person>> GetAdultPeopleQuery()
        {
            return _database.QueryAsync<Person>("Select * from Person where Age >= 18");
        }

        //Create
        public Task<int> SavePersonAsync(Person person)
        {
            return _database.InsertAsync(person);
        }

        //Update
        public Task<int> UpdatePersonAsync(Person person)
        {
            return _database.UpdateAsync(person);
        }

        //Delete
        public Task<int> DeletePersonAsync(Person person)
        {
            return _database.DeleteAsync(person);
        }
    }
}
