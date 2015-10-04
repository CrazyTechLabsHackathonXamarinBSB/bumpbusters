using System;

namespace BumpBuster.Models
{
	public class BumpService
	{
		public Bump[] List() {
			return new [] {
				new Bump {
					Severity = BumpSeverity.Light,
					Latitude = 33.0,
					Longitude = 33.0
				}
			};
		}

		public void Add(double latidute, double longitude, BumpSeverity severity) {
			
		}

		public void Remove(double latidute, double longitude) {

		}
	}
}

