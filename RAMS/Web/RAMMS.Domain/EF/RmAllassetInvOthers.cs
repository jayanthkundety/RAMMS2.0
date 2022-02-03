using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmAllassetInvOthers
    {
        public int AioPkRefNo { get; set; }
        public int? AioAiPkRefNo { get; set; }
        public string AioAssetId { get; set; }
        public string AioAssetGrpCode { get; set; }
        public string AioStrucCodeOthers { get; set; }
        public string AioMaterialOthers { get; set; }
        public string AioAbutFoundOthers { get; set; }
        public string AioPiersPrimCompOthers { get; set; }
        public string AioBearingSeatDiaphgOthers { get; set; }
        public string AioBeamsGridTrusArchOthers { get; set; }
        public string AioDeckPavementOthers { get; set; }
        public string AioUtilitiesOthers { get; set; }
        public string AioWaterwayOthers { get; set; }
        public string AioWaterDownpipeOthers { get; set; }
        public string AioParapetRaiolingOthers { get; set; }
        public string AioSidewalksAppSlabOthers { get; set; }
        public string AioExpanJointOthers { get; set; }
        public string AioSlopeRetAionWallOthers { get; set; }
        public string AioModBy { get; set; }
        public DateTime? AioModDt { get; set; }
        public string AioCrBy { get; set; }
        public DateTime? AioCrDt { get; set; }
        public bool AioSubmitSts { get; set; }
        public bool? AioActiveYn { get; set; }
        public string AioGrpTypeOthers { get; set; }
        public string AioCulvertTypeOthers { get; set; }

        public virtual RmAllassetInventory AioAiPkRefNoNavigation { get; set; }
    }
}
