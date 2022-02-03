namespace RAMMS.MobileApps
{
    public class ResponseBaseObject<T>
    {
        public T data { get; set; }

        public bool success { get; set; }

        public string errorMessage { get; set; }
    }
}