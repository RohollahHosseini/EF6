using EfCore6Feature.Context;
using EfCore6Feature.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore6Feature.Repository
{
    public class ProductRepository
    {
        MyContext Context=new MyContext();

        #region TemporalTable

        public void AddProduct()
        {
            Context.Products.Add(new Models.Product()
            {
                Name = "Product2",
                Price = 1600
            });
            Context.SaveChanges();
        }

        public void ShowProduct()
        {
            var products= Context.Products.ToList();
            ///به این صورت می نویسیم priodEnd و priodStart برای دریافت مقدار های 
            foreach (var product in products)
            {
                var productEntry=Context.Entry(product);

                var start=productEntry.Property<DateTime>("PeriodStart").CurrentValue;
                var end=productEntry.Property<DateTime>("PeriodEnd").CurrentValue;

                Console.WriteLine($"ProductId:{product.Id} Name:{product.Name} Price:{product.Price} PeriodStart:{start} PeriodEnd:{end}");

            }
        }

        public void History()
        {
            ///برای دریافت تاریخچه تغییرات مربوط به یک محصول اینجوری عمل میکنیم
            var products = Context.Products.TemporalAll()
                .Where(c => c.Id == 1)
                .OrderBy(c => EF.Property<DateTime>(c, "PeriodStart"))
                .Select(c=>new
                {
                    Product=c,
                    PeriodStart=EF.Property<DateTime>(c,"PeriodStart"),
                    PeriodEnd=EF.Property<DateTime>(c,"PeriodEnd"),
                })
                .ToList();

            ///cosnole نمایش در 

            Console.WriteLine("**************************************");
            foreach (var product in products)
            {
                Console.WriteLine($"ProductId: {product.Product.Id} Name:{product.Product.Name} Price:{product.Product.Price} " +
                    $"StartPeriod:{product.PeriodStart} PeriodEnd:{product.PeriodEnd}");
            }

        }

        /// <summary>
        /// برای برگشت دادن تغییرات به هر تاریخچه ای که می خواهید به این صورت عمل میکنیم
        /// </summary>
        public void Resore()
        {
            ///دریافت کل تاریخجه  محصول یک
            var history = Context.Products.TemporalAll()
             .Where(c => c.Id == 1)
             .OrderBy(c => EF.Property<DateTime>(c, "PeriodStart"))
             .Select(c => new
             {
                 Product = c,
                 PeriodStart = EF.Property<DateTime>(c, "PeriodStart"),
                 PeriodEnd = EF.Property<DateTime>(c, "PeriodEnd"),
             })
             .FirstOrDefault();

            ///کردن یعنی بازگشت اطلاعات به این تاریخ Restore 
            var product = Context.Products
                .TemporalAsOf(history.PeriodStart)
                .Single(c => c.Id == 1);
            //اضافه کنیم Add اگر محصول حذف شده باشد به این صورت آنرا باز می گردانیم فقط لازم ایت برای آیدی صفر و از
            //product.Id = 0;
            //Context.Add(product);
    
            Context.Update(product);
            Context.SaveChanges();
        }

        #endregion


    }
}
