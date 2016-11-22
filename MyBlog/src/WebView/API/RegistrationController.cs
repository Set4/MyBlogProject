using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelCore;
using Microsoft.AspNetCore.Mvc;
using WebView.Web;

namespace WebView.API
{
    [Route("api/[controller]", Name = "registration")]
    public class RegistrationControllerAPI:Controller
    {
        IRegistrationLogic model;
        IAuthorization auth;


        public RegistrationControllerAPI(IRegistrationLogic registrationLogic, IAuthorization authContext)
        {
            model = registrationLogic;
            auth = authContext;
        }


        [HttpGet("{userName}", Name = "username")]
        public IActionResult ProverkaSvobodenliUserName(string userName)
        {
            bool result = model.ProverkaSvobodenliUserName(userName);

            return new OkObjectResult(result);
        }

        [HttpGet("{userEmail}", Name = "useremail")]
        public IActionResult ProverkaSvobodenliEmail(string userEmail)
        {
            bool result = model.ProverkaSvobodenliEmail(userEmail);

            return new OkObjectResult(result);
        }
        

        // POST api/values
        [HttpPost( Name ="preregistration")]
        public async Task<IActionResult> Post([FromBody]string name, [FromBody]string email, [FromBody]string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool result = model.PreRegistration(name, email, password);

            return new OkObjectResult(result);
        }

        // POST api/values
        [HttpPost(Name = "endregistration")]
        public async Task<IActionResult> Post([FromBody]string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool result = await model.EndRegistration(code);
            return new JsonResult(result);
        }
    }
}
