using System;
using System.Collections.Generic;
using System.Linq;

namespace ESAPrizes.Models {
    public class HomeModel
    {
        public IEnumerable<IGrouping<Tuple<int, string>, Prize>> Prizes { get; set; } 
    }
}