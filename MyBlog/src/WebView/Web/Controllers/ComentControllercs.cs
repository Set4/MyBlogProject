using Microsoft.AspNetCore.Mvc;
using WebView.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebView.Web.Controllers
{
    [NonController]
    public class ComentController:Controller
    {
      
        IAuthorization auth;
        IComentsLogic model;

        public ComentController(IComentsLogic comentLogic, IAuthorization authContext)
        {
            model = comentLogic;
            auth = authContext;
        }

 
        public IActionResult GetCommentCollection(int idPost)
        {
            List<Coment> coments = model.GetComentCollection(idPost);
            if(coments==null)
                return NotFound();
            return PartialView("_GetCommentCollection", coments);
        }


        public async Task<IActionResult> CreateComent([FromBody]int PostId, [FromBody]string UserId, [FromBody]string Text, [FromBody]int? ComentId = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int? id = await model.CreateComent(PostId, UserId, Text, ComentId);
            if (id == null)
                return BadRequest();
            return PartialView("_CreateComent",id);
        }


        public async Task<IActionResult> ChangeComent([FromBody]Coment coment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int? id = await model.ChangeComent(coment);
            if (id == null)
                return BadRequest();
            return PartialView("_ChangeComent", id);
        }
    }

   
}
