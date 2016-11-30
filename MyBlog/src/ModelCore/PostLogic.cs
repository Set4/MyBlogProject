using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ModelCore
{
    public interface IPostlogic
    {
        List<Post> GetPostCollection(int page);
        List<Post> GetPostCollection(DateTime startRange, DateTime endRange);
        List<Post> GetPostCollection(string userId);

        Post GetPost(int id);
        Task<int> CreatePost(string title, string text, User author, List<TagCollection> tags, StateEnum state);
        Task<int> ChangePost(Post post);
        Task<int?> DeletePost(int idPost);
    }

    public class PostLogic:IPostlogic
    {
        DataBaseContext db;      
        int MAX_COUNT_POSTS;


        public PostLogic(DataBaseContext dbContext, int MAX_COUNT_POSTS)
        {
            db = dbContext; 
            this.MAX_COUNT_POSTS = MAX_COUNT_POSTS;
        }

        //Добавить в БД функции по поиску в базе
        /*
        

            CREATE FUNCTION [dbo].[GetPostsByIndexRange]
(
    @Index index,
    @Count count
)
RETURNS @returntable TABLE
(
    Id int,
    i tak dalee perchislit vsy 
)
AS
BEGIN
    INSERT @returntable
    SELECT * FROM Posts ORDER BY date DESC LIMIT index,count
    RETURN




             System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@index", index);
                System.Data.SqlClient.SqlParameter param1 = new System.Data.SqlClient.SqlParameter("@count", count);
    List<Post> posts = db.Database.SqlQuery<Posts>("SELECT * FROM GetPostsByRange (@index,@count)",param,param1);
    



          CREATE FUNCTION [dbo].[GetPostsByDateRange]
(
    @Index stardDate,
    @Count endDate
)
RETURNS @returntable TABLE
(
    Id int,
    i tak dalee perchislit vsy 
)
AS
BEGIN
    INSERT @returntable
    SELECT * FROM Posts WHERE id >= stardDate AND id =< endDate ORDER BY date DESC
    RETURN





            CREATE FUNCTION [dbo].[GetPostsByUserId]
(
    @UserId userId
   
)
RETURNS @returntable TABLE
(
    Id int,
    i tak dalee perchislit vsy 
)
AS
BEGIN
    INSERT @returntable
    SELECT * FROM Posts WHERE UserId == userId ORDER BY date DESC
    RETURN



        */

        public List<Post> GetPostCollection(int index = 1)
        {

            System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@index", (index-1)*MAX_COUNT_POSTS+1);
            System.Data.SqlClient.SqlParameter param1 = new System.Data.SqlClient.SqlParameter("@count", MAX_COUNT_POSTS);
            List<Post> posts = db.Posts.FromSql("SELECT * FROM GetPostsByIndexRange (@index,@count)", param, param1).ToList();
           
            return posts;
        }

        public List<Post> GetPostCollection(DateTime startRange, DateTime endRange)
        {
            System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@stardDate", startRange);
            System.Data.SqlClient.SqlParameter param1 = new System.Data.SqlClient.SqlParameter("@endDate", endRange);
            List<Post> posts = db.Posts.FromSql("SELECT * FROM GetPostsByDateRange (@stardDate,@endDate)", param, param1).ToList();

            return posts;
        }

        public List<Post> GetPostCollection(string userId)
        {
            System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@userId", userId);
            List<Post> posts = db.Posts.FromSql("SELECT * FROM GetPostsByUserId (@userId)", param).ToList();

            return posts;
        }

        public Post GetPost(int id)
        {
            return db.Posts.FirstOrDefault(p => p.Id == id);
        }

        public async Task<int> CreatePost(string title, string text, User author, List<TagCollection> tags, StateEnum state)
        {
            Post newPost = new Post(title, text, author, tags, state);
            db.Posts.Add(newPost);
            await db.SaveChangesAsync();
            return newPost.Id;            
        }

        public async Task<int> ChangePost(Post post)
        {
            post.DateChange = DateTime.Now;
            db.Posts.Update(post);
            return await db.SaveChangesAsync();          
        }
        public async Task<int?> DeletePost(int idPost)
        {
           Post post= db.Posts.FirstOrDefault(p => p.Id == idPost);
            if (post == null)
                return null;
            post.DateChange = DateTime.Now;
            post.StateElement= StateEnum.Removed;
            db.Posts.Update(post);
            return await db.SaveChangesAsync();
        }

    }
}
