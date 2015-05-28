using System;
using System.Collections.Generic;

namespace SpaceAlert.Web.Models
{
    public class SalleViewModel
    {
        public Dictionary<Tuple<int, int>, string> ContainersPos { get; set; }
    }
}