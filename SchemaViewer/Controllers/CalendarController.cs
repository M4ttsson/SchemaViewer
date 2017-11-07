using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchemaViewer.Models;

namespace SchemaViewer.Controllers
{
    [Route("api/[controller]")]
    public class CalendarController : Controller
    {
		[HttpGet("[action]")] 
		public IEnumerable<CalendarEvent> GetCalendarEvents(string url)
		{
			return new List<CalendarEvent>()
			{
				new CalendarEvent()
				{
					Start = DateTime.Now.AddDays(1).ToString("yy-MM-dd HH:mm"),
					End = DateTime.Now.AddDays(1).AddHours(4).ToString("yy-MM-dd HH:mm"),
					Location = "B089",
					Summary = "Teacher, course name"
				}
			};
		}
    }
}