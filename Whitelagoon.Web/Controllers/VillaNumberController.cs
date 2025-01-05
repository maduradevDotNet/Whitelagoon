using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Whitelagoon.Domain.Entities;
using Whitelagoon.Infrastructure.Data;
using Whitelagoon.Web.ViewModel;

namespace Whitelagoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDBContext _db;

        public VillaNumberController(ApplicationDBContext db)
        {
            _db=db;
        }
         

        public IActionResult Index()
        {
            var VillaNumber=_db.VillaNumbers.Include(u=>u.Villa).ToList();
            return View(VillaNumber);
        }

        public IActionResult Create()
        {

            //IEnumerable<SelectListItem> list = _db.Villas.ToList().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    }
            //    );

            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(
                     u => new SelectListItem
                     {
                         Text = u.Name,
                         Value = u.Id.ToString()
                     } )
            };

            //ViewData["VillaList"] = list;
          //  ViewBag.VillaList=list;
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {

            // ModelState.Remove("Villa");
            bool roomNumberAlreadyExit = _db.VillaNumbers.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
            
            if (ModelState.IsValid && !roomNumberAlreadyExit)
            {
                _db.VillaNumbers.Add(obj.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "the villa number has been Created successfully!";
                return RedirectToAction("Index");
            }

            if (roomNumberAlreadyExit)
            {
                TempData["error"] = "the RoomNumber  has been Already  Exits!";
            }

            obj.VillaList  = _db.Villas.ToList().Select(
                     u => new SelectListItem
                     {
                         Text = u.Name,
                         Value = u.Id.ToString()
                     });

            return View(obj);
        }

        public IActionResult Update(int villaNumberId)
        {

            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(
                  u => new SelectListItem
                  {
                      Text = u.Name,
                      Value = u.Id.ToString()
                  }),
                VillaNumber=_db.VillaNumbers.FirstOrDefault(x=>x.Villa_Number== villaNumberId)

            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }

            //Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaNumberId);

          //  var villaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy>0).FirstOrDefault();
            //if (obj == null)
            //{
            //    return RedirectToAction("Error", "Home");
            //}
            return View(villaNumberVM);
        }



        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {


            if (ModelState.IsValid)
            {
                _db.VillaNumbers.Update(villaNumberVM.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "the villa number has been Updated successfully!";
                return RedirectToAction("Index");
            }


            villaNumberVM.VillaList = _db.Villas.ToList().Select(
                     u => new SelectListItem
                     {
                         Text = u.Name,
                         Value = u.Id.ToString()
                     });

            return View(villaNumberVM);
        }



        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(
                  u => new SelectListItem
                  {
                      Text = u.Name,
                      Value = u.Id.ToString()
                  }),
                VillaNumber = _db.VillaNumbers.FirstOrDefault(x => x.Villa_Number == villaNumberId)

            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }

            //Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaNumberId);

            //  var villaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy>0).FirstOrDefault();
            //if (obj == null)
            //{
            //    return RedirectToAction("Error", "Home");
            //}
            return View(villaNumberVM);
        }



        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {
            if (ModelState.IsValid)
            {
                _db.VillaNumbers.Remove(villaNumberVM.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "the villa number has been Remove successfully!";
                return RedirectToAction("Index");
            }


            villaNumberVM.VillaList = _db.Villas.ToList().Select(
                     u => new SelectListItem
                     {
                         Text = u.Name,
                         Value = u.Id.ToString()
                     });

            return View(villaNumberVM);
        }


    }
}
