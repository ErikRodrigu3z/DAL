using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VPModel.Models
{
    public class DBContext : DbContext
    {
        public DBContext() : base("MyDBConnectionString")
        {

        }

        //clases de modelo       
        public DbSet<Usuarios> usuariosDB { get; set; }

    }
}