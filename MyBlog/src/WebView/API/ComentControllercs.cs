using ModelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebView.Web.Controllers;

namespace WebView.API
{
    public class ComentControllerAPI
    {

        IAuthorization auth;
        IComentsLogic model;

        public ComentControllerAPI(IComentsLogic comentLogic, IAuthorization authContext)
        {
            model = comentLogic;
            auth = authContext;
        }

    }
}
