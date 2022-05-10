using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Models
{
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }

        public string Product_Name { get; set; }

        public string Product_Brand { get; set; }

        public string Product_Fabric{ get; set; }

        public int Product_Price { get; set; }


        public int Product_Stock { get; set; }

    }
}
