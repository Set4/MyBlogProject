﻿using ModelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebView.Web.Controllers
{
    public class ComentController
    {
      
        IAuthorization auth;
        IComentsLogic model;

        public ComentController(IComentsLogic comentLogic, IAuthorization authContext)
        {
            model = comentLogic;
            auth = authContext;
        }

    }

   
}
