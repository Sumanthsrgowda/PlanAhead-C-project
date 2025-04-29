using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Dataaccesslayer;

namespace WebApplication1.Controllers
{
    public class deptController : Controller
    {

        private readonly IWebHostEnvironment webHostEnvironment;
        public deptController(IWebHostEnvironment webHost)
        {
            webHostEnvironment = webHost;
        }

        public IActionResult events()
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.getevents();
            return View(event1);
        }
        public IActionResult addevent()
        {
            Classcrudoperation op = new Classcrudoperation();
            string name = op.gettempevent();
            eventmodel model = new eventmodel();
            model.EventDept = name;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> addevent(eventmodel eventmodel)
        {
            
                if (eventmodel.coverphoto != null)
                {
                    string folder = "poster/";
                    folder += Guid.NewGuid().ToString() + "_" + eventmodel.coverphoto.FileName;
                    eventmodel.Poster = "/" + folder;
                    string uploadfolder = Path.Combine(webHostEnvironment.WebRootPath, folder);
                    await eventmodel.coverphoto.CopyToAsync(new FileStream(uploadfolder,FileMode.Create));
                }
            
            Classcrudoperation op=new Classcrudoperation();
            string name = op.gettempevent();
            eventmodel.EventDept=name;
            op.saveevent(eventmodel);
            return RedirectToAction("events");
        }
        public IActionResult deleteEvent()
        {
            Classcrudoperation op = new Classcrudoperation();
            string name = op.gettempevent();
            eventmodel model = new eventmodel();
            model.EventDept = name;
            ViewBag.errormsg = TempData["delete"];
            return View(model);
        }
        [HttpPost]
        public IActionResult deleteEvent(eventmodel eventmodel)
        {
            Classcrudoperation op = new Classcrudoperation();
            string name = op.gettempevent();
            eventmodel.EventDept = name;
            var result=op.deleteevent(eventmodel);
            if (result == 0)
            {
                TempData["delete"] = "Invalid Event Name";
            }
            else
            {
                TempData["delete"] = "Event Deleted Successfully";
            }
            return RedirectToAction("deleteEvent","dept");
        }
        public IActionResult veiwdetails(int Id)
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.GetEvent(Id);
            return View(event1);
        }
        public IActionResult addreport(int Id)
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.GetEvent(Id);
            return View(event1);      
        }
        [HttpPost]
        public IActionResult addreport(eventmodel eventmodel)
        {
            Classcrudoperation op = new Classcrudoperation();
            op.addreport(eventmodel);
            ViewBag.errormsg = "Report Added Succesfully";
            return View();
        }
        public IActionResult geteventbydept()
        {
            Classcrudoperation op=new Classcrudoperation();
            string name = op.gettempevent();
            var event1 = op.eventsbydept(name);
            return View(event1);
        }
        public IActionResult viewstudents(int id)
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.getstudentsbyevents(id);
            return View(event1);
        }
        public IActionResult getcompletedeventbydept()
        {
            Classcrudoperation op = new Classcrudoperation();
            string name = op.gettempevent();
            var event1 = op.completedeventsbydept(name);
            return View(event1);
        }
        public IActionResult getpendingeventbydept()
        {
            Classcrudoperation op = new Classcrudoperation();
            string name = op.gettempevent();
            var event1 = op.pendingeventsbydept(name);
            return View(event1);
        }
        public IActionResult getdeniedeventbydept()
        {
            Classcrudoperation op = new Classcrudoperation();
            string name = op.gettempevent();
            var event1 = op.deniedeventsbydept(name);
            return View(event1);
        }

    }
}
