using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Model to hold the neccessary data to be pased through the controller and utilized by the index view
namespace Assignment10.Models.ViewModels
{
    public class IndexViewModel
    {
        public List<Bowlers> Bowlers { get; set; }
        public PageNumberingInfo PageNumberingInfo { get; set; }
        public string Team { get; set; }
    }
}
