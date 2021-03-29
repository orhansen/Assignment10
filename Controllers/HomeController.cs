using Assignment10.Models;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;      //The context of the bowler data set. Allows us to query and filter from it
        }

        public IActionResult Index(long? teamid, string teamname, int pageNum = 0)
        {
            int pageSize = 5;

            return View(new IndexViewModel
            {
                Bowlers = (context.Bowlers                                  //Main info for the individual bowler contact cards
                          .Where(b => b.TeamId == teamid || teamid == null)
                          .OrderBy(b => b.Team)
                          .Skip((pageNum - 1) * pageSize)
                          .Take(pageSize)
                          .ToList()),

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize, //Recieves the data set at our local variables and passes it to the page numbering model
                    CurrentPage = pageNum,

                    //If no team is selected, get the full context count. Otherwise just get the count for the team selected.
                    TotalNumItems = (teamid == null ? context.Bowlers.Count() : context.Bowlers.Where(b => b.TeamId == teamid).Count())
                },

                Team = teamname
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
