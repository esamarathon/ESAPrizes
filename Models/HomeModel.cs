using System;
using System.Collections.Generic;
using System.Linq;

namespace ESAPrizes.Models
{
    public class HomeModel
    {
        public IEnumerable<IGrouping<Category, Prize>> Prizes { get; set; } 
    }
}