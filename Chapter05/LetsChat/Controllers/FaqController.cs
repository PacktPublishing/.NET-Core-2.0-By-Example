using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LetsChat.Controllers
{
    [AllowAnonymous]
    public class FaqController : Controller
    {
        // GET: Faq
        public ActionResult Index()
        {
            return this.View();
        }        
    }
}