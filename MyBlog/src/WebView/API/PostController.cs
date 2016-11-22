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

    [Route("api/[controller]", Name ="post")]
    public class PostControllerAPI: Controller
    {
        IPostlogic model;
        IAuthorization auth;


        public PostControllerAPI(IPostlogic postLogic, IAuthorization authContext)
        {
            model = postLogic;
            auth = authContext;
        }

        //// GET api/values
        //[HttpGet]
        //public IEnumerable<Post> Get()
        //{
        //    //сложная логиа с обновлением постов по времени
        //}


        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Post post = model.GetPost(id);
            if (post == null)
                return NotFound();

            return new OkObjectResult(post);
        }

        // POST api/values
        [HttpPost(Name ="create")]
        public async Task<IActionResult> Post([FromBody]string title, [FromBody]string text, [FromBody]User author, [FromBody]List<TagCollection> tags, [FromBody]State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = await model.CreatePost(title, text, author, tags, state);

            return new OkObjectResult(id);
        }

        // PUT api/values/5
        [HttpPut(Name ="change")]
        public async Task<IActionResult> Put([FromBody]Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = await model.ChangePost(post);

            return new OkObjectResult(id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int? index = await model.DeletePost(id);
            if (index == null)
                return BadRequest();

            return new OkObjectResult(index);
        }
    }





}
