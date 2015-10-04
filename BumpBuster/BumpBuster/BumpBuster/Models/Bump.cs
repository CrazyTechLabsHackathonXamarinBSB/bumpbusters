using System;

namespace BumpBuster.Models
{
	public class Bump
	{
		public Bump ()
		{

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
	}
}

