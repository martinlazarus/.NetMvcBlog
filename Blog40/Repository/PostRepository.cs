using Blog40.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Blog40.Repository
{
    public class PostRepository : BaseRepository, IRepository<Post>
    {
        private SqlConnection conn;
        private string postCategoryAuthorQuery = @"SELECT
	                                                    *
                                                    FROM
	                                                    Post AS P
	                                                    JOIN Category AS C ON P.CategoryId = C.CategoryId
	                                                    JOIN Author AS A ON P.AuthorId = A.AuthorId
                                                    WHERE
	                                                    P.IsDeleted = 0";

        public PostRepository() 
        {
            this.conn = BaseRepository.getConnection();
        }

        public int Add(Post model)
        {
            DateTime nowDate = DateTime.Now;
            model.CreatedAt = nowDate;
            model.UpdatedAt = nowDate;

            return this.conn.Query<int>(@"INSERT INTO Post VALUES (@CategoryId, @AuthorId, @Title, @Content, @CreatedAt, @UpdatedAt, 0);
                              SELECT CAST(SCOPE_IDENTITY() as int);", new {model.Category.CategoryId, model.Author.AuthorId, model.Title, model.Content, model.CreatedAt, model.UpdatedAt} ).FirstOrDefault();

        }

        public IEnumerable<Post> GetAll()
        {
            return this.conn.Query<Post,Category, Author, Post>(this.postCategoryAuthorQuery, (p, c, a) => { p.Category = c; p.Author = a; return p; }, splitOn: "CategoryId,AuthorId");
        }

        public Post Get(int Id)
        {
            return this.conn.Query<Post, Category, Author, Post>(this.postCategoryAuthorQuery + " AND PostId = @Id", (p, c, a) => { p.Category = c; p.Author = a; return p; }, new { Id = Id }, splitOn: "CategoryId,AuthorId").FirstOrDefault();
        }

        public int Update(Post model)
        {
            model.UpdatedAt = DateTime.Now;
            return this.conn.Execute("UPDATE Post SET CategoryId = @CategoryId, AuthorId = @AuthorId, Title = @Title, Content = @Content, UpdatedAt = @UpdatedAt WHERE PostId = @PostId",
                new { CategoryId = model.Category.CategoryId, AuthorId = model.Author.AuthorId, Title = model.Title, Content = model.Content, UpdatedAt = model.UpdatedAt, PostId = model.PostId });
        }

        public int Delete(int Id)
        {
            DateTime UpdatedAt = DateTime.Now;
            return this.conn.Execute("UPDATE Post SET UpdatedAt = @UpdatedAt, IsDeleted = 1 WHERE PostId = @Id", new { UpdatedAt, Id });
        }

        public int UndeleteAll()
        {
            return this.conn.Execute("UPDATE Post SET IsDeleted = 0");
        }
    }
}