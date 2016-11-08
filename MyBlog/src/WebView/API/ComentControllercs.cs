using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<Coment> coments = model.GetComentCollection(id);

            //if(coments==null)
            // return   Error

            return new JsonResult(coments);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]int PostId, [FromBody]string UserId, [FromBody]string Text, [FromBody]int? ComentId = null)
        {
            int? id = await model.CreateComent(PostId, UserId, Text, ComentId);
            //if(id==null)
            // return   Error
            return new JsonResult(id);
        }

        //PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]Coment coment)
        {
            int? id = await model.ChangeComent(coment);
            //if(id==null)
            // return   Error
            return new JsonResult(id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Coment coment = model.GetComent(id);
            coment.State.StateElement = StateEnum.Removed;

            return new JsonResult(await model.ChangeComent(coment));
        }
    }
}
