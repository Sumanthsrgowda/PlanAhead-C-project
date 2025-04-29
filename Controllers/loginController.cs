using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Dataaccesslayer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Routing.Patterns;
using Aspose.Email;

namespace WebApplication1.Controllers
{
    public class loginController : Controller
    {
        public IActionResult login()
        {
          
            return View();

        }
        [HttpPost]
        public IActionResult login(loginmodel loginmodel)
        {
            Classcrudoperation operation = new Classcrudoperation();
            var login = operation.logging(loginmodel);
            if (login.role == 1)
            {
                return RedirectToAction("Index", "admin");
            }
            else
                if (login.role == 2)
            {
                string name = login.UserName;
                operation.addtempevent(name);
                return RedirectToAction("events", "dept");
            }
            else
                if (login.role == 3)
            {
                string name =login.UserName;
                operation.addtempstd(name);
                return RedirectToAction("events", "student");
            }
            else
            {
                ViewBag.errormsg = "Invalid Username or Password";
                return View();
            }

        }
        public IActionResult signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult signup(signupmodel signupmodel)
        {
            Classcrudoperation operation = new Classcrudoperation();
            var login = operation.signing(signupmodel);
            if(login.stdvalid==2)
            {
                ViewBag.errormsg = "Email ID Already Exists";
            }
            else if (login.stdvalid==1)
            {
                ViewBag.success = "Account Created Successfully";
            }
            else
            {
                ViewBag.errormsg = "Invalid Register Number";
            }
            return View(login);

        }
    }
}
