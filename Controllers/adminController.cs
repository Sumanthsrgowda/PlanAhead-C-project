using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dataaccesslayer;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class adminController : Controller
    {
        public IActionResult Index()
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.getevents();
            return View(event1);
        }
        public IActionResult addRes(int Id)
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.GetEvent(Id);
            return View(event1);
        }
        [HttpPost]
        public IActionResult addRes(eventmodel eventmodel) 
        {
            Classcrudoperation op = new Classcrudoperation();
            op.saveres(eventmodel);
            return RedirectToAction("Index");
        }
        public IActionResult veiwdetails(int Id)
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.GetEvent(Id);
            return View(event1);
        }
        public IActionResult department()
        {
            Classcrudoperation op = new Classcrudoperation();
            var dept = op.getdepartment();
            return View(dept);
        }
        public IActionResult GetEventByDept(string name) 
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.eventsbydept(name);
            return View(event1);
        }
        public IActionResult addDepartment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addDepartment(loginmodel loginmodel)
        {
            Classcrudoperation op1 = new Classcrudoperation();
            op1.addDept(loginmodel);
            return RedirectToAction("department","admin");
        }
        public IActionResult deleteDepartment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult deleteDepartment(deptmodel deptmodel)
        {
            Classcrudoperation op1 = new Classcrudoperation();
            op1.deldept(deptmodel);
            return RedirectToAction("department", "admin");
        }
        public IActionResult completedevent()
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.getcompletedevent();
            return View(event1);
        }
        public IActionResult pendingevent()
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.getpendingevent();
            return View(event1);
        }
        public ActionResult approveevent(int id)
        {
            Classcrudoperation op = new Classcrudoperation();
            op.approve(id);
            return RedirectToAction("pendingevent", "admin");
        }
        public ActionResult denyevent(int id)
        {
            Classcrudoperation op = new Classcrudoperation();
            op.deny(id);
            return RedirectToAction("pendingevent", "admin");
        }
        public IActionResult deniedevent()
        {
            Classcrudoperation op = new Classcrudoperation();
            var event1 = op.getdeniedevent();
            return View(event1);
        }
    }
}
