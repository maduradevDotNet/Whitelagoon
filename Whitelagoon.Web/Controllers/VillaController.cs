using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Whitelagoon.Application.Common.Interfaces;
using Whitelagoon.Domain.Entities;
using Whitelagoon.Infrastructure.Data;
using Whitelagoon.Infrastructure.Repository;

namespace Whitelagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        private readonly IWebHostEnvironment _WebHostEnvironment;

        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _UnitOfWork = unitOfWork;
            _WebHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            var Villa = _UnitOfWork.Villa.GetAll();
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

                if (villa.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(villa.Image.FileName);
                    string imagePath = Path.Combine(_WebHostEnvironment.WebRootPath, @"images\Villa");

                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    villa.Image.CopyTo(fileStream);

                    villa.ImageUrl = @"\images\Villa\" + fileName;

                }
                else
                {
                    villa.ImageUrl = "/images/placeholder.png";

                }

                _UnitOfWork.Villa.add(villa);
                _UnitOfWork.Save();
                TempData["success"] = "the villa has been Created successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "the villa has been Created Unsuccessfully!";
            return View(villa);
        }


        public IActionResult Update(int villaId)
        {
            Villa? obj = _UnitOfWork.Villa.Get(x => x.Id == villaId);

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

                if (villa.Image != null)
                {
                    // Generate new file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(villa.Image.FileName);
                    string imagePath = Path.Combine(_WebHostEnvironment.WebRootPath, @"images\Villa");

                    // Check if there's an existing image and delete it
                    if (!string.IsNullOrEmpty(villa.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_WebHostEnvironment.WebRootPath, villa.ImageUrl.TrimStart('\\'));

                        // Check if the old image exists and delete it
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Upload the new image
                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    villa.Image.CopyTo(fileStream);

                    // Update the image URL in the object
                    villa.ImageUrl = @"\images\Villa\" + fileName;
                }



                _UnitOfWork.Villa.Update(villa);
                _UnitOfWork.Save();
                 TempData["success"] = "the villa has been Updated successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "the villa has been Updated Unsuccessfully!";
            return View(villa);
        }


        public IActionResult Delete(int villaId)
        {
            Villa? obj = _UnitOfWork.Villa.Get(x => x.Id == villaId);

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
            Villa? objFromDb = _UnitOfWork.Villa.Get(u => u.Id == obj.Id);

            if (objFromDb is not null)
            {

                if (!string.IsNullOrEmpty(objFromDb.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_WebHostEnvironment.WebRootPath, objFromDb.ImageUrl.TrimStart('\\'));

                    // Check if the old image exists and delete it
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }


                _UnitOfWork.Villa.Remove(objFromDb);
                _UnitOfWork.Save();
                TempData["success"] = "the villa has been deleted successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "the villa has been deleted Unsuccessfully!";
            return View(obj);
        }


    }
}
