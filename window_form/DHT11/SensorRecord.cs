using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DHT11
{
    class SensorRecord
    {
        //[Key]
        public int id { get; set; }
        public string time { get; set; }
        public int temperature { get; set; }
        public int humidity { get; set; }
        public int led { get; set; }

        public SensorRecord(int _id, string _time, int _temperature, int _humidity, int _led)
        {
            id = _id;
            time = _time;
            temperature = _temperature;
            humidity = _humidity;
            led = _led;
        }

        public SensorRecord(string _time, int _temperature, int _humidity, int _led)
        {
            time = _time;
            temperature = _temperature;
            humidity = _humidity;
            led = _led;
        }

        /*public override string ToString()
        {
            return id.ToString();
        }*/
    }
}
