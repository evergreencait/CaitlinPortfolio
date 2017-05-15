using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CaitlinPortfolio.Models;

namespace CaitlinPortfolio.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            var allProjects = Project.GetProjects();
            return View(allProjects);
        }

    }
}
