using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchemaViewer.Models;

using System.IO;
using System.Net;
using Ical.Net;
using SchemaViewer.Exceptions;

namespace SchemaViewer.Controllers
{
    [Route("api/[controller]")]
    public class CalendarController : Controller
    {
		[HttpGet("[action]")] 
		public IEnumerable<Models.CalendarEvent> GetCalendarEvents(string url)
		{
			using (MemoryStream stream = GetIcalStream(url))
			{
				// check if this can be null? will there be exceptions if file is not icalendar valid?
				var calendarCollection = Calendar.LoadFromStream(stream);
				if(calendarCollection == null || !calendarCollection.Any())
				{
					throw new ApiException("File at url contained no valid calendars");
				}

				List<Models.CalendarEvent> events = new List<Models.CalendarEvent>();

				// multiple calendars in same file is unusual but allowed
				foreach (var calendar in calendarCollection)
				{
					events.AddRange(calendar.Events.OrderBy(ev => ev.Start).Select(ev => new Models.CalendarEvent()
					{
						Start = ev.Start.AsSystemLocal.ToString("yy-MM-dd HH:mm"),
						End = ev.End.AsSystemLocal.ToString("yy-MM-dd HH:mm"),
						Location = ev.Location,
						Summary = ev.Summary
					}));
				}
				return events;
			}
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