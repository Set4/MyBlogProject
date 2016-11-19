using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelCore;
using Microsoft.AspNetCore.Mvc;
using WebView.Web.Controllers;

namespace WebView.API
{
    public class RegistrationLogicController
    {
        IRegistrationLogic model;
        IAuthorization auth;


        public RegistrationLogicController(IRegistrationLogic registrationLogic, IAuthorization authContext)
        {
            model = registrationLogic;
            auth = authContext;
        }


        //надо подумать как лучше сделать

        [HttpGet("{id}")]
        public IActionResult Get(string userName)
        {
            bool result = model.ProverkaSvobodenliUserName(userName);

            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string userEmail)
        {
            bool result = model.ProverkaSvobodenliEmail(userEmail);

            return new JsonResult(result);
        }
        

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string name, [FromBody]string email, [FromBody]string password)
        {
            bool result = model.PreRegistration(name, email, password);

            return new JsonResult(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string code)
        {
            bool result = await model.EndRegistration(code);
            return new JsonResult(result);
        }
    }
}
