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
        public IEnumerable<string> Get()
        {
           
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }





}
