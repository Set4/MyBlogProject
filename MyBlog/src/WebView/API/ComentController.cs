using Microsoft.AspNetCore.Mvc;
using ModelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebView.Web.Controllers;

namespace WebView.API
{
    [Route("api/[controller]", Name = "coment")]
    public class ComentControllerAPI:Controller
    {

        IAuthorization auth;
        IComentsLogic model;

        public ComentControllerAPI(IComentsLogic comentLogic, IAuthorization authContext)
        {
            model = comentLogic;
            auth = authContext;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<Coment> coments = model.GetComentCollection(id);

            if (coments == null)
                return NotFound();

            return new OkObjectResult(coments);
        }

        // POST api/values
        [HttpPost(Name ="create")]
        public async Task<IActionResult> Post([FromBody]int PostId, [FromBody]string UserId, [FromBody]string Text, [FromBody]int? ComentId = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int? id = await model.CreateComent(PostId, UserId, Text, ComentId);
            if (id == null)
                return BadRequest();

            return new OkObjectResult(id);
        }

        //PUT api/values/5
        [HttpPut(Name = "change")]
        public async Task<IActionResult> Put([FromBody]Coment coment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int? id = await model.ChangeComent(coment);
            if (id == null)
                return BadRequest();

            return new OkObjectResult(id);
        }

        // DELETE api/values/5
        [HttpDelete("{idComent}")]
        public async Task<IActionResult> Delete(int idComent)
        {
       
            Coment coment = model.GetComent(idComent);
            if(coment==null)
                return BadRequest();
            coment.State.StateElement = StateEnum.Removed;

            int? id = await model.ChangeComent(coment);
            if (id == null)
                return BadRequest();

            return new OkObjectResult(id);
        }
    }
}
