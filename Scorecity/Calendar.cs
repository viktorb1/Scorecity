//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Json;
//using System.Text;
//using System.Threading.Tasks;

//namespace BBScores
//{
//	class CalendarGetter
//	{
//		public async static Task<Dictionary<string, int>> getCalendar()
//		{
//			var http = new HttpClient();
//			var url = "http://data.nba.net/data/10s/prod/v2/calendar.json";
//			var response = await http.GetAsync(url);
//			string result = await response.Content.ReadAsStringAsync();
//			result = result.Remove(1, 142);
//			Dictionary<string, int> values = JsonConvert.DeserializeObject<Dictionary<string, int>>(result);
//			return values;
//		}
//	}
//}

