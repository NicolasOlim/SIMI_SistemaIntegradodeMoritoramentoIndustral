using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class SensorData
    {

        public int Id { get; set; }
        public double Temperatura { get; set; }
        public double Umidade { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
