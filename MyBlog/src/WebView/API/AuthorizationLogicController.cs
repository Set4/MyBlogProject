using Microsoft.AspNetCore.Mvc;
using ModelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebView.Web.Controllers;

namespace WebView.API
{
    public class AuthorizationLogicController
    {
        IAuthorizationLogic model;
        IAuthorization auth;


        public AuthorizationLogicController(IAuthorizationLogic authLogic, IAuthorization authContext)
        {
            model = authLogic;
            auth = authContext;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string email, [FromBody]string password)
        {
            string token = await model.Authorization(email, password);
            //if(id==null)
            // return   Error
            return new JsonResult(token);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string email, [FromBody]string AuthToken)
        {
            User user = model.Autondification(email, AuthToken);
            //if(id==null)
            // return   Error
            return new JsonResult(user);
        }
    }
}
