using Blog40.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Blog40.Repository
{
    public class AuthorRepository: BaseRepository, IRepository<Author>
    {
        private SqlConnection conn = BaseRepository.getConnection();
        private string query = "SELECT * FROM Author WHERE IsDeleted = 0";

        public int Add(Author model)
        {
            return this.conn.Query<int>("INSERT INTO Author VALUES (@DisplayName, @FirstName, @LastName, 0); SELECT CAST(SCOPE_IDENTITY() as int);", model).FirstOrDefault(); ;
        }

        public IEnumerable<Author> GetAll()
        {
            return this.conn.Query<Author>(query);
        }

        public Author Get(int Id)
        {
            return this.conn.Query<Author>(query + " AND AuthorId = @Id", new { Id }).FirstOrDefault();
        }

        public int Update(Author model)
        {
            return this.conn.Execute("UPDATE Author SET DisplayName = @DisplayName, FirstName = @FirstName, LastName = @LastName WHERE AuthorId = @AuthorId", model);
        }

        public int Delete(int Id)
        {
            return this.conn.Execute("UPDATE Author SET IsDeleted = 1 WHERE AuthorId = @Id", Id);
        }
    }
}