using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiBuffer.WebApiCore.DataModels
{
    public class MultiBufferContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=MultiBufferStore;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
        }

        public DbSet<BufferItem> BufferItems { get; set; }
    }
}
