using System;

namespace Core.Models
{
    public class ConfigItem
    {
        public string ID { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public string Action { get; set; }

        public string Filter { get; set; }
    }
}
