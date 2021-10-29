using backend1_uppgift_WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend1_uppgift_WebApi.Data
{
    // Ska ärva från DbContext som är en del av entity framework core
    // All funktionallitet som finns i DbContext finns nu också i SqlContext
    public class SqlContext : DbContext
    {
        // constructor 
        public SqlContext(DbContextOptions<SqlContext>options) : base(options)
        {

        }
        // Lägger in mina modeller

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customors { get; set; }

    }
}
