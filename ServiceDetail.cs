using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dolgozat12._04
{
    public class ServiceDetail
    {
        public int Id { get; set; }
        public int ServiceLogId { get; set; }
        public ServiceLog ServiceLog { get; set; }

        public int PartId { get; set; }
        public Part Part { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalCost => Quantity * UnitPrice;
    }
}
