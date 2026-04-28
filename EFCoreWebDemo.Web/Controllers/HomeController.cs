using System.Diagnostics;
using EFCoreWebDemo.Data;
using EFCoreWebDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace EFCoreWebDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var repo = new PeopleRepository(_configuration.GetConnectionString("ConStr"));
            var vm = new IndexViewModel
            {
                People = repo.GetAll()
            };
            return View(vm);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Person person)
        {
            var repo = new PeopleRepository(_configuration.GetConnectionString("ConStr"));
            repo.Add(person);
            return Redirect("/home/index");
        }

        public IActionResult Edit(int personId)
        {
            var repo = new PeopleRepository(_configuration.GetConnectionString("ConStr"));

            var person = repo.GetById(personId);
            if(person == null)
            {
                return Redirect("/home/index");
            }
            var vm = new EditPersonViewModel { Person =person };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Update(Person person)
        {
            var repo = new PeopleRepository(_configuration.GetConnectionString("ConStr"));
            repo.Update(person);
            return Redirect("/home/index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var repo = new PeopleRepository(_configuration.GetConnectionString("ConStr"));
            repo.Delete(id);
            return Redirect("/home/index");
        }
    }
}
