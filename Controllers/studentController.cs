using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dataaccesslayer;

namespace WebApplication1.Controllers
{
    public class studentController : Controller
    {
        public IActionResult events()
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.getevents();
            return View(event1);
        }
        public IActionResult veiwdetails(int Id)
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.GetEvent(Id);
            ViewBag.message = TempData["message"];
            return View(event1);
        }
        public IActionResult register(int Id)
        {
            Classcrudoperation op =new Classcrudoperation();
            var reg = op.gettempstd();
            var result=op.register(reg,Id);
            if (result==0)
            {
                TempData["message"] = "Already Registered";
            }
            else if (result==10)
            {
                TempData["message"] = "Event is completed";
            }
            else
            {
                TempData["message"] = "Registered Successfully";
            }
            return RedirectToAction("veiwdetails", "student",new { id = Id});
        }
        public IActionResult myevents()
        {
            Classcrudoperation op = new Classcrudoperation();
            var event3=op.myevents();
            return View(event3);
        }
        public IActionResult veiwdetail(int Id)
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.GetEvent(Id);
            return View(event1);
        }
        public IActionResult removemyevent(int Id)
        {
            Classcrudoperation op=new Classcrudoperation();
            var reg = op.gettempstd();
            op.removestdevent(reg,Id);
            return RedirectToAction("myevents", "student");
        }
    }
}
