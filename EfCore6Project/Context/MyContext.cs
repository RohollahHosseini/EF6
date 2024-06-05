using EfCore6Feature.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore6Feature.Context
{
    internal class MyContext:DbContext
    {
       public DbSet<Product> Products { get; set; }
       public DbSet<Category> Categoryes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=EF6Features;Integrated Security=True;")
                //استفاده کردیم CompiledModel دراینجا از 
                //قرار دادم solution در بیرون از  txt دستورات را در فایل 
                .UseModel(CompiledModels.MyContextModel.Instance); 
        }

        /// <summary>
        /// قرار میدهیم MaxLengthیک مقدار  string است یعنی برای تمامی پرورپتی هایی از نوع Pre-Convention این قابلیت 
        /// </summary>
        /// <param name="configurationBuilder"></param>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(500);   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///به این صورت عمل میکنیم Temporal برای تبدیل یک جدول به 
            modelBuilder.Entity<Product>().ToTable("Products", c => c.IsTemporal(
                x =>
                {
                    //  ها به اسن صورت می نیویسیمProperty  برای تغییر اسم این 
                    //نکته:از این پس هم از این اسم های جدید استفاده میشود
                    //x.HasPeriodStart("start");
                    //x.HasPeriodEnd("end");
                }
                )); 
        }
    }
}
