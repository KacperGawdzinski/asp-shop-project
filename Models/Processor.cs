using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models {
    public class Processor {
        public int id { get; set; }
        public string name { get; set; }
        public string hash { get; set; }
        public double price { get; set; }
    }
}