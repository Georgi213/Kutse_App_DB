using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Kutse_App.Models
{
    public class PuhadContext : DbContext
    {
        public DbSet<Puhad> Puhad { get; set; }
    }
}