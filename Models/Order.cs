using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models {
    public class Order {
        public int id { get; set; }
        public int user_id { get; set; }
        public string[] products { get; set; }
        public int[] quantities { get; set; }
        public float price { get; set; }
    }
}