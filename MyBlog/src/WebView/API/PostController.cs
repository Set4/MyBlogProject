using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ModelCore;
using MyBlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebView.Web.Controllers;

namespace WebView.API
{

    [Route("api/[controller]")]
    public class PostControllerAPI: Controller
    {
        IPostlogic model;
        IAuthorization auth;


        public PostControllerAPI(IPostlogic postLogic, IAuthorization authContext)
        {
            model = postLogic;
            auth = authContext;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            //сложная логиа с обновлением постов по времени
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Post post = model.GetPost(id);
            //if (post == null)
            //    return new NotFoundResult();

            return new JsonResult(post);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string title, [FromBody]string text, [FromBody]User author, [FromBody]List<TagCollection> tags, [FromBody]State state)
        {
           int id = await model.CreatePost(title, text, author, tags, state);
           return new JsonResult(id);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]Post post)
        {
            int id = await model.ChangePost(post);
            return new JsonResult(id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int? index = await model.DeletePost(id);
            return new JsonResult(index);
        }
    }





}
