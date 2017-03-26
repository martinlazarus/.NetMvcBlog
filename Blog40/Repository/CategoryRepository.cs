using Blog40.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Blog40.Repository
{
    public class CategoryRepository: BaseRepository, IRepository<Category>
    {
        private SqlConnection conn = BaseRepository.getConnection();
        private string query = "SELECT * FROM Category WHERE IsDeleted = 0";

        public int Add(Category model)
        {
            return this.conn.Query<int>("INSERT INTO Category VALUES (@Name, 0); SELECT CAST(SCOPE_IDENTITY() as int);", new { model.Name }).FirstOrDefault();
        }

        public IEnumerable<Category> GetAll()
        {
            return this.conn.Query<Category>(query);
        }

        public Category Get(int Id)
        {
            return this.conn.Query<Category>(query + " AND CategoryId = @CategoryId", new { CategoryId = Id }).FirstOrDefault();
        }

        public int Update(Category model)
        {
            return this.conn.Execute("UPDATE Category SET Name = @Name WHERE CategoryId = @CategoryId", new { model.Name, model.CategoryId });
        }

        public int Delete(int Id)
        {
            return this.conn.Execute("UPDATE Category SET IsDeleted = 1 WHERE CategoryId = @Id", Id);
        }
    }
}