using Microsoft.WindowsAzure.Mobile.Service;

namespace BumpBuster.Api.DataObjects
{
    public class Bump : EntityData
    {
        public int Severity { get; set; }

        public bool Active { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}