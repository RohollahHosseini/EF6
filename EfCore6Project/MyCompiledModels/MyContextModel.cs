﻿// <auto-generated />
using EfCore6Feature.Context;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable 219, 612, 618
#nullable enable

namespace CompiledModels
{
    [DbContext(typeof(MyContext))]
    public partial class MyContextModel : RuntimeModel
    {
        static MyContextModel()
        {
            var model = new MyContextModel();
            model.Initialize();
            model.Customize();
            _instance = model;
        }

        private static MyContextModel _instance;
        public static IModel Instance => _instance;

        partial void Initialize();

        partial void Customize();
    }
}
