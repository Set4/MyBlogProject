using Microsoft.AspNetCore.Mvc;
using ModelCore;
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

        [HttpGet("{id}")]
        public IActionResult GetCommentCollection(int idPost)
        {
            List<Coment> coments = model.GetComentCollection(idPost);
            //if(coments==null)
            // return   Error
            return PartialView("_GetCommentCollection", coments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComent([FromBody]int PostId, [FromBody]string UserId, [FromBody]string Text, [FromBody]int? ComentId = null)
        {
            int? id = await model.CreateComent(PostId, UserId, Text, ComentId);
            //if(id==null)
            // return   Error
            return PartialView("_CreateComent",id);
        }


        public async Task<IActionResult> ChangeComent([FromBody]Coment coment)
        {
            int? id = await model.ChangeComent(coment);
            //if(id==null)
            // return   Error
            return PartialView("_CreateComent", id);
        }
    }

   
}
