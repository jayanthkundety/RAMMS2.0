using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class AssetInvOtherResDTO
    {
        [MapTo("AioPkRefNo")]
        public int RefNo { get; set; }

        [MapTo("AioAiPkRefNo")]
        public int? PkRefNo { get; set; }

        [MapTo("AioAssetId")]
        public string AssetId { get; set; }

        [MapTo("AioAssetGrpCode")]
        public string AssetGrpCode { get; set; }

        [MapTo("AioStrucCodeOthers")]
        public string StrucCodeOthers { get; set; }

        [MapTo("AioMaterialOthers")]
        public string MaterialOthers { get; set; }

        [MapTo("AioAbutFoundOthers")]
        public string AbutFoundOthers { get; set; }

        [MapTo("AioPiersPrimCompOthers")]
        public string PiersPrimCompOthers { get; set; }

        [MapTo("AioBearingSeatDiaphgOthers")]
        public string BearingSeatDiaphgOthers { get; set; }

        [MapTo("AioBeamsGridTrusArchOthers")]
        public string BeamsGridTrusArchOthers { get; set; }

        [MapTo("AioDeckPavementOthers")]
        public string DeckPavementOthers { get; set; }

        [MapTo("AioUtilitiesOthers")]
        public string UtilitiesOthers { get; set; }

        [MapTo("AioWaterwayOthers")]
        public string WaterwayOthers { get; set; }

        [MapTo("AioWaterDownpipeOthers")]
        public string WaterDownpipeOthers { get; set; }

        [MapTo("AioParapetRaiolingOthers")]
        public string ParapetRaiolingOthers { get; set; }

        [MapTo("AioSidewalksAppSlabOthers")]
        public string SidewalksAppSlabOthers { get; set; }

        [MapTo("AioExpanJointOthers")]
        public string ExpanJointOthers { get; set; }

        [MapTo("AioSlopeRetAionWallOthers")]
        public string SlopeRetAionWallOthers { get; set; }

        [MapTo("AioGrpTypeOthers")]
        public string GrpTypeOthers { get; set; }

        [MapTo("AioCulvertTypeOthers")]
        public string CulvertTypeOthers { get; set; }

    }
}
