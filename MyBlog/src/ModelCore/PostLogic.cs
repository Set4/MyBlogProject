using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelCore
{
    public interface IPostlogic
    {
        List<Post> GetPostCollection(int page);
        List<Post> GetPostCollection(DateTime startRange, DateTime endRange);

        Post GetPost(int id);
        Task<int> CreatePost(string title, string text, User author, DateTime dateCreate, List<TagCollection> tags, State state);
        Task<int> ChangePost(Post post);
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
        

            CREATE FUNCTION [dbo].[GetPostsByRange]
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
    SELECT * FROM Posts WHERE Price < @price
    RETURN




             System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@price", 26000);
    var phones = db.Database.SqlQuery<Phone>("SELECT * FROM GetPhonesByPrice (@price)",param);
    foreach (var phone in phones)
        Console.WriteLine(phone.Name);
        */

        public List<Post> GetPostCollection(int index = 1)
        {

            //сортировать по дате, выбрать с  page-1*5+ до  page*5 и торорые доступны Состояние= public  LINQ TSQL
            List<Post> posts =  db.Posts.Where(i => i.Id == 1).ToList();

            return posts;
        }

        public List<Post> GetPostCollection(DateTime startRange, DateTime endRange)
        {
            //сортировать по дате, выбрать с  startRange до endRange и торорые доступны Состояние= public 
            return new List<Post>();
        }

        public Post GetPost(int id)
        {
            return db.Posts.FirstOrDefault(p => p.Id == id);
        }

        public async Task<int> CreatePost(string title, string text, User author, DateTime dateCreate, List<TagCollection> tags, State state)
        {
            Post newPost = new Post(title, text, author, dateCreate, tags, state);
            db.Posts.Add(newPost);
            await db.SaveChangesAsync();
            return newPost.Id;            
        }

        public async Task<int> ChangePost(Post post)
        {
            db.Posts.Update(post);
            return await db.SaveChangesAsync();          
        }

      
    }
}
