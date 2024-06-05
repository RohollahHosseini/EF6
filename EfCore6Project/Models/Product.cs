using EfCore6Feature.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore6Feature.Models
{

    //میتونیم مشخص کنیم Attributeمقدار دهی میکردیم ولی حالا ااستفاده ازاین  Programقبلا برای اینکه بگیم این کلاس دارای کانفیگ است باید در 
    [EntityTypeConfiguration(typeof(ProductConfigurations))]
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        /// <summary>
        /// را میدهیم برای اعداد اعشاری است Attribute داشته باشیم این  Decimalوقتی فیلدی از
        /// </summary>
        [Precision(10,2)]
        public decimal Price { get; set; }

        /// <summary>
        /// است یا خیر unic برای این است بگیم که این فیلد
        /// </summary>
        [Unicode(false)]
        public int ProductId { get; set; }
    }
}
