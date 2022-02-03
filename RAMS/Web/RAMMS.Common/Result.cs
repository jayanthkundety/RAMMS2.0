using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Common
{
    public class Result<T>
    {
        public Result()
        {
            IsSuccess = true;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
