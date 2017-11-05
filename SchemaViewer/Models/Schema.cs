using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace SchemaViewer.Models
{
	class Schema
	{
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public IList<Session> Sessions { get; set; }

		public Schema()
		{
			Sessions = new List<Session>();
		}
	}
}