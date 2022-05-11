using AutoMapper.Configuration.Conventions;
using System;

namespace RAMMS.DTO.RequestBO
{
  public class RoadRequestDTO
  {
    [MapTo("RdmPkRefNo")]
    public int PkRefNo { get; set; }

    [MapTo("RdmFeatureId")]
    public string FeatureId { get; set; }

    [MapTo("RdmDivCode")]
    public string DivCode { get; set; }

    [MapTo("RdmRmuCode")]
    public string RmuCode { get; set; }

    [MapTo("RdmSecName")]
    public string SecName { get; set; }

    [MapTo("RdmRdCatgName")]
    public string RdCatgName { get; set; }

    [MapTo("RdmRdCatgCode")]
    public string RdCatgCode { get; set; }

    [MapTo("RdmRdCode")]
    public string RdCode { get; set; }

    [MapTo("RdmRdName")]
    public string RdName { get; set; }

    [MapTo("RdmFrmLoc")]
    public string FrmLoc { get; set; }

    [MapTo("RdmToLoc")]
    public string ToLoc { get; set; }

    [MapTo("RdmFrmCh")]
    public int? FrmCh { get; set; }

    [MapTo("RdmFrmChDeci")]
    public int? FrmChDeci { get; set; }

    [MapTo("RdmToCh")]
    public int? ToCh { get; set; }

    [MapTo("RdmToChDeci")]
    public int? ToChDeci { get; set; }

    [MapTo("RdmLengthPaved")]
    public Decimal? LengthPaved { get; set; }

    [MapTo("RdmLengthUnpaved")]
    public Decimal? LengthUnpaved { get; set; }

    [MapTo("RdmOwner")]
    public string Owner { get; set; }

    [MapTo("RdmModBy")]
    public string ModBy { get; set; }

    [MapTo("RdmModDt")]
    public DateTime? ModDt { get; set; }

    [MapTo("RdmCrBy")]
    public string CrBy { get; set; }

    [MapTo("RdmCrDt")]
    public DateTime? CrDt { get; set; }

    [MapTo("RdmActiveYn")]
    public bool? ActiveYn { get; set; }

    [MapTo("RdmSecCode")]
    public int? SecCode { get; set; }

    [MapTo("RdmRmuName")]
    public string RmuName { get; set; }

    [MapTo("RdmRdCdSort")]
    public double? RdCdSort { get; set; }
  }
}
