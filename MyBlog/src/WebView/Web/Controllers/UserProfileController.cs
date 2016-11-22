using Microsoft.AspNetCore.Mvc;
using ModelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebView.Web;
using WebView.Web.Controllers;

namespace WebView.API
{
    [NonController]
    public class UserProfileController: Controller
    {
        IAuthorization auth;
        IUserProfileLogic model;

        public UserProfileController(IUserProfileLogic userProfileLogic, IAuthorization authContext)
        {
            model = userProfileLogic;
            auth = authContext;
        }

       
        public IActionResult GetUser(string userId)
        {
            if (String.IsNullOrWhiteSpace(userId))
                return BadRequest(userId);

            User user = model.GetUserProfile(userId);

            if (user == null)
                return NotFound();

            return PartialView("_GetUser", user);
        }

        public async Task<IActionResult> ChangeUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int? id = await model.ChangeUserProfile(user);
            if (id == null)
                return new NotFoundResult();

            return PartialView("_ChangeUser", id);
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            User user = model.GetUserProfile(userId);
            user.State.StateElement = StateEnum.Removed;

            int? id = await model.ChangeUserProfile(user);
            if (id == null)
                return new NotFoundResult();

            return PartialView("_DeleteUser", id);
        }
    }
}
