using Microsoft.AspNetCore.Mvc;
using Whitelagoon.Domain.Entities;
using Whitelagoon.Infrastructure.Data;

namespace Whitelagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDBContext _db;

        public VillaController(ApplicationDBContext db)
        {
            _db=db;
        }
         

        public IActionResult Index()
        {
            var Villa=_db.Villas.ToList();
            return View(Villa);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            if (villa.Name==villa.Description)
            {
                ModelState.AddModelError("Name","The description can not exactly math the name");
            }
            if (ModelState.IsValid)
            {
                _db.Villas.Add(villa);
                _db.SaveChanges();
                TempData["success"] = "the villa has been Created successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "the villa has been Created Unsuccessfully!";
            return View(villa);
        }


        public IActionResult Update(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaId);

          //  var villaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy>0).FirstOrDefault();
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }



        [HttpPost]
        public IActionResult Update(Villa villa)
        {
            
            if (ModelState.IsValid)
            {
                _db.Villas.Update(villa);
                _db.SaveChanges();
                TempData["success"] = "the villa has been Updated successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "the villa has been Updated Unsuccessfully!";
            return View(villa);
        }


        public IActionResult Delete(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaId);

            //  var villaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy>0).FirstOrDefault();
            if (obj is  null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }



        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _db.Villas.FirstOrDefault(u => u.Id == obj.Id);

            if (objFromDb is not null)
            {
                _db.Villas.Remove(objFromDb);
                _db.SaveChanges();
                TempData["success"] = "the villa has been deleted successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "the villa has been deleted Unsuccessfully!";
            return View(obj);
        }


    }
}
