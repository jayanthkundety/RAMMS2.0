using Microsoft.AspNetCore.Mvc.Rendering;
//using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace RAMMS.Business.ServiceProvider
{
    public class ImportExcel
    {
        public IFormFile file { get; set; }
    }
}
