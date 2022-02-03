using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RAMMS.WebAPI
{
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        public Controller() { }
        public IActionResult RAMMSApiSuccessResponse(object Data = null)
        {
            var obj = new
            {
                success = true,
                errorMessage = "",
                data = Json(Data).Value,
            };

            return Json(obj);
        }
        // return the Error Result for all controller calls
        public IActionResult RAMMSApiErrorResponse(string ErrorMessage = "", object Data = null)
        {
            var obj = new
            {
                success = false,
                errorMessage = ErrorMessage,
                data = Json(Data).Value
            };

            return Json(obj);
        }
    }
}