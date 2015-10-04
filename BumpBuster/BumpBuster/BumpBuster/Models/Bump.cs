using System;

namespace BumpBuster.Models
{
	public class Bump
	{
		public string Id {
			get;
			set;
		}

		public double Latitude {
			get;
			set;
		}

		public double Longitude {
			get;
			set;
		}

		public BumpSeverity Severity {
			get;
			set;
		}

		public bool Active {
			get;
			set;
		}
	}
}

