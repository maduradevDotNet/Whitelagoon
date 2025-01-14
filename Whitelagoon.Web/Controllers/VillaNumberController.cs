using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Whitelagoon.Application.Common.Interfaces;
using Whitelagoon.Domain.Entities;
using Whitelagoon.Infrastructure.Data;
using Whitelagoon.Web.ViewModel;

namespace Whitelagoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
         
        public IActionResult Index()
        {
            var VillaNumber = _UnitOfWork.VillaNumber.GetAll(includeProperties:"Villa");
            return View(VillaNumber);
        }

        public IActionResult Create()
        {


            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _UnitOfWork.Villa.GetAll().Select(
                     u => new SelectListItem
                     {
                         Text = u.Name,
                         Value = u.Id.ToString()
                     } )
            };

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {

            // ModelState.Remove("Villa");
            bool roomNumberAlreadyExit = _UnitOfWork.VillaNumber.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
            
            if (ModelState.IsValid && !roomNumberAlreadyExit)
            {
                _UnitOfWork.VillaNumber.add(obj.VillaNumber);
                _UnitOfWork.Save();
                TempData["success"] = "the villa number has been Created successfully!";
                return RedirectToAction("Index");
            }

            if (roomNumberAlreadyExit)
            {
                TempData["error"] = "the RoomNumber  has been Already  Exits!";
            }

            obj.VillaList  = _UnitOfWork.Villa.GetAll().Select(
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
                VillaList = _UnitOfWork.Villa.GetAll().Select(
                  u => new SelectListItem
                  {
                      Text = u.Name,
                      Value = u.Id.ToString()
                  }),
                VillaNumber=_UnitOfWork.VillaNumber.Get(x=>x.Villa_Number== villaNumberId)

            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(villaNumberVM);
        }



        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {


            if (ModelState.IsValid)
            {
                _UnitOfWork.VillaNumber.Update(villaNumberVM.VillaNumber);
                _UnitOfWork.Save();
                TempData["success"] = "the villa number has been Updated successfully!";
                return RedirectToAction("Index");
            }


            villaNumberVM.VillaList = _UnitOfWork.Villa.GetAll().Select(
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
                VillaList = _UnitOfWork.Villa.GetAll().Select(
                  u => new SelectListItem
                  {
                      Text = u.Name,
                      Value = u.Id.ToString()
                  }),
                VillaNumber = _UnitOfWork.VillaNumber.Get(x => x.Villa_Number == villaNumberId)

            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }



        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.VillaNumber.Remove(villaNumberVM.VillaNumber);
                _UnitOfWork.Save();
                TempData["success"] = "the villa number has been Remove successfully!";
                return RedirectToAction("Index");
            }


            villaNumberVM.VillaList = _UnitOfWork.Villa.GetAll().Select(
                     u => new SelectListItem
                     {
                         Text = u.Name,
                         Value = u.Id.ToString()
                     });

            return View(villaNumberVM);
        }


    }
}
