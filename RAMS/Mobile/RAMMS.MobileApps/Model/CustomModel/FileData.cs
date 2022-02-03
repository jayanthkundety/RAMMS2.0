using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RAMMS.MobileApps
{
    public class FileData
    {
        public string UploadFileName { get; set; }

        public List<MediaFile> Files { get; set; }

        public ObservableCollection<string> FileNames { get; set; }
    }
}