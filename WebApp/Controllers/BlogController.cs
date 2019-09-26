using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp
{
    public class BlogController : Controller
    {
        private BloggingContext _dbContext;
        public BlogController(BloggingContext dbContext)
        {
            this._dbContext = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return Json(_dbContext.Blogs.ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Blog model)
        {
            _dbContext.Blogs.Add(model);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
