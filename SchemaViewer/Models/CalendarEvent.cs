using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaViewer.Models
{
    public class CalendarEvent
    {
		public string Start { get; set; }
		public string End { get; set; }
		public string Location { get; set; }
		public string Summary { get; set; }
	}
}
