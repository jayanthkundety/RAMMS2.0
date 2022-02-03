using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class FormB1B2ImgRequestDTO
    {
        [MapTo("FbriPkRefNo")] public int PkRefNo { get; set; }
        [MapTo("FbriFbrihPkRefNo")] public int? FbrihPkRefNo { get; set; }
        [MapTo("FbriImgRefId")] public string ImgRefId { get; set; }
        [MapTo("FbriImageTypeCode")] public string ImageTypeCode { get; set; }
        [MapTo("FbriImageSrno")] public int? ImageSrno { get; set; }
        [MapTo("FbriImageFilenameSys")] public string ImageFilenameSys { get; set; }
        [MapTo("FbriImageFilenameUpload")] public string ImageFilenameUpload { get; set; }
        [MapTo("FbriImageUserFilepath")] public string ImageUserFilepath { get; set; }
        [MapTo("FbriModBy")] public int? ModBy { get; set; }
        [MapTo("FbriModDt")] public DateTime? ModDt { get; set; }
        [MapTo("FbriCrBy")] public int? CrBy { get; set; }
        [MapTo("FbriCrDt")] public DateTime? CrDt { get; set; }
        [MapTo("FbriSubmitSts")] public bool? SubmitSts { get; set; }
        [MapTo("FbriActiveYn")] public bool? ActiveYn { get; set; }
    }
}
