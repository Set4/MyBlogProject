using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebView.Model;
using MyBlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebView.Web.Controllers
{
    [NonController]
    public class PostController: Controller
    {
        IPostlogic model;
        IAuthorization auth;


        public PostController(IPostlogic postLogic, IAuthorization authContext)
        {
            model = postLogic;
            auth = authContext;
        }


        public IActionResult GetPostCollectionToPage(int page)
        {
            List<Post> post = model.GetPostCollection(page);
            if (post == null)
                return NotFound();
            return PartialView("_GetPostCollection", post);
        }

        public IActionResult GetPostCollectionToUser(string userId)
        {
            List<Post> post = model.GetPostCollection(userId);
            if (post == null)
                return NotFound();
            return PartialView("_GetPostCollection", post);
        }

        public IActionResult GetPost(int idPost)
        {
            Post post = model.GetPost(idPost);
            if (post == null)
                return NotFound();
            return PartialView("_GetPost", post);
        }

        public async Task<IActionResult> CreatePost([FromBody]string title, [FromBody]string text, [FromBody]User author, [FromBody]List<TagCollection> tags, [FromBody]StateEnum stat)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int? id = await model.CreatePost(title, text, author, tags,stat);
            if (id == null)
                return BadRequest();
            return PartialView("_CreatePost", id);
        }


        public async Task<IActionResult> ChangePost([FromBody]Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int? id = await model.ChangePost(post);
            if (id == null)
                return BadRequest();
            return PartialView("_ChangePost", id);
        }
    }
    
}
