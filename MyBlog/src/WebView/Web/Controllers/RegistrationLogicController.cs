using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebView.Model;
using Microsoft.AspNetCore.Mvc;
using WebView.Web;

namespace WebView.API
{
    [NonController]
    public class RegistrationController:Controller
    {
        IRegistrationLogic model;
        IAuthorization auth;


        public RegistrationController(IRegistrationLogic registrationLogic, IAuthorization authContext)
        {
            model = registrationLogic;
            auth = authContext;
        }


        public IActionResult ProverkaSvobodenliUserName(string userName)
        {
            bool result = model.ProverkaSvobodenliUserName(userName);

            return new OkObjectResult(result);
        }

        public IActionResult ProverkaSvobodenliEmail(string userEmail)
        {
            bool result = model.ProverkaSvobodenliEmail(userEmail);

            return new OkObjectResult(result);
        }
        

        public async Task<IActionResult> Post([FromBody]string name, [FromBody]string email, [FromBody]string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool result = model.PreRegistration(name, email, password);

            return PartialView("_ViewPreRegistration", result);
        }

 
        public async Task<IActionResult> Post([FromBody]string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool result = await model.EndRegistration(code);
            return PartialView("_ViewEndRegistration", result);
        }
    }
}
