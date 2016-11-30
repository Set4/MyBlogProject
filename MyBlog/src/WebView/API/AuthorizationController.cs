using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebView.Model;
using WebView.Web;

namespace WebView.API
{
    [Route("api/[controller]", Name = "auth")]
    public class AuthorizationControllerAPI:Controller
    {
        IAuthorizationLogic model;
        IAuthorization auth;


        public AuthorizationControllerAPI(IAuthorizationLogic authLogic, IAuthorization authContext)
        {
            model = authLogic;
            auth = authContext;
        }

        // POST api/values
        [HttpPost(Name ="login")]
        public async Task<IActionResult> LogIn([FromBody]string email, [FromBody]string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string token = await model.Authorization(email, password);
            if (String.IsNullOrWhiteSpace(token))
                return BadRequest();
            return new OkObjectResult(token);
        }

        // POST api/values
        [HttpPost(Name = "access")]
        public async Task<IActionResult> Access([FromBody]string email, [FromBody]string AuthToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = model.Autondification(email, AuthToken);
            if(user == null)
             return BadRequest();
            return new OkObjectResult(user);
        }
    }
}
