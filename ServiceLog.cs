using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dolgozat12._04
{
    public class ServiceLog
    {
        public int Id { get; set; }
        public Brand Brand { get; set; }
        public string CarModel { get; set; }
        public string LicensePlate { get; set; }
        public int Mileage { get; set; }
        public DateTime ServiceDate { get; set; }
        public List<ServiceDetail> ServiceDetails { get; set; }
    }
}
