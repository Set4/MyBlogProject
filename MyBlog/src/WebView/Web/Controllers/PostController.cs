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

        [HttpPost]
        public async Task<IActionResult> Edit(Phone phone)
        {
            db.Phones.Update(phone);
          
            return RedirectToAction("Index");
        }

        public async Task<List<Post>> GetPostCollection(int page=1)
        {
            List<Post> posts = await db.Posts.Select(p=>p.Date=page);
           
            return posts;
        }

        public async Task<List<Post>> GetPostCollection(DateTime startRange, DateTime endRange)
        {
            return new List<Post>();
        }

        public Post GetPost(int id)
        {
            return new Post();
        }

        public int CreatePost(Post post)
        {
            await db.SaveChangesAsync();
            return 0;
        }

        public Post ChangePost()
        {
            return new Post();
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
