using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly AnimalShelterContext _db;

        public AnimalsController(AnimalShelterContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Animal> model = _db.Animals.ToList();
            return View(model);
        }

        public ActionResult Index(string sort)
        {
            IList<Animal> model = _db.Animals.ToList();
            if(sort == "type")
            {
                model = (IList<Animal>) model.OrderBy(s => s.Type);
            }
            else if(sort == "date")
            {
                model = (IList<Animal>) model.OrderBy(s => s.Date);
            }
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Animal animal)
        {
            _db.Animals.Add(animal);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Animal thisAnimal = _db.Animals.FirstOrDefault(animal => animal.AnimalId == id);
            return View(thisAnimal);
        }
    }
}