using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SchemaViewer.Models
{
	class Session
	{
		public DateTime StartTime { get; set; }
		public DateTime StopTime { get; set; }
		public string Class { get; set; }
		public string Teacher { get; set; }
		public string Location { get; set; }
		public string CourseName { get; set; }
		public string Note { get; set; }

		public string GetShortInfo()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(StartTime.ToString("dddd dd/MM HH:mm"));
			sb.Append(" - ");
			sb.Append(StopTime.ToShortTimeString());

			sb.Append(" " + Location);
			sb.Append(" " + Teacher);
			sb.Append(" " + CourseName);
			sb.Append(" " + Note);

			return sb.ToString();
		}
	}
}