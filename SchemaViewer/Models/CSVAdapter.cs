using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace SchemaViewer.Models
{
	class CSVAdapter
	{
		public string CSVUrl { get; set; }

		private int _skipRow;

		public CSVAdapter(int skipRows)
		{
			_skipRow = skipRows;
		}


		private MemoryStream UpdateCSVFromServer()
		{
			byte[] csvData = null;

			using (var wc = new WebClient())
			{
				csvData = wc.DownloadData(CSVUrl);
			}

			return new MemoryStream(csvData);
		}

		public Schema GetSchemaFromCsv()
		{
			using (var stream = new StreamReader(UpdateCSVFromServer()))
			{
				for (int i = 0; i < _skipRow; i++)
				{
					stream.ReadLine();
				}
					
				string firstRow = stream.ReadLine();
				string[] firstSplit = firstRow.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);


				Schema schema = new Schema();
				schema.FromDate = DateTime.Parse(firstSplit[1]);
				schema.ToDate = DateTime.Parse(firstSplit[3]);

				// läs bort rubrikraden
				stream.ReadLine();
				
				while (!stream.EndOfStream)
				{
					string row = stream.ReadLine();

					var result = Regex.Split(row, ",(?=(?:[^\"]* \"[^\"]*\")*[^\"]*$)");

					Session session = new Session()
					{
						StartTime = DateTime.Parse(result[0] + " " + result[1]),
						StopTime = DateTime.Parse(result[2] + " " + result[3]),
						Class = result[4],
						Teacher = result[5],
						Location = result[6],
						CourseName = result[7],
						Note = result[8]
					};

					schema.Sessions.Add(session);
				}

				return schema;
			}
		}

	}
}