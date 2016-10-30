using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebView.Web.Controllers
{
    public class ComentController
    {
        DataBaseContext db;
        IAuthorization auth;

        public ComentController(DataBaseContext dbContext, IAuthorization authContext)
        {
            db = dbContext;
            auth = authContext;
        }
    }

    public class ComentControllerAPI : ComentController
    {
        public ComentControllerAPI(DataBaseContext dbContext, IAuthorization authContext) : base(dbContext, authContext)
        {
        }
    }
}
