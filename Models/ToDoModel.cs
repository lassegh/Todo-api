using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIS.Models
{
	public class ToDoModel
	{
		public bool IsDone { get; set; }
		public string Content { get; set; }
		public int Id { get; set; }
	}
}
