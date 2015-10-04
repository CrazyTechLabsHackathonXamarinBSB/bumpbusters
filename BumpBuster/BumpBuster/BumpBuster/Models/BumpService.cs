using System;
using System.Collections.Generic;
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
			return await Table.ToListAsync ();
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

