using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RAMMS.MobileApps.Interface
{
    public interface IFormFile
    {
        string ContentDisposition { get; }
        string ContentType { get; }
        string FileName { get; }
        List<string> Headers { get; }
        long Length { get; }
        string Name { get; }
        void CopyTo(Stream target);    
        Task CopyToAsync(Stream target, CancellationToken cancellationToken = default);
        Stream OpenReadStream();
    }
}
