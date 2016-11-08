using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelCore;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult GetCommentCollection()
        {

            return PartialView("_GetCommentCollection",);
        }

        [HttpPost]
        public IActionResult CreateComent(Coment coment)
        {

            return PartialView("_CreateComent",);
        }

        [HttpPost]
        public IActionResult ChangeComent(Coment coment)
        {

            return PartialView("_ChangeComent",);
        }


    }
}
