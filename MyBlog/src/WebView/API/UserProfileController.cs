using Microsoft.AspNetCore.Mvc;
using ModelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebView.Web.Controllers;

namespace WebView.API
{
    public class UserProfileControllerAPI
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
            User user = model.GetUserProfile(userId);

            //if(coments==null)
            // return   Error

            return new JsonResult(user);
        }


        //PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            int? id = await model.ChangeUserProfile(user);
            //if(id==null)
            // return   Error
            return new JsonResult(id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string userId)
        {
            User user = model.GetUserProfile(userId);
            user.State.StateElement = StateEnum.Removed;

            return new JsonResult(await model.ChangeUserProfile(user));
        }
    }


       
    
}
