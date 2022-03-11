using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Kutse_App.Models
{
    public class PuhadInitializer : CreateDatabaseIfNotExists<PuhadContext>
    {
        protected override void Seed(PuhadContext pd)
        {
            base.Seed(pd);

        }
    }
}