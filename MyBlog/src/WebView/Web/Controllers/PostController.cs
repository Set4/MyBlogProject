using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ModelCore;
using MyBlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebView.Web.Controllers
{
     
    public class PostController: Controller
    {
        IPostlogic model;
        IAuthorization auth;


        public PostController(IPostlogic postLogic, IAuthorization authContext)
        {
            model = postLogic;
            auth = authContext;
        }

    }
    
}
