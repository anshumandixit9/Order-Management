using HAHADotNet.Data;
using HAHADotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace HAHADotNet.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.categories.ToList();
            return View(objCategoryList);
        }
        //Get

        //Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Customer Error", "The Display Order and the Name cannot be same");
            }
            if (ModelState.IsValid) {
                _db.categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
           
        }

        // Update
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var categoryFromDb = _db.categories.Find(id);
            if (categoryFromDb == null)
                return NotFound();

            return View(categoryFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Customer Error", "The Display Order and the Name cannot be same");
            }
            if (ModelState.IsValid)
            {
                _db.categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            var deleteFromDb = _db.categories.Find(id);
            return View(deleteFromDb);            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            _db.categories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
