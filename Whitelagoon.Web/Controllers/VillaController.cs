using Microsoft.AspNetCore.Mvc;
using Whitelagoon.Application.Common.Interfaces;
using Whitelagoon.Domain.Entities;
using Whitelagoon.Infrastructure.Data;
using Whitelagoon.Infrastructure.Repository;

namespace Whitelagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaRepository _villaRepository;

        public VillaController(IVillaRepository villaRepository)
        {
            _villaRepository = villaRepository;
        }


        public IActionResult Index()
        {
            var Villa = _villaRepository.GetAll();
            return View(Villa);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            if (villa.Name == villa.Description)
            {
                ModelState.AddModelError("Name", "The description can not exactly math the name");
            }
            if (ModelState.IsValid)
            {
                _villaRepository.add(villa);
                _villaRepository.Save();
                TempData["success"] = "the villa has been Created successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "the villa has been Created Unsuccessfully!";
            return View(villa);
        }


        public IActionResult Update(int villaId)
        {
            Villa? obj = _villaRepository.Get(x => x.Id == villaId);

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
                _villaRepository.Update(villa);
                _villaRepository.Save();
                 TempData["success"] = "the villa has been Updated successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "the villa has been Updated Unsuccessfully!";
            return View(villa);
        }


        public IActionResult Delete(int villaId)
        {
            Villa? obj = _villaRepository.Get(x => x.Id == villaId);

            //  var villaList = _db.Villas.Where(u => u.Price > 50 && u.Occupancy>0).FirstOrDefault();
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }



        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _villaRepository.Get(u => u.Id == obj.Id);

            if (objFromDb is not null)
            {
                _villaRepository.Remove(objFromDb);
                _villaRepository.Save();
                TempData["success"] = "the villa has been deleted successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "the villa has been deleted Unsuccessfully!";
            return View(obj);
        }


    }
}
