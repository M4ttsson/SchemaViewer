using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchemaViewer.Models;
using System.IO;
using System.Net;
using Ical.Net;

namespace SchemaViewer.Controllers
{
    [Route("api/[controller]")]
    public class CalendarController : Controller
    {
		[HttpGet("[action]")] 
		public IEnumerable<Models.CalendarEvent> GetCalendarEvents(string url)
		{
			// TODO: Error handling...
			var calendarCollection = Calendar.LoadFromStream(GetIcalStream(url));
			var firstCalendar = calendarCollection.First();

			List<Models.CalendarEvent> events = new List<Models.CalendarEvent>();

			foreach (var ev in firstCalendar.Events)
			{
				events.Add(new Models.CalendarEvent()
				{
					Start = ev.Start.ToString(),
					End = ev.End.ToString(),
					Location = ev.Location,
					Summary = ev.Summary
				});
			}
			return events;
			//return new List<Models.CalendarEvent>()
			//{
			//	new Models.CalendarEvent()
			//	{
			//		Start = DateTime.Now.AddDays(1).ToString("yy-MM-dd HH:mm"),
			//		End = DateTime.Now.AddDays(1).AddHours(4).ToString("yy-MM-dd HH:mm"),
			//		Location = "B089",
			//		Summary = "Teacher, course name"
			//	}
			//};
		}

		private MemoryStream GetIcalStream(string url)
		{
			byte[] data = null;

			using (var wc = new WebClient())
			{
				data = wc.DownloadData(url);
			}

			return new MemoryStream(data);
		}
	}
}