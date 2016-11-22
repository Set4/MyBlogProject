using Microsoft.AspNetCore.Mvc;
using ModelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebView.Web;

namespace WebView.API
{
    [Route("api/[controller]",Name ="user")]
    public class UserProfileControllerAPI:Controller
    {
        IUserProfileLogic model;
        IAuthorization auth;


        public UserProfileControllerAPI(IUserProfileLogic userProfileLogic, IAuthorization authContext)
        {
            model = userProfileLogic;
            auth = authContext;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string userId)
        {
            if(String.IsNullOrWhiteSpace(userId))
                return BadRequest(userId);

            User user = model.GetUserProfile(userId);

            if (user == null)
                return  NotFound();

            return new OkObjectResult(user);
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int? id = await model.ChangeUserProfile(user);
            if(id==null)
                return new NotFoundResult();

            return new OkObjectResult(id);
        }

        // DELETE api/values/5
        [HttpDelete("{userId}/delete")]
        public async Task<IActionResult> Delete(string userId)
        {
            User user = model.GetUserProfile(userId);
            user.State.StateElement = StateEnum.Removed;

            int? id = await model.ChangeUserProfile(user);
            if (id == null)
                return new NotFoundResult();

            return new NoContentResult();
        }
    }


       
    
}
