using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.MobileServices;

namespace BumpBuster.Models
{
	public class BumpService
	{
		private IMobileServiceTable<Bump> Table {
			get {
				var url = @"https://bumpbuster.azure-mobile.net/";
				var key = "FLsNwBhhZNocetZezcHyIkmVaCNZUM45";

				var service = new MobileServiceClient(url, key);

				return service.GetTable<Bump>();
			}
		}

		public async Task<List<Bump>> ListAsync() {
			var list = await Table.ToListAsync ();
			var result = from i in list
				group i by new { X =  Math.Round(i.Latitude, 2), Y = Math.Round(i.Longitude, 2) } into g
				select g.First();

			return result.ToList();
		}

		public async Task AddAsync(double latitude, double longitude, BumpSeverity severity) {
			var item = new Bump {
				Severity = (int)severity,
				Latitude = latitude,
				Longitude = longitude,
				Active = true
			};
				
			await Table.InsertAsync(item);
		}

		public async Task DeleteAsync(double latitude, double longitude) {
			var item = new Bump {
				Severity = (int)BumpSeverity.None,
				Latitude = latitude,
				Longitude = longitude,
				Active = false
			};

			await Table.InsertAsync(item);
		}
	}
}

