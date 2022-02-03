using FreshMvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps.PageModel
{
    public class TestViewModel
    {
        public Func<Task<byte[]>> SignatureFromStream { get; set; }
        public byte[] Signature { get; set; }

        public ICommand MyCommand => new Command(async () =>
        {
            Signature = await SignatureFromStream();
            // Signature should be != null
        });
    }


    public static class ImageConverter
    {
        public static async Task<byte[]> ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
