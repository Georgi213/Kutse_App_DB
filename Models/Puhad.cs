using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kutse_App.Models
{
    public class Puhad
    {
		public int Id { get; set; }
		[Required(ErrorMessage = "On vaja sisesta pidu nime!")]
		public string Name { get; set; }
		[Required(ErrorMessage = "On vaja sisesta kuupäev!")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }
	}
}