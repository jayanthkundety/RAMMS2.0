using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.WebAPI
{
    public class CBadRequest : BadRequestObjectResult
    {
        public CBadRequest(string errorMessage) : base(new { Error = errorMessage, StackTrace = "" })
        {

        }
        public CBadRequest(string errorMessage, object item) : base(new { Error = errorMessage, StackTrace = item })
        {

        }
    }
}
