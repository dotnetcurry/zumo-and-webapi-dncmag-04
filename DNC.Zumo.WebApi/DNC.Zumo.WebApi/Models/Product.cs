using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNC.Zumo.WebApi.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }
    }
}