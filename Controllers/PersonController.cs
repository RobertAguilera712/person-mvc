using Microsoft.AspNetCore.Mvc;
using persons_mvc.Models;
using persons_mvc.Services;

namespace persons_mvc.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        public ActionResult Index()
        {
            var persons = _personService.GetAll();
            return View(persons);
        }

        public ActionResult Details(int id)
        {
            var person = _personService.Get(id);
            return View(person);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            _personService.Insert(person);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id){
            var person = _personService.Get(id);
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(Person person){
             _personService.Update(person);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id){
            _personService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}