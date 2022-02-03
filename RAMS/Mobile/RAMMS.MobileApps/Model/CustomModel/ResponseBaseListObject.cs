using System.Collections.Generic;

namespace RAMMS.MobileApps
{
    public class ResponseBaseListObject<T>
    {
        public List<T> data { get; set; }

        public bool success { get; set; }

        public string errorMessage { get; set; }
    }
}