using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.MobileApps.Interface
{
     public interface IPhotoPickerService
    {
        Task<Dictionary<string,Stream>> GetImageStreamAsync();
    }
}
