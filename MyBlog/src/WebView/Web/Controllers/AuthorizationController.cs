using Microsoft.AspNetCore.Mvc;
using ModelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebView.Web;

namespace WebView.API
{
    [NonController]
    public class AuthorizationController:Controller
    {
        IAuthorizationLogic model;
        IAuthorization auth;


        public AuthorizationController(IAuthorizationLogic authLogic, IAuthorization authContext)
        {
            model = authLogic;
            auth = authContext;
        }

        public async Task<IActionResult> LogIn([FromBody]string email, [FromBody]string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string token = await model.Authorization(email, password);
            if (String.IsNullOrWhiteSpace(token))
                return BadRequest();
            return PartialView("_ViewLogIn", token);
        }


        public async Task<IActionResult> Access([FromBody]string email, [FromBody]string AuthToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = model.Autondification(email, AuthToken);
            if(user == null)
             return BadRequest();
            return PartialView("_ViewAccess", user);
        }
    }
}
