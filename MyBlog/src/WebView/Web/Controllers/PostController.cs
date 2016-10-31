using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyBlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebView.Web.Controllers
{
     
    public class PostController: Controller
    {
        DataBaseContext db;
        IAuthorization auth;
        int MAX_COUNT_POSTS;


        public PostController(DataBaseContext dbContext, IAuthorization authContext, IOptions<GlobalSetting> setting)
        {
            db = dbContext;
            auth = authContext;

            MAX_COUNT_POSTS = setting.Value.MAX_COUNT_POSTS;
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(Phone phone)
        //{
        //    db.Phones.Update(phone);
          
        //    return RedirectToAction("Index");
        //}

        public virtual async Task<List<Post>> GetPostCollection(int page=1)
        {
            //сортировать по дате, выбрать с  page-1*5+ до  page*5 и торорые доступны Состояние= public
            List<Post> posts = await db.Posts.Select(p=>p.Id==page);
           
            return posts;
        }

        public virtual async Task<List<Post>> GetPostCollection(DateTime startRange, DateTime endRange)
        {
            //сортировать по дате, выбрать с  startRange до endRange и торорые доступны Состояние= public 
            return new List<Post>();
        }

        public Post GetPost(int id)
        {
            //выбрать =  id и торорые доступны Состояние = public 
            return new Post();
        }

        public virtual int CreatePost()
        {
            await db.SaveChangesAsync();
            return 0;
        }

        public virtual Post ChangePost()
        {
            return new Post();
        } 
    }

    public class PostControllerWeb : PostController
    {
        public PostControllerWeb(DataBaseContext dbContext, IAuthorization authContext, IOptions<GlobalSetting> setting) : base(dbContext, authContext, setting)
        {
        }
    }

    public class PostControllerAPI : PostController
    {
        public PostControllerAPI(DataBaseContext dbContext, IAuthorization authContext) : base(dbContext, authContext)
        {
        }

        public new List<Post> GetPostCollection(DateTime startRange, DateTime endRange)
        {
            return base.GetPostCollection(startRange, endRange);
        }

    }



}
