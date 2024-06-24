using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace ImageAPI.Models
{
    public class ImageContext: DbContext, IImageContext
    {
        public DbSet<Image> Images { get; set; }

        public string DbPath { get; }

        public ImageContext()
        {
            var path = Environment.CurrentDirectory;
            DbPath = Path.Join(path, "data.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    }
}
