using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RAMMS.Domain.Models
{
    public partial class RAMMS_DevelopmentContext : DbContext
    {

        public RAMMS_DevelopmentContext()
        {
        }

        public virtual DbSet<AssetFieldDtl> AssetFieldDtl { get; set; }
        public virtual DbSet<AssetImport> AssetImport { get; set; }
        public virtual DbSet<ImportAssetUse> ImportAssetUse { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<LogDebugger> LogDebugger { get; set; }
        public virtual DbSet<RmAccUcuImageDtl> RmAccUcuImageDtl { get; set; }
        public virtual DbSet<RmAllassetInvOthers> RmAllassetInvOthers { get; set; }
        public virtual DbSet<RmAllassetInvOthers20210122> RmAllassetInvOthers20210122 { get; set; }
        public virtual DbSet<RmAllassetInvOthersUat> RmAllassetInvOthersUat { get; set; }
        public virtual DbSet<RmAllassetInventory> RmAllassetInventory { get; set; }
        public virtual DbSet<RmAllassetInventory20210122> RmAllassetInventory20210122 { get; set; }
        public virtual DbSet<RmAllassetInventoryUat> RmAllassetInventoryUat { get; set; }
        public virtual DbSet<RmAllassetInventoryUatBackup> RmAllassetInventoryUatBackup { get; set; }
        public virtual DbSet<RmAssetDefectCode> RmAssetDefectCode { get; set; }
        public virtual DbSet<RmAssetGroupType> RmAssetGroupType { get; set; }
        public virtual DbSet<RmAssetImageDtl> RmAssetImageDtl { get; set; }
        public virtual DbSet<RmAssetImageDtl20210122> RmAssetImageDtl20210122 { get; set; }
        public virtual DbSet<RmAuditLogAction> RmAuditLogAction { get; set; }
        public virtual DbSet<RmAuditLogTransaction> RmAuditLogTransaction { get; set; }
        public virtual DbSet<RmDdLookup> RmDdLookup { get; set; }
        public virtual DbSet<RmDepartment> RmDepartment { get; set; }
        public virtual DbSet<RmDivRmuSecMaster> RmDivRmuSecMaster { get; set; }
        public virtual DbSet<RmDivisionMaster> RmDivisionMaster { get; set; }
        public virtual DbSet<RmFieldDisRightsDtl> RmFieldDisRightsDtl { get; set; }
        public virtual DbSet<RmFormADtl> RmFormADtl { get; set; }
        public virtual DbSet<RmFormAHdr> RmFormAHdr { get; set; }
        public virtual DbSet<RmFormASiterefDtl> RmFormASiterefDtl { get; set; }
        public virtual DbSet<RmFormB1b2BrInsDtl> RmFormB1b2BrInsDtl { get; set; }
        public virtual DbSet<RmFormB1b2BrInsHdr> RmFormB1b2BrInsHdr { get; set; }
        public virtual DbSet<RmFormB1b2BrInsImage> RmFormB1b2BrInsImage { get; set; }
        public virtual DbSet<RmFormCvInsDtl> RmFormCvInsDtl { get; set; }
        public virtual DbSet<RmFormCvInsHdr> RmFormCvInsHdr { get; set; }
        public virtual DbSet<RmFormCvInsImage> RmFormCvInsImage { get; set; }
        public virtual DbSet<RmFormDDtl> RmFormDDtl { get; set; }
        public virtual DbSet<RmFormDEquipDtl> RmFormDEquipDtl { get; set; }
        public virtual DbSet<RmFormDHdr> RmFormDHdr { get; set; }
        public virtual DbSet<RmFormDLabourDtl> RmFormDLabourDtl { get; set; }
        public virtual DbSet<RmFormDMaterialDtl> RmFormDMaterialDtl { get; set; }
        public virtual DbSet<RmFormDownloadTbJoin> RmFormDownloadTbJoin { get; set; }
        public virtual DbSet<RmFormDownloadUse> RmFormDownloadUse { get; set; }
        public virtual DbSet<RmFormDownloadUse21122020> RmFormDownloadUse21122020 { get; set; }
        public virtual DbSet<RmFormDownloadUse28122020> RmFormDownloadUse28122020 { get; set; }
        public virtual DbSet<RmFormDownloadUseFormABak> RmFormDownloadUseFormABak { get; set; }
        public virtual DbSet<RmFormDownloadUseFormD> RmFormDownloadUseFormD { get; set; }
        public virtual DbSet<RmFormF2GrInsDtl> RmFormF2GrInsDtl { get; set; }
        public virtual DbSet<RmFormF2GrInsHdr> RmFormF2GrInsHdr { get; set; }
        public virtual DbSet<RmFormF4InsDtl> RmFormF4InsDtl { get; set; }
        public virtual DbSet<RmFormF4InsHdr> RmFormF4InsHdr { get; set; }
        public virtual DbSet<RmFormF5InsDtl> RmFormF5InsDtl { get; set; }
        public virtual DbSet<RmFormF5InsHdr> RmFormF5InsHdr { get; set; }
        public virtual DbSet<RmFormFcInsDtl> RmFormFcInsDtl { get; set; }
        public virtual DbSet<RmFormFcInsHdr> RmFormFcInsHdr { get; set; }
        public virtual DbSet<RmFormFdInsDtl> RmFormFdInsDtl { get; set; }
        public virtual DbSet<RmFormFdInsHdr> RmFormFdInsHdr { get; set; }
        public virtual DbSet<RmFormFsInsDtl> RmFormFsInsDtl { get; set; }
        public virtual DbSet<RmFormFsInsHdr> RmFormFsInsHdr { get; set; }
        public virtual DbSet<RmFormGenDtl> RmFormGenDtl { get; set; }
        public virtual DbSet<RmFormHHdr> RmFormHHdr { get; set; }
        public virtual DbSet<RmFormJDtl> RmFormJDtl { get; set; }
        public virtual DbSet<RmFormJHdr> RmFormJHdr { get; set; }
        public virtual DbSet<RmFormN1Hdr> RmFormN1Hdr { get; set; }
        public virtual DbSet<RmFormN2Hdr> RmFormN2Hdr { get; set; }
        public virtual DbSet<RmFormQa1EqVh> RmFormQa1EqVh { get; set; }
        public virtual DbSet<RmFormQa1Gc> RmFormQa1Gc { get; set; }
        public virtual DbSet<RmFormQa1Gen> RmFormQa1Gen { get; set; }
        public virtual DbSet<RmFormQa1Hdr> RmFormQa1Hdr { get; set; }
        public virtual DbSet<RmFormQa1Lab> RmFormQa1Lab { get; set; }
        public virtual DbSet<RmFormQa1Mat> RmFormQa1Mat { get; set; }
        public virtual DbSet<RmFormQa1Ssc> RmFormQa1Ssc { get; set; }
        public virtual DbSet<RmFormQa1Tes> RmFormQa1Tes { get; set; }
        public virtual DbSet<RmFormQa1Wcq> RmFormQa1Wcq { get; set; }
        public virtual DbSet<RmFormQa1We> RmFormQa1We { get; set; }
        public virtual DbSet<RmFormQa2Dtl> RmFormQa2Dtl { get; set; }
        public virtual DbSet<RmFormQa2Hdr> RmFormQa2Hdr { get; set; }
        public virtual DbSet<RmFormS1Dtl> RmFormS1Dtl { get; set; }
        public virtual DbSet<RmFormS1Hdr> RmFormS1Hdr { get; set; }
        public virtual DbSet<RmFormS1WkDtl> RmFormS1WkDtl { get; set; }
        public virtual DbSet<RmFormS2Dtl> RmFormS2Dtl { get; set; }
        public virtual DbSet<RmFormS2Hdr> RmFormS2Hdr { get; set; }
        public virtual DbSet<RmFormS2QuarDtl> RmFormS2QuarDtl { get; set; }
        public virtual DbSet<RmFormV1Dtl> RmFormV1Dtl { get; set; }
        public virtual DbSet<RmFormV1Hdr> RmFormV1Hdr { get; set; }
        public virtual DbSet<RmFormV2Eqp> RmFormV2Eqp { get; set; }
        public virtual DbSet<RmFormV2Hdr> RmFormV2Hdr { get; set; }
        public virtual DbSet<RmFormV2Lab> RmFormV2Lab { get; set; }
        public virtual DbSet<RmFormV2Mat> RmFormV2Mat { get; set; }
        public virtual DbSet<RmFormV3Dtl> RmFormV3Dtl { get; set; }
        public virtual DbSet<RmFormV3Hdr> RmFormV3Hdr { get; set; }
        public virtual DbSet<RmFormV4Hdr> RmFormV4Hdr { get; set; }
        public virtual DbSet<RmFormV5Dtl> RmFormV5Dtl { get; set; }
        public virtual DbSet<RmFormV5Hdr> RmFormV5Hdr { get; set; }
        public virtual DbSet<RmFormXHdr> RmFormXHdr { get; set; }
        public virtual DbSet<RmFormaImageDtl> RmFormaImageDtl { get; set; }
        public virtual DbSet<RmFormhImageDtl> RmFormhImageDtl { get; set; }
        public virtual DbSet<RmFormjImageDtl> RmFormjImageDtl { get; set; }
        public virtual DbSet<RmGroup> RmGroup { get; set; }
        public virtual DbSet<RmGroupUser> RmGroupUser { get; set; }
        public virtual DbSet<RmInspItemMas> RmInspItemMas { get; set; }
        public virtual DbSet<RmInspItemMasDtl> RmInspItemMasDtl { get; set; }
        public virtual DbSet<RmIwFormW1> RmIwFormW1 { get; set; }
        public virtual DbSet<RmIwFormW2> RmIwFormW2 { get; set; }
        public virtual DbSet<RmIwFormW2Fecm> RmIwFormW2Fecm { get; set; }
        public virtual DbSet<RmIwFormWc> RmIwFormWc { get; set; }
        public virtual DbSet<RmIwFormWd> RmIwFormWd { get; set; }
        public virtual DbSet<RmIwFormWdDtl> RmIwFormWdDtl { get; set; }
        public virtual DbSet<RmIwFormWg> RmIwFormWg { get; set; }
        public virtual DbSet<RmIwFormWn> RmIwFormWn { get; set; }
        public virtual DbSet<RmIwSrvProviderMaster> RmIwSrvProviderMaster { get; set; }
        public virtual DbSet<RmIwWorksDeptMaster> RmIwWorksDeptMaster { get; set; }
        public virtual DbSet<RmIwformImage> RmIwformImage { get; set; }
        public virtual DbSet<RmModule> RmModule { get; set; }
        public virtual DbSet<RmModuleForms> RmModuleForms { get; set; }
        public virtual DbSet<RmModuleGroupFieldRights> RmModuleGroupFieldRights { get; set; }
        public virtual DbSet<RmModuleGroupRights> RmModuleGroupRights { get; set; }
        public virtual DbSet<RmModuleRightByForm> RmModuleRightByForm { get; set; }
        public virtual DbSet<RmModuleRightsCode> RmModuleRightsCode { get; set; }
        public virtual DbSet<RmRmuMaster> RmRmuMaster { get; set; }
        public virtual DbSet<RmRoadMaster> RmRoadMaster { get; set; }
        public virtual DbSet<RmUserGroup> RmUserGroup { get; set; }
        public virtual DbSet<RmUserGroupRights> RmUserGroupRights { get; set; }
        public virtual DbSet<RmUserNotification> RmUserNotification { get; set; }
        public virtual DbSet<RmUsers> RmUsers { get; set; }
        public virtual DbSet<RmUvModuleGroupFieldRights> RmUvModuleGroupFieldRights { get; set; }
        public virtual DbSet<RmUvModuleGroupRights> RmUvModuleGroupRights { get; set; }
        public virtual DbSet<RmWarImageDtl> RmWarImageDtl { get; set; }
        public virtual DbSet<RmWeekLookup> RmWeekLookup { get; set; }
        public virtual DbSet<TestColumns> TestColumns { get; set; }
        public virtual DbSet<UvwSearchData> UvwSearchData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=RAMMSDatabase");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetFieldDtl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Asset_Field_DTL");

                entity.Property(e => e.AssetPkId)
                    .HasColumnName("Asset_PK_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AssetType)
                    .HasColumnName("Asset_Type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FieldName)
                    .HasColumnName("Field_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.HdrDisplayName)
                    .HasColumnName("HDR_Display_Name")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AssetImport>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Asset_import");

                entity.Property(e => e.AiAbutFound)
                    .HasColumnName("AI_Abut_Found")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiAbutType)
                    .HasColumnName("AI_Abut_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetGrpCode)
                    .HasColumnName("AI_Asset_Grp_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetId)
                    .HasColumnName("AI_Asset_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetIdActual)
                    .HasColumnName("AI_Asset_ID_actual")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetNumber)
                    .HasColumnName("AI_Asset_Number")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiBarrelNo)
                    .HasColumnName("AI_Barrel_No")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBeamsGridTrusArch)
                    .HasColumnName("AI_Beams_Grid_Trus_Arch")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBearingSeatDiaphg)
                    .HasColumnName("AI_Bearing_Seat_Diaphg")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBearingType)
                    .HasColumnName("AI_Bearing_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBotWidth)
                    .HasColumnName("AI_Bot_Width")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBound)
                    .HasColumnName("AI_Bound")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiBridgeName)
                    .HasColumnName("AI_Bridge_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBuiltYear)
                    .HasColumnName("AI_Built_year")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiCatchArea)
                    .HasColumnName("AI_Catch_Area")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiCulvertType)
                    .HasColumnName("AI_Culvert_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDeckPavement)
                    .HasColumnName("AI_Deck_pavement")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDeckType)
                    .HasColumnName("AI_Deck_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDesignFlow)
                    .HasColumnName("AI_Design_Flow")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDiameter)
                    .HasColumnName("AI_Diameter")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDist)
                    .HasColumnName("AI_Dist")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiDivCode)
                    .HasColumnName("AI_Div_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJoint)
                    .HasColumnName("AI_Expan_Joint")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJointCount)
                    .HasColumnName("AI_Expan_Joint_Count")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJointSpace)
                    .HasColumnName("AI_Expan_Joint_Space")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanType)
                    .HasColumnName("AI_Expan_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiFeatureId)
                    .HasColumnName("AI_Feature_ID")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiFileNo).HasColumnName("AI_File_No");

                entity.Property(e => e.AiFinRdLevel)
                    .HasColumnName("AI_Fin_Rd_Level")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiFrmCh)
                    .HasColumnName("AI_FRM_CH")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiFrmChDeci)
                    .HasColumnName("AI_FRM_CH_Deci")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiGpsEasting)
                    .HasColumnName("AI_GPS_Easting")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiGpsNorthing)
                    .HasColumnName("AI_GPS_Northing")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiGrpType)
                    .HasColumnName("AI_Grp_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiHasImage)
                    .HasColumnName("AI_Has_Image")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AiHeight)
                    .HasColumnName("AI_Height")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiImportErrorDesc)
                    .HasColumnName("AI_Import_Error_Desc")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.AiIntelLevel)
                    .HasColumnName("AI_Intel_Level")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiIntelStruc)
                    .HasColumnName("AI_Intel_Struc")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiLaneCnt)
                    .HasColumnName("AI_Lane_Cnt")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiLaneNo)
                    .HasColumnName("AI_Lane_No")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiLength)
                    .HasColumnName("AI_Length")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiLengthSpan)
                    .HasColumnName("AI_length_Span")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiLocChKm)
                    .HasColumnName("AI_Loc_CH_KM")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiLocChM)
                    .HasColumnName("AI_Loc_CH_M")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiMaintainedBy)
                    .HasColumnName("AI_maintained_By")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiMaterial)
                    .HasColumnName("AI_Material")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiMedian)
                    .HasColumnName("AI_median")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiOutletLevel)
                    .HasColumnName("AI_Outlet_Level")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiOutletStruc)
                    .HasColumnName("AI_Outlet_Struc")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiOwner)
                    .HasColumnName("AI_Owner")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiParapetRailing)
                    .HasColumnName("AI_Parapet_Railing")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiParapetType)
                    .HasColumnName("AI_Parapet_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPierType)
                    .HasColumnName("AI_Pier_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPiersPrimComp)
                    .HasColumnName("AI_Piers_Prim_Comp")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPostSpacing)
                    .HasColumnName("AI_Post_Spacing")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPrecastSitu)
                    .HasColumnName("AI_Precast_Situ")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdCode)
                    .HasColumnName("AI_Rd_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdName)
                    .HasColumnName("AI_Rd_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdmPkRefNo).HasColumnName("AI_RDM_PK_Ref_No");

                entity.Property(e => e.AiRefNo)
                    .HasColumnName("AI_Ref_No")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRiverName)
                    .HasColumnName("AI_River_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRmuCode)
                    .HasColumnName("AI_RMU_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiRmuName)
                    .HasColumnName("AI_RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiS8)
                    .HasColumnName("AI_S8")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AiSecCode)
                    .HasColumnName("AI_Sec_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiSecName)
                    .HasColumnName("AI_Sec_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSidewalksAppSlab)
                    .HasColumnName("AI_Sidewalks_App_Slab")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSkew)
                    .HasColumnName("AI_Skew")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSlopeRetainWall)
                    .HasColumnName("AI_Slope_Retain_wall")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSpanCnt)
                    .HasColumnName("AI_Span_Cnt")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSrno).HasColumnName("AI_SRNO");

                entity.Property(e => e.AiStrucCode)
                    .HasColumnName("AI_Struc_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiStrucSuper)
                    .HasColumnName("AI_Struc_Super")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiTier)
                    .HasColumnName("AI_Tier")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiToCh)
                    .HasColumnName("AI_To_CH")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiToChDeci)
                    .HasColumnName("AI_To_CH_Deci")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiUtilities)
                    .HasColumnName("AI_Utilities")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWalkway)
                    .HasColumnName("AI_Walkway")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWaterDownpipe)
                    .HasColumnName("AI_Water_Downpipe")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWaterway)
                    .HasColumnName("AI_Waterway")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWidth)
                    .HasColumnName("AI_Width")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWidthLane)
                    .HasColumnName("AI_Width_Lane")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioAbutFoundOthers)
                    .HasColumnName("AIO_Abut_Found_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioAssetGrpCode)
                    .HasColumnName("AIO_Asset_GRP_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioAssetId)
                    .HasColumnName("AIO_Asset_ID")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioBeamsGridTrusArchOthers)
                    .HasColumnName("AIO_Beams_Grid_Trus_Arch_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioBearingSeatDiaphgOthers)
                    .HasColumnName("AIO_Bearing_Seat_Diaphg_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioCulvertTypeOthers)
                    .HasColumnName("AIO_Culvert_Type_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioDeckPavementOthers)
                    .HasColumnName("AIO_Deck_pavement_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioExpanJointOthers)
                    .HasColumnName("AIO_Expan_Joint_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioGrpTypeOthers)
                    .HasColumnName("AIO_Grp_type_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioMaterialOthers)
                    .HasColumnName("AIO_Material_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioParapetRaiolingOthers)
                    .HasColumnName("AIO_Parapet_RAIOling_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioPiersPrimCompOthers)
                    .HasColumnName("AIO_Piers_Prim_Comp_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioSidewalksAppSlabOthers)
                    .HasColumnName("AIO_Sidewalks_App_Slab_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioSlopeRetAionWallOthers)
                    .HasColumnName("AIO_Slope_RetAIOn_wall_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioStrucCodeOthers)
                    .HasColumnName("AIO_Struc_Code_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioUtilitiesOthers)
                    .HasColumnName("AIO_Utilities_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioWaterDownpipeOthers)
                    .HasColumnName("AIO_Water_Downpipe_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioWaterwayOthers)
                    .HasColumnName("AIO_Waterway_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AssetImgPath)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RmAllassetInvOthers)
                    .HasColumnName("RM_Allasset_inv_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImportAssetUse>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Import_Asset_use");

                entity.Property(e => e.AiAbutFound)
                    .HasColumnName("AI_Abut_Found")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiAbutType)
                    .HasColumnName("AI_Abut_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetGrpCode)
                    .HasColumnName("AI_Asset_GRP_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetId)
                    .HasColumnName("AI_Asset_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiBarrelNo)
                    .HasColumnName("AI_Barrel_No")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBeamsGridTrusArch)
                    .HasColumnName("AI_Beams_Grid_Trus_Arch")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBearingSeatDiaphg)
                    .HasColumnName("AI_Bearing_Seat_Diaphg")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBearingType)
                    .HasColumnName("AI_Bearing_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBotWidth)
                    .HasColumnName("AI_Bot_Width")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBound)
                    .HasColumnName("AI_Bound")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiBridgeName)
                    .HasColumnName("AI_Bridge_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBuiltYear)
                    .HasColumnName("AI_Built_year")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiCatchArea)
                    .HasColumnName("AI_Catch_Area")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDeckPavement)
                    .HasColumnName("AI_Deck_pavement")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDeckType)
                    .HasColumnName("AI_Deck_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDesignFlow)
                    .HasColumnName("AI_Design_Flow")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDiameter)
                    .HasColumnName("AI_Diameter")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDist)
                    .HasColumnName("AI_Dist")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiDivCode)
                    .HasColumnName("AI_Div_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJoint)
                    .HasColumnName("AI_Expan_Joint")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJointCount)
                    .HasColumnName("AI_Expan_Joint_Count")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJointSpace)
                    .HasColumnName("AI_Expan_Joint_Space")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanType)
                    .HasColumnName("AI_Expan_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiFeatureId)
                    .HasColumnName("AI_Feature_ID")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiFinRdLevel)
                    .HasColumnName("AI_Fin_Rd_Level")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiFrmCh)
                    .HasColumnName("AI_FRM_CH")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiFrmChDeci)
                    .HasColumnName("AI_FRM_CH_Deci")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiGpsEasting)
                    .HasColumnName("AI_GPS_Easting")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.AiGpsNorthing)
                    .HasColumnName("AI_GPS_Northing")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.AiGrpType)
                    .HasColumnName("AI_Grp_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiHasImage)
                    .HasColumnName("AI_Has_Image")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AiHeight)
                    .HasColumnName("AI_Height")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiIntelLevel)
                    .HasColumnName("AI_Intel_Level")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiIntelStruc)
                    .HasColumnName("AI_Intel_Struc")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiLaneCnt)
                    .HasColumnName("AI_Lane_Cnt")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiLaneNo)
                    .HasColumnName("AI_Lane_No")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiLength)
                    .HasColumnName("AI_Length")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiLengthSpan)
                    .HasColumnName("AI_length_Span")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiLocChKm)
                    .HasColumnName("AI_Loc_CH_KM")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiLocChM)
                    .HasColumnName("AI_Loc_CH_M")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiMaintainedBy)
                    .HasColumnName("AI_maintained_By")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiMaterial)
                    .HasColumnName("AI_Material")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiMedian)
                    .HasColumnName("AI_median")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiOutletLevel)
                    .HasColumnName("AI_Outlet_Level")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiOutletStruc)
                    .HasColumnName("AI_Outlet_Struc")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiOwner)
                    .HasColumnName("AI_Owner")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiParapetRailing)
                    .HasColumnName("AI_Parapet_Railing")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiParapetType)
                    .HasColumnName("AI_Parapet_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPierType)
                    .HasColumnName("AI_Pier_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPiersPrimComp)
                    .HasColumnName("AI_Piers_Prim_Comp")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPostSpacing)
                    .HasColumnName("AI_Post_Spacing")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPrecastSitu)
                    .HasColumnName("AI_Precast_Situ")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdCode)
                    .HasColumnName("AI_Rd_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdName)
                    .HasColumnName("AI_Rd_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRefNo)
                    .HasColumnName("AI_Ref_No")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRiverName)
                    .HasColumnName("AI_River_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRmuCode)
                    .HasColumnName("AI_RMU_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiSecCode)
                    .HasColumnName("AI_Sec_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiSecName)
                    .HasColumnName("AI_Sec_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSidewalksAppSlab)
                    .HasColumnName("AI_Sidewalks_App_Slab")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSkew)
                    .HasColumnName("AI_Skew")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSlopeRetainWall)
                    .HasColumnName("AI_Slope_Retain_wall")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSpanCnt)
                    .HasColumnName("AI_Span_Cnt")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiStrucCode)
                    .HasColumnName("AI_Struc_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiStrucSuper)
                    .HasColumnName("AI_Struc_Super")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiTier)
                    .HasColumnName("AI_Tier")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiToCh)
                    .HasColumnName("AI_To_CH")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiToChDeci)
                    .HasColumnName("AI_To_CH_Deci")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiUtilities)
                    .HasColumnName("AI_Utilities")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWalkway)
                    .HasColumnName("AI_Walkway")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWaterDownpipe)
                    .HasColumnName("AI_Water_Downpipe")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWaterway)
                    .HasColumnName("AI_Waterway")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWidth)
                    .HasColumnName("AI_Width")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWidthLane)
                    .HasColumnName("AI_Width_Lane")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioAbutFoundOthers)
                    .HasColumnName("AIO_Abut_Found_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioAssetGrpCode)
                    .HasColumnName("AIO_Asset_GRP_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioAssetId)
                    .HasColumnName("AIO_Asset_ID")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioBeamsGridTrusArchOthers)
                    .HasColumnName("AIO_Beams_Grid_Trus_Arch_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioBearingSeatDiaphgOthers)
                    .HasColumnName("AIO_Bearing_Seat_Diaphg_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioDeckPavementOthers)
                    .HasColumnName("AIO_Deck_pavement_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioExpanJointOthers)
                    .HasColumnName("AIO_Expan_Joint_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioMaterialOthers)
                    .HasColumnName("AIO_Material_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioParapetRaiolingOthers)
                    .HasColumnName("AIO_Parapet_RAIOling_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioPiersPrimCompOthers)
                    .HasColumnName("AIO_Piers_Prim_Comp_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioSidewalksAppSlabOthers)
                    .HasColumnName("AIO_Sidewalks_App_Slab_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioSlopeRetAionWallOthers)
                    .HasColumnName("AIO_Slope_RetAIOn_wall_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioStrucCodeOthers)
                    .HasColumnName("AIO_Struc_Code_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioUtilitiesOthers)
                    .HasColumnName("AIO_Utilities_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioWaterDownpipeOthers)
                    .HasColumnName("AIO_Water_Downpipe_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioWaterwayOthers)
                    .HasColumnName("AIO_Waterway_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RmAllassetInvOthers)
                    .HasColumnName("RM_Allasset_inv_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.Level).HasMaxLength(128);

                entity.Property(e => e.Properties).HasColumnType("xml");
            });

            modelBuilder.Entity<LogDebugger>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Log_Debugger");

                entity.Property(e => e.LogContent)
                    .HasColumnName("log_content")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmAccUcuImageDtl>(entity =>
            {
                entity.HasKey(e => e.FauPkRefNo)
                    .HasName("PK__RM_ACC_U__AC8746FAE21542DA");

                entity.ToTable("RM_ACC_UCU_image_DTL");

                entity.Property(e => e.FauPkRefNo).HasColumnName("FAU_PK_Ref_No");

                entity.Property(e => e.FauAccUcu)
                    .HasColumnName("FAU_ACC_UCU")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FauActiveYn).HasColumnName("FAU_Active_YN");

                entity.Property(e => e.FauCrBy)
                    .HasColumnName("FAU_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FauCrDt)
                    .HasColumnName("FAU_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FauFddPkRefNo).HasColumnName("FAU_FDD_PK_Ref_No");

                entity.Property(e => e.FauFxhPkRefNo).HasColumnName("FAU_FXH_PK_Ref_No");

                entity.Property(e => e.FauImageFilenameSys)
                    .HasColumnName("FAU_Image_Filename_Sys")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FauImageFilenameUpload)
                    .HasColumnName("FAU_Image_Filename_Upload")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FauImageSrno).HasColumnName("FAU_Image_SRNO");

                entity.Property(e => e.FauImageUserFilename)
                    .HasColumnName("FAU_image_user_filename")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FauModBy)
                    .HasColumnName("FAU_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FauModDt)
                    .HasColumnName("FAU_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FauSubmitSts).HasColumnName("FAU_SUBMIT_STS");

                entity.HasOne(d => d.FauFddPkRefNoNavigation)
                    .WithMany(p => p.RmAccUcuImageDtl)
                    .HasForeignKey(d => d.FauFddPkRefNo)
                    .HasConstraintName("FK_RM_ACC_UCU_image_DTL_RM_FormD_DTL");

                entity.HasOne(d => d.FauFxhPkRefNoNavigation)
                    .WithMany(p => p.RmAccUcuImageDtl)
                    .HasForeignKey(d => d.FauFxhPkRefNo)
                    .HasConstraintName("FK__RM_ACC_UC__FAU_F__7DCDAAA2");
            });

            modelBuilder.Entity<RmAllassetInvOthers>(entity =>
            {
                entity.HasKey(e => e.AioPkRefNo)
                    .HasName("PK__RM_Allas__638388FCE25A8099");

                entity.ToTable("RM_Allasset_inv_Others");

                entity.Property(e => e.AioPkRefNo).HasColumnName("AIO_PK_Ref_No");

                entity.Property(e => e.AioAbutFoundOthers)
                    .HasColumnName("AIO_Abut_Found_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioActiveYn)
                    .IsRequired()
                    .HasColumnName("AIO_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AioAiPkRefNo).HasColumnName("AIO_AI_PK_Ref_No");

                entity.Property(e => e.AioAssetGrpCode)
                    .HasColumnName("AIO_Asset_GRP_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AioAssetId)
                    .HasColumnName("AIO_Asset_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AioBeamsGridTrusArchOthers)
                    .HasColumnName("AIO_Beams_Grid_Trus_Arch_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioBearingSeatDiaphgOthers)
                    .HasColumnName("AIO_Bearing_Seat_Diaphg_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioCrBy)
                    .HasColumnName("AIO_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AioCrDt)
                    .HasColumnName("AIO_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AioCulvertTypeOthers)
                    .HasColumnName("AIO_Culvert_Type_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioDeckPavementOthers)
                    .HasColumnName("AIO_Deck_pavement_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioExpanJointOthers)
                    .HasColumnName("AIO_Expan_Joint_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioGrpTypeOthers)
                    .HasColumnName("AIO_Grp_type_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioMaterialOthers)
                    .HasColumnName("AIO_Material_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioModBy)
                    .HasColumnName("AIO_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AioModDt)
                    .HasColumnName("AIO_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AioParapetRaiolingOthers)
                    .HasColumnName("AIO_Parapet_RAIOling_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioPiersPrimCompOthers)
                    .HasColumnName("AIO_Piers_Prim_Comp_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioSidewalksAppSlabOthers)
                    .HasColumnName("AIO_Sidewalks_App_Slab_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioSlopeRetAionWallOthers)
                    .HasColumnName("AIO_Slope_RetAIOn_wall_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioStrucCodeOthers)
                    .HasColumnName("AIO_Struc_Code_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioSubmitSts).HasColumnName("AIO_SUBMIT_STS");

                entity.Property(e => e.AioUtilitiesOthers)
                    .HasColumnName("AIO_Utilities_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioWaterDownpipeOthers)
                    .HasColumnName("AIO_Water_Downpipe_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioWaterwayOthers)
                    .HasColumnName("AIO_Waterway_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.AioAiPkRefNoNavigation)
                    .WithMany(p => p.RmAllassetInvOthers)
                    .HasForeignKey(d => d.AioAiPkRefNo)
                    .HasConstraintName("FK__RM_Allass__AIO_A__2FCF1A8A");
            });

            modelBuilder.Entity<RmAllassetInvOthers20210122>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RM_Allasset_inv_Others_20210122");

                entity.Property(e => e.AioAbutFoundOthers)
                    .HasColumnName("AIO_Abut_Found_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioActiveYn).HasColumnName("AIO_Active_YN");

                entity.Property(e => e.AioAiPkRefNo).HasColumnName("AIO_AI_PK_Ref_No");

                entity.Property(e => e.AioAssetGrpCode)
                    .HasColumnName("AIO_Asset_GRP_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AioAssetId)
                    .HasColumnName("AIO_Asset_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AioBeamsGridTrusArchOthers)
                    .HasColumnName("AIO_Beams_Grid_Trus_Arch_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioBearingSeatDiaphgOthers)
                    .HasColumnName("AIO_Bearing_Seat_Diaphg_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioCrBy)
                    .HasColumnName("AIO_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AioCrDt)
                    .HasColumnName("AIO_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AioDeckPavementOthers)
                    .HasColumnName("AIO_Deck_pavement_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioExpanJointOthers)
                    .HasColumnName("AIO_Expan_Joint_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioGrpTypeOthers)
                    .HasColumnName("AIO_Grp_type_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioMaterialOthers)
                    .HasColumnName("AIO_Material_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioModBy)
                    .HasColumnName("AIO_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AioModDt)
                    .HasColumnName("AIO_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AioParapetRaiolingOthers)
                    .HasColumnName("AIO_Parapet_RAIOling_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioPiersPrimCompOthers)
                    .HasColumnName("AIO_Piers_Prim_Comp_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioPkRefNo)
                    .HasColumnName("AIO_PK_Ref_No")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AioSidewalksAppSlabOthers)
                    .HasColumnName("AIO_Sidewalks_App_Slab_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioSlopeRetAionWallOthers)
                    .HasColumnName("AIO_Slope_RetAIOn_wall_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioStrucCodeOthers)
                    .HasColumnName("AIO_Struc_Code_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioSubmitSts).HasColumnName("AIO_SUBMIT_STS");

                entity.Property(e => e.AioUtilitiesOthers)
                    .HasColumnName("AIO_Utilities_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioWaterDownpipeOthers)
                    .HasColumnName("AIO_Water_Downpipe_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioWaterwayOthers)
                    .HasColumnName("AIO_Waterway_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmAllassetInvOthersUat>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RM_Allasset_inv_Others_UAT");

                entity.Property(e => e.AioAbutFoundOthers)
                    .HasColumnName("AIO_Abut_Found_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioActiveYn).HasColumnName("AIO_Active_YN");

                entity.Property(e => e.AioAiPkRefNo).HasColumnName("AIO_AI_PK_Ref_No");

                entity.Property(e => e.AioAssetGrpCode)
                    .HasColumnName("AIO_Asset_GRP_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AioAssetId)
                    .HasColumnName("AIO_Asset_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AioBeamsGridTrusArchOthers)
                    .HasColumnName("AIO_Beams_Grid_Trus_Arch_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioBearingSeatDiaphgOthers)
                    .HasColumnName("AIO_Bearing_Seat_Diaphg_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioCrBy)
                    .HasColumnName("AIO_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AioCrDt)
                    .HasColumnName("AIO_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AioDeckPavementOthers)
                    .HasColumnName("AIO_Deck_pavement_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioExpanJointOthers)
                    .HasColumnName("AIO_Expan_Joint_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioGrpTypeOthers)
                    .HasColumnName("AIO_Grp_type_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioMaterialOthers)
                    .HasColumnName("AIO_Material_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioModBy)
                    .HasColumnName("AIO_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AioModDt)
                    .HasColumnName("AIO_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AioParapetRaiolingOthers)
                    .HasColumnName("AIO_Parapet_RAIOling_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioPiersPrimCompOthers)
                    .HasColumnName("AIO_Piers_Prim_Comp_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioSidewalksAppSlabOthers)
                    .HasColumnName("AIO_Sidewalks_App_Slab_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioSlopeRetAionWallOthers)
                    .HasColumnName("AIO_Slope_RetAIOn_wall_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioStrucCodeOthers)
                    .HasColumnName("AIO_Struc_Code_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioSubmitSts).HasColumnName("AIO_SUBMIT_STS");

                entity.Property(e => e.AioUtilitiesOthers)
                    .HasColumnName("AIO_Utilities_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioWaterDownpipeOthers)
                    .HasColumnName("AIO_Water_Downpipe_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AioWaterwayOthers)
                    .HasColumnName("AIO_Waterway_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmAllassetInventory>(entity =>
            {
                entity.HasKey(e => e.AiPkRefNo)
                    .HasName("PK__RM_Allas__76B9B0FCFA16C4CF");

                entity.ToTable("RM_Allasset_inventory");

                entity.Property(e => e.AiPkRefNo).HasColumnName("AI_PK_Ref_No");

                entity.Property(e => e.AiAbutFound)
                    .HasColumnName("AI_Abut_Found")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiAbutType)
                    .HasColumnName("AI_Abut_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiActiveYn)
                    .IsRequired()
                    .HasColumnName("AI_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AiAssetGrpCode)
                    .HasColumnName("AI_Asset_GRP_Code")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetId)
                    .HasColumnName("AI_Asset_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetNumber).HasColumnName("AI_Asset_Number");

                entity.Property(e => e.AiBarrelNo).HasColumnName("AI_Barrel_No");

                entity.Property(e => e.AiBeamsGridTrusArch)
                    .HasColumnName("AI_Beams_Grid_Trus_Arch")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBearingSeatDiaphg)
                    .HasColumnName("AI_Bearing_Seat_Diaphg")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBearingType)
                    .HasColumnName("AI_Bearing_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBotWidth).HasColumnName("AI_Bot_Width");

                entity.Property(e => e.AiBound)
                    .HasColumnName("AI_Bound")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiBridgeName)
                    .HasColumnName("AI_Bridge_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBuiltYear).HasColumnName("AI_Built_year");

                entity.Property(e => e.AiCatchArea).HasColumnName("AI_Catch_Area");

                entity.Property(e => e.AiCrBy)
                    .HasColumnName("AI_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiCrDt)
                    .HasColumnName("AI_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AiCulvertType)
                    .HasColumnName("AI_Culvert_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDeckPavement)
                    .HasColumnName("AI_Deck_pavement")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDeckType)
                    .HasColumnName("AI_Deck_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDesignFlow).HasColumnName("AI_Design_Flow");

                entity.Property(e => e.AiDiameter).HasColumnName("AI_Diameter");

                entity.Property(e => e.AiDist)
                    .HasColumnName("AI_Dist")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDivCode)
                    .HasColumnName("AI_Div_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJoint)
                    .HasColumnName("AI_Expan_Joint")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJointCount).HasColumnName("AI_Expan_Joint_Count");

                entity.Property(e => e.AiExpanJointSpace).HasColumnName("AI_Expan_Joint_Space");

                entity.Property(e => e.AiExpanType)
                    .HasColumnName("AI_Expan_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiFeatureId)
                    .HasColumnName("AI_Feature_ID")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiFinRdLevel).HasColumnName("AI_Fin_Rd_Level");

                entity.Property(e => e.AiFrmCh).HasColumnName("AI_FRM_CH");

                entity.Property(e => e.AiFrmChDeci)
                    .HasColumnName("AI_FRM_CH_Deci")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.AiGpsEasting).HasColumnName("AI_GPS_Easting");

                entity.Property(e => e.AiGpsNorthing).HasColumnName("AI_GPS_Northing");

                entity.Property(e => e.AiGrpType)
                    .HasColumnName("AI_Grp_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiHeight).HasColumnName("AI_Height");

                entity.Property(e => e.AiIntelLevel).HasColumnName("AI_Intel_Level");

                entity.Property(e => e.AiIntelStruc)
                    .HasColumnName("AI_Intel_Struc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiLaneCnt).HasColumnName("AI_Lane_Cnt");

                entity.Property(e => e.AiLaneNo)
                    .HasColumnName("AI_Lane_No")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiLength).HasColumnName("AI_Length");

                entity.Property(e => e.AiLengthSpan).HasColumnName("AI_length_Span");

                entity.Property(e => e.AiLocChKm).HasColumnName("AI_Loc_CH_KM");

                entity.Property(e => e.AiLocChM)
                    .HasColumnName("AI_Loc_CH_M")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.AiMaintainedBy)
                    .HasColumnName("AI_maintained_By")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiMaterial)
                    .HasColumnName("AI_Material")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiMedian).HasColumnName("AI_median");

                entity.Property(e => e.AiModBy)
                    .HasColumnName("AI_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiModDt)
                    .HasColumnName("AI_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AiOutletLevel).HasColumnName("AI_Outlet_Level");

                entity.Property(e => e.AiOutletStruc)
                    .HasColumnName("AI_Outlet_Struc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiOwner)
                    .HasColumnName("AI_Owner")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiParapetRailing)
                    .HasColumnName("AI_Parapet_Railing")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiParapetType)
                    .HasColumnName("AI_Parapet_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPierType)
                    .HasColumnName("AI_Pier_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPiersPrimComp)
                    .HasColumnName("AI_Piers_Prim_Comp")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPostSpacing).HasColumnName("AI_Post_Spacing");

                entity.Property(e => e.AiPrecastSitu)
                    .HasColumnName("AI_Precast_Situ")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdCode)
                    .HasColumnName("AI_Rd_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdName)
                    .HasColumnName("AI_Rd_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdmPkRefNo).HasColumnName("AI_RDM_PK_Ref_No");

                entity.Property(e => e.AiRefNo)
                    .HasColumnName("AI_Ref_No")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRiverName)
                    .HasColumnName("AI_River_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRmuCode)
                    .IsRequired()
                    .HasColumnName("AI_RMU_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiRmuName)
                    .HasColumnName("AI_RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiSecCode)
                    .IsRequired()
                    .HasColumnName("AI_Sec_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiSecName)
                    .HasColumnName("AI_Sec_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSidewalksAppSlab)
                    .HasColumnName("AI_Sidewalks_App_Slab")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSkew).HasColumnName("AI_Skew");

                entity.Property(e => e.AiSlopeRetainWall)
                    .HasColumnName("AI_Slope_Retain_wall")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSpanCnt).HasColumnName("AI_Span_Cnt");

                entity.Property(e => e.AiStrucCode)
                    .HasColumnName("AI_Struc_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiStrucSuper)
                    .HasColumnName("AI_Struc_Super")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiSubmitSts).HasColumnName("AI_SUBMIT_STS");

                entity.Property(e => e.AiTier).HasColumnName("AI_Tier");

                entity.Property(e => e.AiToCh).HasColumnName("AI_To_CH");

                entity.Property(e => e.AiToChDeci)
                    .HasColumnName("AI_To_CH_Deci")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.AiUtilities)
                    .HasColumnName("AI_Utilities")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWalkway).HasColumnName("AI_Walkway");

                entity.Property(e => e.AiWaterDownpipe)
                    .HasColumnName("AI_Water_Downpipe")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWaterway)
                    .HasColumnName("AI_Waterway")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWidth).HasColumnName("AI_Width");

                entity.Property(e => e.AiWidthLane).HasColumnName("AI_Width_Lane");

                entity.HasOne(d => d.AiRdmPkRefNoNavigation)
                    .WithMany(p => p.RmAllassetInventory)
                    .HasForeignKey(d => d.AiRdmPkRefNo)
                    .HasConstraintName("FK_Allasset_Roadmaster");
            });

            modelBuilder.Entity<RmAllassetInventory20210122>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RM_Allasset_inventory_20210122");

                entity.Property(e => e.AiAbutFound)
                    .HasColumnName("AI_Abut_Found")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiAbutType)
                    .HasColumnName("AI_Abut_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiActiveYn).HasColumnName("AI_Active_YN");

                entity.Property(e => e.AiAssetGrpCode)
                    .HasColumnName("AI_Asset_GRP_Code")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetId)
                    .HasColumnName("AI_Asset_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetNumber).HasColumnName("AI_Asset_Number");

                entity.Property(e => e.AiBarrelNo).HasColumnName("AI_Barrel_No");

                entity.Property(e => e.AiBeamsGridTrusArch)
                    .HasColumnName("AI_Beams_Grid_Trus_Arch")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBearingSeatDiaphg)
                    .HasColumnName("AI_Bearing_Seat_Diaphg")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBearingType)
                    .HasColumnName("AI_Bearing_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBotWidth).HasColumnName("AI_Bot_Width");

                entity.Property(e => e.AiBound)
                    .HasColumnName("AI_Bound")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiBridgeName)
                    .HasColumnName("AI_Bridge_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBuiltYear).HasColumnName("AI_Built_year");

                entity.Property(e => e.AiCatchArea).HasColumnName("AI_Catch_Area");

                entity.Property(e => e.AiCrBy)
                    .HasColumnName("AI_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiCrDt)
                    .HasColumnName("AI_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AiDeckPavement)
                    .HasColumnName("AI_Deck_pavement")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDeckType)
                    .HasColumnName("AI_Deck_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDesignFlow).HasColumnName("AI_Design_Flow");

                entity.Property(e => e.AiDiameter).HasColumnName("AI_Diameter");

                entity.Property(e => e.AiDist)
                    .HasColumnName("AI_Dist")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDivCode)
                    .HasColumnName("AI_Div_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJoint)
                    .HasColumnName("AI_Expan_Joint")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJointCount).HasColumnName("AI_Expan_Joint_Count");

                entity.Property(e => e.AiExpanJointSpace).HasColumnName("AI_Expan_Joint_Space");

                entity.Property(e => e.AiExpanType)
                    .HasColumnName("AI_Expan_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiFeatureId)
                    .HasColumnName("AI_Feature_ID")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiFinRdLevel).HasColumnName("AI_Fin_Rd_Level");

                entity.Property(e => e.AiFrmCh).HasColumnName("AI_FRM_CH");

                entity.Property(e => e.AiFrmChDeci)
                    .HasColumnName("AI_FRM_CH_Deci")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AiGpsEasting).HasColumnName("AI_GPS_Easting");

                entity.Property(e => e.AiGpsNorthing).HasColumnName("AI_GPS_Northing");

                entity.Property(e => e.AiGrpType)
                    .HasColumnName("AI_Grp_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiHeight).HasColumnName("AI_Height");

                entity.Property(e => e.AiIntelLevel).HasColumnName("AI_Intel_Level");

                entity.Property(e => e.AiIntelStruc)
                    .HasColumnName("AI_Intel_Struc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiLaneCnt).HasColumnName("AI_Lane_Cnt");

                entity.Property(e => e.AiLaneNo)
                    .HasColumnName("AI_Lane_No")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiLength).HasColumnName("AI_Length");

                entity.Property(e => e.AiLengthSpan).HasColumnName("AI_length_Span");

                entity.Property(e => e.AiLocChKm).HasColumnName("AI_Loc_CH_KM");

                entity.Property(e => e.AiLocChM)
                    .HasColumnName("AI_Loc_CH_M")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.AiMaintainedBy)
                    .HasColumnName("AI_maintained_By")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiMaterial)
                    .HasColumnName("AI_Material")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiMedian).HasColumnName("AI_median");

                entity.Property(e => e.AiModBy)
                    .HasColumnName("AI_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiModDt)
                    .HasColumnName("AI_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AiOutletLevel).HasColumnName("AI_Outlet_Level");

                entity.Property(e => e.AiOutletStruc)
                    .HasColumnName("AI_Outlet_Struc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiOwner)
                    .HasColumnName("AI_Owner")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiParapetRailing)
                    .HasColumnName("AI_Parapet_Railing")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiParapetType)
                    .HasColumnName("AI_Parapet_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPierType)
                    .HasColumnName("AI_Pier_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPiersPrimComp)
                    .HasColumnName("AI_Piers_Prim_Comp")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPkRefNo)
                    .HasColumnName("AI_PK_Ref_No")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AiPostSpacing).HasColumnName("AI_Post_Spacing");

                entity.Property(e => e.AiPrecastSitu)
                    .HasColumnName("AI_Precast_Situ")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdCode)
                    .HasColumnName("AI_Rd_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdName)
                    .HasColumnName("AI_Rd_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdmPkRefNo).HasColumnName("AI_RDM_PK_Ref_No");

                entity.Property(e => e.AiRefNo)
                    .HasColumnName("AI_Ref_No")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRiverName)
                    .HasColumnName("AI_River_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRmuCode)
                    .IsRequired()
                    .HasColumnName("AI_RMU_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiRmuName)
                    .HasColumnName("AI_RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiSecCode)
                    .IsRequired()
                    .HasColumnName("AI_Sec_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiSecName)
                    .HasColumnName("AI_Sec_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSidewalksAppSlab)
                    .HasColumnName("AI_Sidewalks_App_Slab")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSkew).HasColumnName("AI_Skew");

                entity.Property(e => e.AiSlopeRetainWall)
                    .HasColumnName("AI_Slope_Retain_wall")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSpanCnt).HasColumnName("AI_Span_Cnt");

                entity.Property(e => e.AiStrucCode)
                    .HasColumnName("AI_Struc_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiStrucSuper)
                    .HasColumnName("AI_Struc_Super")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiSubmitSts).HasColumnName("AI_SUBMIT_STS");

                entity.Property(e => e.AiTier).HasColumnName("AI_Tier");

                entity.Property(e => e.AiToCh).HasColumnName("AI_To_CH");

                entity.Property(e => e.AiToChDeci)
                    .HasColumnName("AI_To_CH_Deci")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AiUtilities)
                    .HasColumnName("AI_Utilities")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWalkway).HasColumnName("AI_Walkway");

                entity.Property(e => e.AiWaterDownpipe)
                    .HasColumnName("AI_Water_Downpipe")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWaterway)
                    .HasColumnName("AI_Waterway")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWidth).HasColumnName("AI_Width");

                entity.Property(e => e.AiWidthLane).HasColumnName("AI_Width_Lane");
            });

            modelBuilder.Entity<RmAllassetInventoryUat>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RM_Allasset_inventory_UAT");

                entity.Property(e => e.AiAbutFound)
                    .HasColumnName("AI_Abut_Found")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiAbutType)
                    .HasColumnName("AI_Abut_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiActiveYn).HasColumnName("AI_Active_YN");

                entity.Property(e => e.AiAssetGrpCode)
                    .HasColumnName("AI_Asset_GRP_Code")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetId)
                    .HasColumnName("AI_Asset_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetNumber).HasColumnName("AI_Asset_Number");

                entity.Property(e => e.AiBarrelNo).HasColumnName("AI_Barrel_No");

                entity.Property(e => e.AiBeamsGridTrusArch)
                    .HasColumnName("AI_Beams_Grid_Trus_Arch")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBearingSeatDiaphg)
                    .HasColumnName("AI_Bearing_Seat_Diaphg")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBearingType)
                    .HasColumnName("AI_Bearing_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBotWidth).HasColumnName("AI_Bot_Width");

                entity.Property(e => e.AiBound)
                    .HasColumnName("AI_Bound")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiBridgeName)
                    .HasColumnName("AI_Bridge_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBuiltYear).HasColumnName("AI_Built_year");

                entity.Property(e => e.AiCatchArea).HasColumnName("AI_Catch_Area");

                entity.Property(e => e.AiCrBy)
                    .HasColumnName("AI_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiCrDt)
                    .HasColumnName("AI_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AiDeckPavement)
                    .HasColumnName("AI_Deck_pavement")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDeckType)
                    .HasColumnName("AI_Deck_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDesignFlow).HasColumnName("AI_Design_Flow");

                entity.Property(e => e.AiDiameter).HasColumnName("AI_Diameter");

                entity.Property(e => e.AiDist)
                    .HasColumnName("AI_Dist")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDivCode)
                    .HasColumnName("AI_Div_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJoint)
                    .HasColumnName("AI_Expan_Joint")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJointCount).HasColumnName("AI_Expan_Joint_Count");

                entity.Property(e => e.AiExpanJointSpace).HasColumnName("AI_Expan_Joint_Space");

                entity.Property(e => e.AiExpanType)
                    .HasColumnName("AI_Expan_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiFeatureId)
                    .HasColumnName("AI_Feature_ID")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiFinRdLevel).HasColumnName("AI_Fin_Rd_Level");

                entity.Property(e => e.AiFrmCh).HasColumnName("AI_FRM_CH");

                entity.Property(e => e.AiFrmChDeci)
                    .HasColumnName("AI_FRM_CH_Deci")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AiGpsEasting).HasColumnName("AI_GPS_Easting");

                entity.Property(e => e.AiGpsNorthing).HasColumnName("AI_GPS_Northing");

                entity.Property(e => e.AiGrpType)
                    .HasColumnName("AI_Grp_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiHeight).HasColumnName("AI_Height");

                entity.Property(e => e.AiIntelLevel).HasColumnName("AI_Intel_Level");

                entity.Property(e => e.AiIntelStruc)
                    .HasColumnName("AI_Intel_Struc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiLaneCnt).HasColumnName("AI_Lane_Cnt");

                entity.Property(e => e.AiLaneNo)
                    .HasColumnName("AI_Lane_No")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiLength).HasColumnName("AI_Length");

                entity.Property(e => e.AiLengthSpan).HasColumnName("AI_length_Span");

                entity.Property(e => e.AiLocChKm).HasColumnName("AI_Loc_CH_KM");

                entity.Property(e => e.AiLocChM)
                    .HasColumnName("AI_Loc_CH_M")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.AiMaintainedBy)
                    .HasColumnName("AI_maintained_By")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiMaterial)
                    .HasColumnName("AI_Material")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiMedian).HasColumnName("AI_median");

                entity.Property(e => e.AiModBy)
                    .HasColumnName("AI_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiModDt)
                    .HasColumnName("AI_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AiOutletLevel).HasColumnName("AI_Outlet_Level");

                entity.Property(e => e.AiOutletStruc)
                    .HasColumnName("AI_Outlet_Struc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiOwner)
                    .HasColumnName("AI_Owner")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiParapetRailing)
                    .HasColumnName("AI_Parapet_Railing")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiParapetType)
                    .HasColumnName("AI_Parapet_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPierType)
                    .HasColumnName("AI_Pier_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPiersPrimComp)
                    .HasColumnName("AI_Piers_Prim_Comp")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPostSpacing).HasColumnName("AI_Post_Spacing");

                entity.Property(e => e.AiPrecastSitu)
                    .HasColumnName("AI_Precast_Situ")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdCode)
                    .HasColumnName("AI_Rd_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdName)
                    .HasColumnName("AI_Rd_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdmPkRefNo).HasColumnName("AI_RDM_PK_Ref_No");

                entity.Property(e => e.AiRefNo)
                    .HasColumnName("AI_Ref_No")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRiverName)
                    .HasColumnName("AI_River_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRmuCode)
                    .IsRequired()
                    .HasColumnName("AI_RMU_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiRmuName)
                    .HasColumnName("AI_RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiSecCode)
                    .IsRequired()
                    .HasColumnName("AI_Sec_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiSecName)
                    .HasColumnName("AI_Sec_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSidewalksAppSlab)
                    .HasColumnName("AI_Sidewalks_App_Slab")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSkew).HasColumnName("AI_Skew");

                entity.Property(e => e.AiSlopeRetainWall)
                    .HasColumnName("AI_Slope_Retain_wall")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSpanCnt).HasColumnName("AI_Span_Cnt");

                entity.Property(e => e.AiStrucCode)
                    .HasColumnName("AI_Struc_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiStrucSuper)
                    .HasColumnName("AI_Struc_Super")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiSubmitSts).HasColumnName("AI_SUBMIT_STS");

                entity.Property(e => e.AiTier).HasColumnName("AI_Tier");

                entity.Property(e => e.AiToCh).HasColumnName("AI_To_CH");

                entity.Property(e => e.AiToChDeci)
                    .HasColumnName("AI_To_CH_Deci")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AiUtilities)
                    .HasColumnName("AI_Utilities")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWalkway).HasColumnName("AI_Walkway");

                entity.Property(e => e.AiWaterDownpipe)
                    .HasColumnName("AI_Water_Downpipe")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWaterway)
                    .HasColumnName("AI_Waterway")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWidth).HasColumnName("AI_Width");

                entity.Property(e => e.AiWidthLane).HasColumnName("AI_Width_Lane");
            });

            modelBuilder.Entity<RmAllassetInventoryUatBackup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RM_Allasset_inventory_UAT_backup");

                entity.Property(e => e.AiAbutFound)
                    .HasColumnName("AI_Abut_Found")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiAbutType)
                    .HasColumnName("AI_Abut_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiActiveYn).HasColumnName("AI_Active_YN");

                entity.Property(e => e.AiAssetGrpCode)
                    .HasColumnName("AI_Asset_GRP_Code")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetId)
                    .HasColumnName("AI_Asset_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiAssetNumber).HasColumnName("AI_Asset_Number");

                entity.Property(e => e.AiBarrelNo).HasColumnName("AI_Barrel_No");

                entity.Property(e => e.AiBeamsGridTrusArch)
                    .HasColumnName("AI_Beams_Grid_Trus_Arch")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBearingSeatDiaphg)
                    .HasColumnName("AI_Bearing_Seat_Diaphg")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBearingType)
                    .HasColumnName("AI_Bearing_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBotWidth).HasColumnName("AI_Bot_Width");

                entity.Property(e => e.AiBound)
                    .HasColumnName("AI_Bound")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiBridgeName)
                    .HasColumnName("AI_Bridge_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiBuiltYear).HasColumnName("AI_Built_year");

                entity.Property(e => e.AiCatchArea).HasColumnName("AI_Catch_Area");

                entity.Property(e => e.AiCrBy)
                    .HasColumnName("AI_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiCrDt)
                    .HasColumnName("AI_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AiDeckPavement)
                    .HasColumnName("AI_Deck_pavement")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDeckType)
                    .HasColumnName("AI_Deck_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDesignFlow).HasColumnName("AI_Design_Flow");

                entity.Property(e => e.AiDiameter).HasColumnName("AI_Diameter");

                entity.Property(e => e.AiDist)
                    .HasColumnName("AI_Dist")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiDivCode)
                    .HasColumnName("AI_Div_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJoint)
                    .HasColumnName("AI_Expan_Joint")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiExpanJointCount).HasColumnName("AI_Expan_Joint_Count");

                entity.Property(e => e.AiExpanJointSpace).HasColumnName("AI_Expan_Joint_Space");

                entity.Property(e => e.AiExpanType)
                    .HasColumnName("AI_Expan_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiFeatureId)
                    .HasColumnName("AI_Feature_ID")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiFinRdLevel).HasColumnName("AI_Fin_Rd_Level");

                entity.Property(e => e.AiFrmCh).HasColumnName("AI_FRM_CH");

                entity.Property(e => e.AiFrmChDeci)
                    .HasColumnName("AI_FRM_CH_Deci")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AiGpsEasting).HasColumnName("AI_GPS_Easting");

                entity.Property(e => e.AiGpsNorthing).HasColumnName("AI_GPS_Northing");

                entity.Property(e => e.AiGrpType)
                    .HasColumnName("AI_Grp_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiHeight).HasColumnName("AI_Height");

                entity.Property(e => e.AiIntelLevel).HasColumnName("AI_Intel_Level");

                entity.Property(e => e.AiIntelStruc)
                    .HasColumnName("AI_Intel_Struc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiLaneCnt).HasColumnName("AI_Lane_Cnt");

                entity.Property(e => e.AiLaneNo)
                    .HasColumnName("AI_Lane_No")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiLength).HasColumnName("AI_Length");

                entity.Property(e => e.AiLengthSpan).HasColumnName("AI_length_Span");

                entity.Property(e => e.AiLocChKm).HasColumnName("AI_Loc_CH_KM");

                entity.Property(e => e.AiLocChM)
                    .HasColumnName("AI_Loc_CH_M")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.AiMaintainedBy)
                    .HasColumnName("AI_maintained_By")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AiMaterial)
                    .HasColumnName("AI_Material")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiMedian).HasColumnName("AI_median");

                entity.Property(e => e.AiModBy)
                    .HasColumnName("AI_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiModDt)
                    .HasColumnName("AI_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AiOutletLevel).HasColumnName("AI_Outlet_Level");

                entity.Property(e => e.AiOutletStruc)
                    .HasColumnName("AI_Outlet_Struc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiOwner)
                    .HasColumnName("AI_Owner")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiParapetRailing)
                    .HasColumnName("AI_Parapet_Railing")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiParapetType)
                    .HasColumnName("AI_Parapet_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPierType)
                    .HasColumnName("AI_Pier_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPiersPrimComp)
                    .HasColumnName("AI_Piers_Prim_Comp")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiPostSpacing).HasColumnName("AI_Post_Spacing");

                entity.Property(e => e.AiPrecastSitu)
                    .HasColumnName("AI_Precast_Situ")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdCode)
                    .HasColumnName("AI_Rd_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdName)
                    .HasColumnName("AI_Rd_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRdmPkRefNo).HasColumnName("AI_RDM_PK_Ref_No");

                entity.Property(e => e.AiRefNo)
                    .HasColumnName("AI_Ref_No")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRiverName)
                    .HasColumnName("AI_River_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiRmuCode)
                    .IsRequired()
                    .HasColumnName("AI_RMU_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiRmuName)
                    .HasColumnName("AI_RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiSecCode)
                    .IsRequired()
                    .HasColumnName("AI_Sec_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AiSecName)
                    .HasColumnName("AI_Sec_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSidewalksAppSlab)
                    .HasColumnName("AI_Sidewalks_App_Slab")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSkew).HasColumnName("AI_Skew");

                entity.Property(e => e.AiSlopeRetainWall)
                    .HasColumnName("AI_Slope_Retain_wall")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiSpanCnt).HasColumnName("AI_Span_Cnt");

                entity.Property(e => e.AiStrucCode)
                    .HasColumnName("AI_Struc_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiStrucSuper)
                    .HasColumnName("AI_Struc_Super")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AiSubmitSts).HasColumnName("AI_SUBMIT_STS");

                entity.Property(e => e.AiTier).HasColumnName("AI_Tier");

                entity.Property(e => e.AiToCh).HasColumnName("AI_To_CH");

                entity.Property(e => e.AiToChDeci)
                    .HasColumnName("AI_To_CH_Deci")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AiUtilities)
                    .HasColumnName("AI_Utilities")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWalkway).HasColumnName("AI_Walkway");

                entity.Property(e => e.AiWaterDownpipe)
                    .HasColumnName("AI_Water_Downpipe")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWaterway)
                    .HasColumnName("AI_Waterway")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AiWidth).HasColumnName("AI_Width");

                entity.Property(e => e.AiWidthLane).HasColumnName("AI_Width_Lane");
            });

            modelBuilder.Entity<RmAssetDefectCode>(entity =>
            {
                entity.HasKey(e => e.AdcPkRefNo)
                    .HasName("PK__RM_asset__DED42729F8CBE7F2");

                entity.ToTable("RM_asset_defect_code");

                entity.Property(e => e.AdcPkRefNo).HasColumnName("ADC_PK_Ref_No");

                entity.Property(e => e.AdcActiveYn)
                    .IsRequired()
                    .HasColumnName("ADC_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AdcAgtPkRefNo).HasColumnName("ADC_AGT_PK_Ref_No");

                entity.Property(e => e.AdcAssetGrpCode)
                    .HasColumnName("ADC_Asset_GRP_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AdcAssetGrpTypeName)
                    .IsRequired()
                    .HasColumnName("ADC_Asset_GRP_Type_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AdcCrBy)
                    .HasColumnName("ADC_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AdcCrDt)
                    .HasColumnName("ADC_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AdcDefCode)
                    .HasColumnName("ADC_Def_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AdcDefContractCode)
                    .HasColumnName("ADC_Def_Contract_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AdcDefName)
                    .HasColumnName("ADC_Def_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AdcFormNo)
                    .HasColumnName("ADC_Form_No")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AdcModBy)
                    .HasColumnName("ADC_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AdcModDt)
                    .HasColumnName("ADC_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AdcRemarks)
                    .HasColumnName("ADC_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.HasOne(d => d.AdcAgtPkRefNoNavigation)
                    .WithMany(p => p.RmAssetDefectCode)
                    .HasForeignKey(d => d.AdcAgtPkRefNo)
                    .HasConstraintName("FK__RM_asset___ADC_A__00AA174D");
            });

            modelBuilder.Entity<RmAssetGroupType>(entity =>
            {
                entity.HasKey(e => e.AgtPkRefNo)
                    .HasName("PK__RM_asset__05F9536009291682");

                entity.ToTable("RM_asset_Group_Type");

                entity.Property(e => e.AgtPkRefNo).HasColumnName("AGT_PK_Ref_No");

                entity.Property(e => e.AgtActiveYn)
                    .IsRequired()
                    .HasColumnName("AGT_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AgtAssetGrpCode)
                    .IsRequired()
                    .HasColumnName("AGT_Asset_GRP_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AgtAssetGrpName)
                    .IsRequired()
                    .HasColumnName("AGT_Asset_GRP_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AgtAssetGrpTypeCode)
                    .IsRequired()
                    .HasColumnName("AGT_Asset_GRP_Type_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AgtAssetGrpTypeName)
                    .IsRequired()
                    .HasColumnName("AGT_Asset_GRP_Type_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AgtCrBy)
                    .HasColumnName("AGT_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AgtCrDt)
                    .HasColumnName("AGT_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AgtGrpTypeContractCode)
                    .HasColumnName("AGT_GRP_Type_Contract_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AgtModBy)
                    .HasColumnName("AGT_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AgtModDt)
                    .HasColumnName("AGT_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AgtRemarks)
                    .HasColumnName("AGT_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmAssetImageDtl>(entity =>
            {
                entity.HasKey(e => e.AidPkRefNo)
                    .HasName("PK__RM_Asset__E2BB2247DBE6C8C3");

                entity.ToTable("RM_Asset_Image_DTL");

                entity.Property(e => e.AidPkRefNo).HasColumnName("AID_PK_Ref_No");

                entity.Property(e => e.AidActiveYn)
                    .IsRequired()
                    .HasColumnName("AID_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AidAiPkRefNo).HasColumnName("AID_AI_PK_Ref_No");

                entity.Property(e => e.AidCrBy)
                    .HasColumnName("AID_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AidCrDt)
                    .HasColumnName("AID_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AidImageFilenameSys)
                    .HasColumnName("AID_Image_Filename_Sys")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AidImageFilenameUpload)
                    .HasColumnName("AID_Image_Filename_Upload")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AidImageSrno).HasColumnName("AID_Image_SRNO");

                entity.Property(e => e.AidImageTypeCode)
                    .HasColumnName("AID_Image_Type_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AidImageUserFilePath)
                    .HasColumnName("AID_image_user_filePath")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AidModBy)
                    .HasColumnName("AID_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AidModDt)
                    .HasColumnName("AID_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AidSubmitSts).HasColumnName("AID_SUBMIT_STS");

                entity.HasOne(d => d.AidAiPkRefNoNavigation)
                    .WithMany(p => p.RmAssetImageDtl)
                    .HasForeignKey(d => d.AidAiPkRefNo)
                    .HasConstraintName("FK__RM_Asset___AID_A__17036CC0");
            });

            modelBuilder.Entity<RmAssetImageDtl20210122>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RM_Asset_Image_DTL_20210122");

                entity.Property(e => e.AidActiveYn).HasColumnName("AID_Active_YN");

                entity.Property(e => e.AidAiPkRefNo).HasColumnName("AID_AI_PK_Ref_No");

                entity.Property(e => e.AidCrBy)
                    .HasColumnName("AID_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AidCrDt)
                    .HasColumnName("AID_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AidImageFilenameSys)
                    .HasColumnName("AID_Image_Filename_Sys")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AidImageFilenameUpload)
                    .HasColumnName("AID_Image_Filename_Upload")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AidImageSrno).HasColumnName("AID_Image_SRNO");

                entity.Property(e => e.AidImageTypeCode)
                    .HasColumnName("AID_Image_Type_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AidImageUserFilePath)
                    .HasColumnName("AID_image_user_filePath")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AidModBy)
                    .HasColumnName("AID_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AidModDt)
                    .HasColumnName("AID_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AidPkRefNo)
                    .HasColumnName("AID_PK_Ref_No")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AidSubmitSts).HasColumnName("AID_SUBMIT_STS");
            });

            modelBuilder.Entity<RmAuditLogAction>(entity =>
            {
                entity.HasKey(e => e.AlaPkRefNo);

                entity.ToTable("RM_AuditLogAction");

                entity.Property(e => e.AlaPkRefNo).HasColumnName("ALA_PK_Ref_No");

                entity.Property(e => e.AlaActionName)
                    .IsRequired()
                    .HasColumnName("ALA_ActionName")
                    .HasMaxLength(500);

                entity.Property(e => e.AlaCrBy).HasColumnName("ALA_CR_BY");

                entity.Property(e => e.AlaCrDt)
                    .HasColumnName("ALA_CR_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AlaRequestIp)
                    .IsRequired()
                    .HasColumnName("ALA_RequestIP")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AlaRequester)
                    .IsRequired()
                    .HasColumnName("ALA_Requester")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmAuditLogTransaction>(entity =>
            {
                entity.HasKey(e => e.AltPkRefNo);

                entity.ToTable("RM_AuditLogTransaction");

                entity.Property(e => e.AltPkRefNo).HasColumnName("ALT_PK_Ref_No");

                entity.Property(e => e.AltAlaPkRefNo).HasColumnName("ALT_ALA_PK_Ref_No");

                entity.Property(e => e.AltTableName)
                    .IsRequired()
                    .HasColumnName("ALT_TableName")
                    .HasMaxLength(150);

                entity.Property(e => e.AltTransactinDetails)
                    .IsRequired()
                    .HasColumnName("ALT_TransactinDetails");

                entity.Property(e => e.AltTransactionName)
                    .IsRequired()
                    .HasColumnName("ALT_TransactionName")
                    .HasMaxLength(250);

                entity.HasOne(d => d.AltAlaPkRefNoNavigation)
                    .WithMany(p => p.RmAuditLogTransaction)
                    .HasForeignKey(d => d.AltAlaPkRefNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RM_AuditLogTransaction_RM_AuditLogAction");
            });

            modelBuilder.Entity<RmDdLookup>(entity =>
            {
                entity.HasKey(e => e.DdlPkRefNo)
                    .HasName("PK__RM_DD_LO__C0F32C37E4C52338");

                entity.ToTable("RM_DD_LOOKUP");

                entity.Property(e => e.DdlPkRefNo).HasColumnName("DDL_PK_Ref_No");

                entity.Property(e => e.DdlActiveYn)
                    .IsRequired()
                    .HasColumnName("DDL_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DdlCrBy)
                    .HasColumnName("DDL_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.DdlCrDt)
                    .HasColumnName("DDL_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.DdlModBy)
                    .HasColumnName("DDL_MOD_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.DdlModDt)
                    .HasColumnName("DDL_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.DdlType)
                    .HasColumnName("DDL_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DdlTypeCode)
                    .HasColumnName("DDL_Type_code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.DdlTypeDesc)
                    .HasColumnName("DDL_Type_DESC")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DdlTypeRemarks)
                    .HasColumnName("DDL_Type_REMARKS")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.DdlTypeValue)
                    .HasColumnName("DDL_Type_VALUE")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmDepartment>(entity =>
            {
                entity.HasKey(e => e.DeptPkId);

                entity.ToTable("RM_Department");

                entity.Property(e => e.DeptPkId).HasColumnName("Dept_PkId");

                entity.Property(e => e.DeptCreatedBy).HasColumnName("Dept_Created_BY");

                entity.Property(e => e.DeptCreatedOn).HasColumnName("Dept_CreatedOn");

                entity.Property(e => e.DeptDescription)
                    .HasColumnName("Dept_Description")
                    .HasMaxLength(2000);

                entity.Property(e => e.DeptModifiedBy).HasColumnName("Dept_Modified_By");

                entity.Property(e => e.DeptMofiedOn).HasColumnName("Dept_MofiedOn");

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasColumnName("Dept_Name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<RmDivRmuSecMaster>(entity =>
            {
                entity.HasKey(e => e.RdsmPkRefNo)
                    .HasName("PK__RM_div_R__98BD4F44CF3899B8");

                entity.ToTable("RM_div_RMU_Sec_Master");

                entity.Property(e => e.RdsmPkRefNo).HasColumnName("RDSM_PK_Ref_No");

                entity.Property(e => e.RdsmActiveYn)
                    .IsRequired()
                    .HasColumnName("RDSM_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RdsmCrBy)
                    .HasColumnName("RDSM_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.RdsmCrDt)
                    .HasColumnName("RDSM_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.RdsmDivCode)
                    .IsRequired()
                    .HasColumnName("RDSM_Div_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.RdsmDivision)
                    .IsRequired()
                    .HasColumnName("RDSM_Division")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RdsmModBy)
                    .HasColumnName("RDSM_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.RdsmModDt)
                    .HasColumnName("RDSM_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.RdsmRmuCode)
                    .HasColumnName("RDSM_RMU_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.RdsmRmuName)
                    .HasColumnName("RDSM_RMU_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RdsmSectionCode)
                    .HasColumnName("RDSM_Section_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.RdsmSectionName)
                    .HasColumnName("RDSM_Section_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmDivisionMaster>(entity =>
            {
                entity.HasKey(e => e.DivPkRefNo)
                    .HasName("pk_RM_Division_Master_DIV_PK_Ref_No");

                entity.ToTable("RM_Division_Master");

                entity.Property(e => e.DivPkRefNo).HasColumnName("DIV_PK_Ref_No");

                entity.Property(e => e.DivCode)
                    .HasColumnName("DIV_Code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DivIsActive).HasColumnName("DIV_IsActive");

                entity.Property(e => e.DivName)
                    .HasColumnName("DIV_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmFieldDisRightsDtl>(entity =>
            {
                entity.HasKey(e => e.FrdPkId)
                    .HasName("PK__RM_Field__FD4DA7F36290197B");

                entity.ToTable("RM_Field_DIS_Rights_DTL");

                entity.Property(e => e.FrdPkId).HasColumnName("FRD_PK_Id");

                entity.Property(e => e.FrdCrBy)
                    .HasColumnName("FRD_CR_By")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FrdCrDt)
                    .HasColumnName("FRD_CR_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FrdEffFrmDt)
                    .HasColumnName("FRD_Eff_FRM_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FrdEffToDt)
                    .HasColumnName("FRD_Eff_TO_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FrdFieldName)
                    .IsRequired()
                    .HasColumnName("FRD_Field_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FrdFieldObjId)
                    .IsRequired()
                    .HasColumnName("FRD_Field_OBJ_ID")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FrdModBy)
                    .HasColumnName("FRD_Mod_By")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FrdModDt)
                    .HasColumnName("FRD_Mod_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FrdModuleName)
                    .IsRequired()
                    .HasColumnName("FRD_Module_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FrdMrcPkId).HasColumnName("FRD_MRC_PK_id");

                entity.Property(e => e.FrdRemarks)
                    .HasColumnName("FRD_Remarks")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FrdScreenName)
                    .IsRequired()
                    .HasColumnName("FRD_Screen_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FrdSubmitSts).HasColumnName("FRD_SUBMIT_STS");

                entity.HasOne(d => d.FrdMrcPk)
                    .WithMany(p => p.RmFieldDisRightsDtl)
                    .HasForeignKey(d => d.FrdMrcPkId)
                    .HasConstraintName("FK__RM_Field___FRD_M__038683F8");
            });

            modelBuilder.Entity<RmFormADtl>(entity =>
            {
                entity.HasKey(e => e.FadPkRefNo)
                    .HasName("PK__RM_FormA__312C1ECD66A6CC8E");

                entity.ToTable("RM_FormA_DTL");

                entity.Property(e => e.FadPkRefNo).HasColumnName("FAD_PK_Ref_No");

                entity.Property(e => e.FadActCode)
                    .HasColumnName("FAD_ACT_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FadActiveYn)
                    .IsRequired()
                    .HasColumnName("FAD_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FadAdp)
                    .HasColumnName("FAD_ADP")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FadAssetId)
                    .HasColumnName("FAD_Asset_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FadCdr)
                    .HasColumnName("FAD_CDR")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FadCrBy)
                    .HasColumnName("FAD_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FadCrDt)
                    .HasColumnName("FAD_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FadDefCode)
                    .HasColumnName("FAD_Def_code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FadDesc)
                    .HasColumnName("FAD_Desc")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FadDt)
                    .HasColumnName("FAD_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FadFahPkRefNo).HasColumnName("FAD_FAH_PK_Ref_No");

                entity.Property(e => e.FadFormhApp)
                    .HasColumnName("FAD_FORMH_App")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FadFrmCh).HasColumnName("FAD_FRM_CH");

                entity.Property(e => e.FadFrmChDeci)
                    .HasColumnName("FAD_FRM_CH_Deci")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FadHeight).HasColumnName("FAD_Height");

                entity.Property(e => e.FadLength).HasColumnName("FAD_Length");

                entity.Property(e => e.FadModBy)
                    .HasColumnName("FAD_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FadModDt)
                    .HasColumnName("FAD_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FadPriority)
                    .HasColumnName("FAD_Priority")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FadRefId)
                    .HasColumnName("FAD_Ref_ID")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FadRemarks)
                    .HasColumnName("FAD_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FadRt).HasColumnName("FAD_RT");

                entity.Property(e => e.FadSftPs)
                    .HasColumnName("FAD_SFT_PS")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FadSftWis).HasColumnName("FAD_SFT_WIS");

                entity.Property(e => e.FadSiteRef)
                    .HasColumnName("FAD_Site_Ref")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FadSrno).HasColumnName("FAD_SRNO");

                entity.Property(e => e.FadSubmitSts).HasColumnName("FAD_SUBMIT_STS");

                entity.Property(e => e.FadToCh).HasColumnName("FAD_To_CH");

                entity.Property(e => e.FadToChDeci)
                    .HasColumnName("FAD_To_CH_Deci")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FadUnit)
                    .HasColumnName("FAD_Unit")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FadWc).HasColumnName("FAD_WC");

                entity.Property(e => e.FadWi).HasColumnName("FAD_WI");

                entity.Property(e => e.FadWidth).HasColumnName("FAD_Width");

                entity.Property(e => e.FadWs).HasColumnName("FAD_WS");

                entity.Property(e => e.FadWtc).HasColumnName("FAD_WTC");

                entity.HasOne(d => d.FadFahPkRefNoNavigation)
                    .WithMany(p => p.RmFormADtl)
                    .HasForeignKey(d => d.FadFahPkRefNo)
                    .HasConstraintName("FK__RM_FormA___FAD_F__093F5D4E");
            });

            modelBuilder.Entity<RmFormAHdr>(entity =>
            {
                entity.HasKey(e => e.FahPkRefNo)
                    .HasName("PK__RM_FormA__797F99D602112B6A");

                entity.ToTable("RM_FormA_HDR");

                entity.Property(e => e.FahPkRefNo).HasColumnName("FAH_PK_Ref_No");

                entity.Property(e => e.FahActiveYn)
                    .IsRequired()
                    .HasColumnName("FAH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FahAssetGroupCode)
                    .HasColumnName("FAH_Asset_Group_Code")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FahAuditLog).HasColumnName("FAH_AuditLog");

                entity.Property(e => e.FahContNo)
                    .HasColumnName("FAH_CONT_No")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FahCrBy)
                    .HasColumnName("FAH_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FahCrDt)
                    .HasColumnName("FAH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FahDesignationPrp)
                    .HasColumnName("FAH_Designation_PRP")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FahDesignationVer)
                    .HasColumnName("FAH_Designation_VER")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FahDtPrp)
                    .HasColumnName("FAH_DT_PRP")
                    .HasColumnType("datetime");

                entity.Property(e => e.FahDtVer)
                    .HasColumnName("FAH_DT_VER")
                    .HasColumnType("datetime");

                entity.Property(e => e.FahModBy)
                    .HasColumnName("FAH_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FahModDt)
                    .HasColumnName("FAH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FahMonth).HasColumnName("FAH_Month");

                entity.Property(e => e.FahRefId)
                    .HasColumnName("FAH_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FahRemarks)
                    .HasColumnName("FAH_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FahRmu)
                    .HasColumnName("FAH_RMU")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FahRoadCode)
                    .HasColumnName("FAH_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FahRoadName)
                    .HasColumnName("FAH_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FahSection)
                    .HasColumnName("FAH_Section")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FahSignPrp).HasColumnName("FAH_SIgn_PRP");

                entity.Property(e => e.FahSignVer).HasColumnName("FAH_SIgn_VER");

                entity.Property(e => e.FahStatus)
                    .IsRequired()
                    .HasColumnName("FAH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FahSubmitSts).HasColumnName("FAH_SUBMIT_STS");

                entity.Property(e => e.FahUseridPrp).HasColumnName("FAH_Userid_PRP");

                entity.Property(e => e.FahUseridVer).HasColumnName("FAH_Userid_VER");

                entity.Property(e => e.FahUsernamePrp)
                    .HasColumnName("FAH_Username_PRP")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FahUsernameVer)
                    .HasColumnName("FAH_Username_VER")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FahYear).HasColumnName("FAH_Year");
            });

            modelBuilder.Entity<RmFormASiterefDtl>(entity =>
            {
                entity.HasKey(e => e.FsdPkRefId)
                    .HasName("PK__RM_FormA__A2DE63B964C8B1FD");

                entity.ToTable("RM_FormA_Siteref_DTL");

                entity.Property(e => e.FsdPkRefId).HasColumnName("FSD_PK_Ref_ID");

                entity.Property(e => e.AioActiveYn)
                    .IsRequired()
                    .HasColumnName("AIO_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AioCrBy)
                    .HasColumnName("AIO_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AioCrDt)
                    .HasColumnName("AIO_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AioModBy)
                    .HasColumnName("AIO_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AioModDt)
                    .HasColumnName("AIO_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.AioSubmitSts).HasColumnName("AIO_SUBMIT_STS");

                entity.Property(e => e.FsdDefCode)
                    .HasColumnName("FSD_Def_Code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FsdDefDesc)
                    .HasColumnName("FSD_Def_Desc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FsdFadPkRefNo).HasColumnName("FSD_FAD_PK_Ref_No");

                entity.Property(e => e.FsdFadSiteRef)
                    .HasColumnName("FSD_FAD_Site_Ref")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FsdFadSrno).HasColumnName("FSD_FAD_SRNO");

                entity.Property(e => e.FsdSiteRefId).HasColumnName("FSD_Site_Ref_ID");

                entity.HasOne(d => d.FsdFadPkRefNoNavigation)
                    .WithMany(p => p.RmFormASiterefDtl)
                    .HasForeignKey(d => d.FsdFadPkRefNo)
                    .HasConstraintName("FK__RM_FormA___FSD_F__0B27A5C0");
            });

            modelBuilder.Entity<RmFormB1b2BrInsDtl>(entity =>
            {
                entity.HasKey(e => e.FbridPkRefNo)
                    .HasName("PK__RM_FormB__F6E9C9D8E23E3CC5");

                entity.ToTable("RM_FormB1B2_BR_Ins_DTL");

                entity.Property(e => e.FbridPkRefNo).HasColumnName("FBRID_PK_Ref_No");

                entity.Property(e => e.FbridAbutFoundDistress)
                    .HasColumnName("FBRID_Abut_Found_Distress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridAbutFoundDistressOthers)
                    .HasColumnName("FBRID_Abut_Found_Distress_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridAbutFoundInspCode)
                    .HasColumnName("FBRID_Abut_Found_Insp_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FbridAbutFoundMat)
                    .HasColumnName("FBRID_Abut_Found_Mat")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridAbutFoundMatCode)
                    .HasColumnName("FBRID_Abut_Found_Mat_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridAbutFoundSeverity).HasColumnName("FBRID_Abut_Found_Severity");

                entity.Property(e => e.FbridActiveYn)
                    .IsRequired()
                    .HasColumnName("FBRID_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FbridBeamsGridTrusArch)
                    .HasColumnName("FBRID_Beams_Grid_Trus_Arch")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridBeamsGridTrusArchCode)
                    .HasColumnName("FBRID_Beams_Grid_Trus_Arch_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridBeamsGridTrusArchDistress)
                    .HasColumnName("FBRID_Beams_Grid_Trus_Arch_Distress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridBeamsGridTrusArchDistressOthers)
                    .HasColumnName("FBRID_Beams_Grid_Trus_Arch_Distress_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridBeamsGridTrusArchInspCode)
                    .HasColumnName("FBRID_Beams_Grid_Trus_Arch_Insp_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FbridBeamsGridTrusArchSeverity).HasColumnName("FBRID_Beams_Grid_Trus_Arch_Severity");

                entity.Property(e => e.FbridBearingStDiaphgDistress)
                    .HasColumnName("FBRID_Bearing_St_Diaphg_Distress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridBearingStDiaphgDistressOthers)
                    .HasColumnName("FBRID_Bearing_St_Diaphg_Distress_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridBearingStDiaphgInspCode)
                    .HasColumnName("FBRID_Bearing_St_Diaphg_Insp_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FbridBearingStDiaphgMat)
                    .HasColumnName("FBRID_Bearing_St_Diaphg_Mat")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridBearingStDiaphgMatCode)
                    .HasColumnName("FBRID_Bearing_St_Diaphg_Mat_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridBearingStDiaphgSeverity).HasColumnName("FBRID_Bearing_St_Diaphg_Severity");

                entity.Property(e => e.FbridCrBy).HasColumnName("FBRID_CR_By");

                entity.Property(e => e.FbridCrDt)
                    .HasColumnName("FBRID_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FbridDeckPavement)
                    .HasColumnName("FBRID_Deck_pavement")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridDeckPavementCode)
                    .HasColumnName("FBRID_Deck_pavement_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridDeckPavementCodeInspCode)
                    .HasColumnName("FBRID_Deck_pavement_Code_Insp_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FbridDeckPavementDistress)
                    .HasColumnName("FBRID_Deck_pavement_Distress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridDeckPavementDistressOthers)
                    .HasColumnName("FBRID_Deck_pavement_Distress_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridDeckPavementSeverity).HasColumnName("FBRID_Deck_pavement_Severity");

                entity.Property(e => e.FbridExpanJoint)
                    .HasColumnName("FBRID_Expan_Joint")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridExpanJointCode)
                    .HasColumnName("FBRID_Expan_Joint_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridExpanJointDistress)
                    .HasColumnName("FBRID_Expan_Joint_Distress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridExpanJointDistressOthers)
                    .HasColumnName("FBRID_Expan_Joint_Distress_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridExpanJointInspCode)
                    .HasColumnName("FBRID_Expan_Joint_Insp_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FbridExpanJointSeverity).HasColumnName("FBRID_Expan_Joint_Severity");

                entity.Property(e => e.FbridFbrihPkRefNo).HasColumnName("FBRID_FBRIH_PK_Ref_No");

                entity.Property(e => e.FbridModBy).HasColumnName("FBRID_Mod_By");

                entity.Property(e => e.FbridModDt)
                    .HasColumnName("FBRID_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FbridParapetRailing)
                    .HasColumnName("FBRID_Parapet_Railing")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridParapetRailingCode)
                    .HasColumnName("FBRID_Parapet_Railing_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridParapetRailingDistress)
                    .HasColumnName("FBRID_Parapet_Railing_Distress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridParapetRailingDistressOthers)
                    .HasColumnName("FBRID_Parapet_Railing_Distress_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridParapetRailingInspCode)
                    .HasColumnName("FBRID_Parapet_Railing_Insp_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FbridParapetRailingSeverity).HasColumnName("FBRID_Parapet_Railing_Severity");

                entity.Property(e => e.FbridPiersPrimCompDistress)
                    .HasColumnName("FBRID_Piers_Prim_Comp_Distress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridPiersPrimCompDistressOthers)
                    .HasColumnName("FBRID_Piers_Prim_Comp_Distress_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridPiersPrimCompInspCode)
                    .HasColumnName("FBRID_Piers_Prim_Comp_Insp_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FbridPiersPrimCompMat)
                    .HasColumnName("FBRID_Piers_Prim_Comp_Mat")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridPiersPrimCompMatCode)
                    .HasColumnName("FBRID_Piers_Prim_Comp_Mat_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridPiersPrimCompSeverity).HasColumnName("FBRID_Piers_Prim_Comp_Severity");

                entity.Property(e => e.FbridSidewalksAppSlab)
                    .HasColumnName("FBRID_Sidewalks_App_Slab")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridSidewalksAppSlabCode)
                    .HasColumnName("FBRID_Sidewalks_App_Slab_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridSidewalksAppSlabDistress)
                    .HasColumnName("FBRID_Sidewalks_App_Slab_Distress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridSidewalksAppSlabDistressOthers)
                    .HasColumnName("FBRID_Sidewalks_App_Slab_Distress_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridSidewalksAppSlabInspCode)
                    .HasColumnName("FBRID_Sidewalks_App_Slab_Insp_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FbridSidewalksAppSlabSeverity).HasColumnName("FBRID_Sidewalks_App_Slab_Severity");

                entity.Property(e => e.FbridSlopeRetainWall)
                    .HasColumnName("FBRID_Slope_Retain_wall")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridSlopeRetainWallCode)
                    .HasColumnName("FBRID_Slope_Retain_wall_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridSlopeRetainWallDistress)
                    .HasColumnName("FBRID_Slope_Retain_wall_Distress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridSlopeRetainWallDistressOthers)
                    .HasColumnName("FBRID_Slope_Retain_wall_Distress_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridSlopeRetainWallInspCode)
                    .HasColumnName("FBRID_Slope_Retain_wall_Insp_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FbridSlopeRetainWallSeverity).HasColumnName("FBRID_Slope_Retain_wall_Severity");

                entity.Property(e => e.FbridSubmitSts).HasColumnName("FBRID_SUBMIT_STS");

                entity.Property(e => e.FbridUtilities)
                    .HasColumnName("FBRID_Utilities")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridUtilitiesCode)
                    .HasColumnName("FBRID_Utilities_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridUtilitiesDistress)
                    .HasColumnName("FBRID_Utilities_Distress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridUtilitiesDistressOthers)
                    .HasColumnName("FBRID_Utilities_Distress_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridUtilitiesInspCode)
                    .HasColumnName("FBRID_Utilities_Insp_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FbridUtilitiesSeverity).HasColumnName("FBRID_Utilities_Severity");

                entity.Property(e => e.FbridWaterDownpipe)
                    .HasColumnName("FBRID_Water_Downpipe")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridWaterDownpipeCode)
                    .HasColumnName("FBRID_Water_Downpipe_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridWaterDownpipeDistress)
                    .HasColumnName("FBRID_Water_Downpipe_Distress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridWaterDownpipeDistressOthers)
                    .HasColumnName("FBRID_Water_Downpipe_Distress_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridWaterDownpipeInspCode)
                    .HasColumnName("FBRID_Water_Downpipe_Insp_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FbridWaterDownpipeSeverity).HasColumnName("FBRID_Water_Downpipe_Severity");

                entity.Property(e => e.FbridWaterway)
                    .HasColumnName("FBRID_Waterway")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridWaterwayCode)
                    .HasColumnName("FBRID_Waterway_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridWaterwayDistress)
                    .HasColumnName("FBRID_Waterway_Distress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbridWaterwayDistressOthers)
                    .HasColumnName("FBRID_Waterway_Distress_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbridWaterwayInspCode)
                    .HasColumnName("FBRID_Waterway_Insp_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FbridWaterwaySeverity).HasColumnName("FBRID_Waterway_Severity");

                entity.HasOne(d => d.FbridFbrihPkRefNoNavigation)
                    .WithMany(p => p.RmFormB1b2BrInsDtl)
                    .HasForeignKey(d => d.FbridFbrihPkRefNo)
                    .HasConstraintName("FK__RM_FormB1__FBRID__0D0FEE32");
            });

            modelBuilder.Entity<RmFormB1b2BrInsHdr>(entity =>
            {
                entity.HasKey(e => e.FbrihPkRefNo)
                    .HasName("PK__RM_FormB__FBB0931FB61160F7");

                entity.ToTable("RM_FormB1B2_BR_Ins_HDR");

                entity.Property(e => e.FbrihPkRefNo).HasColumnName("FBRIH_PK_Ref_No");

                entity.Property(e => e.FbrihActiveYn)
                    .IsRequired()
                    .HasColumnName("FBRIH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FbrihAiAbutType)
                    .HasColumnName("FBRIH_AI_Abut_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiAssetId)
                    .HasColumnName("FBRIH_AI_Asset_id")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiBearingType)
                    .HasColumnName("FBRIH_AI_Bearing_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiDeckType)
                    .HasColumnName("FBRIH_AI_Deck_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiDivCode)
                    .HasColumnName("FBRIH_AI_Div_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiExpanJointCount).HasColumnName("FBRIH_AI_Expan_Joint_Count");

                entity.Property(e => e.FbrihAiExpanType)
                    .HasColumnName("FBRIH_AI_Expan_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiGpsEasting).HasColumnName("FBRIH_AI_GPS_Easting");

                entity.Property(e => e.FbrihAiGpsNorthing).HasColumnName("FBRIH_AI_GPS_Northing");

                entity.Property(e => e.FbrihAiLaneCnt).HasColumnName("FBRIH_AI_Lane_Cnt");

                entity.Property(e => e.FbrihAiLength).HasColumnName("FBRIH_AI_Length");

                entity.Property(e => e.FbrihAiLengthSpan).HasColumnName("FBRIH_AI_length_Span");

                entity.Property(e => e.FbrihAiLocChKm).HasColumnName("FBRIH_AI_Loc_CH_KM");

                entity.Property(e => e.FbrihAiLocChM)
                    .HasColumnName("FBRIH_AI_Loc_CH_M")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiMedian).HasColumnName("FBRIH_AI_median");

                entity.Property(e => e.FbrihAiParapetType)
                    .HasColumnName("FBRIH_AI_Parapet_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiPierType)
                    .HasColumnName("FBRIH_AI_Pier_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiPkRefNo).HasColumnName("FBRIH_AI_PK_Ref_No");

                entity.Property(e => e.FbrihAiRdCode)
                    .HasColumnName("FBRIH_AI_Rd_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiRdName)
                    .HasColumnName("FBRIH_AI_Rd_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiRiverName)
                    .HasColumnName("FBRIH_AI_River_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiRmuName)
                    .HasColumnName("FBRIH_AI_RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiSpanCnt).HasColumnName("FBRIH_AI_Span_Cnt");

                entity.Property(e => e.FbrihAiStrucCode)
                    .HasColumnName("FBRIH_AI_Struc_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiStrucSuper)
                    .HasColumnName("FBRIH_AI_Struc_Super")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAiWalkway).HasColumnName("FBRIH_AI_Walkway");

                entity.Property(e => e.FbrihAiWidth).HasColumnName("FBRIH_AI_Width");

                entity.Property(e => e.FbrihAiWidthLane).HasColumnName("FBRIH_AI_Width_Lane");

                entity.Property(e => e.FbrihAuditLog).HasColumnName("FBRIH_AuditLog");

                entity.Property(e => e.FbrihAuthDefFeedback)
                    .HasColumnName("FBRIH_Auth_Def_Feedback")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAuthDefGenCom)
                    .HasColumnName("FBRIH_Auth_Def_Gen_Com")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihAuthDefObs)
                    .HasColumnName("FBRIH_Auth_Def_Obs")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihBridgeConditionRat).HasColumnName("FBRIH_Bridge_Condition_Rat");

                entity.Property(e => e.FbrihCInspRefNo)
                    .HasColumnName("FBRIH_C_Insp_Ref_No")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihCrBy).HasColumnName("FBRIH_CR_By");

                entity.Property(e => e.FbrihCrDt)
                    .HasColumnName("FBRIH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FbrihDtAud)
                    .HasColumnName("FBRIH_DT_Aud")
                    .HasColumnType("datetime");

                entity.Property(e => e.FbrihDtOfInsp)
                    .HasColumnName("FBRIH_DT_Of_Insp")
                    .HasColumnType("datetime");

                entity.Property(e => e.FbrihModBy).HasColumnName("FBRIH_Mod_By");

                entity.Property(e => e.FbrihModDt)
                    .HasColumnName("FBRIH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FbrihRecordNo).HasColumnName("FBRIH_Record_No");

                entity.Property(e => e.FbrihReqFurtherInv).HasColumnName("FBRIH_Req_Further_Inv");

                entity.Property(e => e.FbrihSerProviderDefFeedback)
                    .HasColumnName("FBRIH_Ser_provider_Def_Feedback")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihSerProviderDefGenCom)
                    .HasColumnName("FBRIH_Ser_provider_Def_Gen_Com")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihSerProviderDefObs)
                    .HasColumnName("FBRIH_Ser_provider_Def_Obs")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihSerProviderInsDt)
                    .HasColumnName("FBRIH_Ser_provider_Ins_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FbrihSerProviderUserDesignation)
                    .HasColumnName("FBRIH_Ser_provider_User_Designation")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihSerProviderUserId).HasColumnName("FBRIH_Ser_provider_User_id");

                entity.Property(e => e.FbrihSerProviderUserName)
                    .HasColumnName("FBRIH_Ser_provider_User_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihSignpathAud).HasColumnName("FBRIH_Signpath_Aud");

                entity.Property(e => e.FbrihSignpathSerProvider).HasColumnName("FBRIH_Signpath_Ser_provider");

                entity.Property(e => e.FbrihStatus)
                    .IsRequired()
                    .HasColumnName("FBRIH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FbrihSubmitSts).HasColumnName("FBRIH_SUBMIT_STS");

                entity.Property(e => e.FbrihUserDesignationAud)
                    .HasColumnName("FBRIH_User_Designation_Aud")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihUserIdAud).HasColumnName("FBRIH_User_id_Aud");

                entity.Property(e => e.FbrihUserNameAud)
                    .HasColumnName("FBRIH_User_Name_Aud")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbrihYearOfInsp).HasColumnName("FBRIH_Year_Of_Insp");

                entity.HasOne(d => d.FbrihAiPkRefNoNavigation)
                    .WithMany(p => p.RmFormB1b2BrInsHdr)
                    .HasForeignKey(d => d.FbrihAiPkRefNo)
                    .HasConstraintName("FK__RM_FormB1__FBRIH__6C6E1476");
            });

            modelBuilder.Entity<RmFormB1b2BrInsImage>(entity =>
            {
                entity.HasKey(e => e.FbriPkRefNo)
                    .HasName("PK__RM_FormB__AE33A08A279F69FD");

                entity.ToTable("RM_FormB1B2_BR_Ins_Image");

                entity.Property(e => e.FbriPkRefNo).HasColumnName("FBRI_PK_Ref_No");

                entity.Property(e => e.FbriActiveYn).HasColumnName("FBRI_Active_YN");

                entity.Property(e => e.FbriCrBy).HasColumnName("FBRI_CR_By");

                entity.Property(e => e.FbriCrDt)
                    .HasColumnName("FBRI_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FbriFbrihPkRefNo).HasColumnName("FBRI_FBRIH_PK_Ref_No");

                entity.Property(e => e.FbriImageFilenameSys)
                    .HasColumnName("FBRI_Image_Filename_Sys")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbriImageFilenameUpload)
                    .HasColumnName("FBRI_Image_Filename_Upload")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbriImageSrno).HasColumnName("FBRI_Image_SRNO");

                entity.Property(e => e.FbriImageTypeCode)
                    .HasColumnName("FBRI_Image_Type_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbriImageUserFilePath).HasColumnName("FBRI_image_user_filePath");

                entity.Property(e => e.FbriImgRefId)
                    .HasColumnName("FBRI_Img_Ref_ID")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FbriModBy).HasColumnName("FBRI_Mod_By");

                entity.Property(e => e.FbriModDt)
                    .HasColumnName("FBRI_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FbriSubmitSts).HasColumnName("FBRI_SUBMIT_STS");

                entity.HasOne(d => d.FbriFbrihPkRefNoNavigation)
                    .WithMany(p => p.RmFormB1b2BrInsImage)
                    .HasForeignKey(d => d.FbriFbrihPkRefNo)
                    .HasConstraintName("FK__RM_FormB1__FBRI___11D4A34F");
            });

            modelBuilder.Entity<RmFormCvInsDtl>(entity =>
            {
                entity.HasKey(e => e.FcvidPkRefNo)
                    .HasName("PK__RM_Form___B5D09FE2B392DD4D");

                entity.ToTable("RM_Form_CV_Ins_DTL");

                entity.Property(e => e.FcvidPkRefNo).HasColumnName("FCVID_PK_Ref_No");

                entity.Property(e => e.FcvidActiveYn).HasColumnName("FCVID_Active_YN");

                entity.Property(e => e.FcvidCrBy).HasColumnName("FCVID_CR_By");

                entity.Property(e => e.FcvidCrDt)
                    .HasColumnName("FCVID_CR_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FcvidDistress)
                    .HasColumnName("FCVID_Distress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FcvidDistressOthers)
                    .HasColumnName("FCVID_Distress_Others")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcvidFcvihPkRefNo).HasColumnName("FCVID_FCVIH_PK_Ref_No");

                entity.Property(e => e.FcvidIimPkRefNo).HasColumnName("FCVID_IIM_PK_Ref_No");

                entity.Property(e => e.FcvidIimdPkRefNo).HasColumnName("FCVID_IIMD_PK_Ref_No");

                entity.Property(e => e.FcvidInspCode)
                    .HasColumnName("FCVID_Insp_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FcvidInspCodeDesc)
                    .HasColumnName("FCVID_Insp_Code_Desc")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FcvidModBy).HasColumnName("FCVID_Mod_By");

                entity.Property(e => e.FcvidModDt)
                    .HasColumnName("FCVID_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FcvidSeverity).HasColumnName("FCVID_Severity");

                entity.Property(e => e.FcvidSubmitSts)
                    .IsRequired()
                    .HasColumnName("FCVID_SUBMIT_STS")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FcvidFcvihPkRefNoNavigation)
                    .WithMany(p => p.RmFormCvInsDtl)
                    .HasForeignKey(d => d.FcvidFcvihPkRefNo)
                    .HasConstraintName("FK__RM_Form_C__FCVID__056ECC6A");

                entity.HasOne(d => d.FcvidIimPkRefNoNavigation)
                    .WithMany(p => p.RmFormCvInsDtl)
                    .HasForeignKey(d => d.FcvidIimPkRefNo)
                    .HasConstraintName("FK__RM_Form_C__FCVID__047AA831");

                entity.HasOne(d => d.FcvidIimdPkRefNoNavigation)
                    .WithMany(p => p.RmFormCvInsDtl)
                    .HasForeignKey(d => d.FcvidIimdPkRefNo)
                    .HasConstraintName("FK__RM_Form_C__FCVID__0662F0A3");
            });

            modelBuilder.Entity<RmFormCvInsHdr>(entity =>
            {
                entity.HasKey(e => e.FcvihPkRefNo)
                    .HasName("PK__RM_Form___A6A7CCCB5FB30581");

                entity.ToTable("RM_Form_CV_Ins_HDR");

                entity.Property(e => e.FcvihPkRefNo).HasColumnName("FCVIH_PK_Ref_No");

                entity.Property(e => e.FcvihAccessibility).HasColumnName("FCVIH_Accessibility");

                entity.Property(e => e.FcvihActiveYn)
                    .IsRequired()
                    .HasColumnName("FCVIH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FcvihAiAssetId)
                    .HasColumnName("FCVIH_AI_Asset_id")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAiBarrelNo).HasColumnName("FCVIH_AI_Barrel_No");

                entity.Property(e => e.FcvihAiCatchArea).HasColumnName("FCVIH_AI_Catch_Area");

                entity.Property(e => e.FcvihAiDesignFlow).HasColumnName("FCVIH_AI_Design_Flow");

                entity.Property(e => e.FcvihAiDivCode)
                    .HasColumnName("FCVIH_AI_Div_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAiFinRdLevel).HasColumnName("FCVIH_AI_Fin_Rd_Level");

                entity.Property(e => e.FcvihAiGpsEasting).HasColumnName("FCVIH_AI_GPS_Easting");

                entity.Property(e => e.FcvihAiGpsNorthing).HasColumnName("FCVIH_AI_GPS_Northing");

                entity.Property(e => e.FcvihAiGrpType)
                    .HasColumnName("FCVIH_AI_Grp_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAiIntelLevel).HasColumnName("FCVIH_AI_Intel_Level");

                entity.Property(e => e.FcvihAiIntelStruc)
                    .HasColumnName("FCVIH_AI_Intel_Struc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAiLength).HasColumnName("FCVIH_AI_Length");

                entity.Property(e => e.FcvihAiLocChKm).HasColumnName("FCVIH_AI_Loc_CH_KM");

                entity.Property(e => e.FcvihAiLocChM)
                    .HasColumnName("FCVIH_AI_Loc_CH_M")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAiMaterial)
                    .HasColumnName("FCVIH_AI_Material")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAiOutletLevel).HasColumnName("FCVIH_AI_Outlet_Level");

                entity.Property(e => e.FcvihAiOutletStruc)
                    .HasColumnName("FCVIH_AI_Outlet_Struc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAiPkRefNo).HasColumnName("FCVIH_AI_PK_Ref_No");

                entity.Property(e => e.FcvihAiPrecastSitu)
                    .HasColumnName("FCVIH_AI_Precast_Situ")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAiRdCode)
                    .HasColumnName("FCVIH_AI_Rd_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAiRdName)
                    .HasColumnName("FCVIH_AI_Rd_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAiRmuName)
                    .HasColumnName("FCVIH_AI_RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAiSkew).HasColumnName("FCVIH_AI_Skew");

                entity.Property(e => e.FcvihAiStrucCode)
                    .HasColumnName("FCVIH_AI_Struc_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAuditLog).HasColumnName("FCVIH_AuditLog");

                entity.Property(e => e.FcvihAuthDefFeedback)
                    .HasColumnName("FCVIH_Auth_Def_Feedback")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAuthDefGenCom)
                    .HasColumnName("FCVIH_Auth_Def_Gen_Com")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihAuthDefObs)
                    .HasColumnName("FCVIH_Auth_Def_Obs")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihCInspRefNo)
                    .HasColumnName("FCVIH_C_Insp_Ref_No")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihCrBy).HasColumnName("FCVIH_CR_By");

                entity.Property(e => e.FcvihCrDt)
                    .HasColumnName("FCVIH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FcvihCulvertConditionRat).HasColumnName("FCVIH_Culvert_Condition_Rat");

                entity.Property(e => e.FcvihDtAud)
                    .HasColumnName("FCVIH_DT_Aud")
                    .HasColumnType("datetime");

                entity.Property(e => e.FcvihDtOfInsp)
                    .HasColumnName("FCVIH_DT_Of_Insp")
                    .HasColumnType("datetime");

                entity.Property(e => e.FcvihModBy).HasColumnName("FCVIH_Mod_By");

                entity.Property(e => e.FcvihModDt)
                    .HasColumnName("FCVIH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FcvihPotentialHazards).HasColumnName("FCVIH_Potential_Hazards");

                entity.Property(e => e.FcvihPrkPosition).HasColumnName("FCVIH_Prk_Position");

                entity.Property(e => e.FcvihRecordNo).HasColumnName("FCVIH_Record_No");

                entity.Property(e => e.FcvihReqFurtherInv).HasColumnName("FCVIH_Req_Further_Inv");

                entity.Property(e => e.FcvihSerProviderDefFeedback)
                    .HasColumnName("FCVIH_Ser_provider_Def_Feedback")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihSerProviderDefGenCom)
                    .HasColumnName("FCVIH_Ser_provider_Def_Gen_Com")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihSerProviderDefObs)
                    .HasColumnName("FCVIH_Ser_provider_Def_Obs")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihSerProviderInsDt)
                    .HasColumnName("FCVIH_Ser_provider_Ins_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FcvihSerProviderUserDesignation)
                    .HasColumnName("FCVIH_Ser_provider_User_Designation")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihSerProviderUserId).HasColumnName("FCVIH_Ser_provider_User_id");

                entity.Property(e => e.FcvihSerProviderUserName)
                    .HasColumnName("FCVIH_Ser_provider_User_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihSignpathAud).HasColumnName("FCVIH_Signpath_Aud");

                entity.Property(e => e.FcvihSignpathSerProvider).HasColumnName("FCVIH_Signpath_Ser_provider");

                entity.Property(e => e.FcvihStatus)
                    .IsRequired()
                    .HasColumnName("FCVIH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FcvihSubmitSts).HasColumnName("FCVIH_SUBMIT_STS");

                entity.Property(e => e.FcvihUserDesignationAud)
                    .HasColumnName("FCVIH_User_Designation_Aud")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihUserIdAud).HasColumnName("FCVIH_User_id_Aud");

                entity.Property(e => e.FcvihUserNameAud)
                    .HasColumnName("FCVIH_User_Name_Aud")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcvihYearOfInsp).HasColumnName("FCVIH_Year_Of_Insp");

                entity.HasOne(d => d.FcvihAiPkRefNoNavigation)
                    .WithMany(p => p.RmFormCvInsHdr)
                    .HasForeignKey(d => d.FcvihAiPkRefNo)
                    .HasConstraintName("FK__RM_Form_C__FCVIH__075714DC");
            });

            modelBuilder.Entity<RmFormCvInsImage>(entity =>
            {
                entity.HasKey(e => e.FcviPkRefNo)
                    .HasName("PK__RM_Form___7E761F096D142282");

                entity.ToTable("RM_Form_CV_Ins_Image");

                entity.Property(e => e.FcviPkRefNo).HasColumnName("FCVI_PK_Ref_No");

                entity.Property(e => e.FcviActiveYn)
                    .IsRequired()
                    .HasColumnName("FCVI_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FcviCrBy).HasColumnName("FCVI_CR_By");

                entity.Property(e => e.FcviCrDt)
                    .HasColumnName("FCVI_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FcviFcvihPkRefNo).HasColumnName("FCVI_FCVIH_PK_Ref_No");

                entity.Property(e => e.FcviImageFilenameSys)
                    .HasColumnName("FCVI_Image_Filename_Sys")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcviImageFilenameUpload)
                    .HasColumnName("FCVI_Image_Filename_Upload")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcviImageSrno).HasColumnName("FCVI_Image_SRNO");

                entity.Property(e => e.FcviImageTypeCode)
                    .HasColumnName("FCVI_Image_Type_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcviImageUserFilePath).HasColumnName("FCVI_image_user_filePath");

                entity.Property(e => e.FcviImgRefId)
                    .HasColumnName("FCVI_Img_Ref_ID")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcviModBy).HasColumnName("FCVI_Mod_By");

                entity.Property(e => e.FcviModDt)
                    .HasColumnName("FCVI_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FcviSubmitSts).HasColumnName("FCVI_SUBMIT_STS");

                entity.HasOne(d => d.FcviFcvihPkRefNoNavigation)
                    .WithMany(p => p.RmFormCvInsImage)
                    .HasForeignKey(d => d.FcviFcvihPkRefNo)
                    .HasConstraintName("FK__RM_Form_C__FCVI___084B3915");
            });

            modelBuilder.Entity<RmFormDDtl>(entity =>
            {
                entity.HasKey(e => e.FddPkRefNo)
                    .HasName("PK__RM_FormD__4151D5A82852AB26");

                entity.ToTable("RM_FormD_DTL");

                entity.Property(e => e.FddPkRefNo).HasColumnName("FDD_PK_Ref_No");

                entity.Property(e => e.FddActCode).HasColumnName("FDD_ACT_Code");

                entity.Property(e => e.FddActiveYn)
                    .IsRequired()
                    .HasColumnName("FDD_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FddCrBy)
                    .HasColumnName("FDD_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FddCrDt)
                    .HasColumnName("FDD_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FddFdhPkRefNo).HasColumnName("FDD_FDH_PK_Ref_No");

                entity.Property(e => e.FddFrmCh).HasColumnName("FDD_FRM_CH");

                entity.Property(e => e.FddFrmChDeci).HasColumnName("FDD_FRM_CH_Deci");

                entity.Property(e => e.FddFxhPkRefNo).HasColumnName("FDD_FXH_PK_Ref_No");

                entity.Property(e => e.FddGenRemarks)
                    .HasColumnName("FDD_Gen_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FddHeight).HasColumnName("FDD_Height");

                entity.Property(e => e.FddLength).HasColumnName("FDD_Length");

                entity.Property(e => e.FddModBy)
                    .HasColumnName("FDD_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FddModDt)
                    .HasColumnName("FDD_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FddProdQty).HasColumnName("FDD_PROD_QTY");

                entity.Property(e => e.FddProdUnit)
                    .HasColumnName("FDD_PROD_Unit")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FddRefId)
                    .HasColumnName("FDD_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FddRemarks)
                    .HasColumnName("FDD_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FddRoadCode)
                    .HasColumnName("FDD_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FddSiteRef)
                    .HasColumnName("FDD_Site_Ref")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FddSourceRefId)
                    .HasColumnName("FDD_Source_Ref_ID")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FddSourceType)
                    .HasColumnName("FDD_Source_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FddSrno).HasColumnName("FDD_SRNO");

                entity.Property(e => e.FddSubmitSts).HasColumnName("FDD_SUBMIT_STS");

                entity.Property(e => e.FddTimeArr)
                    .HasColumnName("FDD_TIME_ARR")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FddTimeDep)
                    .HasColumnName("FDD_TIME_DEP")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FddToCh).HasColumnName("FDD_To_CH");

                entity.Property(e => e.FddToChDeci).HasColumnName("FDD_To_CH_Deci");

                entity.Property(e => e.FddUnit)
                    .HasColumnName("FDD_Unit")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FddWidth).HasColumnName("FDD_Width");

                entity.Property(e => e.FddWorkSts)
                    .HasColumnName("FDD_Work_STS")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.HasOne(d => d.FddFdhPkRefNoNavigation)
                    .WithMany(p => p.RmFormDDtl)
                    .HasForeignKey(d => d.FddFdhPkRefNo)
                    .HasConstraintName("FK__RM_FormD___FDD_F__5D95E53A");
            });

            modelBuilder.Entity<RmFormDEquipDtl>(entity =>
            {
                entity.HasKey(e => e.FdedPkRefNo)
                    .HasName("PK__RM_FormD__D836D4156829A214");

                entity.ToTable("RM_FormD_Equip_DTL");

                entity.Property(e => e.FdedPkRefNo).HasColumnName("FDED_PK_Ref_No");

                entity.Property(e => e.FdedActiveYn)
                    .IsRequired()
                    .HasColumnName("FDED_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FdedCodeDesc)
                    .HasColumnName("FDED_Code_Desc")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FdedCrBy)
                    .HasColumnName("FDED_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdedCrDt)
                    .HasColumnName("FDED_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdedEqpCode)
                    .HasColumnName("FDED_EQP_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdedEqpDesc)
                    .HasColumnName("FDED_EQP_Desc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdedEqpQty)
                    .HasColumnName("FDED_EQP_QTY")
                    .HasColumnType("decimal(6, 3)");

                entity.Property(e => e.FdedEqpUnit)
                    .HasColumnName("FDED_EQP_Unit")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdedFdhPkRefNo).HasColumnName("FDED_FDH_PK_Ref_No");

                entity.Property(e => e.FdedModBy)
                    .HasColumnName("FDED_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdedModDt)
                    .HasColumnName("FDED_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdedSrno).HasColumnName("FDED_SRNO");

                entity.Property(e => e.FdedSubmitSts).HasColumnName("FDED_SUBMIT_STS");

                entity.HasOne(d => d.FdedFdhPkRefNoNavigation)
                    .WithMany(p => p.RmFormDEquipDtl)
                    .HasForeignKey(d => d.FdedFdhPkRefNo)
                    .HasConstraintName("FK__RM_FormD___FDED___5E8A0973");
            });

            modelBuilder.Entity<RmFormDHdr>(entity =>
            {
                entity.HasKey(e => e.FdhPkRefNo)
                    .HasName("PK__RM_FormD__183617DC2F2AACD7");

                entity.ToTable("RM_FormD_HDR");

                entity.Property(e => e.FdhPkRefNo).HasColumnName("FDH_PK_Ref_No");

                entity.Property(e => e.FdhActiveYn)
                    .HasColumnName("FDH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FdhAuditLog).HasColumnName("FDH_AuditLog");

                entity.Property(e => e.FdhContNo).HasColumnName("FDH_CONT_No");

                entity.Property(e => e.FdhCrBy)
                    .HasColumnName("FDH_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdhCrDt)
                    .HasColumnName("FDH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdhCrewSupName)
                    .HasColumnName("FDH_Crew_Sup_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdhCrewUnit)
                    .HasColumnName("FDH_Crew_unit")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdhDate)
                    .HasColumnName("FDH_Date")
                    .HasColumnType("date");

                entity.Property(e => e.FdhDay)
                    .HasColumnName("FDH_Day")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FdhDesignationAgrdSo)
                    .HasColumnName("FDH_Designation_agrd_SO")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdhDesignationPrcdSo)
                    .HasColumnName("FDH_Designation_prcd_SO")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdhDesignationPrp)
                    .HasColumnName("FDH_Designation_PRP")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdhDesignationVer)
                    .HasColumnName("FDH_Designation_VER")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdhDesignationVerSo)
                    .HasColumnName("FDH_Designation_VER_SO")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdhDesignationVet)
                    .HasColumnName("FDH_Designation_VET")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdhDivName)
                    .HasColumnName("FDH_DIV_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdhDtAgrdSo)
                    .HasColumnName("FDH_DT_agrd_SO")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdhDtPrcdSo)
                    .HasColumnName("FDH_DT_prcd_SO")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdhDtPrp)
                    .HasColumnName("FDH_DT_PRP")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdhDtRcvdAuth)
                    .HasColumnName("FDH_DT_RCVD_AUTH")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdhDtSubAuth)
                    .HasColumnName("FDH_DT_SUB_AUTH")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdhDtVer)
                    .HasColumnName("FDH_DT_VER")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdhDtVerSo)
                    .HasColumnName("FDH_DT_VER_SO")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdhDtVet)
                    .HasColumnName("FDH_DT_VET")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdhDtVetAuth)
                    .HasColumnName("FDH_DT_VET_AUTH")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdhModBy)
                    .HasColumnName("FDH_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdhModDt)
                    .HasColumnName("FDH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdhMonth).HasColumnName("FDH_Month");

                entity.Property(e => e.FdhRcvdAuthSts)
                    .HasColumnName("FDH_RCVD_AUTH_STS")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdhRefId)
                    .HasColumnName("FDH_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdhRmu)
                    .HasColumnName("FDH_RMU")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdhRoadCode)
                    .HasColumnName("FDH_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdhSignAgrdSo).HasColumnName("FDH_SIgn_agrd_SO");

                entity.Property(e => e.FdhSignPrcdSo).HasColumnName("FDH_SIgn_prcd_SO");

                entity.Property(e => e.FdhSignPrp).HasColumnName("FDH_SIgn_PRP");

                entity.Property(e => e.FdhSignVer).HasColumnName("FDH_SIgn_VER");

                entity.Property(e => e.FdhSignVerSo).HasColumnName("FDH_SIgn_VER_SO");

                entity.Property(e => e.FdhSignVet).HasColumnName("FDH_SIgn_VET");

                entity.Property(e => e.FdhSn)
                    .HasColumnName("FDH_SN")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdhStatus)
                    .IsRequired()
                    .HasColumnName("FDH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FdhSubAuthSts)
                    .HasColumnName("FDH_SUB_AUTH_STS")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdhSubmitSts).HasColumnName("FDH_SUBMIT_STS");

                entity.Property(e => e.FdhUseridAgrdSo)
                    .HasColumnName("FDH_Userid_agrd_SO")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdhUseridPrcdSo)
                    .HasColumnName("FDH_Userid_prcd_SO")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdhUseridPrp)
                    .HasColumnName("FDH_Userid_PRP")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdhUseridVer)
                    .HasColumnName("FDH_Userid_VER")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdhUseridVerSo)
                    .HasColumnName("FDH_Userid_VER_SO")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdhUseridVet)
                    .HasColumnName("FDH_Userid_VET")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdhUsernameAgrdSo)
                    .HasColumnName("FDH_Username_agrd_SO")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdhUsernamePrcdSo)
                    .HasColumnName("FDH_Username_prcd_SO")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdhUsernamePrp)
                    .HasColumnName("FDH_Username_PRP")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdhUsernameVer)
                    .HasColumnName("FDH_Username_VER")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdhUsernameVerSo)
                    .HasColumnName("FDH_Username_VER_SO")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdhUsernameVet)
                    .HasColumnName("FDH_Username_VET")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdhVetAuthSts)
                    .HasColumnName("FDH_VET_AUTH_STS")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdhWeekNo).HasColumnName("FDH_Week_No");

                entity.Property(e => e.FdhYear).HasColumnName("FDH_Year");
            });

            modelBuilder.Entity<RmFormDLabourDtl>(entity =>
            {
                entity.HasKey(e => e.FdldPkRefNo)
                    .HasName("PK__RM_FormD__A24A9F034E7ABDC6");

                entity.ToTable("RM_FormD_Labour_DTL");

                entity.Property(e => e.FdldPkRefNo).HasColumnName("FDLD_PK_Ref_No");

                entity.Property(e => e.FdldActiveYn)
                    .IsRequired()
                    .HasColumnName("FDLD_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FdldCodeDesc)
                    .HasColumnName("FDLD_Code_Desc")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FdldCrBy)
                    .HasColumnName("FDLD_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdldCrDt)
                    .HasColumnName("FDLD_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdldFdhPkRefNo).HasColumnName("FDLD_FDH_PK_Ref_No");

                entity.Property(e => e.FdldLabCode)
                    .HasColumnName("FDLD_LAB_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdldLabDesc)
                    .HasColumnName("FDLD_LAB_Desc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdldLabQty)
                    .HasColumnName("FDLD_LAB_QTY")
                    .HasColumnType("decimal(6, 3)");

                entity.Property(e => e.FdldLabUnit)
                    .HasColumnName("FDLD_LAB_Unit")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdldModBy)
                    .HasColumnName("FDLD_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdldModDt)
                    .HasColumnName("FDLD_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdldSrno).HasColumnName("FDLD_SRNO");

                entity.Property(e => e.FdldSubmitSts).HasColumnName("FDLD_SUBMIT_STS");

                entity.HasOne(d => d.FdldFdhPkRefNoNavigation)
                    .WithMany(p => p.RmFormDLabourDtl)
                    .HasForeignKey(d => d.FdldFdhPkRefNo)
                    .HasConstraintName("FK__RM_FormD___FDLD___5F7E2DAC");
            });

            modelBuilder.Entity<RmFormDMaterialDtl>(entity =>
            {
                entity.HasKey(e => e.FdmdPkRefNo)
                    .HasName("PK__RM_FormD__2D441C63AA51481E");

                entity.ToTable("RM_FormD_Material_DTL");

                entity.Property(e => e.FdmdPkRefNo).HasColumnName("FDMD_PK_Ref_No");

                entity.Property(e => e.FdmdActiveYn)
                    .IsRequired()
                    .HasColumnName("FDMD_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FdmdCodeDesc)
                    .HasColumnName("FDMD_Code_Desc")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FdmdCrBy)
                    .HasColumnName("FDMD_CR_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FdmdCrDt)
                    .HasColumnName("FDMD_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdmdFdhPkRefNo).HasColumnName("FDMD_FDH_PK_Ref_No");

                entity.Property(e => e.FdmdModBy)
                    .HasColumnName("FDMD_Mod_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FdmdModDt)
                    .HasColumnName("FDMD_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdmdMtCode)
                    .HasColumnName("FDMD_MT_Code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FdmdMtDesc)
                    .HasColumnName("FDMD_MT_Desc")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FdmdMtQty)
                    .HasColumnName("FDMD_MT_QTY")
                    .HasColumnType("decimal(6, 3)");

                entity.Property(e => e.FdmdMtUnit)
                    .HasColumnName("FDMD_MT_Unit")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FdmdSrno).HasColumnName("FDMD_SRNO");

                entity.Property(e => e.FdmdSubmitSts).HasColumnName("FDMD_SUBMIT_STS");

                entity.HasOne(d => d.FdmdFdhPkRefNoNavigation)
                    .WithMany(p => p.RmFormDMaterialDtl)
                    .HasForeignKey(d => d.FdmdFdhPkRefNo)
                    .HasConstraintName("FK__RM_FormD___FDMD___31B762FC");
            });

            modelBuilder.Entity<RmFormDownloadTbJoin>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RM_Form_Download_TB_Join");

                entity.Property(e => e.FdtFormType)
                    .HasColumnName("FDT_Form_Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FdtPk)
                    .HasColumnName("FDT_PK")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FdtTableTypeHdrDtl)
                    .HasColumnName("FDT_Table_Type_HDR_DTL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdtTbJoins)
                    .HasColumnName("FDT_TB_Joins")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FdtTblPkFldName)
                    .HasColumnName("FDT_TBL_PK_FLD_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmFormDownloadUse>(entity =>
            {
                entity.HasKey(e => e.FduPkRefNo)
                    .HasName("PK__RM_Form___049F05813A98D099");

                entity.ToTable("RM_Form_Download_Use");

                entity.Property(e => e.FduPkRefNo).HasColumnName("FDU_PK_Ref_No");

                entity.Property(e => e.Endindex).HasColumnName("endindex");

                entity.Property(e => e.FduActiveYn)
                    .IsRequired()
                    .HasColumnName("FDU_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FduAppendOverwrite)
                    .HasColumnName("FDU_Append_Overwrite")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FduCrBy)
                    .HasColumnName("FDU_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FduCrDt)
                    .HasColumnName("FDU_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FduExcelCellNo)
                    .HasColumnName("FDU_Excel_Cell_No")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FduExcelColumnNo).HasColumnName("FDU_Excel_Column_No");

                entity.Property(e => e.FduExcelRowNo).HasColumnName("FDU_Excel_Row_No");

                entity.Property(e => e.FduFormType)
                    .HasColumnName("FDU_Form_Type")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.FduHeaderName)
                    .HasColumnName("FDU_Header_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduModBy)
                    .HasColumnName("FDU_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FduModDt)
                    .HasColumnName("FDU_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FduSeperator)
                    .HasColumnName("FDU_Seperator")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FduSubmitSts).HasColumnName("FDU_SUBMIT_STS");

                entity.Property(e => e.FduTableFieldName)
                    .HasColumnName("FDU_Table_Field_Name")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FduTableName)
                    .HasColumnName("FDU_Table_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduTableTypeHdrDtl)
                    .HasColumnName("FDU_Table_Type_HDR_DTL")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Maxlength).HasColumnName("maxlength");

                entity.Property(e => e.Mutilpleline)
                    .HasColumnName("mutilpleline")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Startindex).HasColumnName("startindex");
            });

            modelBuilder.Entity<RmFormDownloadUse21122020>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RM_Form_Download_Use_21122020");

                entity.Property(e => e.Endindex).HasColumnName("endindex");

                entity.Property(e => e.FduActiveYn).HasColumnName("FDU_Active_YN");

                entity.Property(e => e.FduAppendOverwrite)
                    .HasColumnName("FDU_Append_Overwrite")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FduCrBy)
                    .HasColumnName("FDU_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FduCrDt)
                    .HasColumnName("FDU_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FduExcelCellNo)
                    .HasColumnName("FDU_Excel_Cell_No")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FduExcelColumnNo).HasColumnName("FDU_Excel_Column_No");

                entity.Property(e => e.FduExcelRowNo).HasColumnName("FDU_Excel_Row_No");

                entity.Property(e => e.FduFormType)
                    .HasColumnName("FDU_Form_Type")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.FduHeaderName)
                    .HasColumnName("FDU_Header_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduModBy)
                    .HasColumnName("FDU_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FduModDt)
                    .HasColumnName("FDU_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FduPkRefNo)
                    .HasColumnName("FDU_PK_Ref_No")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FduSeperator)
                    .HasColumnName("FDU_Seperator")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduSubmitSts).HasColumnName("FDU_SUBMIT_STS");

                entity.Property(e => e.FduTableFieldName)
                    .HasColumnName("FDU_Table_Field_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduTableName)
                    .HasColumnName("FDU_Table_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduTableTypeHdrDtl)
                    .HasColumnName("FDU_Table_Type_HDR_DTL")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Maxlength).HasColumnName("maxlength");

                entity.Property(e => e.Mutilpleline).HasColumnName("mutilpleline");

                entity.Property(e => e.Startindex).HasColumnName("startindex");
            });

            modelBuilder.Entity<RmFormDownloadUse28122020>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Rm_Form_Download_Use_28122020");

                entity.Property(e => e.Endindex).HasColumnName("endindex");

                entity.Property(e => e.FduActiveYn).HasColumnName("FDU_Active_YN");

                entity.Property(e => e.FduAppendOverwrite)
                    .HasColumnName("FDU_Append_Overwrite")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FduCrBy)
                    .HasColumnName("FDU_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FduCrDt)
                    .HasColumnName("FDU_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FduExcelCellNo)
                    .HasColumnName("FDU_Excel_Cell_No")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FduExcelColumnNo).HasColumnName("FDU_Excel_Column_No");

                entity.Property(e => e.FduExcelRowNo).HasColumnName("FDU_Excel_Row_No");

                entity.Property(e => e.FduFormType)
                    .HasColumnName("FDU_Form_Type")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.FduHeaderName)
                    .HasColumnName("FDU_Header_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduModBy)
                    .HasColumnName("FDU_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FduModDt)
                    .HasColumnName("FDU_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FduPkRefNo)
                    .HasColumnName("FDU_PK_Ref_No")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FduSeperator)
                    .HasColumnName("FDU_Seperator")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduSubmitSts).HasColumnName("FDU_SUBMIT_STS");

                entity.Property(e => e.FduTableFieldName)
                    .HasColumnName("FDU_Table_Field_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduTableName)
                    .HasColumnName("FDU_Table_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduTableTypeHdrDtl)
                    .HasColumnName("FDU_Table_Type_HDR_DTL")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Maxlength).HasColumnName("maxlength");

                entity.Property(e => e.Mutilpleline).HasColumnName("mutilpleline");

                entity.Property(e => e.Startindex).HasColumnName("startindex");
            });

            modelBuilder.Entity<RmFormDownloadUseFormABak>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RM_FORM_DOWNLOAD_USE_formA_Bak");

                entity.Property(e => e.Endindex).HasColumnName("endindex");

                entity.Property(e => e.FduActiveYn).HasColumnName("FDU_Active_YN");

                entity.Property(e => e.FduAppendOverwrite)
                    .HasColumnName("FDU_Append_Overwrite")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FduCrBy)
                    .HasColumnName("FDU_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FduCrDt)
                    .HasColumnName("FDU_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FduExcelCellNo)
                    .HasColumnName("FDU_Excel_Cell_No")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FduExcelColumnNo).HasColumnName("FDU_Excel_Column_No");

                entity.Property(e => e.FduExcelRowNo).HasColumnName("FDU_Excel_Row_No");

                entity.Property(e => e.FduFormType)
                    .HasColumnName("FDU_Form_Type")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.FduHeaderName)
                    .HasColumnName("FDU_Header_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduModBy)
                    .HasColumnName("FDU_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FduModDt)
                    .HasColumnName("FDU_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FduPkRefNo)
                    .HasColumnName("FDU_PK_Ref_No")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FduSeperator)
                    .HasColumnName("FDU_Seperator")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduSubmitSts).HasColumnName("FDU_SUBMIT_STS");

                entity.Property(e => e.FduTableFieldName)
                    .HasColumnName("FDU_Table_Field_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduTableName)
                    .HasColumnName("FDU_Table_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduTableTypeHdrDtl)
                    .HasColumnName("FDU_Table_Type_HDR_DTL")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Maxlength).HasColumnName("maxlength");

                entity.Property(e => e.Mutilpleline).HasColumnName("mutilpleline");

                entity.Property(e => e.Startindex).HasColumnName("startindex");
            });

            modelBuilder.Entity<RmFormDownloadUseFormD>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RM_Form_Download_Use_formD");

                entity.Property(e => e.Endindex).HasColumnName("endindex");

                entity.Property(e => e.FduActiveYn).HasColumnName("FDU_Active_YN");

                entity.Property(e => e.FduAppendOverwrite)
                    .HasColumnName("FDU_Append_Overwrite")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FduCrBy)
                    .HasColumnName("FDU_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FduCrDt)
                    .HasColumnName("FDU_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FduExcelCellNo)
                    .HasColumnName("FDU_Excel_Cell_No")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FduExcelColumnNo).HasColumnName("FDU_Excel_Column_No");

                entity.Property(e => e.FduExcelRowNo).HasColumnName("FDU_Excel_Row_No");

                entity.Property(e => e.FduFormType)
                    .HasColumnName("FDU_Form_Type")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.FduHeaderName)
                    .HasColumnName("FDU_Header_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduModBy)
                    .HasColumnName("FDU_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FduModDt)
                    .HasColumnName("FDU_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FduPkRefNo)
                    .HasColumnName("FDU_PK_Ref_No")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FduSeperator)
                    .HasColumnName("FDU_Seperator")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduSubmitSts).HasColumnName("FDU_SUBMIT_STS");

                entity.Property(e => e.FduTableFieldName)
                    .HasColumnName("FDU_Table_Field_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduTableName)
                    .HasColumnName("FDU_Table_Name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FduTableTypeHdrDtl)
                    .HasColumnName("FDU_Table_Type_HDR_DTL")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Maxlength).HasColumnName("maxlength");

                entity.Property(e => e.Mutilpleline).HasColumnName("mutilpleline");

                entity.Property(e => e.Startindex).HasColumnName("startindex");
            });

            modelBuilder.Entity<RmFormF2GrInsDtl>(entity =>
            {
                entity.HasKey(e => e.FgridPkRefNo)
                    .HasName("PK__RM_FormF__0F30EEBF86BE9281");

                entity.ToTable("RM_FormF2_GR_Ins_DTL");

                entity.Property(e => e.FgridPkRefNo).HasColumnName("FGRID_PK_Ref_No");

                entity.Property(e => e.FgridActiveYn)
                    .IsRequired()
                    .HasColumnName("FGRID_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FgridCrBy).HasColumnName("FGRID_CR_By");

                entity.Property(e => e.FgridCrDt)
                    .HasColumnName("FGRID_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FgridFgrihPkRefNo).HasColumnName("FGRID_FGRIH_PK_Ref_No");

                entity.Property(e => e.FgridGrCode)
                    .HasColumnName("FGRID_GR_Code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FgridGrCondition1).HasColumnName("FGRID_GR_Condition1");

                entity.Property(e => e.FgridGrCondition2).HasColumnName("FGRID_GR_Condition2");

                entity.Property(e => e.FgridGrCondition3).HasColumnName("FGRID_GR_Condition3");

                entity.Property(e => e.FgridLength).HasColumnName("FGRID_Length");

                entity.Property(e => e.FgridModBy).HasColumnName("FGRID_Mod_By");

                entity.Property(e => e.FgridModDt)
                    .HasColumnName("FGRID_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FgridPostSpac).HasColumnName("FGRID_Post_Spac");

                entity.Property(e => e.FgridRemarks)
                    .HasColumnName("FGRID_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FgridRhsMLhs)
                    .HasColumnName("FGRID_RHS_M_LHS")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FgridStartingChKm).HasColumnName("FGRID_Starting_CH_KM");

                entity.Property(e => e.FgridStartingChM)
                    .HasColumnName("FGRID_Starting_CH_M")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FgridSubmitSts).HasColumnName("FGRID_SUBMIT_STS");

                entity.Property(e => e.FgrihAiPkRefNo).HasColumnName("FGRIH_AI_PK_Ref_No");

                entity.HasOne(d => d.FgridFgrihPkRefNoNavigation)
                    .WithMany(p => p.RmFormF2GrInsDtl)
                    .HasForeignKey(d => d.FgridFgrihPkRefNo)
                    .HasConstraintName("FK__RM_FormF2__FGRID__12C8C788");
            });

            modelBuilder.Entity<RmFormF2GrInsHdr>(entity =>
            {
                entity.HasKey(e => e.FgrihPkRefNo)
                    .HasName("PK__RM_FormF__7CB958B9D96F7C86");

                entity.ToTable("RM_FormF2_GR_Ins_HDR");

                entity.Property(e => e.FgrihPkRefNo).HasColumnName("FGRIH_PK_Ref_No");

                entity.Property(e => e.FgrihActiveYn)
                    .IsRequired()
                    .HasColumnName("FGRIH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FgrihAuditLog).HasColumnName("FGRIH_AuditLog");

                entity.Property(e => e.FgrihCrBy).HasColumnName("FGRIH_CR_By");

                entity.Property(e => e.FgrihCrDt)
                    .HasColumnName("FGRIH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FgrihCrewLeaderId).HasColumnName("FGRIH_Crew_Leader_ID");

                entity.Property(e => e.FgrihCrewLeaderName)
                    .HasColumnName("FGRIH_Crew_Leader_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FgrihDist)
                    .HasColumnName("FGRIH_Dist")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FgrihDivCode)
                    .HasColumnName("FGRIH_Div_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FgrihDtInspBy)
                    .HasColumnName("FGRIH_DT_Insp_by")
                    .HasColumnType("datetime");

                entity.Property(e => e.FgrihDtOfInsp)
                    .HasColumnName("FGRIH_DT_Of_Insp")
                    .HasColumnType("datetime");

                entity.Property(e => e.FgrihFormRefId)
                    .HasColumnName("FGRIH_Form_Ref_ID")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FgrihModBy).HasColumnName("FGRIH_Mod_By");

                entity.Property(e => e.FgrihModDt)
                    .HasColumnName("FGRIH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FgrihRoadCode)
                    .HasColumnName("FGRIH_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FgrihRoadId).HasColumnName("FGRIH_Road_Id");

                entity.Property(e => e.FgrihRoadLength)
                    .HasColumnName("FGRIH_Road_Length")
                    .HasColumnType("decimal(6, 3)");

                entity.Property(e => e.FgrihRoadName)
                    .HasColumnName("FGRIH_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FgrihSignpathInspBy).HasColumnName("FGRIH_Signpath_Insp_by");

                entity.Property(e => e.FgrihStatus)
                    .IsRequired()
                    .HasColumnName("FGRIH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FgrihSubmitSts).HasColumnName("FGRIH_SUBMIT_STS");

                entity.Property(e => e.FgrihUserDesignationInspBy)
                    .HasColumnName("FGRIH_User_Designation_Insp_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FgrihUserIdInspBy).HasColumnName("FGRIH_User_id_Insp_by");

                entity.Property(e => e.FgrihUserNameInspBy)
                    .HasColumnName("FGRIH_User_Name_Insp_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FgrihYearOfInsp).HasColumnName("FGRIH_Year_Of_Insp");

                entity.HasOne(d => d.FgrihRoad)
                    .WithMany(p => p.RmFormF2GrInsHdr)
                    .HasForeignKey(d => d.FgrihRoadId)
                    .HasConstraintName("FK__RM_FormF2__FGRIH__13BCEBC1");
            });

            modelBuilder.Entity<RmFormF4InsDtl>(entity =>
            {
                entity.HasKey(e => e.FivadPkRefNo)
                    .HasName("PK__RM_FormF__3B46FFCAA64A3568");

                entity.ToTable("RM_FormF4_Ins_DTL");

                entity.Property(e => e.FivadPkRefNo).HasColumnName("FIVAD_PK_Ref_No");

                entity.Property(e => e.FivadActiveYn)
                    .IsRequired()
                    .HasColumnName("FIVAD_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FivadBarrelNo).HasColumnName("FIVAD_Barrel_No");

                entity.Property(e => e.FivadCondition).HasColumnName("FIVAD_Condition");

                entity.Property(e => e.FivadCrBy).HasColumnName("FIVAD_CR_By");

                entity.Property(e => e.FivadCrDt)
                    .HasColumnName("FIVAD_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FivadFcvihPkRefNo).HasColumnName("FIVAD_FCVIH_PK_Ref_No");

                entity.Property(e => e.FivadFivahPkRefNo).HasColumnName("FIVAD_FIVAH_PK_Ref_No");

                entity.Property(e => e.FivadHeight).HasColumnName("FIVAD_Height");

                entity.Property(e => e.FivadIntelStruc).HasColumnName("FIVAD_Intel_Struc");

                entity.Property(e => e.FivadLength).HasColumnName("FIVAD_Length");

                entity.Property(e => e.FivadLocChKm).HasColumnName("FIVAD_Loc_CH_KM");

                entity.Property(e => e.FivadLocChM)
                    .HasColumnName("FIVAD_Loc_CH_M")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FivadModBy).HasColumnName("FIVAD_Mod_By");

                entity.Property(e => e.FivadModDt)
                    .HasColumnName("FIVAD_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FivadOutletStruc).HasColumnName("FIVAD_Outlet_Struc");

                entity.Property(e => e.FivadRemarks)
                    .HasColumnName("FIVAD_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FivadStrucCode)
                    .HasColumnName("FIVAD_Struc_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FivadSubmitSts).HasColumnName("FIVAD_SUBMIT_STS");

                entity.Property(e => e.FivadWidth).HasColumnName("FIVAD_Width");

                entity.HasOne(d => d.FivadFcvihPkRefNoNavigation)
                    .WithMany(p => p.RmFormF4InsDtl)
                    .HasForeignKey(d => d.FivadFcvihPkRefNo)
                    .HasConstraintName("FK__RM_FormF4__FIVAD__15A53433");

                entity.HasOne(d => d.FivadFivahPkRefNoNavigation)
                    .WithMany(p => p.RmFormF4InsDtl)
                    .HasForeignKey(d => d.FivadFivahPkRefNo)
                    .HasConstraintName("FK__RM_FormF4__FIVAD__14B10FFA");
            });

            modelBuilder.Entity<RmFormF4InsHdr>(entity =>
            {
                entity.HasKey(e => e.FivahPkRefNo)
                    .HasName("PK__RM_FormF__8C6E55866579BEF7");

                entity.ToTable("RM_FormF4_Ins_HDR");

                entity.Property(e => e.FivahPkRefNo).HasColumnName("FIVAH_PK_Ref_No");

                entity.Property(e => e.FivahActiveYn)
                    .IsRequired()
                    .HasColumnName("FIVAH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FivahAuditLog).HasColumnName("FIVAH_AuditLog");

                entity.Property(e => e.FivahCrBy).HasColumnName("FIVAH_CR_By");

                entity.Property(e => e.FivahCrDt)
                    .HasColumnName("FIVAH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FivahCrewLeaderId).HasColumnName("FIVAH_Crew_Leader_ID");

                entity.Property(e => e.FivahCrewLeaderName)
                    .HasColumnName("FIVAH_Crew_Leader_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FivahDist)
                    .HasColumnName("FIVAH_Dist")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FivahDivCode)
                    .HasColumnName("FIVAH_Div_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FivahDtInspBy)
                    .HasColumnName("FIVAH_DT_Insp_by")
                    .HasColumnType("datetime");

                entity.Property(e => e.FivahFormRefId)
                    .HasColumnName("FIVAH_Form_Ref_ID")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FivahModBy).HasColumnName("FIVAH_Mod_By");

                entity.Property(e => e.FivahModDt)
                    .HasColumnName("FIVAH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FivahRmuName)
                    .HasColumnName("FIVAH_RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FivahRoadCode)
                    .HasColumnName("FIVAH_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FivahRoadId).HasColumnName("FIVAH_Road_Id");

                entity.Property(e => e.FivahRoadLength)
                    .HasColumnName("FIVAH_Road_Length")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.FivahRoadName)
                    .HasColumnName("FIVAH_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FivahSignpathInspBy).HasColumnName("FIVAH_Signpath_Insp_by");

                entity.Property(e => e.FivahStatus)
                    .IsRequired()
                    .HasColumnName("FIVAH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FivahSubmitSts).HasColumnName("FIVAH_SUBMIT_STS");

                entity.Property(e => e.FivahUserDesignationInspBy)
                    .HasColumnName("FIVAH_User_Designation_Insp_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FivahUserIdInspBy).HasColumnName("FIVAH_User_id_Insp_by");

                entity.Property(e => e.FivahUserNameInspBy)
                    .HasColumnName("FIVAH_User_Name_Insp_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FivahYearOfInsp).HasColumnName("FIVAH_Year_Of_Insp");

                entity.HasOne(d => d.FivahRoad)
                    .WithMany(p => p.RmFormF4InsHdr)
                    .HasForeignKey(d => d.FivahRoadId)
                    .HasConstraintName("FK__RM_FormF4__FIVAH__1699586C");
            });

            modelBuilder.Entity<RmFormF5InsDtl>(entity =>
            {
                entity.HasKey(e => e.FvadPkRefNo)
                    .HasName("PK__RM_FormF__CEB2790C1D3214C0");

                entity.ToTable("RM_FormF5_Ins_DTL");

                entity.Property(e => e.FvadPkRefNo).HasColumnName("FVAD_PK_Ref_No");

                entity.Property(e => e.FvadActiveYn)
                    .IsRequired()
                    .HasColumnName("FVAD_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FvadCondition).HasColumnName("FVAD_Condition");

                entity.Property(e => e.FvadCrBy).HasColumnName("FVAD_CR_By");

                entity.Property(e => e.FvadCrDt)
                    .HasColumnName("FVAD_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FvadFbrihPkRefNo).HasColumnName("FVAD_FBRIH_PK_Ref_No");

                entity.Property(e => e.FvadFvahPkRefNo).HasColumnName("FVAD_FVAH_PK_Ref_No");

                entity.Property(e => e.FvadLength).HasColumnName("FVAD_Length");

                entity.Property(e => e.FvadLocChKm).HasColumnName("FVAD_Loc_CH_KM");

                entity.Property(e => e.FvadLocChM)
                    .HasColumnName("FVAD_Loc_CH_M")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FvadModBy).HasColumnName("FVAD_Mod_By");

                entity.Property(e => e.FvadModDt)
                    .HasColumnName("FVAD_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FvadRemarks)
                    .HasColumnName("FVAD_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FvadRiverName)
                    .HasColumnName("FVAD_River_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FvadSpanCnt).HasColumnName("FVAD_Span_Cnt");

                entity.Property(e => e.FvadStrucCode)
                    .HasColumnName("FVAD_Struc_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FvadSubmitSts).HasColumnName("FVAD_SUBMIT_STS");

                entity.Property(e => e.FvadWidth).HasColumnName("FVAD_Width");

                entity.HasOne(d => d.FvadFbrihPkRefNoNavigation)
                    .WithMany(p => p.RmFormF5InsDtl)
                    .HasForeignKey(d => d.FvadFbrihPkRefNo)
                    .HasConstraintName("FK__RM_FormF5__FVAD___4277DAAA");

                entity.HasOne(d => d.FvadFvahPkRefNoNavigation)
                    .WithMany(p => p.RmFormF5InsDtl)
                    .HasForeignKey(d => d.FvadFvahPkRefNo)
                    .HasConstraintName("FK__RM_FormF5__FVAD___178D7CA5");
            });

            modelBuilder.Entity<RmFormF5InsHdr>(entity =>
            {
                entity.HasKey(e => e.FvahPkRefNo)
                    .HasName("PK__RM_FormF__7E3F420D6C3F6E63");

                entity.ToTable("RM_FormF5_Ins_HDR");

                entity.Property(e => e.FvahPkRefNo).HasColumnName("FVAH_PK_Ref_No");

                entity.Property(e => e.FvahActiveYn)
                    .IsRequired()
                    .HasColumnName("FVAH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FvahAuditLog).HasColumnName("FVAH_AuditLog");

                entity.Property(e => e.FvahCrBy).HasColumnName("FVAH_CR_By");

                entity.Property(e => e.FvahCrDt)
                    .HasColumnName("FVAH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FvahCrewLeaderId).HasColumnName("FVAH_Crew_Leader_ID");

                entity.Property(e => e.FvahCrewLeaderName)
                    .HasColumnName("FVAH_Crew_Leader_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FvahDist)
                    .HasColumnName("FVAH_Dist")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FvahDivCode)
                    .HasColumnName("FVAH_Div_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FvahDtInspBy)
                    .HasColumnName("FVAH_DT_Insp_by")
                    .HasColumnType("datetime");

                entity.Property(e => e.FvahFormRefId)
                    .HasColumnName("FVAH_Form_Ref_ID")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FvahModBy).HasColumnName("FVAH_Mod_By");

                entity.Property(e => e.FvahModDt)
                    .HasColumnName("FVAH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FvahRmuName)
                    .HasColumnName("FVAH_RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FvahRoadCode)
                    .HasColumnName("FVAH_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FvahRoadId).HasColumnName("FVAH_Road_Id");

                entity.Property(e => e.FvahRoadLength)
                    .HasColumnName("FVAH_Road_Length")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.FvahRoadName)
                    .HasColumnName("FVAH_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FvahSignpathInspBy).HasColumnName("FVAH_Signpath_Insp_by");

                entity.Property(e => e.FvahStatus)
                    .IsRequired()
                    .HasColumnName("FVAH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FvahSubmitSts).HasColumnName("FVAH_SUBMIT_STS");

                entity.Property(e => e.FvahUserDesignationInspBy)
                    .HasColumnName("FVAH_User_Designation_Insp_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FvahUserIdInspBy).HasColumnName("FVAH_User_id_Insp_by");

                entity.Property(e => e.FvahUserNameInspBy)
                    .HasColumnName("FVAH_User_Name_Insp_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FvahYearOfInsp).HasColumnName("FVAH_Year_Of_Insp");

                entity.HasOne(d => d.FvahRoad)
                    .WithMany(p => p.RmFormF5InsHdr)
                    .HasForeignKey(d => d.FvahRoadId)
                    .HasConstraintName("FK__RM_FormF5__FVAH___1975C517");
            });

            modelBuilder.Entity<RmFormFcInsDtl>(entity =>
            {
                entity.HasKey(e => e.FcidPkRefNo)
                    .HasName("PK__RM_FormF__9501D0F627988BC9");

                entity.ToTable("RM_FormFC_Ins_DTL");

                entity.Property(e => e.FcidPkRefNo).HasColumnName("FCID_PK_Ref_No");

                entity.Property(e => e.FcidActiveYn)
                    .IsRequired()
                    .HasColumnName("FCID_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FcidAiAssetGrpCode)
                    .HasColumnName("FCID_AI_Asset_GRP_Code")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FcidAiBound)
                    .HasColumnName("FCID_AI_Bound")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FcidAiFrmCh).HasColumnName("FCID_AI_FRM_CH");

                entity.Property(e => e.FcidAiFrmChDeci)
                    .HasColumnName("FCID_AI_FRM_CH_Deci")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FcidAiGrpType)
                    .HasColumnName("FCID_AI_Grp_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcidAiPkRefNo).HasColumnName("FCID_AI_PK_Ref_No");

                entity.Property(e => e.FcidAiToCh).HasColumnName("FCID_AI_To_CH");

                entity.Property(e => e.FcidAiToChDeci)
                    .HasColumnName("FCID_AI_To_CH_Deci")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FcidCondition).HasColumnName("FCID_Condition");

                entity.Property(e => e.FcidCrBy).HasColumnName("FCID_CR_By");

                entity.Property(e => e.FcidCrDt)
                    .HasColumnName("FCID_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FcidFcihPkRefNo).HasColumnName("FCID_FCIH_PK_Ref_No");

                entity.Property(e => e.FcidLength).HasColumnName("FCID_Length");

                entity.Property(e => e.FcidModBy).HasColumnName("FCID_Mod_By");

                entity.Property(e => e.FcidModDt)
                    .HasColumnName("FCID_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FcidRemarks)
                    .HasColumnName("FCID_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FcidSubmitSts).HasColumnName("FCID_SUBMIT_STS");

                entity.Property(e => e.FcidWidth).HasColumnName("FCID_Width");

                entity.HasOne(d => d.FcidAiPkRefNoNavigation)
                    .WithMany(p => p.RmFormFcInsDtl)
                    .HasForeignKey(d => d.FcidAiPkRefNo)
                    .HasConstraintName("FK__RM_FormFC__FCID___1B5E0D89");

                entity.HasOne(d => d.FcidFcihPkRefNoNavigation)
                    .WithMany(p => p.RmFormFcInsDtl)
                    .HasForeignKey(d => d.FcidFcihPkRefNo)
                    .HasConstraintName("FK__RM_FormFC__FCID___1A69E950");
            });

            modelBuilder.Entity<RmFormFcInsHdr>(entity =>
            {
                entity.HasKey(e => e.FcihPkRefNo)
                    .HasName("PK__RM_FormF__E3AB924A644C394B");

                entity.ToTable("RM_FormFC_Ins_HDR");

                entity.Property(e => e.FcihPkRefNo).HasColumnName("FCIH_PK_Ref_No");

                entity.Property(e => e.FcihActiveYn)
                    .IsRequired()
                    .HasColumnName("FCIH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FcihAssetTypes).HasColumnName("FCIH_AssetTypes");

                entity.Property(e => e.FcihAuditLog).HasColumnName("FCIH_AuditLog");

                entity.Property(e => e.FcihCrBy).HasColumnName("FCIH_CR_By");

                entity.Property(e => e.FcihCrDt)
                    .HasColumnName("FCIH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FcihCrewLeaderId).HasColumnName("FCIH_Crew_Leader_ID");

                entity.Property(e => e.FcihCrewLeaderName)
                    .HasColumnName("FCIH_Crew_Leader_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcihDist)
                    .HasColumnName("FCIH_Dist")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcihDivCode)
                    .HasColumnName("FCIH_Div_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcihDtInspBy)
                    .HasColumnName("FCIH_DT_Insp_by")
                    .HasColumnType("datetime");

                entity.Property(e => e.FcihFormRefId)
                    .HasColumnName("FCIH_Form_Ref_ID")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FcihFrmCh)
                    .HasColumnName("FCIH_FRM_CH")
                    .HasColumnType("decimal(6, 3)");

                entity.Property(e => e.FcihModBy).HasColumnName("FCIH_Mod_By");

                entity.Property(e => e.FcihModDt)
                    .HasColumnName("FCIH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FcihRemarks)
                    .HasColumnName("FCIH_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FcihRmuName)
                    .HasColumnName("FCIH_RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FcihRoadCode)
                    .HasColumnName("FCIH_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FcihRoadId).HasColumnName("FCIH_Road_Id");

                entity.Property(e => e.FcihRoadLength)
                    .HasColumnName("FCIH_Road_Length")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.FcihRoadName)
                    .HasColumnName("FCIH_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcihSignpathInspBy).HasColumnName("FCIH_Signpath_Insp_by");

                entity.Property(e => e.FcihStatus)
                    .IsRequired()
                    .HasColumnName("FCIH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FcihSubmitSts).HasColumnName("FCIH_SUBMIT_STS");

                entity.Property(e => e.FcihToCh)
                    .HasColumnName("FCIH_To_CH")
                    .HasColumnType("decimal(6, 3)");

                entity.Property(e => e.FcihUserDesignationInspBy)
                    .HasColumnName("FCIH_User_Designation_Insp_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcihUserIdInspBy).HasColumnName("FCIH_User_id_Insp_by");

                entity.Property(e => e.FcihUserNameInspBy)
                    .HasColumnName("FCIH_User_Name_Insp_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FcihYearOfInsp).HasColumnName("FCIH_Year_Of_Insp");

                entity.HasOne(d => d.FcihRoad)
                    .WithMany(p => p.RmFormFcInsHdr)
                    .HasForeignKey(d => d.FcihRoadId)
                    .HasConstraintName("FK__RM_FormFC__FCIH___1C5231C2");
            });

            modelBuilder.Entity<RmFormFdInsDtl>(entity =>
            {
                entity.HasKey(e => e.FdidPkRefNo)
                    .HasName("PK__RM_FormF__3C10A35C10034A63");

                entity.ToTable("RM_FormFD_Ins_DTL");

                entity.Property(e => e.FdidPkRefNo).HasColumnName("FDID_PK_Ref_No");

                entity.Property(e => e.FdidActiveYn)
                    .IsRequired()
                    .HasColumnName("FDID_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FdidAiAssetGrpCode)
                    .HasColumnName("FDID_AI_Asset_GRP_Code")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FdidAiBound)
                    .HasColumnName("FDID_AI_Bound")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdidAiFrmCh).HasColumnName("FDID_AI_FRM_CH");

                entity.Property(e => e.FdidAiFrmChDeci)
                    .HasColumnName("FDID_AI_FRM_CH_Deci")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FdidAiGrpType)
                    .HasColumnName("FDID_AI_Grp_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdidAiPkRefNo).HasColumnName("FDID_AI_PK_Ref_No");

                entity.Property(e => e.FdidAiToCh).HasColumnName("FDID_AI_To_CH");

                entity.Property(e => e.FdidAiToChDeci)
                    .HasColumnName("FDID_AI_To_CH_Deci")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FdidCondition).HasColumnName("FDID_Condition");

                entity.Property(e => e.FdidCrBy).HasColumnName("FDID_CR_By");

                entity.Property(e => e.FdidCrDt)
                    .HasColumnName("FDID_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdidFdihPkRefNo).HasColumnName("FDID_FDIH_PK_Ref_No");

                entity.Property(e => e.FdidLength).HasColumnName("FDID_Length");

                entity.Property(e => e.FdidModBy).HasColumnName("FDID_Mod_By");

                entity.Property(e => e.FdidModDt)
                    .HasColumnName("FDID_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdidRemarks)
                    .HasColumnName("FDID_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FdidSubmitSts).HasColumnName("FDID_SUBMIT_STS");

                entity.Property(e => e.FdidWidth).HasColumnName("FDID_Width");

                entity.HasOne(d => d.FdidAiPkRefNoNavigation)
                    .WithMany(p => p.RmFormFdInsDtl)
                    .HasForeignKey(d => d.FdidAiPkRefNo)
                    .HasConstraintName("FK__RM_FormFD__FDID___1E3A7A34");

                entity.HasOne(d => d.FdidFdihPkRefNoNavigation)
                    .WithMany(p => p.RmFormFdInsDtl)
                    .HasForeignKey(d => d.FdidFdihPkRefNo)
                    .HasConstraintName("FK__RM_FormFD__FDID___1D4655FB");
            });

            modelBuilder.Entity<RmFormFdInsHdr>(entity =>
            {
                entity.HasKey(e => e.FdihPkRefNo)
                    .HasName("PK__RM_FormF__FCF91F3D48C4C931");

                entity.ToTable("RM_FormFD_Ins_HDR");

                entity.Property(e => e.FdihPkRefNo).HasColumnName("FDIH_PK_Ref_No");

                entity.Property(e => e.FdihActiveYn)
                    .IsRequired()
                    .HasColumnName("FDIH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FdihAssetTypes).HasColumnName("FDIH_AssetTypes");

                entity.Property(e => e.FdihAuditLog).HasColumnName("FDIH_AuditLog");

                entity.Property(e => e.FdihCrBy).HasColumnName("FDIH_CR_By");

                entity.Property(e => e.FdihCrDt)
                    .HasColumnName("FDIH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdihCrewLeaderId).HasColumnName("FDIH_Crew_Leader_ID");

                entity.Property(e => e.FdihCrewLeaderName)
                    .HasColumnName("FDIH_Crew_Leader_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdihDist)
                    .HasColumnName("FDIH_Dist")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdihDivCode)
                    .HasColumnName("FDIH_Div_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdihDtInsBy)
                    .HasColumnName("FDIH_DT_Ins_by")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdihFormRefId)
                    .HasColumnName("FDIH_Form_Ref_ID")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FdihFrmCh)
                    .HasColumnName("FDIH_FRM_CH")
                    .HasColumnType("decimal(6, 3)");

                entity.Property(e => e.FdihModBy).HasColumnName("FDIH_Mod_By");

                entity.Property(e => e.FdihModDt)
                    .HasColumnName("FDIH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FdihRemarks)
                    .HasColumnName("FDIH_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FdihRmuName)
                    .HasColumnName("FDIH_RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FdihRoadCode)
                    .HasColumnName("FDIH_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FdihRoadId).HasColumnName("FDIH_Road_Id");

                entity.Property(e => e.FdihRoadLength)
                    .HasColumnName("FDIH_Road_Length")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.FdihRoadName)
                    .HasColumnName("FDIH_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdihSecName)
                    .HasColumnName("FDIH_Sec_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdihSignpathInspBy).HasColumnName("FDIH_Signpath_Insp_by");

                entity.Property(e => e.FdihStatus)
                    .IsRequired()
                    .HasColumnName("FDIH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FdihSubmitSts).HasColumnName("FDIH_SUBMIT_STS");

                entity.Property(e => e.FdihToCh)
                    .HasColumnName("FDIH_To_CH")
                    .HasColumnType("decimal(6, 3)");

                entity.Property(e => e.FdihUserDesignationInspBy)
                    .HasColumnName("FDIH_User_Designation_Insp_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdihUserIdInspBy).HasColumnName("FDIH_User_id_Insp_by");

                entity.Property(e => e.FdihUserNameInspBy)
                    .HasColumnName("FDIH_User_Name_Insp_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FdihYearOfInsp).HasColumnName("FDIH_Year_Of_Insp");

                entity.HasOne(d => d.FdihRoad)
                    .WithMany(p => p.RmFormFdInsHdr)
                    .HasForeignKey(d => d.FdihRoadId)
                    .HasConstraintName("FK__RM_FormFD__FDIH___1F2E9E6D");
            });

            modelBuilder.Entity<RmFormFsInsDtl>(entity =>
            {
                entity.HasKey(e => e.FsdPkRefNo)
                    .HasName("PK__RM_FormF__A2DE4BFAECCAA2E3");

                entity.ToTable("RM_FormFS_Ins_DTL");

                entity.Property(e => e.FsdPkRefNo).HasColumnName("FSD_PK_Ref_No");

                entity.Property(e => e.FsdActiveYn)
                    .IsRequired()
                    .HasColumnName("FSD_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FsdCondition1)
                    .HasColumnName("FSD_Condition1")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.FsdCondition2)
                    .HasColumnName("FSD_Condition2")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.FsdCondition3)
                    .HasColumnName("FSD_Condition3")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.FsdCrBy).HasColumnName("FSD_CR_By");

                entity.Property(e => e.FsdCrDt)
                    .HasColumnName("FSD_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsdFeature)
                    .HasColumnName("FSD_Feature")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FsdFshPkRefNo).HasColumnName("FSD_FSH_PK_Ref_No");

                entity.Property(e => e.FsdGrpCode)
                    .HasColumnName("FSD_Grp_Code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FsdGrpType)
                    .HasColumnName("FSD_Grp_Type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FsdLength).HasColumnName("FSD_Length");

                entity.Property(e => e.FsdModBy).HasColumnName("FSD_Mod_By");

                entity.Property(e => e.FsdModDt)
                    .HasColumnName("FSD_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsdNeeded)
                    .HasColumnName("FSD_Needed")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.FsdRemarks)
                    .HasColumnName("FSD_Remarks")
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.FsdStrucCode)
                    .HasColumnName("FSD_Struc_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FsdSubmitSts).HasColumnName("FSD_SUBMIT_STS");

                entity.Property(e => e.FsdUnit)
                    .HasColumnName("FSD_Unit")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FsdWidth).HasColumnName("FSD_Width");

                entity.HasOne(d => d.FsdFshPkRefNoNavigation)
                    .WithMany(p => p.RmFormFsInsDtl)
                    .HasForeignKey(d => d.FsdFshPkRefNo)
                    .HasConstraintName("FK__RM_FormFS__FSD_F__2022C2A6");
            });

            modelBuilder.Entity<RmFormFsInsHdr>(entity =>
            {
                entity.HasKey(e => e.FshPkRefNo)
                    .HasName("PK__RM_FormF__7D8649FFE6D42123");

                entity.ToTable("RM_FormFS_Ins_HDR");

                entity.Property(e => e.FshPkRefNo).HasColumnName("FSH_PK_Ref_No");

                entity.Property(e => e.FshActiveYn)
                    .IsRequired()
                    .HasColumnName("FSH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FshAuditLog).HasColumnName("FSH_AuditLog");

                entity.Property(e => e.FshCrBy).HasColumnName("FSH_CR_By");

                entity.Property(e => e.FshCrDt)
                    .HasColumnName("FSH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FshCrewLeaderId).HasColumnName("FSH_Crew_Leader_ID");

                entity.Property(e => e.FshCrewLeaderName)
                    .HasColumnName("FSH_Crew_Leader_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FshDist)
                    .HasColumnName("FSH_Dist")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FshDivCode)
                    .HasColumnName("FSH_Div_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FshDtChckdBy)
                    .HasColumnName("FSH_DT_Chckd_by")
                    .HasColumnType("datetime");

                entity.Property(e => e.FshDtInspBy)
                    .HasColumnName("FSH_DT_Insp_by")
                    .HasColumnType("datetime");

                entity.Property(e => e.FshDtSmzdBy)
                    .HasColumnName("FSH_DT_Smzd_by")
                    .HasColumnType("datetime");

                entity.Property(e => e.FshFormRefId)
                    .HasColumnName("FSH_Form_Ref_ID")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FshModBy).HasColumnName("FSH_Mod_By");

                entity.Property(e => e.FshModDt)
                    .HasColumnName("FSH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FshRmuName)
                    .HasColumnName("FSH_RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FshRoadCode)
                    .HasColumnName("FSH_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FshRoadId).HasColumnName("FSH_Road_Id");

                entity.Property(e => e.FshRoadLength)
                    .HasColumnName("FSH_Road_Length")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.FshRoadName)
                    .HasColumnName("FSH_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FshSignpathChckdBy).HasColumnName("FSH_Signpath_Chckd_by");

                entity.Property(e => e.FshSignpathInspBy).HasColumnName("FSH_Signpath_Insp_by");

                entity.Property(e => e.FshSignpathSmzdBy).HasColumnName("FSH_Signpath_Smzd_by");

                entity.Property(e => e.FshStatus)
                    .IsRequired()
                    .HasColumnName("FSH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FshSubmitSts).HasColumnName("FSH_SUBMIT_STS");

                entity.Property(e => e.FshUserDesignationChckdBy)
                    .HasColumnName("FSH_User_Designation_Chckd_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FshUserDesignationInspY)
                    .HasColumnName("FSH_User_Designation_Insp_y")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FshUserDesignationSmzdY)
                    .HasColumnName("FSH_User_Designation_Smzd_y")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FshUserIdChckdBy).HasColumnName("FSH_User_id_Chckd_by");

                entity.Property(e => e.FshUserIdInspBy).HasColumnName("FSH_User_id_Insp_by");

                entity.Property(e => e.FshUserIdSmzdBy).HasColumnName("FSH_User_id_Smzd_by");

                entity.Property(e => e.FshUserNameChckdBy)
                    .HasColumnName("FSH_User_Name_Chckd_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FshUserNameInspBy)
                    .HasColumnName("FSH_User_Name_Insp_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FshUserNameSmzdBy)
                    .HasColumnName("FSH_User_Name_Smzd_by")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FshYearOfInsp).HasColumnName("FSH_Year_Of_Insp");

                entity.HasOne(d => d.FshRoad)
                    .WithMany(p => p.RmFormFsInsHdr)
                    .HasForeignKey(d => d.FshRoadId)
                    .HasConstraintName("FK__RM_FormFS__FSH_R__2116E6DF");
            });

            modelBuilder.Entity<RmFormGenDtl>(entity =>
            {
                entity.HasKey(e => e.FgdPkId)
                    .HasName("PK__RM_FORM___11F7869534723080");

                entity.ToTable("RM_FORM_GEN_DTL");

                entity.Property(e => e.FgdPkId).HasColumnName("FGD_PK_Id");

                entity.Property(e => e.FgdCrBy)
                    .HasColumnName("FGD_CR_By")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FgdCrDt)
                    .HasColumnName("FGD_CR_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FgdFileName)
                    .HasColumnName("FGD_FILE_NAME")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FgdFilePath)
                    .HasColumnName("FGD_FILE_PATH")
                    .HasMaxLength(500);

                entity.Property(e => e.FgdFormName)
                    .HasColumnName("FGD_FORM_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FgdModBy)
                    .HasColumnName("FGD_Mod_By")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FgdModDt)
                    .HasColumnName("FGD_Mod_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FgdRemarks)
                    .HasColumnName("FGD_Remarks")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FgdSubmitSts).HasColumnName("FGD_SUBMIT_STS");
            });

            modelBuilder.Entity<RmFormHHdr>(entity =>
            {
                entity.HasKey(e => e.FhhPkRefNo)
                    .HasName("PK__RM_FormH__E221D2BF58BC0F76");

                entity.ToTable("RM_FormH_HDR");

                entity.Property(e => e.FhhPkRefNo).HasColumnName("FHH_PK_Ref_No");

                entity.Property(e => e.FhhActiveYn)
                    .IsRequired()
                    .HasColumnName("FHH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FhhAssetGroupCode)
                    .HasColumnName("FHH_Asset_Group_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FhhAssetId)
                    .HasColumnName("FHH_Asset_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FhhAuditLog).HasColumnName("FHH_AuditLog");

                entity.Property(e => e.FhhAuthRecmd)
                    .HasColumnName("FHH_AUTH_RECMD")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FhhAuthRemarks)
                    .HasColumnName("FHH_AUTH_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FhhAuthRepNo).HasColumnName("FHH_AUTH_Rep_NO");

                entity.Property(e => e.FhhCltRemarks)
                    .HasColumnName("FHH_CLT_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FhhContNo)
                    .HasColumnName("FHH_CONT_No")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FhhCrBy)
                    .HasColumnName("FHH_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FhhCrDt)
                    .HasColumnName("FHH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FhhDamCausedby)
                    .HasColumnName("FHH_DAM_Causedby")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FhhDamDtl)
                    .HasColumnName("FHH_DAM_DTL")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FhhDesignationPrp)
                    .HasColumnName("FHH_Designation_PRP")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FhhDesignationRcvdAuth)
                    .HasColumnName("FHH_Designation_RCVD_AUTH")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FhhDesignationVer)
                    .HasColumnName("FHH_Designation_VER")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FhhDesignationVetAuth)
                    .HasColumnName("FHH_Designation_VET_AUTH")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FhhDiv)
                    .HasColumnName("FHH_DIV")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FhhDtPrp)
                    .HasColumnName("FHH_DT_PRP")
                    .HasColumnType("datetime");

                entity.Property(e => e.FhhDtRcvdAuth)
                    .HasColumnName("FHH_DT_RCVD_AUTH")
                    .HasColumnType("datetime");

                entity.Property(e => e.FhhDtVer)
                    .HasColumnName("FHH_DT_VER")
                    .HasColumnType("datetime");

                entity.Property(e => e.FhhDtVetAuth)
                    .HasColumnName("FHH_DT_VET_AUTH")
                    .HasColumnType("datetime");

                entity.Property(e => e.FhhFadPkRefNo).HasColumnName("FHH_FAD_PK_Ref_No");

                entity.Property(e => e.FhhFjdPkRefNo).HasColumnName("FHH_FJD_PK_Ref_No");

                entity.Property(e => e.FhhFrmCh).HasColumnName("FHH_FRM_CH");

                entity.Property(e => e.FhhFrmChDeci).HasColumnName("FHH_FRM_CH_Deci");

                entity.Property(e => e.FhhInspDt)
                    .HasColumnName("FHH_INSP_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FhhModBy)
                    .HasColumnName("FHH_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FhhModDt)
                    .HasColumnName("FHH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FhhRcvdAuthSts)
                    .HasColumnName("FHH_RCVD_AUTH_STS")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FhhRdName)
                    .HasColumnName("FHH_RD_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FhhRefId)
                    .HasColumnName("FHH_Ref_ID")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FhhRemarks)
                    .HasColumnName("FHH_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FhhRmu)
                    .HasColumnName("FHH_RMU")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FhhRoadCode)
                    .HasColumnName("FHH_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FhhSection)
                    .HasColumnName("FHH_Section")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FhhSignPrp).HasColumnName("FHH_SIgn_PRP");

                entity.Property(e => e.FhhSignRcvdAuth).HasColumnName("FHH_SIgn_RCVD_AUTH");

                entity.Property(e => e.FhhSignVer).HasColumnName("FHH_SIgn_VER");

                entity.Property(e => e.FhhSignVetAuth).HasColumnName("FHH_SIgn_VET_AUTH");

                entity.Property(e => e.FhhStatus)
                    .IsRequired()
                    .HasColumnName("FHH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FhhStsDtRcvdAuth)
                    .HasColumnName("FHH_STS_DT_RCVD_AUTH")
                    .HasColumnType("datetime");

                entity.Property(e => e.FhhStsDtSubAuth)
                    .HasColumnName("FHH_STS_DT_SUB_AUTH")
                    .HasColumnType("datetime");

                entity.Property(e => e.FhhSubAuthSts)
                    .HasColumnName("FHH_SUB_AUTH_STS")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FhhSubmitSts).HasColumnName("FHH_SUBMIT_STS");

                entity.Property(e => e.FhhToCh).HasColumnName("FHH_To_CH");

                entity.Property(e => e.FhhToChDeci).HasColumnName("FHH_To_CH_Deci");

                entity.Property(e => e.FhhUseridPrp).HasColumnName("FHH_Userid_PRP");

                entity.Property(e => e.FhhUseridRcvdAuth).HasColumnName("FHH_Userid_RCVD_AUTH");

                entity.Property(e => e.FhhUseridVer).HasColumnName("FHH_Userid_VER");

                entity.Property(e => e.FhhUseridVetAuth).HasColumnName("FHH_Userid_VET_AUTH");

                entity.Property(e => e.FhhUsernamePrp)
                    .HasColumnName("FHH_Username_PRP")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FhhUsernameRcvdAuth)
                    .HasColumnName("FHH_Username_RCVD_AUTH")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FhhUsernameVer)
                    .HasColumnName("FHH_Username_VER")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FhhUsernameVetAuth)
                    .HasColumnName("FHH_Username_VET_AUTH")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.FhhFadPkRefNoNavigation)
                    .WithMany(p => p.RmFormHHdr)
                    .HasForeignKey(d => d.FhhFadPkRefNo)
                    .HasConstraintName("FK__RM_FormH___FHH_F__220B0B18");

                entity.HasOne(d => d.FhhFjdPkRefNoNavigation)
                    .WithMany(p => p.RmFormHHdr)
                    .HasForeignKey(d => d.FhhFjdPkRefNo)
                    .HasConstraintName("FK__RM_FormH___FHH_F__22FF2F51");
            });

            modelBuilder.Entity<RmFormJDtl>(entity =>
            {
                entity.HasKey(e => e.FjdPkRefNo)
                    .HasName("PK__RM_FormJ__CAC96AA8E256A1B8");

                entity.ToTable("RM_FormJ_DTL");

                entity.Property(e => e.FjdPkRefNo).HasColumnName("FJD_PK_Ref_No");

                entity.Property(e => e.FjdActiveYn)
                    .IsRequired()
                    .HasColumnName("FJD_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FjdAssetId)
                    .HasColumnName("FJD_Asset_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FjdCrBy)
                    .HasColumnName("FJD_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FjdCrDt)
                    .HasColumnName("FJD_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FjdDefCode)
                    .HasColumnName("FJD_DEF_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FjdDt)
                    .HasColumnName("FJD_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FjdFjhPkRefNo).HasColumnName("FJD_FJH_PK_Ref_No");

                entity.Property(e => e.FjdFormhApp)
                    .HasColumnName("FJD_FORMH_App")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FjdFrmCh).HasColumnName("FJD_FRM_CH");

                entity.Property(e => e.FjdFrmChDeci)
                    .HasColumnName("FJD_FRM_CH_Deci")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FjdHeight).HasColumnName("FJD_Height");

                entity.Property(e => e.FjdLength).HasColumnName("FJD_Length");

                entity.Property(e => e.FjdModBy)
                    .HasColumnName("FJD_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FjdModDt)
                    .HasColumnName("FJD_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FjdPrblmDesc)
                    .HasColumnName("FJD_PRBLM_Desc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FjdPriority)
                    .HasColumnName("FJD_Priority")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FjdRefId)
                    .HasColumnName("FJD_Ref_ID")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FjdRemarks)
                    .HasColumnName("FJD_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FjdRt).HasColumnName("FJD_RT");

                entity.Property(e => e.FjdSiteRef)
                    .HasColumnName("FJD_Site_Ref")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FjdSrno).HasColumnName("FJD_SRNO");

                entity.Property(e => e.FjdSubmitSts).HasColumnName("FJD_SUBMIT_STS");

                entity.Property(e => e.FjdToCh).HasColumnName("FJD_To_CH");

                entity.Property(e => e.FjdToChDeci)
                    .HasColumnName("FJD_To_CH_Deci")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FjdWc).HasColumnName("FJD_WC");

                entity.Property(e => e.FjdWi).HasColumnName("FJD_WI");

                entity.Property(e => e.FjdWidth).HasColumnName("FJD_Width");

                entity.Property(e => e.FjdWrkNeed)
                    .HasColumnName("FJD_WRK_NEED")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FjdWs).HasColumnName("FJD_WS");

                entity.Property(e => e.FjdWtc).HasColumnName("FJD_WTC");

                entity.HasOne(d => d.FjdFjhPkRefNoNavigation)
                    .WithMany(p => p.RmFormJDtl)
                    .HasForeignKey(d => d.FjdFjhPkRefNo)
                    .HasConstraintName("FK__RM_FormJ___FJD_F__24E777C3");
            });

            modelBuilder.Entity<RmFormJHdr>(entity =>
            {
                entity.HasKey(e => e.FjhPkRefNo)
                    .HasName("PK__RM_FormJ__8E84C3630621E12F");

                entity.ToTable("RM_FormJ_HDR");

                entity.Property(e => e.FjhPkRefNo).HasColumnName("FJH_PK_Ref_No");

                entity.Property(e => e.FjhActiveYn)
                    .IsRequired()
                    .HasColumnName("FJH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FjhAssetGroupCode)
                    .HasColumnName("FJH_Asset_Group_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FjhAuditLog).HasColumnName("FJH_AuditLog");

                entity.Property(e => e.FjhContNo)
                    .HasColumnName("FJH_CONT_No")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FjhCrBy)
                    .HasColumnName("FJH_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FjhCrDt)
                    .HasColumnName("FJH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FjhDesignationPrp)
                    .HasColumnName("FJH_Designation_PRP")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FjhDesignationVer)
                    .HasColumnName("FJH_Designation_VER")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FjhDesignationVet)
                    .HasColumnName("FJH_Designation_VET")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FjhDtPrp)
                    .HasColumnName("FJH_DT_PRP")
                    .HasColumnType("datetime");

                entity.Property(e => e.FjhDtVer)
                    .HasColumnName("FJH_DT_VER")
                    .HasColumnType("datetime");

                entity.Property(e => e.FjhDtVet)
                    .HasColumnName("FJH_DT_VET")
                    .HasColumnType("datetime");

                entity.Property(e => e.FjhModBy)
                    .HasColumnName("FJH_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FjhModDt)
                    .HasColumnName("FJH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FjhMonth).HasColumnName("FJH_Month");

                entity.Property(e => e.FjhRefId)
                    .HasColumnName("FJH_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FjhRemarks)
                    .HasColumnName("FJH_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FjhRmu)
                    .HasColumnName("FJH_RMU")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FjhRoadCode)
                    .HasColumnName("FJH_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FjhRoadName)
                    .HasColumnName("FJH_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FjhSection)
                    .HasColumnName("FJH_Section")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FjhSignPrp).HasColumnName("FJH_SIgn_PRP");

                entity.Property(e => e.FjhSignVer).HasColumnName("FJH_SIgn_VER");

                entity.Property(e => e.FjhSignVet).HasColumnName("FJH_SIgn_VET");

                entity.Property(e => e.FjhStatus)
                    .IsRequired()
                    .HasColumnName("FJH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FjhSubmitSts).HasColumnName("FJH_SUBMIT_STS");

                entity.Property(e => e.FjhUseridPrp).HasColumnName("FJH_Userid_PRP");

                entity.Property(e => e.FjhUseridVer).HasColumnName("FJH_Userid_VER");

                entity.Property(e => e.FjhUseridVet).HasColumnName("FJH_Userid_VET");

                entity.Property(e => e.FjhUsernamePrp)
                    .HasColumnName("FJH_Username_PRP")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FjhUsernameVer)
                    .HasColumnName("FJH_Username_VER")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FjhUsernameVet)
                    .HasColumnName("FJH_Username_VET")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FjhYear).HasColumnName("FJH_Year");
            });

            modelBuilder.Entity<RmFormN1Hdr>(entity =>
            {
                entity.HasKey(e => e.FnihPkRefNo)
                    .HasName("PK__RM_FormN__89C1C7EC22A63217");

                entity.ToTable("RM_FormN1_HDR");

                entity.Property(e => e.FnihPkRefNo).HasColumnName("FNIH_PK_Ref_No");

                entity.Property(e => e.FnihActiveYn)
                    .IsRequired()
                    .HasColumnName("FNIH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FnihAuditLog).HasColumnName("FNIH_AuditLog");

                entity.Property(e => e.FnihContNo)
                    .HasColumnName("FNIH_CONT_No")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihCorrectionTkn).HasColumnName("FNIH_Correction_tkn");

                entity.Property(e => e.FnihCrBy)
                    .HasColumnName("FNIH_CR_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FnihCrDt)
                    .HasColumnName("FNIH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnihCrctTknBef)
                    .HasColumnName("FNIH_Crct_tkn_bef")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnihDesignationAccptd)
                    .HasColumnName("FNIH_Designation_Accptd")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihDesignationAttnTo)
                    .HasColumnName("FNIH_Designation_Attn_To")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FnihDesignationCc)
                    .HasColumnName("FNIH_Designation_CC")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FnihDesignationCorrective)
                    .HasColumnName("FNIH_Designation_Corrective")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihDesignationIssued)
                    .HasColumnName("FNIH_Designation_Issued")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FnihDesignationRcvd)
                    .HasColumnName("FNIH_Designation_RCVD")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihDesignationVer)
                    .HasColumnName("FNIH_Designation_ver")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihDiv)
                    .HasColumnName("FNIH_Div")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FnihDtAccptd)
                    .HasColumnName("FNIH_DT_Accptd")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnihDtCorrectTkn)
                    .HasColumnName("FNIH_DT_Correct_TKN")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnihDtCorrective)
                    .HasColumnName("FNIH_DT_Corrective")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnihDtIssue)
                    .HasColumnName("FNIH_DT_Issue")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnihDtRcvd)
                    .HasColumnName("FNIH_DT_RCVD")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnihDtVer)
                    .HasColumnName("FNIH_DT_ver")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnihFqaiidPkRefNo).HasColumnName("FNIH_FQAIID_PK_Ref_No");

                entity.Property(e => e.FnihFrmCh).HasColumnName("FNIH_FRM_CH");

                entity.Property(e => e.FnihFrmChDeci).HasColumnName("FNIH_FRM_CH_Deci");

                entity.Property(e => e.FnihFsidPkRefNo).HasColumnName("FNIH_FSID_PK_Ref_No");

                entity.Property(e => e.FnihIssueDt)
                    .HasColumnName("FNIH_Issue_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnihModBy)
                    .HasColumnName("FNIH_Mod_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FnihModDt)
                    .HasColumnName("FNIH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnihNcDesc)
                    .HasColumnName("FNIH_NC_Desc")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FnihNcnNo)
                    .HasColumnName("FNIH_NCN_No")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihNcrIssue).HasColumnName("FNIH_NCR_Issue");

                entity.Property(e => e.FnihOthrFllwAct)
                    .HasColumnName("FNIH_OTHR_Fllw_act")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FnihProposedRewrkSpec)
                    .HasColumnName("FNIH_Proposed_Rewrk_spec")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FnihRefId)
                    .HasColumnName("FNIH_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihRemarks)
                    .HasColumnName("FNIH_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FnihRmu)
                    .HasColumnName("FNIH_RMU")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FnihRoadCode)
                    .HasColumnName("FNIH_Road_Code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FnihRoadName)
                    .HasColumnName("FNIH_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FnihSignAccptd)
                    .HasColumnName("FNIH_SIgn_Accptd")
                    .HasMaxLength(1);

                entity.Property(e => e.FnihSignCorrective)
                    .HasColumnName("FNIH_SIgn_Corrective")
                    .HasMaxLength(1);

                entity.Property(e => e.FnihSignIssued)
                    .HasColumnName("FNIH_SIgn_Issued")
                    .HasMaxLength(1);

                entity.Property(e => e.FnihSignRcvd)
                    .HasColumnName("FNIH_SIgn_RCVD")
                    .HasMaxLength(1);

                entity.Property(e => e.FnihSignVer)
                    .HasColumnName("FNIH_SIgn_Ver")
                    .HasMaxLength(1);

                entity.Property(e => e.FnihSourceType)
                    .HasColumnName("FNIH_Source_Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FnihSrProvider)
                    .HasColumnName("FNIH_SR_Provider")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihStatus)
                    .IsRequired()
                    .HasColumnName("FNIH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FnihSubmitSts).HasColumnName("FNIH_SUBMIT_STS");

                entity.Property(e => e.FnihToCh).HasColumnName("FNIH_To_CH");

                entity.Property(e => e.FnihToChDeci).HasColumnName("FNIH_To_CH_Deci");

                entity.Property(e => e.FnihUseridAccptd).HasColumnName("FNIH_Userid_Accptd");

                entity.Property(e => e.FnihUseridAttnTo)
                    .HasColumnName("FNIH_Userid_Attn_To")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihUseridCc)
                    .HasColumnName("FNIH_Userid_CC")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihUseridCorrective).HasColumnName("FNIH_Userid_Corrective");

                entity.Property(e => e.FnihUseridIssued).HasColumnName("FNIH_Userid_Issued");

                entity.Property(e => e.FnihUseridRcvd).HasColumnName("FNIH_Userid_RCVD");

                entity.Property(e => e.FnihUseridVer).HasColumnName("FNIH_Userid_ver");

                entity.Property(e => e.FnihUsernameAccptd)
                    .HasColumnName("FNIH_Username_Accptd")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihUsernameAttnTo)
                    .HasColumnName("FNIH_Username_Attn_To")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihUsernameCc)
                    .HasColumnName("FNIH_Username_CC")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihUsernameCorrective)
                    .HasColumnName("FNIH_Username_Corrective")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihUsernameIssued)
                    .HasColumnName("FNIH_Username_Issued")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihUsernameRcvd)
                    .HasColumnName("FNIH_Username_RCVD")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnihUsernameVer)
                    .HasColumnName("FNIH_Username_ver")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.FnihFqaiidPkRefNoNavigation)
                    .WithMany(p => p.RmFormN1Hdr)
                    .HasForeignKey(d => d.FnihFqaiidPkRefNo)
                    .HasConstraintName("FK__RM_FormN1__FNIH___26CFC035");

                entity.HasOne(d => d.FnihFsidPkRefNoNavigation)
                    .WithMany(p => p.RmFormN1Hdr)
                    .HasForeignKey(d => d.FnihFsidPkRefNo)
                    .HasConstraintName("FK_RM_FormN1_6501FCD8");
            });

            modelBuilder.Entity<RmFormN2Hdr>(entity =>
            {
                entity.HasKey(e => e.FnthPkRefNo)
                    .HasName("PK__RM_FormN__85079DD43DECA75F");

                entity.ToTable("RM_FormN2_HDR");

                entity.Property(e => e.FnthPkRefNo).HasColumnName("FNTH_PK_Ref_No");

                entity.Property(e => e.FnihActiveYn)
                    .IsRequired()
                    .HasColumnName("FNIH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FnihCrBy)
                    .HasColumnName("FNIH_CR_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FnihCrDt)
                    .HasColumnName("FNIH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnihDesignationAttnTo)
                    .HasColumnName("FNIH_Designation_Attn_To")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FnihDesignationCc)
                    .HasColumnName("FNIH_Designation_CC")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FnihModBy)
                    .HasColumnName("FNIH_Mod_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FnihModDt)
                    .HasColumnName("FNIH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnihSubmitSts).HasColumnName("FNIH_SUBMIT_STS");

                entity.Property(e => e.FnohOthrFllwAct)
                    .HasColumnName("FNOH_OTHR_Fllw_act")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FnthAttnTo)
                    .HasColumnName("FNTH_Attn_To")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthAuditLog).HasColumnName("FNTH_AuditLog");

                entity.Property(e => e.FnthCc)
                    .HasColumnName("FNTH_CC")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthCloseOutDt)
                    .HasColumnName("FNTH_Close_out_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnthCloseOutRemarks)
                    .HasColumnName("FNTH_Close_out_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FnthContNo)
                    .HasColumnName("FNTH_CONT_No")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthDesignationAccptd)
                    .HasColumnName("FNTH_Designation_Accptd")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthDesignationCorrective)
                    .HasColumnName("FNTH_Designation_Corrective")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthDesignationIssued)
                    .HasColumnName("FNTH_Designation_Issued")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthDesignationPreventive)
                    .HasColumnName("FNTH_Designation_Preventive")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthDesignationPrvAccptd)
                    .HasColumnName("FNTH_Designation_PRV_Accptd")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthDesignationRcvd)
                    .HasColumnName("FNTH_Designation_RCVD")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthDesignationVer)
                    .HasColumnName("FNTH_Designation_ver")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthDiv)
                    .HasColumnName("FNTH_Div")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FnthDtAccptd)
                    .HasColumnName("FNTH_DT_Accptd")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnthDtCorrective)
                    .HasColumnName("FNTH_DT_Corrective")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnthDtPreventive)
                    .HasColumnName("FNTH_DT_Preventive")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnthDtPrvAccptd)
                    .HasColumnName("FNTH_DT_PRV_Accptd")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnthDtRcvd)
                    .HasColumnName("FNTH_DT_RCVD")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnthDtVer)
                    .HasColumnName("FNTH_DT_ver")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnthFnihPkRefNo).HasColumnName("FNTH_FNIH_PK_Ref_No");

                entity.Property(e => e.FnthIssueDt)
                    .HasColumnName("FNTH_Issue_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FnthNcrNo)
                    .HasColumnName("FNTH_NCR_No")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthNonConfDtl)
                    .HasColumnName("FNTH_Non_Conf_DTL")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FnthPreventiveAct)
                    .HasColumnName("FNTH_Preventive_Act")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FnthProposedCrctAct)
                    .HasColumnName("FNTH_Proposed_Crct_Act")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FnthRefId)
                    .HasColumnName("FNTH_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthRegion)
                    .HasColumnName("FNTH_Region")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthRmu)
                    .HasColumnName("FNTH_RMU")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FnthSignAccptd)
                    .HasColumnName("FNTH_SIgn_Accptd")
                    .HasMaxLength(1);

                entity.Property(e => e.FnthSignCorrective)
                    .HasColumnName("FNTH_SIgn_Corrective")
                    .HasMaxLength(1);

                entity.Property(e => e.FnthSignIssued)
                    .HasColumnName("FNTH_SIgn_Issued")
                    .HasMaxLength(1);

                entity.Property(e => e.FnthSignPreventive)
                    .HasColumnName("FNTH_SIgn_Preventive")
                    .HasMaxLength(1);

                entity.Property(e => e.FnthSignPrvAccptd).HasColumnName("FNTH_SIgn_PRV_Accptd");

                entity.Property(e => e.FnthSignRcvd)
                    .HasColumnName("FNTH_SIgn_RCVD")
                    .HasMaxLength(1);

                entity.Property(e => e.FnthSignVer)
                    .HasColumnName("FNTH_SIgn_Ver")
                    .HasMaxLength(1);

                entity.Property(e => e.FnthSourceType)
                    .HasColumnName("FNTH_Source_Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FnthSrProvider)
                    .HasColumnName("FNTH_SR_Provider")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthStatus)
                    .IsRequired()
                    .HasColumnName("FNTH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FnthSubject)
                    .HasColumnName("FNTH_Subject")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FnthUseridAccptd).HasColumnName("FNTH_Userid_Accptd");

                entity.Property(e => e.FnthUseridCorrective).HasColumnName("FNTH_Userid_Corrective");

                entity.Property(e => e.FnthUseridIssued).HasColumnName("FNTH_Userid_Issued");

                entity.Property(e => e.FnthUseridPreventive).HasColumnName("FNTH_Userid_Preventive");

                entity.Property(e => e.FnthUseridPrvAccptd).HasColumnName("FNTH_Userid_PRV_Accptd");

                entity.Property(e => e.FnthUseridRcvd).HasColumnName("FNTH_Userid_RCVD");

                entity.Property(e => e.FnthUseridVer).HasColumnName("FNTH_Userid_ver");

                entity.Property(e => e.FnthUsernameAccptd)
                    .HasColumnName("FNTH_Username_Accptd")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthUsernameAttnTo)
                    .HasColumnName("FNTH_Username_Attn_To")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthUsernameCc)
                    .HasColumnName("FNTH_Username_CC")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthUsernameCorrective)
                    .HasColumnName("FNTH_Username_Corrective")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthUsernameIssued)
                    .HasColumnName("FNTH_Username_Issued")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthUsernamePreventive)
                    .HasColumnName("FNTH_Username_Preventive")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthUsernamePrvAccptd)
                    .HasColumnName("FNTH_Username_PRV_Accptd")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthUsernameRcvd)
                    .HasColumnName("FNTH_Username_RCVD")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FnthUsernameVer)
                    .HasColumnName("FNTH_Username_ver")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.FnthFnihPkRefNoNavigation)
                    .WithMany(p => p.RmFormN2Hdr)
                    .HasForeignKey(d => d.FnthFnihPkRefNo)
                    .HasConstraintName("FK_RM_FormN2_HDR_N1");
            });

            modelBuilder.Entity<RmFormQa1EqVh>(entity =>
            {
                entity.HasKey(e => e.Fqa1evPkRefNo);

                entity.ToTable("RM_FormQA1_EQ_VH");

                entity.Property(e => e.Fqa1evPkRefNo)
                    .HasColumnName("FQA1EV_PK_Ref_No")
                    .ValueGeneratedNever();

                entity.Property(e => e.Fqa1evCapacity)
                    .HasColumnName("FQA1EV_Capacity")
                    .HasColumnType("decimal(8, 3)");

                entity.Property(e => e.Fqa1evCondition)
                    .HasColumnName("FQA1EV_Condition")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1evCrBy).HasColumnName("FQA1EV_CR_By");

                entity.Property(e => e.Fqa1evCrDt)
                    .HasColumnName("FQA1EV_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1evFqa1hPkRefNo).HasColumnName("FQA1EV_FQA1H_PK_Ref_No");

                entity.Property(e => e.Fqa1evLabourRemark)
                    .HasColumnName("FQA1EV_Labour_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1evModBy).HasColumnName("FQA1EV_Mod_By");

                entity.Property(e => e.Fqa1evModDt)
                    .HasColumnName("FQA1EV_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1evType)
                    .HasColumnName("FQA1EV_Type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1evUnit).HasColumnName("FQA1EV_Unit");

                entity.Property(e => e.Fqa1evVNo)
                    .HasColumnName("FQA1EV_V_No")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmFormQa1Gc>(entity =>
            {
                entity.HasKey(e => e.Fqa1gcPkRefNo);

                entity.ToTable("RM_FormQA1_GC");

                entity.Property(e => e.Fqa1gcPkRefNo)
                    .HasColumnName("FQA1GC_PK_Ref_No")
                    .ValueGeneratedNever();

                entity.Property(e => e.Fqa1gcCrBy).HasColumnName("FQA1GC_CR_By");

                entity.Property(e => e.Fqa1gcCrDt)
                    .HasColumnName("FQA1GC_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1gcFqa1hPkRefNo).HasColumnName("FQA1GC_FQA1H_PK_Ref_No");

                entity.Property(e => e.Fqa1gcModBy).HasColumnName("FQA1GC_Mod_By");

                entity.Property(e => e.Fqa1gcModDt)
                    .HasColumnName("FQA1GC_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1gcQualityRating)
                    .HasColumnName("FQA1GC_Quality_Rating")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1gcQualityRemarks)
                    .HasColumnName("FQA1GC_Quality_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1gcUsRatingReason)
                    .HasColumnName("FQA1GC_US_Rating_Reason")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fqa1gcFqa1hPkRefNoNavigation)
                    .WithMany(p => p.RmFormQa1Gc)
                    .HasForeignKey(d => d.Fqa1gcFqa1hPkRefNo)
                    .HasConstraintName("FK_RM_FormQA1_GC_RM_FormQA1_HDR");
            });

            modelBuilder.Entity<RmFormQa1Gen>(entity =>
            {
                entity.HasKey(e => e.Fqa1genPkRefNo);

                entity.ToTable("RM_FormQA1_GEN");

                entity.Property(e => e.Fqa1genPkRefNo)
                    .HasColumnName("FQA1GEN_PK_Ref_No")
                    .ValueGeneratedNever();

                entity.Property(e => e.Fqa1genAttRemarks)
                    .HasColumnName("FQA1GEN_Att_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1genAttTo)
                    .HasColumnName("FQA1GEN_Att_To")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1genCrBy).HasColumnName("FQA1GEN_CR_By");

                entity.Property(e => e.Fqa1genCrDt)
                    .HasColumnName("FQA1GEN_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1genFqa1hPkRefNo).HasColumnName("FQA1GEN_FQA1H_PK_Ref_No");

                entity.Property(e => e.Fqa1genItem)
                    .HasColumnName("FQA1GEN_Item")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1genModBy).HasColumnName("FQA1GEN_Mod_By");

                entity.Property(e => e.Fqa1genModDt)
                    .HasColumnName("FQA1GEN_Mod_DT")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Fqa1genFqa1hPkRefNoNavigation)
                    .WithMany(p => p.RmFormQa1Gen)
                    .HasForeignKey(d => d.Fqa1genFqa1hPkRefNo)
                    .HasConstraintName("FK_RM_FormQA1_GEN_RM_FormQA1_HDR");
            });

            modelBuilder.Entity<RmFormQa1Hdr>(entity =>
            {
                entity.HasKey(e => e.Fqa1hPkRefNo);

                entity.ToTable("RM_FormQA1_HDR");

                entity.Property(e => e.Fqa1hPkRefNo)
                    .HasColumnName("FQA1H_PK_Ref_No")
                    .ValueGeneratedNever();

                entity.Property(e => e.Fqa1hActCode)
                    .HasColumnName("FQA1H_ACT_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hActiveYn).HasColumnName("FQA1H_Active_YN");

                entity.Property(e => e.Fqa1hAuditLog).HasColumnName("FQA1H_AuditLog");

                entity.Property(e => e.Fqa1hContNo).HasColumnName("FQA1H_CONT_No");

                entity.Property(e => e.Fqa1hCrBy).HasColumnName("FQA1H_CR_By");

                entity.Property(e => e.Fqa1hCrDt)
                    .HasColumnName("FQA1H_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1hCrew).HasColumnName("FQA1H_Crew");

                entity.Property(e => e.Fqa1hDt)
                    .HasColumnName("FQA1H_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1hDtAssgn)
                    .HasColumnName("FQA1H_DT_Assgn")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1hDtChked)
                    .HasColumnName("FQA1H_DT_Chked")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1hDtExec)
                    .HasColumnName("FQA1H_DT_Exec")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1hInitialAssgn)
                    .HasColumnName("FQA1H_Initial_Assgn")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hInitialChked)
                    .HasColumnName("FQA1H_Initial_Chked")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hInitialExec)
                    .HasColumnName("FQA1H_Initial_Exec")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hModBy).HasColumnName("FQA1H_Mod_By");

                entity.Property(e => e.Fqa1hModDt)
                    .HasColumnName("FQA1H_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1hRefId)
                    .HasColumnName("FQA1H_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hRemarks)
                    .HasColumnName("FQA1H_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hRmu)
                    .HasColumnName("FQA1H_RMU")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hRoadCode)
                    .HasColumnName("FQA1H_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hRoadName)
                    .HasColumnName("FQA1H_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hSecCode)
                    .HasColumnName("FQA1H_Sec_Code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hSecName)
                    .HasColumnName("FQA1H_Sec_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hStatus)
                    .HasColumnName("FQA1H_Status")
                    .HasMaxLength(30);

                entity.Property(e => e.Fqa1hSubmitSts).HasColumnName("FQA1H_SUBMIT_STS");

                entity.Property(e => e.Fqa1hUseridAssgn).HasColumnName("FQA1H_Userid_Assgn");

                entity.Property(e => e.Fqa1hUseridChked).HasColumnName("FQA1H_Userid_Chked");

                entity.Property(e => e.Fqa1hUseridExec).HasColumnName("FQA1H_Userid_Exec");

                entity.Property(e => e.Fqa1hUsernameAssgn)
                    .HasColumnName("FQA1H_Username_Assgn")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hUsernameChked)
                    .HasColumnName("FQA1H_Username_Chked")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hUsernameExec)
                    .HasColumnName("FQA1H_Username_Exec")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hDateAudit)
                    .HasColumnName("FQA1H_Date_AUDIT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1hDateWit)
                    .HasColumnName("FQA1H_Date_WIT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1hDesignationAudit)
                    .HasColumnName("FQA1H_Designation_AUDIT")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hDesignationWit)
                    .HasColumnName("FQA1H_Designation_WIT")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hOfAudit)
                    .HasColumnName("FQA1H_Of_AUDIT")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hOfWit)
                    .HasColumnName("FQA1H_Of_WIT")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hSignAudit).HasColumnName("FQA1H_Sign_AUDIT");

                entity.Property(e => e.Fqa1hSignWit).HasColumnName("FQA1H_Sign_WIT");

                entity.Property(e => e.Fqa1hUseridAudit).HasColumnName("FQA1H_Userid_AUDIT");

                entity.Property(e => e.Fqa1hUseridWit).HasColumnName("FQA1H_Userid_WIT");

                entity.Property(e => e.Fqa1hUsernameAudit)
                    .HasColumnName("FQA1H_Username_AUDIT")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1hUsernameWit)
                    .HasColumnName("FQA1H_Username_WIT")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmFormQa1Lab>(entity =>
            {
                entity.HasKey(e => e.Fqa1lPkRefNo);

                entity.ToTable("RM_FormQA1_LAB");

                entity.Property(e => e.Fqa1lPkRefNo)
                    .HasColumnName("FQA1L_PK_Ref_No")
                    .ValueGeneratedNever();

                entity.Property(e => e.Fqa1lCrBy).HasColumnName("FQA1L_CR_By");

                entity.Property(e => e.Fqa1lCrDt)
                    .HasColumnName("FQA1L_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1lFqa1hPkRefNo).HasColumnName("FQA1L_FQA1H_PK_Ref_No");

                entity.Property(e => e.Fqa1lLabour)
                    .HasColumnName("FQA1L_Labour")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1lLabourOnLeave).HasColumnName("FQA1L_Labour_On_Leave");

                entity.Property(e => e.Fqa1lLabourOnSite).HasColumnName("FQA1L_Labour_On_Site");

                entity.Property(e => e.Fqa1lLabourPerformanceStd).HasColumnName("FQA1L_Labour_Performance_Std");

                entity.Property(e => e.Fqa1lLabourRemark)
                    .HasColumnName("FQA1L_Labour_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1lModBy).HasColumnName("FQA1L_Mod_By");

                entity.Property(e => e.Fqa1lModDt)
                    .HasColumnName("FQA1L_Mod_DT")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Fqa1lFqa1hPkRefNoNavigation)
                    .WithMany(p => p.RmFormQa1Lab)
                    .HasForeignKey(d => d.Fqa1lFqa1hPkRefNo)
                    .HasConstraintName("FK_RM_FormQA1_LAB_RM_FormQA1_HDR");
            });

            modelBuilder.Entity<RmFormQa1Mat>(entity =>
            {
                entity.HasKey(e => e.Fqa1mPkRefNo);

                entity.ToTable("RM_FormQA1_MAT");

                entity.Property(e => e.Fqa1mPkRefNo)
                    .HasColumnName("FQA1M_PK_Ref_No")
                    .ValueGeneratedNever();

                entity.Property(e => e.Fqa1mCrBy).HasColumnName("FQA1M_CR_By");

                entity.Property(e => e.Fqa1mCrDt)
                    .HasColumnName("FQA1M_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1mFqa1hPkRefNo).HasColumnName("FQA1M_FQA1H_PK_Ref_No");

                entity.Property(e => e.Fqa1mModBy).HasColumnName("FQA1M_Mod_By");

                entity.Property(e => e.Fqa1mModDt)
                    .HasColumnName("FQA1M_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1mQty)
                    .HasColumnName("FQA1M_Qty")
                    .HasColumnType("decimal(8, 3)");

                entity.Property(e => e.Fqa1mRemark)
                    .HasColumnName("FQA1M_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1mSpec).HasColumnName("FQA1M_Spec");

                entity.Property(e => e.Fqa1mType)
                    .HasColumnName("FQA1M_Type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1mUnit).HasColumnName("FQA1M_Unit");

                entity.HasOne(d => d.Fqa1mFqa1hPkRefNoNavigation)
                    .WithMany(p => p.RmFormQa1Mat)
                    .HasForeignKey(d => d.Fqa1mFqa1hPkRefNo)
                    .HasConstraintName("FK_RM_FormQA1_MAT_RM_FormQA1_HDR");
            });

            modelBuilder.Entity<RmFormQa1Ssc>(entity =>
            {
                entity.HasKey(e => e.Fqa1sscPkRefNo)
                    .HasName(@"PK_RM_FormQA1_SSC
");

                entity.ToTable("RM_FormQA1_SSC");

                entity.Property(e => e.Fqa1sscPkRefNo)
                    .HasColumnName("FQA1SSC_PK_Ref_No")
                    .ValueGeneratedNever();

                entity.Property(e => e.Fqa1sscAsd)
                    .HasColumnName("FQA1SSC__ASD")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1sscAsdRemark)
                    .HasColumnName("FQA1SSC__ASD_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1sscCrBy).HasColumnName("FQA1SSC_CR_By");

                entity.Property(e => e.Fqa1sscCrDt)
                    .HasColumnName("FQA1SSC_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1sscEd)
                    .HasColumnName("FQA1SSC__ED")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1sscEdRemark)
                    .HasColumnName("FQA1SSC__ED_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1sscFqa1hPkRefNo).HasColumnName("FQA1SSC_FQA1H_PK_Ref_No");

                entity.Property(e => e.Fqa1sscIms)
                    .HasColumnName("FQA1SSC__IMS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1sscImsRemark)
                    .HasColumnName("FQA1SSC__IMS_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1sscModBy).HasColumnName("FQA1SSC_Mod_By");

                entity.Property(e => e.Fqa1sscModDt)
                    .HasColumnName("FQA1SSC_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1sscSp)
                    .HasColumnName("FQA1SSC__SP")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1sscSpRemark)
                    .HasColumnName("FQA1SSC__SP_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1sscWpe)
                    .HasColumnName("FQA1SSC__WPE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1sscWpeRemark)
                    .HasColumnName("FQA1SSC__WPE_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fqa1sscFqa1hPkRefNoNavigation)
                    .WithMany(p => p.RmFormQa1Ssc)
                    .HasForeignKey(d => d.Fqa1sscFqa1hPkRefNo)
                    .HasConstraintName("FK_RM_FormQA1_SSC_RM_FormQA1_HDR");
            });

            modelBuilder.Entity<RmFormQa1Tes>(entity =>
            {
                entity.HasKey(e => e.Fqa1tesPkRefNo);

                entity.ToTable("RM_FormQA1_TES");

                entity.Property(e => e.Fqa1tesPkRefNo)
                    .HasColumnName("FQA1TES_PK_Ref_No")
                    .ValueGeneratedNever();

                entity.Property(e => e.Fqa1tesCbrCs)
                    .HasColumnName("FQA1TES_CBR_CS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1tesCbrCsA).HasColumnName("FQA1TES_CBR_CS_A");

                entity.Property(e => e.Fqa1tesCbrCsRemark)
                    .HasColumnName("FQA1TES_CBR_CS_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1tesCrBy).HasColumnName("FQA1TES_CR_By");

                entity.Property(e => e.Fqa1tesCrDt)
                    .HasColumnName("FQA1TES_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1tesCtCs)
                    .HasColumnName("FQA1TES_CT_CS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1tesCtCsA).HasColumnName("FQA1TES_CT_CS_A");

                entity.Property(e => e.Fqa1tesCtCsRemark)
                    .HasColumnName("FQA1TES_CT_CS_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1tesDtCs)
                    .HasColumnName("FQA1TES_DT_CS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1tesDtCsA).HasColumnName("FQA1TES_DT_CS_A");

                entity.Property(e => e.Fqa1tesDtCsRemark)
                    .HasColumnName("FQA1TES_DT_CS_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1tesFqa1hPkRefNo).HasColumnName("FQA1TES_FQA1H_PK_Ref_No");

                entity.Property(e => e.Fqa1tesMgtCs)
                    .HasColumnName("FQA1TES_MGT_CS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1tesMgtCsA).HasColumnName("FQA1TES_MGT_CS_A");

                entity.Property(e => e.Fqa1tesMgtCsRemark)
                    .HasColumnName("FQA1TES_MGT_CS_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1tesModBy).HasColumnName("FQA1TES_Mod_By");

                entity.Property(e => e.Fqa1tesModDt)
                    .HasColumnName("FQA1TES_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1tesOtCs)
                    .HasColumnName("FQA1TES_OT_CS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1tesOtCsA).HasColumnName("FQA1TES_OT_CS_A");

                entity.Property(e => e.Fqa1tesOtCsRemark)
                    .HasColumnName("FQA1TES_OT_CS_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fqa1tesFqa1hPkRefNoNavigation)
                    .WithMany(p => p.RmFormQa1Tes)
                    .HasForeignKey(d => d.Fqa1tesFqa1hPkRefNo)
                    .HasConstraintName("FK_RM_FormQA1_TES_RM_FormQA1_HDR");
            });

            modelBuilder.Entity<RmFormQa1Wcq>(entity =>
            {
                entity.HasKey(e => e.Fqa1wcqPkRefNo);

                entity.ToTable("RM_FormQA1_WCQ");

                entity.Property(e => e.Fqa1wcqPkRefNo)
                    .HasColumnName("FQA1WCQ_PK_Ref_No")
                    .ValueGeneratedNever();

                entity.Property(e => e.Fqa1wcqCrBy).HasColumnName("FQA1WCQ_CR_By");

                entity.Property(e => e.Fqa1wcqCrDt)
                    .HasColumnName("FQA1WCQ_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1wcqFl)
                    .HasColumnName("FQA1WCQ_FL")
                    .HasColumnType("decimal(8, 3)");

                entity.Property(e => e.Fqa1wcqFlRemark)
                    .HasColumnName("FQA1WCQ_FL_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wcqFlType)
                    .HasColumnName("FQA1WCQ_FL_Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wcqFlUnit).HasColumnName("FQA1WCQ_FL_Unit");

                entity.Property(e => e.Fqa1wcqFqa1hPkRefNo).HasColumnName("FQA1WCQ_FQA1H_PK_Ref_No");

                entity.Property(e => e.Fqa1wcqJRemark)
                    .HasColumnName("FQA1WCQ_J_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wcqJType)
                    .HasColumnName("FQA1WCQ_J_Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wcqModBy).HasColumnName("FQA1WCQ_Mod_By");

                entity.Property(e => e.Fqa1wcqModDt)
                    .HasColumnName("FQA1WCQ_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1wcqSrRemark)
                    .HasColumnName("FQA1WCQ_SR_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wcqSrType)
                    .HasColumnName("FQA1WCQ_SR_Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fqa1wcqFqa1hPkRefNoNavigation)
                    .WithMany(p => p.RmFormQa1Wcq)
                    .HasForeignKey(d => d.Fqa1wcqFqa1hPkRefNo)
                    .HasConstraintName("FK_RM_FormQA1_WCQ_RM_FormQA1_HDR");
            });

            modelBuilder.Entity<RmFormQa1We>(entity =>
            {
                entity.HasKey(e => e.Fqa1wPkRefNo);

                entity.ToTable("RM_FormQA1_WE");

                entity.Property(e => e.Fqa1wPkRefNo)
                    .HasColumnName("FQA1W_PK_Ref_No")
                    .ValueGeneratedNever();

                entity.Property(e => e.Fqa1wAcbcPasses).HasColumnName("FQA1W_ACBC_Passes");

                entity.Property(e => e.Fqa1wAcbcRemark)
                    .HasColumnName("FQA1W_ACBC_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wAcbcTemperature)
                    .HasColumnName("FQA1W_ACBC_Temperature")
                    .HasColumnType("decimal(8, 3)");

                entity.Property(e => e.Fqa1wAcbcTemperatureUnit).HasColumnName("FQA1W_ACBC_Temperature_Unit");

                entity.Property(e => e.Fqa1wAcbcThinkness)
                    .HasColumnName("FQA1W_ACBC_Thinkness")
                    .HasColumnType("decimal(8, 3)");

                entity.Property(e => e.Fqa1wAcbcThinknessUnit).HasColumnName("FQA1W_ACBC_Thinkness_Unit");

                entity.Property(e => e.Fqa1wAcwcPasses).HasColumnName("FQA1W_ACWC_Passes");

                entity.Property(e => e.Fqa1wAcwcRemark)
                    .HasColumnName("FQA1W_ACWC_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wAcwcTemperature)
                    .HasColumnName("FQA1W_ACWC_Temperature")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Fqa1wAcwcTemperatureUnit).HasColumnName("FQA1W_ACWC_Temperature_Unit");

                entity.Property(e => e.Fqa1wAcwcThinkness)
                    .HasColumnName("FQA1W_ACWC_Thinkness")
                    .HasColumnType("decimal(8, 3)");

                entity.Property(e => e.Fqa1wAcwcThinknessUnit).HasColumnName("FQA1W_ACWC_Thinkness_Unit");

                entity.Property(e => e.Fqa1wCrBy).HasColumnName("FQA1W_CR_By");

                entity.Property(e => e.Fqa1wCrDt)
                    .HasColumnName("FQA1W_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1wFqa1hPkRefNo).HasColumnName("FQA1W_FQA1H_PK_Ref_No");

                entity.Property(e => e.Fqa1wModBy).HasColumnName("FQA1W_Mod_By");

                entity.Property(e => e.Fqa1wModDt)
                    .HasColumnName("FQA1W_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fqa1wPcDRate)
                    .HasColumnName("FQA1W_PC_D_Rate")
                    .HasColumnType("decimal(8, 3)");

                entity.Property(e => e.Fqa1wPcDRateUnit).HasColumnName("FQA1W_PC_D_Rate_Unit");

                entity.Property(e => e.Fqa1wPcEvenlySpread).HasColumnName("FQA1W_PC_Evenly_Spread");

                entity.Property(e => e.Fqa1wPcRemark)
                    .HasColumnName("FQA1W_PC_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wPcType)
                    .HasColumnName("FQA1W_PC_Type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wRbLayers).HasColumnName("FQA1W_RB_Layers");

                entity.Property(e => e.Fqa1wRbPasses).HasColumnName("FQA1W_RB_Passes");

                entity.Property(e => e.Fqa1wRbRemark)
                    .HasColumnName("FQA1W_RB_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wRbThinkness)
                    .HasColumnName("FQA1W_RB_Thinkness")
                    .HasColumnType("decimal(8, 3)");

                entity.Property(e => e.Fqa1wRbThinknessUnit).HasColumnName("FQA1W_RB_Thinkness_Unit");

                entity.Property(e => e.Fqa1wSbLayers).HasColumnName("FQA1W_SB_Layers");

                entity.Property(e => e.Fqa1wSbPasses).HasColumnName("FQA1W_SB_Passes");

                entity.Property(e => e.Fqa1wSbRemark)
                    .HasColumnName("FQA1W_SB_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wSbThinkness)
                    .HasColumnName("FQA1W_SB_Thinkness")
                    .HasColumnType("decimal(8, 3)");

                entity.Property(e => e.Fqa1wSbThinknessUnit).HasColumnName("FQA1W_SB_Thinkness_Unit");

                entity.Property(e => e.Fqa1wSgLayers).HasColumnName("FQA1W_SG_Layers");

                entity.Property(e => e.Fqa1wSgPasses).HasColumnName("FQA1W_SG_Passes");

                entity.Property(e => e.Fqa1wSgRemark)
                    .HasColumnName("FQA1W_SG_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wSgThinkness)
                    .HasColumnName("FQA1W_SG_Thinkness")
                    .HasColumnType("decimal(8, 3)");

                entity.Property(e => e.Fqa1wSgThinknessUnit).HasColumnName("FQA1W_SG_Thinkness_Unit");

                entity.Property(e => e.Fqa1wSsdPp)
                    .HasColumnName("FQA1W_SSD_PP")
                    .HasColumnType("decimal(8, 3)");

                entity.Property(e => e.Fqa1wSsdPpUnit).HasColumnName("FQA1W_SSD_PP_Unit");

                entity.Property(e => e.Fqa1wSsdRemark)
                    .HasColumnName("FQA1W_SSD_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wSsdSb)
                    .HasColumnName("FQA1W_SSD_SB")
                    .HasColumnType("decimal(8, 3)");

                entity.Property(e => e.Fqa1wSsdSbUnit).HasColumnName("FQA1W_SSD_SB_Unit");

                entity.Property(e => e.Fqa1wTcDRate)
                    .HasColumnName("FQA1W_TC_D_Rate")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Fqa1wTcDRateUnit).HasColumnName("FQA1W_TC_D_Rate_Unit");

                entity.Property(e => e.Fqa1wTcEvenlySpread).HasColumnName("FQA1W_TC_Evenly_Spread");

                entity.Property(e => e.Fqa1wTcRemark)
                    .HasColumnName("FQA1W_TC_Remark")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fqa1wTcType)
                    .HasColumnName("FQA1W_TC_Type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fqa1wFqa1hPkRefNoNavigation)
                    .WithMany(p => p.RmFormQa1We)
                    .HasForeignKey(d => d.Fqa1wFqa1hPkRefNo)
                    .HasConstraintName("FK_RM_FormQA1_WE_RM_FormQA1_HDR");
            });

            modelBuilder.Entity<RmFormQa2Dtl>(entity =>
            {
                entity.HasKey(e => e.FqaiidPkRefNo)
                    .HasName("PK__RM_FormQ__B9E996F1F7A877F0");

                entity.ToTable("RM_FormQA2_DTL");

                entity.Property(e => e.FqaiidPkRefNo).HasColumnName("FQAIID_PK_Ref_No");

                entity.Property(e => e.FqaiidActiveYn)
                    .IsRequired()
                    .HasColumnName("FQAIID_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FqaiidCrBy)
                    .HasColumnName("FQAIID_CR_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidCrDt)
                    .HasColumnName("FQAIID_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FqaiidDefCode)
                    .HasColumnName("FQAIID_Def_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidDefDesc)
                    .HasColumnName("FQAIID_Def_desc")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidDtInitialCond)
                    .HasColumnName("FQAIID_DT_Initial_Cond")
                    .HasColumnType("datetime");

                entity.Property(e => e.FqaiidDtQaI)
                    .HasColumnName("FQAIID_DT_QA_I")
                    .HasColumnType("datetime");

                entity.Property(e => e.FqaiidDtQaIi)
                    .HasColumnName("FQAIID_DT_QA_II")
                    .HasColumnType("datetime");

                entity.Property(e => e.FqaiidDtQaIii)
                    .HasColumnName("FQAIID_DT_QA_III")
                    .HasColumnType("datetime");

                entity.Property(e => e.FqaiidDtQaIv)
                    .HasColumnName("FQAIID_DT_QA_IV")
                    .HasColumnType("datetime");

                entity.Property(e => e.FqaiidFqaiihPkRefNo).HasColumnName("FQAIID_FQAIIH_PK_Ref_No");

                entity.Property(e => e.FqaiidFrmCh).HasColumnName("FQAIID_FRM_CH");

                entity.Property(e => e.FqaiidFrmChDeci).HasColumnName("FQAIID_FRM_CH_Deci");

                entity.Property(e => e.FqaiidFsidPkRefNo).HasColumnName("FQAIID_FSID_PK_Ref_No");

                entity.Property(e => e.FqaiidIniCycType)
                    .HasColumnName("FQAIID_Ini_Cyc_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidInitialCond).HasColumnName("FQAIID_Initial_Cond");

                entity.Property(e => e.FqaiidModBy)
                    .HasColumnName("FQAIID_Mod_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidModDt)
                    .HasColumnName("FQAIID_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FqaiidQaI).HasColumnName("FQAIID_QA_I");

                entity.Property(e => e.FqaiidQaIi).HasColumnName("FQAIID_QA_II");

                entity.Property(e => e.FqaiidQaIii).HasColumnName("FQAIID_QA_III");

                entity.Property(e => e.FqaiidQaIv).HasColumnName("FQAIID_QA_IV");

                entity.Property(e => e.FqaiidQaiCycType)
                    .HasColumnName("FQAIID_QAI_Cyc_type")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidQaiiCycType)
                    .HasColumnName("FQAIID_QAII_Cyc_type")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidQaiiiCycType)
                    .HasColumnName("FQAIID_QAIII_Cyc_type")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidQaivCycType)
                    .HasColumnName("FQAIID_QAIV_Cyc_type")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidRefId)
                    .HasColumnName("FQAIID_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidRemarks)
                    .HasColumnName("FQAIID_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidRwrkDimH).HasColumnName("FQAIID_RWRK_DIM_H");

                entity.Property(e => e.FqaiidRwrkDimL).HasColumnName("FQAIID_RWRK_DIM_L");

                entity.Property(e => e.FqaiidRwrkDimW).HasColumnName("FQAIID_RWRK_DIM_W");

                entity.Property(e => e.FqaiidSiteRef)
                    .HasColumnName("FQAIID_Site_Ref")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidSourceType)
                    .HasColumnName("FQAIID_Source_Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidSrno).HasColumnName("FQAIID_SRNO");

                entity.Property(e => e.FqaiidSubmitSts).HasColumnName("FQAIID_SUBMIT_STS");

                entity.Property(e => e.FqaiidToCh).HasColumnName("FQAIID_To_CH");

                entity.Property(e => e.FqaiidToChDeci).HasColumnName("FQAIID_To_CH_Deci");

                entity.Property(e => e.FqaiidWrkAct)
                    .HasColumnName("FQAIID_Wrk_Act")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiidWws13aFol)
                    .HasColumnName("FQAIID_WWS_13A_Fol")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihNcnYn).HasColumnName("FQAIIH_NCN_YN");

                entity.HasOne(d => d.FqaiidFqaiihPkRefNoNavigation)
                    .WithMany(p => p.RmFormQa2Dtl)
                    .HasForeignKey(d => d.FqaiidFqaiihPkRefNo)
                    .HasConstraintName("FK__RM_FormQA__FQAII__29AC2CE0");

                entity.HasOne(d => d.FqaiidFsidPkRefNoNavigation)
                    .WithMany(p => p.RmFormQa2Dtl)
                    .HasForeignKey(d => d.FqaiidFsidPkRefNo)
                    .HasConstraintName("FQAIID_FSID_PK_Ref_No");
            });

            modelBuilder.Entity<RmFormQa2Hdr>(entity =>
            {
                entity.HasKey(e => e.FqaiihPkRefNo)
                    .HasName("PK__RM_FormQ__66942BBD40726B79");

                entity.ToTable("RM_FormQA2_HDR");

                entity.Property(e => e.FqaiihPkRefNo).HasColumnName("FQAIIH_PK_Ref_No");

                entity.Property(e => e.FqaiihActiveYn)
                    .IsRequired()
                    .HasColumnName("FQAIIH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FqaiihComments)
                    .HasColumnName("FQAIIH_Comments")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihContNo)
                    .HasColumnName("FQAIIH_CONT_No")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihCrBy)
                    .HasColumnName("FQAIIH_CR_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihCrDt)
                    .HasColumnName("FQAIIH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FqaiihCrewSup)
                    .HasColumnName("FQAIIH_Crew_Sup")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihCrewSupName)
                    .HasColumnName("FQAIIH_Crew_Sup_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihDesignationQaI)
                    .HasColumnName("FQAIIH_Designation_QA_I")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihDesignationQaIi)
                    .HasColumnName("FQAIIH_Designation_QA_II")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihDesignationQaIii)
                    .HasColumnName("FQAIIH_Designation_QA_III")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihDesignationQaIni)
                    .HasColumnName("FQAIIH_Designation_QA_Ini")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihDesignationQaIv)
                    .HasColumnName("FQAIIH_Designation_QA_IV")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihModBy)
                    .HasColumnName("FQAIIH_Mod_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihModDt)
                    .HasColumnName("FQAIIH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FqaiihMonth).HasColumnName("FQAIIH_Month");

                entity.Property(e => e.FqaiihRefId)
                    .HasColumnName("FQAIIH_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihRemarksQaI)
                    .HasColumnName("FQAIIH_Remarks_QA_I")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihRemarksQaIi)
                    .HasColumnName("FQAIIH_Remarks_QA_II")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihRemarksQaIii)
                    .HasColumnName("FQAIIH_Remarks_QA_III")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihRemarksQaIni)
                    .HasColumnName("FQAIIH_Remarks_QA_Ini")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihRemarksQaIv)
                    .HasColumnName("FQAIIH_Remarks_QA_IV")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihRmu)
                    .HasColumnName("FQAIIH_RMU")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihRoadCode)
                    .HasColumnName("FQAIIH_Road_Code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihRoadName)
                    .HasColumnName("FQAIIH_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihSection)
                    .HasColumnName("FQAIIH_Section")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihSignQaI)
                    .HasColumnName("FQAIIH_SIgn_QA_I")
                    .HasMaxLength(1);

                entity.Property(e => e.FqaiihSignQaIi)
                    .HasColumnName("FQAIIH_SIgn_QA_II")
                    .HasMaxLength(1);

                entity.Property(e => e.FqaiihSignQaIii)
                    .HasColumnName("FQAIIH_SIgn_QA_III")
                    .HasMaxLength(1);

                entity.Property(e => e.FqaiihSignQaIni)
                    .HasColumnName("FQAIIH_SIgn_QA_Ini")
                    .HasMaxLength(1);

                entity.Property(e => e.FqaiihSignQaIv)
                    .HasColumnName("FQAIIH_SIgn_QA_IV")
                    .HasMaxLength(1);

                entity.Property(e => e.FqaiihSubmitSts).HasColumnName("FQAIIH_SUBMIT_STS");

                entity.Property(e => e.FqaiihUseridQaI).HasColumnName("FQAIIH_Userid_QA_I");

                entity.Property(e => e.FqaiihUseridQaIi).HasColumnName("FQAIIH_Userid_QA_II");

                entity.Property(e => e.FqaiihUseridQaIii).HasColumnName("FQAIIH_Userid_QA_III");

                entity.Property(e => e.FqaiihUseridQaIni).HasColumnName("FQAIIH_Userid_QA_Ini");

                entity.Property(e => e.FqaiihUseridQaIv).HasColumnName("FQAIIH_Userid_QA_IV");

                entity.Property(e => e.FqaiihUsernameQaI)
                    .HasColumnName("FQAIIH_Username_QA_I")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihUsernameQaIi)
                    .HasColumnName("FQAIIH_Username_QA_II")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihUsernameQaIii)
                    .HasColumnName("FQAIIH_Username_QA_III")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihUsernameQaIni)
                    .HasColumnName("FQAIIH_Username_QA_Ini")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihUsernameQaIv)
                    .HasColumnName("FQAIIH_Username_QA_IV")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FqaiihYear).HasColumnName("FQAIIH_Year");
            });

            modelBuilder.Entity<RmFormS1Dtl>(entity =>
            {
                entity.HasKey(e => e.FsidPkRefNo)
                    .HasName("PK__RM_FormS__85C6AAAA109BDA5C");

                entity.ToTable("RM_FormS1_DTL");

                entity.Property(e => e.FsidPkRefNo).HasColumnName("FSID_PK_Ref_No");

                entity.Property(e => e.FsidActCode)
                    .HasColumnName("FSID_Act_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FsidActId).HasColumnName("FSID_Act_Id");

                entity.Property(e => e.FsidActName)
                    .HasColumnName("FSID_Act_Name")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FsidActiveYn)
                    .IsRequired()
                    .HasColumnName("FSID_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FsidCrBy).HasColumnName("FSID_CR_By");

                entity.Property(e => e.FsidCrDt)
                    .HasColumnName("FSID_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsidCrewSupervisor).HasColumnName("FSID_Crew_Supervisor");

                entity.Property(e => e.FsidCrewSupervisorName)
                    .HasColumnName("FSID_Crew_Supervisor_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsidFapMt).HasColumnName("FSID_FAP_MT");

                entity.Property(e => e.FsidFapN1).HasColumnName("FSID_FAP_N1");

                entity.Property(e => e.FsidFapN2).HasColumnName("FSID_FAP_N2");

                entity.Property(e => e.FsidFapQa1).HasColumnName("FSID_FAP_QA1");

                entity.Property(e => e.FsidFapQa2).HasColumnName("FSID_FAP_QA2");

                entity.Property(e => e.FsidFapRa).HasColumnName("FSID_FAP_RA");

                entity.Property(e => e.FsidFapSa).HasColumnName("FSID_FAP_SA");

                entity.Property(e => e.FsidFormACdr)
                    .HasColumnName("FSID_FormA_CDR")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FsidFormAPriority)
                    .HasColumnName("FSID_FormA_Priority")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsidFormASiteRef)
                    .HasColumnName("FSID_FormA_SiteRef")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsidFormAWorkQty)
                    .HasColumnName("FSID_FormA_WorkQty")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FsidFormType)
                    .HasColumnName("FSID_Form_Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FsidFormTypeRefNo).HasColumnName("FSID_FormType_Ref_No");

                entity.Property(e => e.FsidFrmChKm).HasColumnName("FSID_FRM_CH_KM");

                entity.Property(e => e.FsidFrmChM)
                    .HasColumnName("FSID_FRM_CH_M")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FsidFsihPkRefNo).HasColumnName("FSID_FSIH_PK_Ref_No");

                entity.Property(e => e.FsidModBy).HasColumnName("FSID_Mod_By");

                entity.Property(e => e.FsidModDt)
                    .HasColumnName("FSID_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsidRcvFromJkrs).HasColumnName("FSID_Rcv_From_JKRS");

                entity.Property(e => e.FsidRcvFromJkrsDt)
                    .HasColumnName("FSID_Rcv_From_JKRS_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsidRefId)
                    .HasColumnName("FSID_Ref_Id")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsidRemarks)
                    .HasColumnName("FSID_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FsidSentToJkrs).HasColumnName("FSID_Sent_To_JKRS");

                entity.Property(e => e.FsidSentToJkrsDt)
                    .HasColumnName("FSID_Sent_To_JKRS_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsidSubmitSts).HasColumnName("FSID_SUBMIT_STS");

                entity.Property(e => e.FsidToChKm).HasColumnName("FSID_TO_CH_KM");

                entity.Property(e => e.FsidToChM)
                    .HasColumnName("FSID_TO_CH_M")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FsiidRoadCode)
                    .HasColumnName("FSIID_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FsiidRoadId).HasColumnName("FSIID_Road_Id");

                entity.Property(e => e.FsiidRoadName)
                    .HasColumnName("FSIID_Road_Name")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.HasOne(d => d.FsidFsihPkRefNoNavigation)
                    .WithMany(p => p.RmFormS1Dtl)
                    .HasForeignKey(d => d.FsidFsihPkRefNo)
                    .HasConstraintName("FK__RM_FormS1__FSID___1E6F845E");
            });

            modelBuilder.Entity<RmFormS1Hdr>(entity =>
            {
                entity.HasKey(e => e.FsihPkRefNo)
                    .HasName("PK__RM_FormS__5959C3421DFDD7BD");

                entity.ToTable("RM_FormS1_HDR");

                entity.Property(e => e.FsihPkRefNo).HasColumnName("FSIH_PK_Ref_No");

                entity.Property(e => e.FsihActiveYn).HasColumnName("FSIH_Active_YN");

                entity.Property(e => e.FsihAuditLog).HasColumnName("FSIH_AuditLog");

                entity.Property(e => e.FsihCrBy).HasColumnName("FSIH_CR_By");

                entity.Property(e => e.FsihCrDt)
                    .HasColumnName("FSIH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsihDt)
                    .HasColumnName("FSIH_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsihFromDt)
                    .HasColumnName("FSIH_From_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsihModBy).HasColumnName("FSIH_Mod_By");

                entity.Property(e => e.FsihModDt)
                    .HasColumnName("FSIH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsihRefId)
                    .HasColumnName("FSIH_Ref_Id")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsihRemarks)
                    .HasColumnName("FSIH_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FsihRmu)
                    .HasColumnName("FSIH_RMU")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FsihStatus)
                    .IsRequired()
                    .HasColumnName("FSIH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FsihSubmitSts).HasColumnName("FSIH_SUBMIT_STS");

                entity.Property(e => e.FsihToDt)
                    .HasColumnName("FSIH_To_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsihWeekNo).HasColumnName("FSIH_WeekNo");

                entity.Property(e => e.FsiihDtAgrd)
                    .HasColumnName("FSIIH_DT_Agrd")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiihDtPlan)
                    .HasColumnName("FSIIH_DT_Plan")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiihDtVet)
                    .HasColumnName("FSIIH_DT_Vet")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiihUserDesignationAgrd)
                    .HasColumnName("FSIIH_User_Designation_Agrd")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserDesignationPlan)
                    .HasColumnName("FSIIH_User_Designation_Plan")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserDesignationVet)
                    .HasColumnName("FSIIH_User_Designation_Vet")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserNameAgrd)
                    .HasColumnName("FSIIH_User_Name_Agrd")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserNamePlan)
                    .HasColumnName("FSIIH_User_Name_Plan")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserNameVet)
                    .HasColumnName("FSIIH_User_Name_Vet")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUseridAgrd).HasColumnName("FSIIH_Userid_Agrd");

                entity.Property(e => e.FsiihUseridPlan).HasColumnName("FSIIH_Userid_Plan");

                entity.Property(e => e.FsiihUseridVet).HasColumnName("FSIIH_Userid_Vet");
            });

            modelBuilder.Entity<RmFormS1WkDtl>(entity =>
            {
                entity.HasKey(e => e.FsiwdPkRefNo)
                    .HasName("PK__RM_FormS__96B8BA5CBAC076AB");

                entity.ToTable("RM_FormS1_WK_DTL");

                entity.Property(e => e.FsiwdPkRefNo).HasColumnName("FSIWD_PK_Ref_No");

                entity.Property(e => e.FsiwdActual).HasColumnName("FSIWD_Actual");

                entity.Property(e => e.FsiwdCrBy).HasColumnName("FSIWD_CR_By");

                entity.Property(e => e.FsiwdCrDt)
                    .HasColumnName("FSIWD_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiwdFsidPkRefNo).HasColumnName("FSIWD_FSID_PK_Ref_No");

                entity.Property(e => e.FsiwdPlanned).HasColumnName("FSIWD_Planned");

                entity.Property(e => e.FsiwdSchldDate)
                    .HasColumnName("FSIWD_Schld_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiwdSchldDayOfWeek).HasColumnName("FSIWD_Schld_Day_of_week");

                entity.HasOne(d => d.FsiwdFsidPkRefNoNavigation)
                    .WithMany(p => p.RmFormS1WkDtl)
                    .HasForeignKey(d => d.FsiwdFsidPkRefNo)
                    .HasConstraintName("FK__RM_FormS1__FSIWD__24285DB4");
            });

            modelBuilder.Entity<RmFormS2Dtl>(entity =>
            {
                entity.HasKey(e => e.FsiidPkRefNo)
                    .HasName("PK__RM_FormS__929B49DBC4ADF3B2");

                entity.ToTable("RM_FormS2_DTL");

                entity.Property(e => e.FsiidPkRefNo).HasColumnName("FSIID_PK_Ref_No");

                entity.Property(e => e.FsiidActiveYn)
                    .IsRequired()
                    .HasColumnName("FSIID_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FsiidAdp).HasColumnName("FSIID_ADP");

                entity.Property(e => e.FsiidCil).HasColumnName("FSIID_CIL");

                entity.Property(e => e.FsiidCrBy).HasColumnName("FSIID_CR_By");

                entity.Property(e => e.FsiidCrDt)
                    .HasColumnName("FSIID_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiidCrwAllwcdQuar).HasColumnName("FSIID_CRW_Allwcd_Quar");

                entity.Property(e => e.FsiidCrwDaysReq).HasColumnName("FSIID_CRW_Days_req");

                entity.Property(e => e.FsiidFsiihPkRefNo).HasColumnName("FSIID_FSIIH_PK_Ref_No");

                entity.Property(e => e.FsiidModBy).HasColumnName("FSIID_Mod_By");

                entity.Property(e => e.FsiidModDt)
                    .HasColumnName("FSIID_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiidPriority).HasColumnName("FSIID_Priority");

                entity.Property(e => e.FsiidPriorityI).HasColumnName("FSIID_Priority_I");

                entity.Property(e => e.FsiidPriorityIi).HasColumnName("FSIID_Priority_II");

                entity.Property(e => e.FsiidRdLocSeq)
                    .HasColumnName("FSIID_RD_Loc_Seq")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsiidRefId)
                    .HasColumnName("FSIID_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsiidRemarks)
                    .HasColumnName("FSIID_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FsiidRoadCode)
                    .HasColumnName("FSIID_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FsiidRoadId).HasColumnName("FSIID_Road_Id");

                entity.Property(e => e.FsiidRoadName)
                    .HasColumnName("FSIID_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FsiidRoadPavedLength)
                    .HasColumnName("FSIID_Road_Paved_Length")
                    .HasColumnType("decimal(6, 3)");

                entity.Property(e => e.FsiidRoadUnPavedLength)
                    .HasColumnName("FSIID_Road_UnPaved_Length")
                    .HasColumnType("decimal(6, 3)");

                entity.Property(e => e.FsiidSubmitSts).HasColumnName("FSIID_SUBMIT_STS");

                entity.Property(e => e.FsiidTargetPercent)
                    .HasColumnName("FSIID_Target_Percent")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.FsiidWorkQty)
                    .HasColumnName("FSIID_WorkQty")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FsiidFsiihPkRefNoNavigation)
                    .WithMany(p => p.RmFormS2Dtl)
                    .HasForeignKey(d => d.FsiidFsiihPkRefNo)
                    .HasConstraintName("FK__RM_FormS2__FSIID__2D7CBDC4");
            });

            modelBuilder.Entity<RmFormS2Hdr>(entity =>
            {
                entity.HasKey(e => e.FsiihPkRefNo)
                    .HasName("PK__RM_FormS__52EC544226430E73");

                entity.ToTable("RM_FormS2_HDR");

                entity.Property(e => e.FsiihPkRefNo).HasColumnName("FSIIH_PK_Ref_No");

                entity.Property(e => e.FsiihActCode)
                    .HasColumnName("FSIIH_Act_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihActId).HasColumnName("FSIIH_ACT_Id");

                entity.Property(e => e.FsiihActName)
                    .HasColumnName("FSIIH_Act_Name")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihActiveYn)
                    .IsRequired()
                    .HasColumnName("FSIIH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FsiihAuditLog).HasColumnName("FSIIH_AuditLog");

                entity.Property(e => e.FsiihContNo)
                    .HasColumnName("FSIIH_CONT_No")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihCrBy).HasColumnName("FSIIH_CR_By");

                entity.Property(e => e.FsiihCrDt)
                    .HasColumnName("FSIIH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiihDtAgrd)
                    .HasColumnName("FSIIH_DT_Agrd")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiihDtPrioritised)
                    .HasColumnName("FSIIH_DT_Prioritised")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiihDtSchld)
                    .HasColumnName("FSIIH_DT_Schld")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiihDtSub)
                    .HasColumnName("FSIIH_DT_Sub")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiihDtVet)
                    .HasColumnName("FSIIH_DT_Vet")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiihModBy).HasColumnName("FSIIH_Mod_By");

                entity.Property(e => e.FsiihModDt)
                    .HasColumnName("FSIIH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiihQuaterId).HasColumnName("FSIIH_Quater_Id");

                entity.Property(e => e.FsiihRefId)
                    .HasColumnName("FSIIH_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihRmu)
                    .HasColumnName("FSIIH_RMU")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihStatus)
                    .IsRequired()
                    .HasColumnName("FSIIH_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Initialize')");

                entity.Property(e => e.FsiihSubmitSts).HasColumnName("FSIIH_SUBMIT_STS");

                entity.Property(e => e.FsiihUserDesignationAgrd)
                    .HasColumnName("FSIIH_User_Designation_Agrd")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserDesignationPrioritised)
                    .HasColumnName("FSIIH_User_Designation_Prioritised")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserDesignationSchId)
                    .HasColumnName("FSIIH_User_Designation_SchId")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserDesignationSub)
                    .HasColumnName("FSIIH_User_Designation_Sub")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserDesignationVet)
                    .HasColumnName("FSIIH_User_Designation_Vet")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserNameAgrd)
                    .HasColumnName("FSIIH_User_Name_Agrd")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserNamePrioritised)
                    .HasColumnName("FSIIH_User_Name_Prioritised")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserNameSchId)
                    .HasColumnName("FSIIH_User_Name_SchId")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserNameSub)
                    .HasColumnName("FSIIH_User_Name_Sub")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUserNameVet)
                    .HasColumnName("FSIIH_User_Name_Vet")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FsiihUseridAgrd).HasColumnName("FSIIH_Userid_Agrd");

                entity.Property(e => e.FsiihUseridPrioritised).HasColumnName("FSIIH_Userid_Prioritised");

                entity.Property(e => e.FsiihUseridSchld).HasColumnName("FSIIH_Userid_Schld");

                entity.Property(e => e.FsiihUseridSub).HasColumnName("FSIIH_Userid_Sub");

                entity.Property(e => e.FsiihUseridVet).HasColumnName("FSIIH_Userid_Vet");

                entity.Property(e => e.FsiihYear).HasColumnName("FSIIH_Year");
            });

            modelBuilder.Entity<RmFormS2QuarDtl>(entity =>
            {
                entity.HasKey(e => e.FsiiqdPkRefNo)
                    .HasName("PK__RM_FormS__7BCCE6DAD87FCDBF");

                entity.ToTable("RM_FormS2_Quar_DTL");

                entity.Property(e => e.FsiiqdPkRefNo).HasColumnName("FSIIQD_PK_Ref_No");

                entity.Property(e => e.FsiiqdClkPkRefNo).HasColumnName("FSIIQD_CLK_PK_Ref_No");

                entity.Property(e => e.FsiiqdCrBy).HasColumnName("FSIIQD_CR_By");

                entity.Property(e => e.FsiiqdCrDt)
                    .HasColumnName("FSIIQD_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FsiiqdFsiidPkRefNo).HasColumnName("FSIIQD_FSIID_PK_Ref_No");

                entity.HasOne(d => d.FsiiqdClkPkRefNoNavigation)
                    .WithMany(p => p.RmFormS2QuarDtl)
                    .HasForeignKey(d => d.FsiiqdClkPkRefNo)
                    .HasConstraintName("FK__RM_FormS2__FSIIQ__2F650636");

                entity.HasOne(d => d.FsiiqdFsiidPkRefNoNavigation)
                    .WithMany(p => p.RmFormS2QuarDtl)
                    .HasForeignKey(d => d.FsiiqdFsiidPkRefNo)
                    .HasConstraintName("FK__RM_FormS2__FSIIQ__2E70E1FD");
            });

            modelBuilder.Entity<RmFormV1Dtl>(entity =>
            {
                entity.HasKey(e => e.Fv1dPkRefNo);

                entity.ToTable("RM_FormV1_DTL");

                entity.Property(e => e.Fv1dPkRefNo).HasColumnName("FV1D_PK_Ref_No");

                entity.Property(e => e.Fv1dActiveYn).HasColumnName("FV1D_Active_YN");

                entity.Property(e => e.Fv1dCrBy).HasColumnName("FV1D_CR_By");

                entity.Property(e => e.Fv1dCrDt)
                    .HasColumnName("FV1D_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv1dFrmCh).HasColumnName("FV1D_FRM_CH");

                entity.Property(e => e.Fv1dFrmChDeci).HasColumnName("FV1D_FRM_CH_Deci");

                entity.Property(e => e.Fv1dFv1hPkRefNo).HasColumnName("FV1D_FV1H_PK_Ref_No");

                entity.Property(e => e.Fv1dModBy).HasColumnName("FV1D_Mod_By");

                entity.Property(e => e.Fv1dModDt)
                    .HasColumnName("FV1D_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv1dRemarks)
                    .HasColumnName("FV1D_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1dRoadCode)
                    .HasColumnName("FV1D_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1dRoadName)
                    .HasColumnName("FV1D_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1dS1dPkRefNo).HasColumnName("FV1D_S1D_PK_Ref_No");

                entity.Property(e => e.Fv1dSiteRef)
                    .HasColumnName("FV1D_Site_Ref")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1dStartTime)
                    .HasColumnName("FV1D_Start_Time")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1dSubmitSts).HasColumnName("FV1D_SUBMIT_STS");

                entity.Property(e => e.Fv1dToCh).HasColumnName("FV1D_To_CH");

                entity.Property(e => e.Fv1dToChDeci).HasColumnName("FV1D_To_CH_Deci");

                entity.HasOne(d => d.Fv1dFv1hPkRefNoNavigation)
                    .WithMany(p => p.RmFormV1Dtl)
                    .HasForeignKey(d => d.Fv1dFv1hPkRefNo)
                    .HasConstraintName("FK_RM_FormV1_DTL_RM_FormV1_HDR");
            });

            modelBuilder.Entity<RmFormV1Hdr>(entity =>
            {
                entity.HasKey(e => e.Fv1hPkRefNo);

                entity.ToTable("RM_FormV1_HDR");

                entity.Property(e => e.Fv1hPkRefNo).HasColumnName("FV1H_PK_Ref_No");

                entity.Property(e => e.Fv1hActCode)
                    .HasColumnName("FV1H_ACT_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hActName)
                    .HasColumnName("FV1H_ACT_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hActiveYn).HasColumnName("FV1H_Active_YN");

                entity.Property(e => e.Fv1hAuditLog).HasColumnName("FV1H_AuditLog");

                entity.Property(e => e.Fv1hContNo).HasColumnName("FV1H_CONT_No");

                entity.Property(e => e.Fv1hCrBy).HasColumnName("FV1H_CR_By");

                entity.Property(e => e.Fv1hCrDt)
                    .HasColumnName("FV1H_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv1hCrew).HasColumnName("FV1H_Crew");

                entity.Property(e => e.Fv1hCrewname)
                    .HasColumnName("FV1H_Crewname")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hDesignationAck)
                    .HasColumnName("FV1H_Designation_ACK")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hDesignationAgr)
                    .HasColumnName("FV1H_Designation_AGR")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hDesignationSch)
                    .HasColumnName("FV1H_Designation_SCH")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hDivCode)
                    .HasColumnName("FV1H_Div_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hDivName)
                    .HasColumnName("FV1H_Div_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hDt)
                    .HasColumnName("FV1H_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv1hDtAck)
                    .HasColumnName("FV1H_DT_ACK")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv1hDtAgr)
                    .HasColumnName("FV1H_DT_AGR")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv1hDtSch)
                    .HasColumnName("FV1H_DT_SCH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv1hFacilitator)
                    .HasColumnName("FV1H_Facilitator")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hModBy).HasColumnName("FV1H_Mod_By");

                entity.Property(e => e.Fv1hModDt)
                    .HasColumnName("FV1H_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv1hRefId)
                    .HasColumnName("FV1H_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hRemarks)
                    .HasColumnName("FV1H_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hRmu)
                    .HasColumnName("FV1H_RMU")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hS1hPkRefNo).HasColumnName("FV1H_S1H_PK_Ref_No");

                entity.Property(e => e.Fv1hSecCode)
                    .HasColumnName("FV1H_Sec_Code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hSecName)
                    .HasColumnName("FV1H_Sec_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hServiceProvider)
                    .HasColumnName("FV1H_Service_Provider")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hSignAck).HasColumnName("FV1H_Sign_ACK");

                entity.Property(e => e.Fv1hSignAgr).HasColumnName("FV1H_Sign_AGR");

                entity.Property(e => e.Fv1hSignSch).HasColumnName("FV1H_Sign_SCH");

                entity.Property(e => e.Fv1hSource)
                    .HasColumnName("FV1H_Source")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hStatus)
                    .HasColumnName("FV1H_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'Initialize')");

                entity.Property(e => e.Fv1hSubmitSts).HasColumnName("FV1H_SUBMIT_STS");

                entity.Property(e => e.Fv1hUseridAck).HasColumnName("FV1H_Userid_ACK");

                entity.Property(e => e.Fv1hUseridAgr).HasColumnName("FV1H_Userid_AGR");

                entity.Property(e => e.Fv1hUseridSch).HasColumnName("FV1H_Userid_SCH");

                entity.Property(e => e.Fv1hUsernameAck)
                    .HasColumnName("FV1H_Username_ACK")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hUsernameAgr)
                    .HasColumnName("FV1H_Username_AGR")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hUsernameSch)
                    .HasColumnName("FV1H_Username_SCH")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv1hVerifier)
                    .HasColumnName("FV1H_Verifier")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmFormV2Eqp>(entity =>
            {
                entity.HasKey(e => e.Fv2ePkRefNo);

                entity.ToTable("RM_FormV2_EQP");

                entity.Property(e => e.Fv2ePkRefNo).HasColumnName("FV2E_PK_Ref_No");

                entity.Property(e => e.Fv2eActiveYn).HasColumnName("FV2E_Active_YN");

                entity.Property(e => e.Fv2eCapacity)
                    .HasColumnName("FV2E_Capacity")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2eCond).HasColumnName("FV2E_Cond");

                entity.Property(e => e.Fv2eCrBy).HasColumnName("FV2E_CR_By");

                entity.Property(e => e.Fv2eCrDt)
                    .HasColumnName("FV2E_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv2eDesc)
                    .HasColumnName("FV2E_Desc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2eEqpRefCode)
                    .HasColumnName("FV2E_EQP_Ref_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2eFv2hPkRefNo).HasColumnName("FV2E_FV2H_PK_Ref_No");

                entity.Property(e => e.Fv2eModBy).HasColumnName("FV2E_Mod_By");

                entity.Property(e => e.Fv2eModDt)
                    .HasColumnName("FV2E_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv2eModel)
                    .HasColumnName("FV2E_Model")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2eSubmitSts).HasColumnName("FV2E_SUBMIT_STS");

                entity.HasOne(d => d.Fv2eFv2hPkRefNoNavigation)
                    .WithMany(p => p.RmFormV2Eqp)
                    .HasForeignKey(d => d.Fv2eFv2hPkRefNo)
                    .HasConstraintName("FK_RM_FormV2_EQP_RM_FormV2_HDR");
            });

            modelBuilder.Entity<RmFormV2Hdr>(entity =>
            {
                entity.HasKey(e => e.Fv2hPkRefNo);

                entity.ToTable("RM_FormV2_HDR");

                entity.Property(e => e.Fv2hPkRefNo).HasColumnName("FV2H_PK_Ref_No");

                entity.Property(e => e.Fv2hActCode)
                    .HasColumnName("FV2H_ACT_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.Fv2hActName)
                    .HasColumnName("FV2H_ACT_Name")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hActiveYn).HasColumnName("FV2H_Active_YN");

                entity.Property(e => e.Fv2hAuditLog).HasColumnName("FV2H_AuditLog");

                entity.Property(e => e.Fv2hContNo).HasColumnName("FV2H_CONT_No");

                entity.Property(e => e.Fv2hCrBy).HasColumnName("FV2H_CR_By");

                entity.Property(e => e.Fv2hCrDt)
                    .HasColumnName("FV2H_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv2hCrew).HasColumnName("FV2H_Crew");

                entity.Property(e => e.Fv2hCrewname)
                    .HasColumnName("FV2H_Crewname")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hDesignationAck)
                    .HasColumnName("FV2H_Designation_ACK")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hDesignationAgr)
                    .HasColumnName("FV2H_Designation_AGR")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hDesignationSch)
                    .HasColumnName("FV2H_Designation_SCH")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hDivCode)
                    .HasColumnName("FV2H_Div_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hDivName)
                    .HasColumnName("FV2H_Div_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hDt)
                    .HasColumnName("FV2H_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv2hDtAck)
                    .HasColumnName("FV2H_DT_ACK")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv2hDtAgr)
                    .HasColumnName("FV2H_DT_AGR")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv2hDtSch)
                    .HasColumnName("FV2H_DT_SCH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv2hFacilitator)
                    .HasColumnName("FV2H_Facilitator")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hFv1hPkRefNo).HasColumnName("FV2H_FV1H_PK_Ref_No");

                entity.Property(e => e.Fv2hModBy).HasColumnName("FV2H_Mod_By");

                entity.Property(e => e.Fv2hModDt)
                    .HasColumnName("FV2H_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv2hRefId)
                    .HasColumnName("FV2H_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hRemarks)
                    .HasColumnName("FV2H_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hRmu)
                    .HasColumnName("FV2H_RMU")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hSecCode)
                    .HasColumnName("FV2H_Sec_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.Fv2hSecName)
                    .HasColumnName("FV2H_Sec_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hServiceProvider)
                    .HasColumnName("FV2H_Service_Provider")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hSignAck).HasColumnName("FV2H_Sign_ACK");

                entity.Property(e => e.Fv2hSignAgr).HasColumnName("FV2H_Sign_AGR");

                entity.Property(e => e.Fv2hSignSch).HasColumnName("FV2H_Sign_SCH");

                entity.Property(e => e.Fv2hStatus)
                    .HasColumnName("FV2H_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'Initialize')");

                entity.Property(e => e.Fv2hSubmitSts).HasColumnName("FV2H_SUBMIT_STS");

                entity.Property(e => e.Fv2hUseridAck).HasColumnName("FV2H_Userid_ACK");

                entity.Property(e => e.Fv2hUseridAgr).HasColumnName("FV2H_Userid_AGR");

                entity.Property(e => e.Fv2hUseridSch).HasColumnName("FV2H_Userid_SCH");

                entity.Property(e => e.Fv2hUsernameAck)
                    .HasColumnName("FV2H_Username_ACK")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hUsernameAgr)
                    .HasColumnName("FV2H_Username_AGR")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hUsernameSch)
                    .HasColumnName("FV2H_Username_SCH")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2hVerifier)
                    .HasColumnName("FV2H_Verifier")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fv2hFv1hPkRefNoNavigation)
                    .WithMany(p => p.RmFormV2Hdr)
                    .HasForeignKey(d => d.Fv2hFv1hPkRefNo)
                    .HasConstraintName("FK_RM_FormV2_HDR_RM_FormV2_HDR");
            });

            modelBuilder.Entity<RmFormV2Lab>(entity =>
            {
                entity.HasKey(e => e.Fv2lPkRefNo);

                entity.ToTable("RM_FormV2_LAB");

                entity.Property(e => e.Fv2lPkRefNo).HasColumnName("FV2L_PK_Ref_No");

                entity.Property(e => e.Fv2lActiveYn).HasColumnName("FV2L_Active_YN");

                entity.Property(e => e.Fv2lCrBy).HasColumnName("FV2L_CR_By");

                entity.Property(e => e.Fv2lCrDt)
                    .HasColumnName("FV2L_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv2lDesc)
                    .HasColumnName("FV2L_Desc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2lFv2hPkRefNo).HasColumnName("FV2L_FV2H_PK_Ref_No");

                entity.Property(e => e.Fv2lLabRefCode)
                    .HasColumnName("FV2L_LAB_Ref_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2lModBy).HasColumnName("FV2L_Mod_By");

                entity.Property(e => e.Fv2lModDt)
                    .HasColumnName("FV2L_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv2lQty)
                    .HasColumnName("FV2L_Qty")
                    .HasColumnType("decimal(9, 3)");

                entity.Property(e => e.Fv2lRemark)
                    .HasColumnName("FV2L_Remark")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2lSubmitSts).HasColumnName("FV2L_SUBMIT_STS");

                entity.Property(e => e.Fv2lUnit)
                    .HasColumnName("FV2L_Unit")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fv2lFv2hPkRefNoNavigation)
                    .WithMany(p => p.RmFormV2Lab)
                    .HasForeignKey(d => d.Fv2lFv2hPkRefNo)
                    .HasConstraintName("FK_RM_FormV2_LAB_RM_FormV2_HDR");
            });

            modelBuilder.Entity<RmFormV2Mat>(entity =>
            {
                entity.HasKey(e => e.Fv2mPkRefNo);

                entity.ToTable("RM_FormV2_MAT");

                entity.Property(e => e.Fv2mPkRefNo).HasColumnName("FV2M_PK_Ref_No");

                entity.Property(e => e.Fv2mActiveYn).HasColumnName("FV2M_Active_YN");

                entity.Property(e => e.Fv2mCrBy).HasColumnName("FV2M_CR_By");

                entity.Property(e => e.Fv2mCrDt)
                    .HasColumnName("FV2M_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv2mDesc)
                    .HasColumnName("FV2M_Desc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2mFv2hPkRefNo).HasColumnName("FV2M_FV2H_PK_Ref_No");

                entity.Property(e => e.Fv2mMatRefCode)
                    .HasColumnName("FV2M_MAT_Ref_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv2mModBy).HasColumnName("FV2M_Mod_By");

                entity.Property(e => e.Fv2mModDt)
                    .HasColumnName("FV2M_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv2mQnty)
                    .HasColumnName("FV2M_QNTY")
                    .HasColumnType("decimal(9, 3)");

                entity.Property(e => e.Fv2mSubmitSts).HasColumnName("FV2M_SUBMIT_STS");

                entity.Property(e => e.Fv2mUnit)
                    .HasColumnName("FV2M_Unit")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fv2mFv2hPkRefNoNavigation)
                    .WithMany(p => p.RmFormV2Mat)
                    .HasForeignKey(d => d.Fv2mFv2hPkRefNo)
                    .HasConstraintName("FK_RM_FormV2_MAT_RM_FormV2_HDR");
            });

            modelBuilder.Entity<RmFormV3Dtl>(entity =>
            {
                entity.HasKey(e => e.Fv3dPkRefNo);

                entity.ToTable("RM_FormV3_DTL");

                entity.Property(e => e.Fv3dPkRefNo).HasColumnName("FV3D_PK_Ref_No");

                entity.Property(e => e.Fv3dActiveYn).HasColumnName("FV3D_Active_YN");

                entity.Property(e => e.Fv3dAdp).HasColumnName("FV3D_ADP");

                entity.Property(e => e.Fv3dCrBy).HasColumnName("FV3D_CR_By");

                entity.Property(e => e.Fv3dCrDt)
                    .HasColumnName("FV3D_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv3dFrmCh).HasColumnName("FV3D_FRM_CH");

                entity.Property(e => e.Fv3dFrmChDeci).HasColumnName("FV3D_FRM_CH_Deci");

                entity.Property(e => e.Fv3dFv3hPkRefNo).HasColumnName("FV3D_FV3H_PK_Ref_No");

                entity.Property(e => e.Fv3dLength).HasColumnName("FV3D_Length");

                entity.Property(e => e.Fv3dModBy).HasColumnName("FV3D_Mod_By");

                entity.Property(e => e.Fv3dModDt)
                    .HasColumnName("FV3D_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv3dRoadCode)
                    .HasColumnName("FV3D_Road_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv3dRoadName)
                    .HasColumnName("FV3D_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv3dSubmitSts).HasColumnName("FV3D_SUBMIT_STS");

                entity.Property(e => e.Fv3dTimeTakenTo)
                    .HasColumnName("FV3D_TimeTaken_TO")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv3dTimeTakenTotal)
                    .HasColumnName("FV3D_TimeTaken_Total")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Fv3dTimetakenFrm)
                    .HasColumnName("FV3D_Timetaken_FRM")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv3dToCh).HasColumnName("FV3D_To_CH");

                entity.Property(e => e.Fv3dToChDeci).HasColumnName("FV3D_To_CH_Deci");

                entity.Property(e => e.Fv3dTransitTimeFrm)
                    .HasColumnName("FV3D_Transit_Time_FRM")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv3dTransitTimeTo)
                    .HasColumnName("FV3D_Transit_Time_To")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv3dTransitTimeTotal)
                    .HasColumnName("FV3D_Transit_Time_Total")
                    .HasColumnType("decimal(4, 0)");

                entity.Property(e => e.Fv3dWidth).HasColumnName("FV3D_Width");

                entity.HasOne(d => d.Fv3dFv3hPkRefNoNavigation)
                    .WithMany(p => p.RmFormV3Dtl)
                    .HasForeignKey(d => d.Fv3dFv3hPkRefNo)
                    .HasConstraintName("FK_RM_FormV3_DTL_RM_FormV3_HDR");
            });

            modelBuilder.Entity<RmFormV3Hdr>(entity =>
            {
                entity.HasKey(e => e.Fv3hPkRefNo);

                entity.ToTable("RM_FormV3_HDR");

                entity.Property(e => e.Fv3hPkRefNo).HasColumnName("FV3H_PK_Ref_No");

                entity.Property(e => e.Fv3hActCode).HasColumnName("FV3H_ACT_Code");

                entity.Property(e => e.Fv3hActiveYn).HasColumnName("FV3H_Active_YN");

                entity.Property(e => e.Fv3hAuditLog).HasColumnName("FV3H_AuditLog");

                entity.Property(e => e.Fv3hContNo).HasColumnName("FV3H_CONT_No");

                entity.Property(e => e.Fv3hCrBy).HasColumnName("FV3H_CR_By");

                entity.Property(e => e.Fv3hCrDt)
                    .HasColumnName("FV3H_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv3hCrew).HasColumnName("FV3H_Crew");

                entity.Property(e => e.Fv3hCrewname)
                    .HasColumnName("FV3H_Crewname")
                    .HasMaxLength(250);

                entity.Property(e => e.Fv3hDesignationAgr)
                    .HasColumnName("FV3H_Designation_AGR")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv3hDesignationFac)
                    .HasColumnName("FV3H_Designation_FAC")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv3hDesignationRec)
                    .HasColumnName("FV3H_Designation_REC")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv3hDt)
                    .HasColumnName("FV3H_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv3hDtAgr)
                    .HasColumnName("FV3H_DT_AGR")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv3hDtRec)
                    .HasColumnName("FV3H_DT_REC")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv3hDtSch)
                    .HasColumnName("FV3H_DT_SCH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv3hFacilitator)
                    .HasColumnName("FV3H_Facilitator")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv3hModBy).HasColumnName("FV3H_Mod_By");

                entity.Property(e => e.Fv3hModDt)
                    .HasColumnName("FV3H_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv3hRefId)
                    .HasColumnName("FV3H_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fv3hRemarks)
                    .HasColumnName("FV3H_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fv3hRmu)
                    .HasColumnName("FV3H_RMU")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv3hSecCode)
                    .HasColumnName("FV3H_Sec_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.Fv3hServiceProvider)
                    .HasColumnName("FV3H_Service_Provider")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv3hSignAgr).HasColumnName("FV3H_Sign_AGR");

                entity.Property(e => e.Fv3hSignFac).HasColumnName("FV3H_Sign_FAC");

                entity.Property(e => e.Fv3hSignRec).HasColumnName("FV3H_Sign_REC");

                entity.Property(e => e.Fv3hStatus)
                    .HasColumnName("FV3H_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'Initialize')");

                entity.Property(e => e.Fv3hSubmitSts).HasColumnName("FV3H_SUBMIT_STS");

                entity.Property(e => e.Fv3hUseridAgr).HasColumnName("FV3H_Userid_AGR");

                entity.Property(e => e.Fv3hUseridFac).HasColumnName("FV3H_Userid_FAC");

                entity.Property(e => e.Fv3hUseridRec).HasColumnName("FV3H_Userid_REC");

                entity.Property(e => e.Fv3hUsernameAgr)
                    .HasColumnName("FV3H_Username_AGR")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv3hUsernameFac)
                    .HasColumnName("FV3H_Username_FAC")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv3hUsernameRec)
                    .HasColumnName("FV3H_Username_REC")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv3hVerifier)
                    .HasColumnName("FV3H_Verifier")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmFormV4Hdr>(entity =>
            {
                entity.HasKey(e => e.Fv4hPkRefNo);

                entity.ToTable("RM_FormV4_HDR");

                entity.Property(e => e.Fv4hPkRefNo).HasColumnName("FV4H_PK_Ref_No");

                entity.Property(e => e.Fv4hActCode).HasColumnName("FV4H_ACT_Code");

                entity.Property(e => e.Fv4hActiveYn).HasColumnName("FV4H_Active_YN");

                entity.Property(e => e.Fv4hAuditLog).HasColumnName("FV4H_AuditLog");

                entity.Property(e => e.Fv4hContNo).HasColumnName("FV4H_CONT_No");

                entity.Property(e => e.Fv4hCrBy).HasColumnName("FV4H_CR_By");

                entity.Property(e => e.Fv4hCrDt)
                    .HasColumnName("FV4H_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv4hCrew).HasColumnName("FV4H_Crew");

                entity.Property(e => e.Fv4hCrewname)
                    .HasColumnName("FV4H_Crewname")
                    .HasMaxLength(250);

                entity.Property(e => e.Fv4hDesignationAgr)
                    .HasColumnName("FV4H_Designation_AGR")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv4hDesignationFac)
                    .HasColumnName("FV4H_Designation_FAC")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv4hDesignationVet)
                    .HasColumnName("FV4H_Designation_VET")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv4hDivision)
                    .HasColumnName("FV4H_Division")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv4hDt)
                    .HasColumnName("FV4H_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv4hDtAgr)
                    .HasColumnName("FV4H_DT_AGR")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv4hDtSch)
                    .HasColumnName("FV4H_DT_SCH")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv4hDtVet)
                    .HasColumnName("FV4H_DT_VET")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv4hEndTime)
                    .HasColumnName("FV4H_End_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv4hFacilitator)
                    .HasColumnName("FV4H_Facilitator")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv4hModBy).HasColumnName("FV4H_Mod_By");

                entity.Property(e => e.Fv4hModDt)
                    .HasColumnName("FV4H_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv4hRefId)
                    .HasColumnName("FV4H_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fv4hRemarks)
                    .HasColumnName("FV4H_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fv4hRmu)
                    .HasColumnName("FV4H_RMU")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv4hSecCode)
                    .HasColumnName("FV4H_Sec_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.Fv4hServiceProvider)
                    .HasColumnName("FV4H_Service_Provider")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv4hSignAgr).HasColumnName("FV4H_Sign_AGR");

                entity.Property(e => e.Fv4hSignFac).HasColumnName("FV4H_Sign_FAC");

                entity.Property(e => e.Fv4hSignVet).HasColumnName("FV4H_Sign_VET");

                entity.Property(e => e.Fv4hStartTime)
                    .HasColumnName("FV4H_Start_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv4hStatus)
                    .HasColumnName("FV4H_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'Initialize')");

                entity.Property(e => e.Fv4hSubmitSts).HasColumnName("FV4H_SUBMIT_STS");

                entity.Property(e => e.Fv4hTotalProduction)
                    .HasColumnName("FV4H_Total_Production")
                    .HasColumnType("decimal(9, 0)");

                entity.Property(e => e.Fv4hUseridAgr)
                    .HasColumnName("FV4H_Userid_AGR")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fv4hUseridFac).HasColumnName("FV4H_Userid_FAC");

                entity.Property(e => e.Fv4hUseridVet).HasColumnName("FV4H_Userid_VET");

                entity.Property(e => e.Fv4hUsernameAgr)
                    .HasColumnName("FV4H_Username_AGR")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv4hUsernameFac)
                    .HasColumnName("FV4H_Username_FAC")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv4hUsernameVet)
                    .HasColumnName("FV4H_Username_VET")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv4hVerifier)
                    .HasColumnName("FV4H_Verifier")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmFormV5Dtl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RM_FormV5_DTL");

                entity.Property(e => e.Fv5dActiveYn).HasColumnName("FV5D_Active_YN");

                entity.Property(e => e.Fv5dCrBy).HasColumnName("FV5D_CR_By");

                entity.Property(e => e.Fv5dCrDt)
                    .HasColumnName("FV5D_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv5dDesc)
                    .HasColumnName("FV5D_Desc")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Fv5dFf5hPkRefNo).HasColumnName("FV5D_FF5H_PK_Ref_No");

                entity.Property(e => e.Fv5dFileNameFrm)
                    .HasColumnName("FV5D_FileName_FRM")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv5dFileNameTo)
                    .HasColumnName("FV5D_FileName_TO")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv5dImageFilenameSys)
                    .HasColumnName("FV5D_Image_Filename_Sys")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv5dImageFilenameUpload)
                    .HasColumnName("FV5D_Image_Filename_Upload")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv5dImageUserFilePath)
                    .HasColumnName("FV5D_image_user_filePath")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv5dModBy).HasColumnName("FV5D_Mod_By");

                entity.Property(e => e.Fv5dModDt)
                    .HasColumnName("FV5D_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv5dPkRefNo)
                    .HasColumnName("FV5D_PK_Ref_No")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Fv5dSubmitSts).HasColumnName("FV5D_SUBMIT_STS");
            });

            modelBuilder.Entity<RmFormV5Hdr>(entity =>
            {
                entity.HasKey(e => e.Fv5hPkRefNo);

                entity.ToTable("RM_FormV5_HDR");

                entity.Property(e => e.Fv5hPkRefNo).HasColumnName("FV5H_PK_Ref_No");

                entity.Property(e => e.Fv5hActCode).HasColumnName("FV5H_ACT_Code");

                entity.Property(e => e.Fv5hActiveYn).HasColumnName("FV5H_Active_YN");

                entity.Property(e => e.Fv5hAuditLog).HasColumnName("FV5H_AuditLog");

                entity.Property(e => e.Fv5hContNo).HasColumnName("FV5H_CONT_No");

                entity.Property(e => e.Fv5hCrBy).HasColumnName("FV5H_CR_By");

                entity.Property(e => e.Fv5hCrDt)
                    .HasColumnName("FV5H_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv5hCrew).HasColumnName("FV5H_Crew");

                entity.Property(e => e.Fv5hCrewname)
                    .HasColumnName("FV5H_Crewname")
                    .HasMaxLength(250);

                entity.Property(e => e.Fv5hDesignationRec)
                    .HasColumnName("FV5H_Designation_REC")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv5hDt)
                    .HasColumnName("FV5H_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv5hDtRec)
                    .HasColumnName("FV5H_DT_REC")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv5hModBy).HasColumnName("FV5H_Mod_By");

                entity.Property(e => e.Fv5hModDt)
                    .HasColumnName("FV5H_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fv5hRefId)
                    .HasColumnName("FV5H_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fv5hRemarks)
                    .HasColumnName("FV5H_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Fv5hRmu)
                    .HasColumnName("FV5H_RMU")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Fv5hSecCode)
                    .HasColumnName("FV5H_Sec_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.Fv5hSignRec).HasColumnName("FV5H_Sign_REC");

                entity.Property(e => e.Fv5hStatus)
                    .HasColumnName("FV5H_Status")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("(N'Initialize')");

                entity.Property(e => e.Fv5hSubmitSts).HasColumnName("FV5H_SUBMIT_STS");

                entity.Property(e => e.Fv5hUseridRec).HasColumnName("FV5H_Userid_REC");

                entity.Property(e => e.Fv5hUsernameRec)
                    .HasColumnName("FV5H_Username_REC")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv5hVerifier)
                    .HasColumnName("FV5H_Verifier")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fv5hYear).HasColumnName("FV5H_Year");
            });

            modelBuilder.Entity<RmFormXHdr>(entity =>
            {
                entity.HasKey(e => e.FxhPkRefNo)
                    .HasName("PK__RM_FormX__B2E09EA3B2FE5EF0");

                entity.ToTable("RM_FormX_HDR");

                entity.Property(e => e.FxhPkRefNo).HasColumnName("FXH_PK_Ref_No");

                entity.Property(e => e.FxhActMainCode).HasColumnName("FXH_ACT_Main_Code");

                entity.Property(e => e.FxhActMainName)
                    .HasColumnName("FXH_ACT_Main_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FxhActSubCode)
                    .HasColumnName("FXH_ACT_Sub_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FxhActSubName)
                    .HasColumnName("FXH_ACT_Sub_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FxhActiveYn)
                    .IsRequired()
                    .HasColumnName("FXH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FxhAssgnWrk)
                    .HasColumnName("FXH_ASSGN_WRK")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FxhClsd)
                    .HasColumnName("FXH_CLSD")
                    .HasColumnType("datetime");

                entity.Property(e => e.FxhComments)
                    .HasColumnName("FXH_Comments")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FxhContNo)
                    .HasColumnName("FXH_CONT_No")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FxhCrBy)
                    .HasColumnName("FXH_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FxhCrDt)
                    .HasColumnName("FXH_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FxhDate)
                    .HasColumnName("FXH_Date")
                    .HasColumnType("date");

                entity.Property(e => e.FxhDesc)
                    .HasColumnName("FXH_Desc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FxhDesignationPrp)
                    .HasColumnName("FXH_Designation_PRP")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FxhDesignationSchdVer)
                    .HasColumnName("FXH_Designation_SCHD_VER")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FxhDesignationVer)
                    .HasColumnName("FXH_Designation_VER")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FxhDesignationVet)
                    .HasColumnName("FXH_Designation_VET")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FxhDtAssgn)
                    .HasColumnName("FXH_DT_Assgn")
                    .HasColumnType("datetime");

                entity.Property(e => e.FxhDtJkrRcvdFrm)
                    .HasColumnName("FXH_DT_JKR_Rcvd_FRM")
                    .HasColumnType("datetime");

                entity.Property(e => e.FxhDtJkrSent)
                    .HasColumnName("FXH_DT_JKR_Sent")
                    .HasColumnType("datetime");

                entity.Property(e => e.FxhDtPrp)
                    .HasColumnName("FXH_DT_PRP")
                    .HasColumnType("datetime");

                entity.Property(e => e.FxhDtSchdVer)
                    .HasColumnName("FXH_DT_SCHD_VER")
                    .HasColumnType("datetime");

                entity.Property(e => e.FxhDtVer)
                    .HasColumnName("FXH_DT_VER")
                    .HasColumnType("datetime");

                entity.Property(e => e.FxhDtVet)
                    .HasColumnName("FXH_DT_VET")
                    .HasColumnType("datetime");

                entity.Property(e => e.FxhEmailId)
                    .HasColumnName("FXH_Email_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FxhEstDate)
                    .HasColumnName("FXH_EST_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.FxhEstDays)
                    .HasColumnName("FXH_EST_Days")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FxhFddPkRefNo).HasColumnName("FXH_FDD_PK_Ref_No");

                entity.Property(e => e.FxhHandPh)
                    .HasColumnName("FXH_Hand_PH")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FxhJkrRemarks)
                    .HasColumnName("FXH_JKR_Remarks")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.FxhLoc)
                    .HasColumnName("FXH_LOC")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FxhLocReportedDesc)
                    .HasColumnName("FXH_Loc_Reported_Desc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FxhModBy)
                    .HasColumnName("FXH_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FxhModComDesc)
                    .HasColumnName("FXH_Mod_com_Desc")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FxhModComType)
                    .HasColumnName("FXH_Mod_com_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FxhModComUpload)
                    .HasColumnName("FXH_Mod_com_Upload")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FxhModDt)
                    .HasColumnName("FXH_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FxhName)
                    .HasColumnName("FXH_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FxhRefId)
                    .HasColumnName("FXH_Ref_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FxhRemarks)
                    .HasColumnName("FXH_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FxhRmu)
                    .HasColumnName("FXH_RMU")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FxhRoadCode)
                    .HasColumnName("FXH_Road_Code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FxhRoadName)
                    .HasColumnName("FXH_Road_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FxhSection)
                    .HasColumnName("FXH_Section")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FxhSignPrp).HasColumnName("FXH_SIgn_PRP");

                entity.Property(e => e.FxhSignSchdVer).HasColumnName("FXH_Sign_SCHD_VER");

                entity.Property(e => e.FxhSignVer).HasColumnName("FXH_SIgn_VER");

                entity.Property(e => e.FxhSignVet).HasColumnName("FXH_SIgn_VET");

                entity.Property(e => e.FxhStsJkr)
                    .HasColumnName("FXH_STS_JKR")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FxhSubmitSts).HasColumnName("FXH_SUBMIT_STS");

                entity.Property(e => e.FxhTime).HasColumnName("FXH_Time");

                entity.Property(e => e.FxhUseridAssgn).HasColumnName("FXH_Userid_Assgn");

                entity.Property(e => e.FxhUseridAttnTo).HasColumnName("FXH_userid_ATTN_TO");

                entity.Property(e => e.FxhUseridPrp).HasColumnName("FXH_Userid_PRP");

                entity.Property(e => e.FxhUseridSchdVer).HasColumnName("FXH_Userid_SCHD_VER");

                entity.Property(e => e.FxhUseridVer).HasColumnName("FXH_Userid_VER");

                entity.Property(e => e.FxhUseridVet).HasColumnName("FXH_Userid_VET");

                entity.Property(e => e.FxhUsernameAssgn)
                    .HasColumnName("FXH_Username_Assgn")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FxhUsernameAttnTo)
                    .HasColumnName("FXH_username_ATTN_TO")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FxhUsernamePrp)
                    .HasColumnName("FXH_Username_PRP")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FxhUsernameSchdVer)
                    .HasColumnName("FXH_Username_SCHD_VER")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FxhUsernameVer)
                    .HasColumnName("FXH_Username_VER")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FxhUsernameVet)
                    .HasColumnName("FXH_Username_VET")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FxhWorkPh)
                    .HasColumnName("FXH_Work_PH")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FxhWrkCmpld)
                    .HasColumnName("FXH_WRK_CMPLD")
                    .HasColumnType("datetime");

                entity.Property(e => e.FxhWrkSc)
                    .HasColumnName("FXH_WRK_SC")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.FxhFddPkRefNoNavigation)
                    .WithMany(p => p.RmFormXHdr)
                    .HasForeignKey(d => d.FxhFddPkRefNo)
                    .HasConstraintName("FK__RM_FormX___FXH_F__30592A6F");
            });

            modelBuilder.Entity<RmFormaImageDtl>(entity =>
            {
                entity.HasKey(e => e.FaiPkRefNo)
                    .HasName("PK__RM_FORMA__F68F136653BCC95B");

                entity.ToTable("RM_FORMA_image_DTL");

                entity.Property(e => e.FaiPkRefNo).HasColumnName("FAI_PK_Ref_No");

                entity.Property(e => e.FaiActiveYn)
                    .IsRequired()
                    .HasColumnName("FAI_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FaiCrBy)
                    .HasColumnName("FAI_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FaiCrDt)
                    .HasColumnName("FAI_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FaiFadPkRefNo).HasColumnName("FAI_FAD_PK_Ref_No");

                entity.Property(e => e.FaiImageFilenameSys)
                    .HasColumnName("FAI_Image_Filename_Sys")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FaiImageFilenameUpload)
                    .HasColumnName("FAI_Image_Filename_Upload")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FaiImageSrno).HasColumnName("FAI_Image_SRNO");

                entity.Property(e => e.FaiImageTypeCode)
                    .HasColumnName("FAI_Image_Type_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FaiImageUserFilePath)
                    .HasColumnName("FAI_image_user_filePath")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FaiModBy)
                    .HasColumnName("FAI_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FaiModDt)
                    .HasColumnName("FAI_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FaiSubmitSts).HasColumnName("FAI_SUBMIT_STS");

                entity.HasOne(d => d.FaiFadPkRefNoNavigation)
                    .WithMany(p => p.RmFormaImageDtl)
                    .HasForeignKey(d => d.FaiFadPkRefNo)
                    .HasConstraintName("FK__RM_FORMA___FAI_F__0A338187");
            });

            modelBuilder.Entity<RmFormhImageDtl>(entity =>
            {
                entity.HasKey(e => e.FhiPkRefNo)
                    .HasName("PK__RM_FORMH__9D7817859EA7488A");

                entity.ToTable("RM_FORMH_image_DTL");

                entity.Property(e => e.FhiPkRefNo).HasColumnName("FHI_PK_Ref_No");

                entity.Property(e => e.FhiActiveYn)
                    .IsRequired()
                    .HasColumnName("FHI_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FhiCrBy)
                    .HasColumnName("FHI_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FhiCrDt)
                    .HasColumnName("FHI_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FhiFhhPkRefNo).HasColumnName("FHI_FHH_PK_Ref_No");

                entity.Property(e => e.FhiImageFilenameSys)
                    .HasColumnName("FHI_Image_Filename_Sys")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FhiImageFilenameUpload)
                    .HasColumnName("FHI_Image_Filename_Upload")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FhiImageSrno).HasColumnName("FHI_Image_SRNO");

                entity.Property(e => e.FhiImageTypeCode)
                    .HasColumnName("FHI_Image_Type_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FhiImageUserFilePath)
                    .HasColumnName("FHI_image_user_filePath")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FhiModBy)
                    .HasColumnName("FHI_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FhiModDt)
                    .HasColumnName("FHI_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FhiSubmitSts).HasColumnName("FHI_SUBMIT_STS");

                entity.HasOne(d => d.FhiFhhPkRefNoNavigation)
                    .WithMany(p => p.RmFormhImageDtl)
                    .HasForeignKey(d => d.FhiFhhPkRefNo)
                    .HasConstraintName("FK__RM_FORMH___FHI_F__23F3538A");
            });

            modelBuilder.Entity<RmFormjImageDtl>(entity =>
            {
                entity.HasKey(e => e.FjiPkRefNo)
                    .HasName("PK__RM_FORMJ__D85FB9EBD08223D7");

                entity.ToTable("RM_FORMJ_image_DTL");

                entity.Property(e => e.FjiPkRefNo).HasColumnName("FJI_PK_Ref_No");

                entity.Property(e => e.FjiActiveYn)
                    .IsRequired()
                    .HasColumnName("FJI_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FjiCrBy)
                    .HasColumnName("FJI_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FjiCrDt)
                    .HasColumnName("FJI_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FjiFjdPkRefNo).HasColumnName("FJI_FJD_PK_Ref_No");

                entity.Property(e => e.FjiImageFilenameSys)
                    .HasColumnName("FJI_Image_Filename_Sys")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FjiImageFilenameUpload)
                    .HasColumnName("FJI_Image_Filename_Upload")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FjiImageSrno).HasColumnName("FJI_Image_SRNO");

                entity.Property(e => e.FjiImageTypeCode)
                    .HasColumnName("FJI_Image_Type_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FjiImageUserFilePath)
                    .HasColumnName("FJI_image_user_filePath")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FjiModBy)
                    .HasColumnName("FJI_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FjiModDt)
                    .HasColumnName("FJI_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FjiSubmitSts).HasColumnName("FJI_SUBMIT_STS");

                entity.HasOne(d => d.FjiFjdPkRefNoNavigation)
                    .WithMany(p => p.RmFormjImageDtl)
                    .HasForeignKey(d => d.FjiFjdPkRefNo)
                    .HasConstraintName("FK__RM_FORMJ___FJI_F__25DB9BFC");
            });

            modelBuilder.Entity<RmGroup>(entity =>
            {
                entity.HasKey(e => e.UgPkId);

                entity.ToTable("RM_Group");

                entity.HasIndex(e => e.DepartmentDeptPkId);

                entity.Property(e => e.DepartmentDeptPkId).HasColumnName("DepartmentDept_PkId");

                entity.Property(e => e.UgGroupCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UgGroupName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.DepartmentDeptPk)
                    .WithMany(p => p.RmGroup)
                    .HasForeignKey(d => d.DepartmentDeptPkId);
            });

            modelBuilder.Entity<RmGroupUser>(entity =>
            {
                entity.HasKey(e => e.UsrGpkid);

                entity.ToTable("RM_Group_User");

                entity.HasIndex(e => e.RmGroupsUgPkId);

                entity.HasIndex(e => e.RmUsersUsrPkId);

                entity.Property(e => e.UsrGpkid).HasColumnName("UsrGPKId");

                entity.HasOne(d => d.RmGroupsUgPk)
                    .WithMany(p => p.RmGroupUser)
                    .HasForeignKey(d => d.RmGroupsUgPkId);

                entity.HasOne(d => d.RmUsersUsrPk)
                    .WithMany(p => p.RmGroupUser)
                    .HasForeignKey(d => d.RmUsersUsrPkId);
            });

            modelBuilder.Entity<RmInspItemMas>(entity =>
            {
                entity.HasKey(e => e.IimPkRefNo)
                    .HasName("PK__RM_Insp___DDDD8E989A236EB8");

                entity.ToTable("RM_Insp_Item_Mas");

                entity.Property(e => e.IimPkRefNo).HasColumnName("IIM_PK_Ref_No");

                entity.Property(e => e.IimActiveYn)
                    .IsRequired()
                    .HasColumnName("IIM_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IimCrBy).HasColumnName("IIM_CR_By");

                entity.Property(e => e.IimCrDt)
                    .HasColumnName("IIM_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.IimInspName)
                    .HasColumnName("IIM_Insp_Name")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IimModBy).HasColumnName("IIM_Mod_By");

                entity.Property(e => e.IimModDt)
                    .HasColumnName("IIM_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.IimSubmitSts).HasColumnName("IIM_SUBMIT_STS");
            });

            modelBuilder.Entity<RmInspItemMasDtl>(entity =>
            {
                entity.HasKey(e => e.IimdPkRefNo)
                    .HasName("PK__RM_Insp___8D6E61068980CAF3");

                entity.ToTable("RM_Insp_Item_Mas_DTL");

                entity.Property(e => e.IimdPkRefNo).HasColumnName("IIMD_PK_Ref_No");

                entity.Property(e => e.IimActiveYn)
                    .IsRequired()
                    .HasColumnName("IIM_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IimCrBy).HasColumnName("IIM_CR_By");

                entity.Property(e => e.IimCrDt)
                    .HasColumnName("IIM_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.IimModBy).HasColumnName("IIM_Mod_By");

                entity.Property(e => e.IimModDt)
                    .HasColumnName("IIM_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.IimSubmitSts).HasColumnName("IIM_SUBMIT_STS");

                entity.Property(e => e.IimdIimPkRefNo).HasColumnName("IIMD_IIM_PK_Ref_No");

                entity.Property(e => e.IimdInspCode)
                    .HasColumnName("IIMD_Insp_Code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IimdInspCodeDesc)
                    .HasColumnName("IIMD_Insp_Code_Desc")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.HasOne(d => d.IimdIimPkRefNoNavigation)
                    .WithMany(p => p.RmInspItemMasDtl)
                    .HasForeignKey(d => d.IimdIimPkRefNo)
                    .HasConstraintName("FK__RM_Insp_I__IIMD___3429BB53");
            });

            modelBuilder.Entity<RmIwFormW1>(entity =>
            {
                entity.HasKey(e => e.Fw1PkRefNo)
                    .HasName("PK_RM_IW_ FormW1");

                entity.ToTable("RM_IW_FormW1");

                entity.Property(e => e.Fw1PkRefNo).HasColumnName("FW1_PK_Ref_No");

                entity.Property(e => e.Fw1ActiveYn).HasColumnName("FW1_Active_YN");

                entity.Property(e => e.Fw1AuditLog).HasColumnName("FW1_AuditLog");

                entity.Property(e => e.Fw1Ch).HasColumnName("FW1_CH");

                entity.Property(e => e.Fw1ChDeci).HasColumnName("FW1_CH_Deci");

                entity.Property(e => e.Fw1ConsulFeeAmt).HasColumnName("FW1_Consul_Fee_AMT");

                entity.Property(e => e.Fw1ConsulTaxPercent).HasColumnName("FW1_Consul_Tax_Percent");

                entity.Property(e => e.Fw1CrBy).HasColumnName("FW1_CR_By");

                entity.Property(e => e.Fw1CrDt)
                    .HasColumnName("FW1_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw1DesignationRep)
                    .HasColumnName("FW1_Designation_REP")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw1DesignationReq)
                    .HasColumnName("FW1_Designation_REQ")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw1DesignationVer)
                    .HasColumnName("FW1_Designation_VER")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw1DetailsOfWork)
                    .HasColumnName("FW1_Details_Of_Work")
                    .HasMaxLength(700);

                entity.Property(e => e.Fw1DivnCode)
                    .HasColumnName("FW1_Divn_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.Fw1Dt)
                    .HasColumnName("FW1_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw1DtRep)
                    .HasColumnName("FW1_DT_REP")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw1DtReq)
                    .HasColumnName("FW1_DT_REQ")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw1DtVer)
                    .HasColumnName("FW1_DT_VER")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw1EstimTotalCostAmt).HasColumnName("FW1_Estim_Total_cost_AMT");

                entity.Property(e => e.Fw1FormhRefNo)
                    .HasColumnName("FW1_FORMH_RefNo")
                    .HasMaxLength(100);

                entity.Property(e => e.Fw1GenPrelimsAmt).HasColumnName("FW1_Gen_Prelims_AMT");

                entity.Property(e => e.Fw1InitialProposedDate)
                    .HasColumnName("FW1_Initial_Proposed_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw1IsBq).HasColumnName("FW1_IsBQ");

                entity.Property(e => e.Fw1IsDrawing).HasColumnName("FW1_IsDrawing");

                entity.Property(e => e.Fw1IwRefNo)
                    .HasColumnName("FW1_IW_Ref_NO")
                    .HasMaxLength(100);

                entity.Property(e => e.Fw1ModBy).HasColumnName("FW1_Mod_By");

                entity.Property(e => e.Fw1ModDt)
                    .HasColumnName("FW1_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw1OfficeReq)
                    .HasColumnName("FW1_Office_REQ")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw1OfficeVer)
                    .HasColumnName("FW1_Office_VER")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw1OtherCostAmt).HasColumnName("FW1_Other_Cost_AMT");

                entity.Property(e => e.Fw1OtherCostLabel)
                    .HasColumnName("FW1_Other_Cost_Label")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw1PhyWorksAmt).HasColumnName("FW1_Phy_Works_AMT");

                entity.Property(e => e.Fw1ProjectTitle)
                    .HasColumnName("FW1_Project_Title")
                    .HasMaxLength(200);

                entity.Property(e => e.Fw1PropCompletionPeriod).HasColumnName("FW1_Prop_Completion_Period");

                entity.Property(e => e.Fw1PropDesignDuration).HasColumnName("FW1_Prop_Design_Duration");

                entity.Property(e => e.Fw1RecomdBydeYn).HasColumnName("FW1_RecomdBYDE_YN");

                entity.Property(e => e.Fw1RecomdType).HasColumnName("FW1_Recomd_Type");

                entity.Property(e => e.Fw1RecomdYn).HasColumnName("FW1_Recomd_YN");

                entity.Property(e => e.Fw1RmuCode)
                    .HasColumnName("FW1_RMU_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.Fw1RoadCode)
                    .HasColumnName("FW1_Road_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.Fw1RoadName)
                    .HasColumnName("FW1_Road_Name")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw1SecCode)
                    .HasColumnName("FW1_Sec_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.Fw1ServAddress1)
                    .HasColumnName("FW1_Serv_Address1")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw1ServAddress2)
                    .HasColumnName("FW1_Serv_Address2")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw1ServAddress3)
                    .HasColumnName("FW1_Serv_Address3")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw1ServFax)
                    .HasColumnName("FW1_Serv_Fax")
                    .HasMaxLength(50);

                entity.Property(e => e.Fw1ServPhone)
                    .HasColumnName("FW1_Serv_Phone")
                    .HasMaxLength(50);

                entity.Property(e => e.Fw1ServPropName)
                    .HasColumnName("FW1_Serv_Prop_Name")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw1ServPropRefNo)
                    .HasColumnName("FW1_Serv_Prop_RefNo")
                    .HasMaxLength(100);

                entity.Property(e => e.Fw1SignRep).HasColumnName("FW1_Sign_REP");

                entity.Property(e => e.Fw1SignReq).HasColumnName("FW1_Sign_REQ");

                entity.Property(e => e.Fw1SignVer).HasColumnName("FW1_Sign_VER");

                entity.Property(e => e.Fw1SiteInvestAmt).HasColumnName("FW1_Site_Invest_AMT");

                entity.Property(e => e.Fw1SiteInvestPercent).HasColumnName("FW1_Site_Invest_Percent");

                entity.Property(e => e.Fw1Status)
                    .HasColumnName("FW1_Status")
                    .HasMaxLength(30);

                entity.Property(e => e.Fw1Sts)
                    .HasColumnName("FW1_STS")
                    .HasMaxLength(16);

                entity.Property(e => e.Fw1StsRemarks)
                    .HasColumnName("FW1_STS_Remarks")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw1SubmitSts).HasColumnName("FW1_SUBMIT_STS");

                entity.Property(e => e.Fw1SurvyWorksAmt).HasColumnName("FW1_Survy_Works_AMT");

                entity.Property(e => e.Fw1SurvyWorksPercent).HasColumnName("FW1_Survy_Works_Percent");

                entity.Property(e => e.Fw1TecmDt)
                    .HasColumnName("FW1_TECM_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw1UseridRep).HasColumnName("FW1_USERID_REP");

                entity.Property(e => e.Fw1UseridReq).HasColumnName("FW1_USERID_REQ");

                entity.Property(e => e.Fw1UseridVer).HasColumnName("FW1_USERID_VER");

                entity.Property(e => e.Fw1UsernameRep)
                    .HasColumnName("FW1_USERNAME_REP")
                    .HasMaxLength(50);

                entity.Property(e => e.Fw1UsernameReq)
                    .HasColumnName("FW1_USERNAME_REQ")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw1UsernameVer)
                    .HasColumnName("FW1_USERNAME_VER")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<RmIwFormW2>(entity =>
            {
                entity.HasKey(e => e.Fw2PkRefNo)
                    .HasName("PK_RM_IW_ FormW2");

                entity.ToTable("RM_IW_FormW2");

                entity.Property(e => e.Fw2PkRefNo).HasColumnName("FW2_PK_Ref_No");

                entity.Property(e => e.Fw2ActiveYn)
                    .IsRequired()
                    .HasColumnName("FW2_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Fw2Attn)
                    .HasColumnName("FW2_Attn")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2AuditLog).HasColumnName("FW2_AuditLog");

                entity.Property(e => e.Fw2Cc)
                    .HasColumnName("FW2_CC")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2Ch).HasColumnName("FW2_CH");

                entity.Property(e => e.Fw2ChDeci).HasColumnName("FW2_CH_DECI");

                entity.Property(e => e.Fw2CrBy).HasColumnName("FW2_CR_By");

                entity.Property(e => e.Fw2CrDt)
                    .HasColumnName("FW2_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw2DateOfInitation)
                    .HasColumnName("FW2_Date_Of_Initation")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw2DesignationIssu)
                    .HasColumnName("FW2_Designation_ISSU")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2DesignationReq)
                    .HasColumnName("FW2_Designation_REQ")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2DetailsOfWorks)
                    .HasColumnName("FW2_DETAILS_OF_WORKS")
                    .HasMaxLength(1000);

                entity.Property(e => e.Fw2DivCode)
                    .HasColumnName("FW2_Div_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.Fw2DivText)
                    .HasColumnName("FW2_Div_Text")
                    .HasMaxLength(100);

                entity.Property(e => e.Fw2DivisonName)
                    .HasColumnName("FW2_Divison_Name")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2DtCommence)
                    .HasColumnName("FW2_DT_Commence")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw2DtCompl)
                    .HasColumnName("FW2_DT_Compl")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw2DtIssu)
                    .HasColumnName("FW2_DT_ISSU")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw2DtReq)
                    .HasColumnName("FW2_DT_REQ")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw2EstCostAmt)
                    .HasColumnName("FW2_EST_COST_AMT")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Fw2Fw1IwRefNo)
                    .HasColumnName("FW2_FW1_IW_Ref_No")
                    .HasMaxLength(100);

                entity.Property(e => e.Fw2Fw1PkRefNo).HasColumnName("FW2_FW1_PK_Ref_No");

                entity.Property(e => e.Fw2Fw1ProjectTitle)
                    .HasColumnName("FW2_FW1_Project_Title")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2IwDuration)
                    .HasColumnName("FW2_IW_Duration")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Fw2JkrRefNo)
                    .HasColumnName("FW2_JKR_RefNo")
                    .HasMaxLength(100);

                entity.Property(e => e.Fw2ModBy).HasColumnName("FW2_Mod_By");

                entity.Property(e => e.Fw2ModDt)
                    .HasColumnName("FW2_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fw2OfficeIssu)
                    .HasColumnName("FW2_Office_ISSU")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2OfficeReq)
                    .HasColumnName("FW2_Office_REQ")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2RegionName)
                    .HasColumnName("FW2_Region_Name")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2RegionText)
                    .HasColumnName("FW2_Region_Text")
                    .HasMaxLength(10);

                entity.Property(e => e.Fw2Remarks)
                    .HasColumnName("FW2_Remarks")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2RmuCode)
                    .HasColumnName("FW2_RMU_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.Fw2RmuName)
                    .HasColumnName("FW2_RMU_Name")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2RmuText)
                    .HasColumnName("FW2_RMU_Text")
                    .HasMaxLength(10);

                entity.Property(e => e.Fw2RoadCode)
                    .HasColumnName("FW2_Road_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.Fw2RoadName)
                    .HasColumnName("FW2_Road_Name")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2SecCode)
                    .HasColumnName("FW2_Sec_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.Fw2SerProvRefNo)
                    .HasColumnName("FW2_Ser_Prov_RefNo")
                    .HasMaxLength(100);

                entity.Property(e => e.Fw2ServProvName)
                    .HasColumnName("FW2_Serv_Prov_Name")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2SignIssu).HasColumnName("FW2_Sign_ISSU");

                entity.Property(e => e.Fw2SignReq).HasColumnName("FW2_Sign_REQ");

                entity.Property(e => e.Fw2Status)
                    .HasColumnName("FW2_Status")
                    .HasMaxLength(30);

                entity.Property(e => e.Fw2SubmitSts).HasColumnName("FW2_SUBMIT_STS");

                entity.Property(e => e.Fw2UseridIssu).HasColumnName("FW2_USERID_ISSU");

                entity.Property(e => e.Fw2UseridReq).HasColumnName("FW2_USERID_REQ");

                entity.Property(e => e.Fw2UsernameIssu)
                    .HasColumnName("FW2_USERNAME_ISSU")
                    .HasMaxLength(250);

                entity.Property(e => e.Fw2UsernameReq)
                    .HasColumnName("FW2_USERNAME_REQ")
                    .HasMaxLength(250);

                entity.HasOne(d => d.Fw2Fw1PkRefNoNavigation)
                    .WithMany(p => p.RmIwFormW2)
                    .HasForeignKey(d => d.Fw2Fw1PkRefNo)
                    .HasConstraintName("FK_RM_IW_ FormW2_RM_IW_ FormW1");
            });

            modelBuilder.Entity<RmIwFormW2Fecm>(entity =>
            {
                entity.HasKey(e => e.FecmPkRefNo)
                    .HasName("PK_RM_IW_Form_W2_FCEM");

                entity.ToTable("RM_IW_Form_W2_FECM");

                entity.Property(e => e.FecmPkRefNo).HasColumnName("FECM_PK_Ref_No");

                entity.Property(e => e.FecmActiveYn).HasColumnName("FECM_Active_YN");

                entity.Property(e => e.FecmAgreedNegoLetrYn).HasColumnName("FECM_Agreed_Nego_Letr_YN");

                entity.Property(e => e.FecmCrBy).HasColumnName("FECM_CR_By");

                entity.Property(e => e.FecmCrDt)
                    .HasColumnName("FECM_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FecmDt)
                    .HasColumnName("FECM_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FecmDtAgreedNego)
                    .HasColumnName("FECM_DT_Agreed_Nego")
                    .HasColumnType("datetime");

                entity.Property(e => e.FecmDtTecm)
                    .HasColumnName("FECM_DT_TECM")
                    .HasColumnType("datetime");

                entity.Property(e => e.FecmFw2PkRefNo).HasColumnName("FECM_FW2_PK_Ref_No");

                entity.Property(e => e.FecmIsBq).HasColumnName("FECM_IsBQ");

                entity.Property(e => e.FecmIsDrawing).HasColumnName("FECM_IsDrawing");

                entity.Property(e => e.FecmIwRefNo)
                    .HasColumnName("FECM_IW_Ref_No")
                    .HasMaxLength(100);

                entity.Property(e => e.FecmModBy).HasColumnName("FECM_Mod_By");

                entity.Property(e => e.FecmModDt)
                    .HasColumnName("FECM_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FecmProgressPerc).HasColumnName("FECM_Progress_Perc");

                entity.Property(e => e.FecmProjectTitle)
                    .HasColumnName("FECM_Project_Title")
                    .HasMaxLength(250);

                entity.Property(e => e.FecmRemark)
                    .HasColumnName("FECM_Remark")
                    .HasMaxLength(250);

                entity.Property(e => e.FecmSts)
                    .HasColumnName("FECM_STS")
                    .HasMaxLength(16);

                entity.Property(e => e.FecmStsRemarks)
                    .HasColumnName("FECM_STS__Remarks")
                    .HasMaxLength(250);

                entity.Property(e => e.FecmSubmitSts).HasColumnName("FECM_SUBMIT_STS");

                entity.HasOne(d => d.FecmFw2PkRefNoNavigation)
                    .WithMany(p => p.RmIwFormW2Fecm)
                    .HasForeignKey(d => d.FecmFw2PkRefNo)
                    .HasConstraintName("FK_RM_IW_Form_W2_FECM_RM_IW_FormW2");
            });

            modelBuilder.Entity<RmIwFormWc>(entity =>
            {
                entity.HasKey(e => e.FwcPkRefNo);

                entity.ToTable("RM_IW_Form_WC");

                entity.Property(e => e.FwcPkRefNo).HasColumnName("FWC_PK_Ref_no");

                entity.Property(e => e.FwcActiveYn).HasColumnName("FWC_Active_YN");

                entity.Property(e => e.FwcAuditLog).HasColumnName("FWC_AuditLog");

                entity.Property(e => e.FwcCh).HasColumnName("FWC_CH");

                entity.Property(e => e.FwcChDeci).HasColumnName("FWC_CH_Deci");

                entity.Property(e => e.FwcCrBy).HasColumnName("FWC_CR_By");

                entity.Property(e => e.FwcCrDt)
                    .HasColumnName("FWC_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwcDesignationIssu)
                    .HasColumnName("FWC_Designation_ISSU")
                    .HasMaxLength(250);

                entity.Property(e => e.FwcDlpPeriod).HasColumnName("FWC_DLP_Period");

                entity.Property(e => e.FwcDtCompl)
                    .HasColumnName("FWC_DT_Compl")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwcDtDlpExtn)
                    .HasColumnName("FWC_DT_DLP_Extn")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwcDtIssu)
                    .HasColumnName("FWC_DT_ISSU")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwcDtWc)
                    .HasColumnName("FWC_DT_WC")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwcFw1PkRefNo).HasColumnName("FWC_FW1_PK_Ref_no");

                entity.Property(e => e.FwcIwProjectTitle)
                    .HasColumnName("FWC_IW_Project_Title")
                    .HasMaxLength(250);

                entity.Property(e => e.FwcIwRefNo)
                    .HasColumnName("FWC_IW_Ref_No")
                    .HasMaxLength(100);

                entity.Property(e => e.FwcIwWrksDeptId).HasColumnName("FWC_IW_WRKS_Dept_ID");

                entity.Property(e => e.FwcModBy).HasColumnName("FWC_Mod_By");

                entity.Property(e => e.FwcModDt)
                    .HasColumnName("FWC_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwcOfficeIssu)
                    .HasColumnName("FWC_Office_ISSU")
                    .HasMaxLength(250);

                entity.Property(e => e.FwcOurRefNo)
                    .HasColumnName("FWC_Our_Ref_No")
                    .HasMaxLength(100);

                entity.Property(e => e.FwcRmuCode)
                    .HasColumnName("FWC_RMU_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.FwcRoadCode)
                    .HasColumnName("FWC_Road_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.FwcRoadName)
                    .HasColumnName("FWC_Road_Name")
                    .HasMaxLength(250);

                entity.Property(e => e.FwcSecCode)
                    .HasColumnName("FWC_Sec_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.FwcSignIssu).HasColumnName("FWC_Sign_ISSU");

                entity.Property(e => e.FwcStatus)
                    .HasColumnName("FWC_Status")
                    .HasMaxLength(30);

                entity.Property(e => e.FwcSubmitSts).HasColumnName("FWC_SUBMIT_STS");

                entity.Property(e => e.FwcUseridIssu).HasColumnName("FWC_USERID_ISSU");

                entity.Property(e => e.FwcUsernameIssu)
                    .HasColumnName("FWC_USERNAME_ISSU")
                    .HasMaxLength(250);

                entity.Property(e => e.FwcYourRefNo)
                    .HasColumnName("FWC_Your_Ref_No")
                    .HasMaxLength(100);

                entity.HasOne(d => d.FwcFw1PkRefNoNavigation)
                    .WithMany(p => p.RmIwFormWc)
                    .HasForeignKey(d => d.FwcFw1PkRefNo)
                    .HasConstraintName("FK_RM_IW_Form_WC_RM_IW_FormW1");

                entity.HasOne(d => d.FwcIwWrksDept)
                    .WithMany(p => p.RmIwFormWc)
                    .HasForeignKey(d => d.FwcIwWrksDeptId)
                    .HasConstraintName("FK_RM_IW_Form_WC_RM_IW_Works_Dept_Master");
            });

            modelBuilder.Entity<RmIwFormWd>(entity =>
            {
                entity.HasKey(e => e.FwdPkRefNo);

                entity.ToTable("RM_IW_Form_WD");

                entity.Property(e => e.FwdPkRefNo).HasColumnName("FWD_PK_Ref_No");

                entity.Property(e => e.FwdActiveYn).HasColumnName("FWD_Active_YN");

                entity.Property(e => e.FwdAuditLog).HasColumnName("FWD_AuditLog");

                entity.Property(e => e.FwdCertificateDelay)
                    .HasColumnName("FWD_Certificate_Delay")
                    .HasMaxLength(250);

                entity.Property(e => e.FwdCh).HasColumnName("FWD_CH");

                entity.Property(e => e.FwdChDeci).HasColumnName("FWD_CH_Deci");

                entity.Property(e => e.FwdCrBy).HasColumnName("FWD_CR_By");

                entity.Property(e => e.FwdCrDt)
                    .HasColumnName("FWD_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwdDesignationIssu)
                    .HasColumnName("FWD_Designation_ISSU")
                    .HasMaxLength(520);

                entity.Property(e => e.FwdDtExtn)
                    .HasColumnName("FWD_DT_Extn")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwdDtIssu)
                    .HasColumnName("FWD_DT_ISSU")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwdDtPervCompl)
                    .HasColumnName("FWD_DT_Perv_Compl")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwdDtWd)
                    .HasColumnName("FWD_DT_WD")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwdFw1PkRefNo).HasColumnName("FWD_FW1_PK_Ref_No");

                entity.Property(e => e.FwdIwProjectTitle)
                    .HasColumnName("FWD_IW_Project_Title")
                    .HasMaxLength(250);

                entity.Property(e => e.FwdIwRefNo)
                    .HasColumnName("FWD_IW_Ref_No")
                    .HasMaxLength(100);

                entity.Property(e => e.FwdIwWrksDeptId).HasColumnName("FWD_IW_WRKS_Dept_ID");

                entity.Property(e => e.FwdModBy).HasColumnName("FWD_Mod_By");

                entity.Property(e => e.FwdModDt)
                    .HasColumnName("FWD_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwdOfficeIssu)
                    .HasColumnName("FWD_Office_ISSU")
                    .HasMaxLength(250);

                entity.Property(e => e.FwdOurRefNo)
                    .HasColumnName("FWD_Our_Ref_No")
                    .HasMaxLength(100);

                entity.Property(e => e.FwdRmuCode)
                    .HasColumnName("FWD_RMU_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.FwdRoadCode)
                    .HasColumnName("FWD_Road_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.FwdRoadName)
                    .HasColumnName("FWD_Road_Name")
                    .HasMaxLength(250);

                entity.Property(e => e.FwdSecCode)
                    .HasColumnName("FWD_Sec_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.FwdServRefNo)
                    .HasColumnName("FWD_Serv_Ref_No")
                    .HasMaxLength(100);

                entity.Property(e => e.FwdSignIssu).HasColumnName("FWD_Sign_ISSU");

                entity.Property(e => e.FwdStatus)
                    .HasColumnName("FWD_Status")
                    .HasMaxLength(30);

                entity.Property(e => e.FwdSubmitSts).HasColumnName("FWD_SUBMIT_STS");

                entity.Property(e => e.FwdUseridIssu).HasColumnName("FWD_USERID_ISSU");

                entity.Property(e => e.FwdUsernameIssu)
                    .HasColumnName("FWD_USERNAME_ISSU")
                    .HasMaxLength(250);

                entity.Property(e => e.FwdYourRefNo)
                    .HasColumnName("FWD_Your_Ref_No")
                    .HasMaxLength(100);

                entity.HasOne(d => d.FwdFw1PkRefNoNavigation)
                    .WithMany(p => p.RmIwFormWd)
                    .HasForeignKey(d => d.FwdFw1PkRefNo)
                    .HasConstraintName("FK_RM_IW_Form_WD_RM_IW_FormW1");

                entity.HasOne(d => d.FwdIwWrksDept)
                    .WithMany(p => p.RmIwFormWd)
                    .HasForeignKey(d => d.FwdIwWrksDeptId)
                    .HasConstraintName("FK_RM_IW_Form_WD_RM_IW_Works_Dept_Master");
            });

            modelBuilder.Entity<RmIwFormWdDtl>(entity =>
            {
                entity.HasKey(e => e.FwddPkRefNo);

                entity.ToTable("RM_IW_Form_WD_Dtl");

                entity.Property(e => e.FwddPkRefNo).HasColumnName("FWDD_PK_Ref_No");

                entity.Property(e => e.FwddClause)
                    .HasColumnName("FWDD_Clause")
                    .HasMaxLength(250);

                entity.Property(e => e.FwddCrBy).HasColumnName("FWDD_CR_By");

                entity.Property(e => e.FwddCrDt)
                    .HasColumnName("FWDD_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwddExtnPrd)
                    .HasColumnName("FWDD_EXTN_Prd")
                    .HasMaxLength(100);

                entity.Property(e => e.FwddFwdPkRefNo).HasColumnName("FWDD_FWD_PK_Ref_No");

                entity.Property(e => e.FwddModBy).HasColumnName("FWDD_Mod_By");

                entity.Property(e => e.FwddModDt)
                    .HasColumnName("FWDD_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwddReason)
                    .HasColumnName("FWDD_Reason")
                    .HasMaxLength(250);

                entity.HasOne(d => d.FwddFwdPkRefNoNavigation)
                    .WithMany(p => p.RmIwFormWdDtl)
                    .HasForeignKey(d => d.FwddFwdPkRefNo)
                    .HasConstraintName("FK_RM_IW_Form_WD_Dtl_RM_IW_Form_WD_Dtl");
            });

            modelBuilder.Entity<RmIwFormWg>(entity =>
            {
                entity.HasKey(e => e.FwgPkRefNo);

                entity.ToTable("RM_IW_Form_WG");

                entity.Property(e => e.FwgPkRefNo).HasColumnName("FWG_PK_Ref_no");

                entity.Property(e => e.FwgActiveYn).HasColumnName("FWG_Active_YN");

                entity.Property(e => e.FwgAuditLog).HasColumnName("FWG_AuditLog");

                entity.Property(e => e.FwgCh).HasColumnName("FWG_CH");

                entity.Property(e => e.FwgChDeci).HasColumnName("FWG_CH_Deci");

                entity.Property(e => e.FwgCrBy).HasColumnName("FWG_CR_By");

                entity.Property(e => e.FwgCrDt)
                    .HasColumnName("FWG_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwgDesignationIssu)
                    .HasColumnName("FWG_Designation_ISSU")
                    .HasMaxLength(250);

                entity.Property(e => e.FwgDtDefectCompl)
                    .HasColumnName("FWG_DT_Defect_Compl")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwgDtIssu)
                    .HasColumnName("FWG_DT_ISSU")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwgDtWg)
                    .HasColumnName("FWG_DT_WG")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwgFw1PkRefNo).HasColumnName("FWG_FW1_PK_Ref_no");

                entity.Property(e => e.FwgIwProjectTitle)
                    .HasColumnName("FWG_IW_Project_Title")
                    .HasMaxLength(250);

                entity.Property(e => e.FwgIwRefNo)
                    .HasColumnName("FWG_IW_Ref_No")
                    .HasMaxLength(100);

                entity.Property(e => e.FwgIwWrksDeptId).HasColumnName("FWG_IW_WRKS_Dept_ID");

                entity.Property(e => e.FwgModBy).HasColumnName("FWG_Mod_By");

                entity.Property(e => e.FwgModDt)
                    .HasColumnName("FWG_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwgOfficeIssu)
                    .HasColumnName("FWG_Office_ISSU")
                    .HasMaxLength(250);

                entity.Property(e => e.FwgOurRefNo)
                    .HasColumnName("FWG_Our_Ref_No")
                    .HasMaxLength(100);

                entity.Property(e => e.FwgRmuCode)
                    .HasColumnName("FWG_RMU_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.FwgRoadCode)
                    .HasColumnName("FWG_Road_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.FwgRoadName)
                    .HasColumnName("FWG_Road_Name")
                    .HasMaxLength(250);

                entity.Property(e => e.FwgSecCode)
                    .HasColumnName("FWG_Sec_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.FwgSignIssu).HasColumnName("FWG_Sign_ISSU");

                entity.Property(e => e.FwgStatus)
                    .HasColumnName("FWG_Status")
                    .HasMaxLength(30);

                entity.Property(e => e.FwgSubmitSts).HasColumnName("FWG_SUBMIT_STS");

                entity.Property(e => e.FwgUseridIssu).HasColumnName("FWG_USERID_ISSU");

                entity.Property(e => e.FwgUsernameIssu)
                    .HasColumnName("FWG_USERNAME_ISSU")
                    .HasMaxLength(250);

                entity.Property(e => e.FwgYourRefNo)
                    .HasColumnName("FWG_Your_Ref_No")
                    .HasMaxLength(100);

                entity.HasOne(d => d.FwgFw1PkRefNoNavigation)
                    .WithMany(p => p.RmIwFormWg)
                    .HasForeignKey(d => d.FwgFw1PkRefNo)
                    .HasConstraintName("FK_RM_IW_Form_WG_RM_IW_FormW1");

                entity.HasOne(d => d.FwgIwWrksDept)
                    .WithMany(p => p.RmIwFormWg)
                    .HasForeignKey(d => d.FwgIwWrksDeptId)
                    .HasConstraintName("FK_RM_IW_Form_WG_RM_IW_Works_Dept_Master");
            });

            modelBuilder.Entity<RmIwFormWn>(entity =>
            {
                entity.HasKey(e => e.FwnPkRefNo);

                entity.ToTable("RM_IW_Form_WN");

                entity.Property(e => e.FwnPkRefNo).HasColumnName("FWN_PK_Ref_No");

                entity.Property(e => e.FwnActiveYn).HasColumnName("FWN_Active_YN");

                entity.Property(e => e.FwnAuditLog).HasColumnName("FWN_AuditLog");

                entity.Property(e => e.FwnCh).HasColumnName("FWN_CH");

                entity.Property(e => e.FwnChDeci).HasColumnName("FWN_CH_Deci");

                entity.Property(e => e.FwnCrBy).HasColumnName("FWN_CR_By");

                entity.Property(e => e.FwnCrDt)
                    .HasColumnName("FWN_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwnDesignationIssu)
                    .HasColumnName("FWN_Designation_ISSU")
                    .HasMaxLength(250);

                entity.Property(e => e.FwnDtIssu)
                    .HasColumnName("FWN_DT_ISSU")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwnDtW2Compl)
                    .HasColumnName("FWN_DT_W2_Compl")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwnDtW2Initiation)
                    .HasColumnName("FWN_DT_W2_Initiation")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwnDtWn)
                    .HasColumnName("FWN_DT_WN")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwnFw1PkRefNo).HasColumnName("FWN_FW1_PK_Ref_No");

                entity.Property(e => e.FwnIwProjectTitle)
                    .HasColumnName("FWN_IW_Project_Title")
                    .HasMaxLength(250);

                entity.Property(e => e.FwnIwRefNo)
                    .HasColumnName("FWN_IW_Ref_No")
                    .HasMaxLength(100);

                entity.Property(e => e.FwnIwWrksDeptId).HasColumnName("FWN_IW_WRKS_Dept_ID");

                entity.Property(e => e.FwnLadAmt).HasColumnName("FWN_LAD_AMT");

                entity.Property(e => e.FwnModBy).HasColumnName("FWN_Mod_By");

                entity.Property(e => e.FwnModDt)
                    .HasColumnName("FWN_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwnOfficeIssu)
                    .HasColumnName("FWN_Office_ISSU")
                    .HasMaxLength(250);

                entity.Property(e => e.FwnOurRefNo)
                    .HasColumnName("FWN_Our_Ref_No")
                    .HasMaxLength(100);

                entity.Property(e => e.FwnRmuCode)
                    .HasColumnName("FWN_RMU_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.FwnRoadCode)
                    .HasColumnName("FWN_Road_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.FwnRoadName)
                    .HasColumnName("FWN_Road_Name")
                    .HasMaxLength(250);

                entity.Property(e => e.FwnSecCode)
                    .HasColumnName("FWN_Sec_Code")
                    .HasMaxLength(16);

                entity.Property(e => e.FwnServRefNo)
                    .HasColumnName("FWN_Serv_Ref_No")
                    .HasMaxLength(100);

                entity.Property(e => e.FwnSignIssu).HasColumnName("FWN_Sign_ISSU");

                entity.Property(e => e.FwnStatus)
                    .HasColumnName("FWN_Status")
                    .HasMaxLength(30);

                entity.Property(e => e.FwnSubmitSts).HasColumnName("FWN_SUBMIT_STS");

                entity.Property(e => e.FwnUseridIssu).HasColumnName("FWN_USERID_ISSU");

                entity.Property(e => e.FwnUsernameIssu)
                    .HasColumnName("FWN_USERNAME_ISSU")
                    .HasMaxLength(250);

                entity.HasOne(d => d.FwnFw1PkRefNoNavigation)
                    .WithMany(p => p.RmIwFormWn)
                    .HasForeignKey(d => d.FwnFw1PkRefNo)
                    .HasConstraintName("FK_RM_IW_Form_WN_RM_IW_FormW1");

                entity.HasOne(d => d.FwnIwWrksDept)
                    .WithMany(p => p.RmIwFormWn)
                    .HasForeignKey(d => d.FwnIwWrksDeptId)
                    .HasConstraintName("FK_RM_IW_Form_WN_RM_IW_Works_Dept_Master");
            });

            modelBuilder.Entity<RmIwSrvProviderMaster>(entity =>
            {
                entity.HasKey(e => e.FiwSrvProviderId);

                entity.ToTable("RM_IW_SRV_Provider_Master");

                entity.Property(e => e.FiwSrvProviderId).HasColumnName("FIW_SRV_Provider_ID");

                entity.Property(e => e.FiwSrvProviderActiveYn).HasColumnName("FIW_SRV_Provider_Active_YN");

                entity.Property(e => e.FiwSrvProviderAddress1)
                    .HasColumnName("FIW_SRV_Provider_Address1")
                    .HasMaxLength(250);

                entity.Property(e => e.FiwSrvProviderAddress2)
                    .HasColumnName("FIW_SRV_Provider_Address2")
                    .HasMaxLength(250);

                entity.Property(e => e.FiwSrvProviderAddress3)
                    .HasColumnName("FIW_SRV_Provider_Address3")
                    .HasMaxLength(250);

                entity.Property(e => e.FiwSrvProviderCode)
                    .HasColumnName("FIW_SRV_Provider_Code")
                    .HasMaxLength(50);

                entity.Property(e => e.FiwSrvProviderCrBy).HasColumnName("FIW_SRV_Provider_CR_By");

                entity.Property(e => e.FiwSrvProviderCrDt)
                    .HasColumnName("FIW_SRV_Provider_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FiwSrvProviderFaxNo)
                    .HasColumnName("FIW_SRV_Provider_FAX_No")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.FiwSrvProviderModBy).HasColumnName("FIW_SRV_Provider_Mod_By");

                entity.Property(e => e.FiwSrvProviderModDt)
                    .HasColumnName("FIW_SRV_Provider_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FiwSrvProviderName)
                    .HasColumnName("FIW_SRV_Provider_Name")
                    .HasMaxLength(250);

                entity.Property(e => e.FiwSrvProviderPhoneNo)
                    .HasColumnName("FIW_SRV_Provider_Phone_No")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.FiwSrvProviderZipcode)
                    .HasColumnName("FIW_SRV_Provider_ZIPCODE")
                    .HasColumnType("numeric(10, 0)");
            });

            modelBuilder.Entity<RmIwWorksDeptMaster>(entity =>
            {
                entity.HasKey(e => e.FiwWrksDeptId);

                entity.ToTable("RM_IW_Works_Dept_Master");

                entity.Property(e => e.FiwWrksDeptId).HasColumnName("FIW_WRKS_Dept_ID");

                entity.Property(e => e.FiwWrksDeptActiveYn).HasColumnName("FIW_WRKS_Dept_Active_YN");

                entity.Property(e => e.FiwWrksDeptAddress1)
                    .HasColumnName("FIW_WRKS_Dept_Address1")
                    .HasMaxLength(250);

                entity.Property(e => e.FiwWrksDeptAddress2)
                    .HasColumnName("FIW_WRKS_Dept_Address2")
                    .HasMaxLength(250);

                entity.Property(e => e.FiwWrksDeptAddress3)
                    .HasColumnName("FIW_WRKS_Dept_Address3")
                    .HasMaxLength(250);

                entity.Property(e => e.FiwWrksDeptCode)
                    .HasColumnName("FIW_WRKS_Dept_Code")
                    .HasMaxLength(50);

                entity.Property(e => e.FiwWrksDeptCrBy).HasColumnName("FIW_WRKS_Dept_CR_By");

                entity.Property(e => e.FiwWrksDeptCrDt)
                    .HasColumnName("FIW_WRKS_Dept_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FiwWrksDeptFaxNo)
                    .HasColumnName("FIW_WRKS_Dept_FAX_No")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.FiwWrksDeptModBy).HasColumnName("FIW_WRKS_Dept_Mod_By");

                entity.Property(e => e.FiwWrksDeptModDt)
                    .HasColumnName("FIW_WRKS_Dept_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FiwWrksDeptName)
                    .HasColumnName("FIW_WRKS_Dept_Name")
                    .HasMaxLength(250);

                entity.Property(e => e.FiwWrksDeptPhoneNo)
                    .HasColumnName("FIW_WRKS_Dept_Phone_No")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.FiwWrksDeptZipcode)
                    .HasColumnName("FIW_WRKS_Dept_ZIPCODE")
                    .HasColumnType("numeric(10, 0)");
            });

            modelBuilder.Entity<RmIwformImage>(entity =>
            {
                entity.HasKey(e => e.FiwiPkRefNo)
                    .HasName("PK_RM_IWForm_W1_Image");

                entity.ToTable("RM_IWForm_Image");

                entity.Property(e => e.FiwiPkRefNo).HasColumnName("FIWI_PK_Ref_No");

                entity.Property(e => e.FiwiActiveYn).HasColumnName("FIWI_Active_YN");

                entity.Property(e => e.FiwiCrBy).HasColumnName("FIWI_CR_By");

                entity.Property(e => e.FiwiCrDt)
                    .HasColumnName("FIWI_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FiwiFw1IwRefNo)
                    .HasColumnName("FIWI_FW1_IW_Ref_No")
                    .HasMaxLength(50);

                entity.Property(e => e.FiwiFw1PkRefNo).HasColumnName("FIWI_FW1_PK_Ref_No");

                entity.Property(e => e.FiwiImageFilenameSys)
                    .HasColumnName("FIWI_Image_Filename_Sys")
                    .HasMaxLength(250);

                entity.Property(e => e.FiwiImageFilenameUpload)
                    .HasColumnName("FIWI_Image_Filename_Upload")
                    .HasMaxLength(250);

                entity.Property(e => e.FiwiImageSrno).HasColumnName("FIWI_Image_SRNO");

                entity.Property(e => e.FiwiImageTypeCode)
                    .HasColumnName("FIWI_Image_Type_Code")
                    .HasMaxLength(250);

                entity.Property(e => e.FiwiImageUserFilePath)
                    .HasColumnName("FIWI_image_user_filePath")
                    .HasMaxLength(250);

                entity.Property(e => e.FiwiImgRefId)
                    .HasColumnName("FIWI_Img_Ref_ID")
                    .HasMaxLength(250);

                entity.Property(e => e.FiwiModBy).HasColumnName("FIWI_Mod_By");

                entity.Property(e => e.FiwiModDt)
                    .HasColumnName("FIWI_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FiwiSource)
                    .HasColumnName("FIWI_Source")
                    .HasMaxLength(16);

                entity.Property(e => e.FiwiSubmitSts).HasColumnName("FIWI_SUBMIT_STS");
            });

            modelBuilder.Entity<RmModule>(entity =>
            {
                entity.HasKey(e => e.ModPkId);

                entity.ToTable("RM_Module");

                entity.Property(e => e.ModPkId).HasColumnName("Mod_PkId");

                entity.Property(e => e.ModCreatedBy).HasColumnName("Mod_CreatedBy");

                entity.Property(e => e.ModCreatedOn).HasColumnName("Mod_CreatedOn");

                entity.Property(e => e.ModDescription).HasColumnName("Mod_Description");

                entity.Property(e => e.ModModifiedBy).HasColumnName("Mod_ModifiedBy");

                entity.Property(e => e.ModModifiedOn).HasColumnName("Mod_ModifiedOn");

                entity.Property(e => e.ModName).HasColumnName("Mod_Name");
            });

            modelBuilder.Entity<RmModuleForms>(entity =>
            {
                entity.HasKey(e => e.MfPkId)
                    .HasName("PK_RM_Module_Forms_1");

                entity.ToTable("RM_Module_Forms");

                entity.Property(e => e.MfPkId).HasColumnName("MF_PkId");

                entity.Property(e => e.MfActiveYn).HasColumnName("MF_Active_YN");

                entity.Property(e => e.MfCrBy).HasColumnName("MF_CR_By");

                entity.Property(e => e.MfCrDt)
                    .HasColumnName("MF_CR_DT")
                    .HasColumnType("date");

                entity.Property(e => e.MfFormName)
                    .HasColumnName("MF_FormName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MfModBy).HasColumnName("MF_Mod_By");

                entity.Property(e => e.MfModDt)
                    .HasColumnName("MF_Mod_DT")
                    .HasColumnType("date");

                entity.Property(e => e.MfModPkId).HasColumnName("MF_Mod_PkId");
            });

            modelBuilder.Entity<RmModuleGroupFieldRights>(entity =>
            {
                entity.HasKey(e => e.MgfrPkId);

                entity.ToTable("RM_Module_Group_Field_Rights");

                entity.Property(e => e.MgfrPkId).HasColumnName("MGFR_PkId");

                entity.Property(e => e.MgfrCreatedBy)
                    .IsRequired()
                    .HasColumnName("MGFR_CreatedBy")
                    .HasMaxLength(200);

                entity.Property(e => e.MgfrCreatedOn)
                    .HasColumnName("MGFR_CreatedOn")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MgfrFieldName)
                    .IsRequired()
                    .HasColumnName("MGFR_FieldName")
                    .HasMaxLength(100);

                entity.Property(e => e.MgfrIsDisabled).HasColumnName("MGFR_IsDisabled");

                entity.Property(e => e.MgfrIsHide).HasColumnName("MGFR_IsHide");

                entity.Property(e => e.MgfrModifiedBy)
                    .IsRequired()
                    .HasColumnName("MGFR_ModifiedBy")
                    .HasMaxLength(200);

                entity.Property(e => e.MgfrModifiedOn)
                    .HasColumnName("MGFR_ModifiedOn")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModPkId).HasColumnName("Mod_PkId");

                entity.HasOne(d => d.ModPk)
                    .WithMany(p => p.RmModuleGroupFieldRights)
                    .HasForeignKey(d => d.ModPkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RM_Module_Group_Field_Rights_RM_Module");

                entity.HasOne(d => d.UgPk)
                    .WithMany(p => p.RmModuleGroupFieldRights)
                    .HasForeignKey(d => d.UgPkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RM_Module_Group_Field_Rights_RM_Group");
            });

            modelBuilder.Entity<RmModuleGroupRights>(entity =>
            {
                entity.HasKey(e => e.MgrPkId)
                    .HasName("PK__RM_Modul__A40FCC69A21F4782");

                entity.ToTable("RM_Module_Group_Rights");

                entity.Property(e => e.MgrPkId).HasColumnName("MGR_PkId");

                entity.Property(e => e.DvIsAdd).HasColumnName("DV_IsAdd");

                entity.Property(e => e.DvIsDelete).HasColumnName("DV_IsDelete");

                entity.Property(e => e.DvIsModify).HasColumnName("DV_IsModify");

                entity.Property(e => e.DvIsView).HasColumnName("DV_IsView");

                entity.Property(e => e.MgrCreatedBy)
                    .IsRequired()
                    .HasColumnName("MGR_CreatedBy")
                    .HasMaxLength(200);

                entity.Property(e => e.MgrCreatedOn)
                    .HasColumnName("MGR_CreatedOn")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MgrModifiedBy)
                    .IsRequired()
                    .HasColumnName("MGR_ModifiedBy")
                    .HasMaxLength(200);

                entity.Property(e => e.MgrModifiedOn)
                    .HasColumnName("MGR_ModifiedOn")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModPkId).HasColumnName("Mod_PkId");

                entity.Property(e => e.PcIsAdd).HasColumnName("PC_IsAdd");

                entity.Property(e => e.PcIsDelete).HasColumnName("PC_IsDelete");

                entity.Property(e => e.PcIsModify).HasColumnName("PC_IsModify");

                entity.Property(e => e.PcIsView).HasColumnName("PC_IsView");

                entity.HasOne(d => d.ModPk)
                    .WithMany(p => p.RmModuleGroupRights)
                    .HasForeignKey(d => d.ModPkId)
                    .HasConstraintName("FK_RM_Module_Group_Rights_RM_Module");

                entity.HasOne(d => d.UgPk)
                    .WithMany(p => p.RmModuleGroupRights)
                    .HasForeignKey(d => d.UgPkId)
                    .HasConstraintName("FK_RM_Module_Group_Rights_RM_Group");

                entity.HasOne(d => d.UsrPk)
                    .WithMany(p => p.RmModuleGroupRights)
                    .HasForeignKey(d => d.UsrPkId)
                    .HasConstraintName("FK_RM_Module_Group_Rights_RM_USERS");
            });

            modelBuilder.Entity<RmModuleRightByForm>(entity =>
            {
                entity.HasKey(e => e.MfrPkId);

                entity.ToTable("RM_Module_Right_By_Form");

                entity.Property(e => e.MfrPkId).HasColumnName("MFR_PkId");

                entity.Property(e => e.MfrActiveYn).HasColumnName("MFR_Active_YN");

                entity.Property(e => e.MfrCanAdd)
                    .HasColumnName("MFR_Can_Add")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MfrCanApprove)
                    .HasColumnName("MFR_Can_Approve")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MfrCanDelete)
                    .HasColumnName("MFR_Can_Delete")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MfrCanEdit)
                    .HasColumnName("MFR_Can_Edit")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MfrCanPrint)
                    .HasColumnName("MFR_Can_Print")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MfrCanSignature)
                    .HasColumnName("MFR_Can_Signature")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MfrCanSubmit)
                    .HasColumnName("MFR_Can_Submit")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MfrCanView)
                    .HasColumnName("MFR_Can_View")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MfrCrBy).HasColumnName("MFR_CR_By");

                entity.Property(e => e.MfrCrDt)
                    .HasColumnName("MFR_CR_DT")
                    .HasColumnType("date");

                entity.Property(e => e.MfrGroupName)
                    .HasColumnName("MFR_GroupName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MfrMfPkId).HasColumnName("MFR_MF_PkId");

                entity.Property(e => e.MfrModBy).HasColumnName("MFR_Mod_By");

                entity.Property(e => e.MfrModDt)
                    .HasColumnName("MFR_Mod_DT")
                    .HasColumnType("date");

                entity.Property(e => e.MfrModFormName)
                    .HasColumnName("MFR_Mod_Form_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MfrModPkId).HasColumnName("MFR_Mod_PkId");

                entity.Property(e => e.MfrModuleName)
                    .HasColumnName("MFR_ModuleName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MfrUgPkId).HasColumnName("MFR_Ug_PkId");

                entity.Property(e => e.MfrUserid).HasColumnName("MFR_Userid");
            });

            modelBuilder.Entity<RmModuleRightsCode>(entity =>
            {
                entity.HasKey(e => e.MrcPkId)
                    .HasName("PK__RM_Modul__5B82B2E3BADDCA7B");

                entity.ToTable("RM_Module_Rights_Code");

                entity.HasIndex(e => e.MrcPermLevel)
                    .HasName("UQ__RM_Modul__FA9FC659C4DA5D22")
                    .IsUnique();

                entity.Property(e => e.MrcPkId).HasColumnName("MRC_PK_Id");

                entity.Property(e => e.MrcAddYn).HasColumnName("MRC_ADD_YN");

                entity.Property(e => e.MrcCrBy)
                    .HasColumnName("MRC_CR_By")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MrcCrDt)
                    .HasColumnName("MRC_CR_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MrcDelYn).HasColumnName("MRC_DEL_YN");

                entity.Property(e => e.MrcEdtYn).HasColumnName("MRC_EDT_YN");

                entity.Property(e => e.MrcEffFrmDt)
                    .HasColumnName("MRC_Eff_FRM_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.MrcEffToDt)
                    .HasColumnName("MRC_Eff_TO_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.MrcModBy)
                    .HasColumnName("MRC_Mod_By")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MrcModDt)
                    .HasColumnName("MRC_Mod_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MrcModuleName)
                    .IsRequired()
                    .HasColumnName("MRC_Module_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MrcPermLevel)
                    .IsRequired()
                    .HasColumnName("MRC_Perm_Level")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MrcRemarks)
                    .HasColumnName("MRC_Remarks")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.MrcScreenName)
                    .IsRequired()
                    .HasColumnName("MRC_Screen_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MrcSubmitSts).HasColumnName("MRC_SUBMIT_STS");

                entity.Property(e => e.MrcViewYn).HasColumnName("MRC_VIEW_YN");
            });

            modelBuilder.Entity<RmRmuMaster>(entity =>
            {
                entity.HasKey(e => e.RmuPkRefNo)
                    .HasName("pk_RM_RMU_Master_DIV_PK_Ref_No");

                entity.ToTable("RM_RMU_Master");

                entity.Property(e => e.RmuPkRefNo).HasColumnName("RMU_PK_Ref_No");

                entity.Property(e => e.DivCode)
                    .HasColumnName("DIV_Code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RmuCode)
                    .HasColumnName("RMU_Code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RmuIsActive).HasColumnName("RMU_IsActive");

                entity.Property(e => e.RmuName)
                    .HasColumnName("RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmRoadMaster>(entity =>
            {
                entity.HasKey(e => e.RdmPkRefNo)
                    .HasName("PK__RM_Road___D2C3D8CDD6844D97");

                entity.ToTable("RM_Road_Master");

                entity.Property(e => e.RdmPkRefNo).HasColumnName("RDM_PK_Ref_No");

                entity.Property(e => e.RdmActiveYn)
                    .IsRequired()
                    .HasColumnName("RDM_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RdmCrBy)
                    .HasColumnName("RDM_CR_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RdmCrDt)
                    .HasColumnName("RDM_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.RdmDivCode)
                    .IsRequired()
                    .HasColumnName("RDM_Div_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.RdmFeatureId)
                    .IsRequired()
                    .HasColumnName("RDM_Feature_ID")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RdmFrmCh).HasColumnName("RDM_FRM_CH");

                entity.Property(e => e.RdmFrmChDeci).HasColumnName("RDM_FRM_CH_Deci");

                entity.Property(e => e.RdmFrmLoc)
                    .HasColumnName("RDM_FRM_Loc")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RdmLengthPaved)
                    .HasColumnName("RDM_Length_Paved")
                    .HasColumnType("decimal(6, 3)");

                entity.Property(e => e.RdmLengthUnpaved)
                    .HasColumnName("RDM_Length_Unpaved")
                    .HasColumnType("decimal(6, 3)");

                entity.Property(e => e.RdmModBy)
                    .HasColumnName("RDM_Mod_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RdmModDt)
                    .HasColumnName("RDM_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.RdmOwner)
                    .HasColumnName("RDM_Owner")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RdmRdCatgCode)
                    .HasColumnName("RDM_RD_Catg_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.RdmRdCatgName)
                    .HasColumnName("RDM_RD_Catg_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RdmRdCdSort).HasColumnName("RDM_RD_Cd_Sort");

                entity.Property(e => e.RdmRdCode)
                    .HasColumnName("RDM_RD_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.RdmRdName)
                    .HasColumnName("RDM_RD_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RdmRmuCode)
                    .IsRequired()
                    .HasColumnName("RDM_RMU_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.RdmRmuName)
                    .HasColumnName("RDM_RMU_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RdmSecCode).HasColumnName("RDM_Sec_Code");

                entity.Property(e => e.RdmSecName)
                    .IsRequired()
                    .HasColumnName("RDM_Sec_name")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.RdmToCh).HasColumnName("RDM_To_CH");

                entity.Property(e => e.RdmToChDeci).HasColumnName("RDM_To_CH_Deci");

                entity.Property(e => e.RdmToLoc)
                    .HasColumnName("RDM_TO_Loc")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmUserGroup>(entity =>
            {
                entity.HasKey(e => e.UgPkId)
                    .HasName("PK__RM_User___3B6DA8CFBDA3A046");

                entity.ToTable("RM_User_Group");

                entity.Property(e => e.UgPkId).HasColumnName("UG_PK_Id");

                entity.Property(e => e.UgCrBy)
                    .HasColumnName("UG_CR_By")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UgCrDt)
                    .HasColumnName("UG_CR_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UgDfltYn)
                    .IsRequired()
                    .HasColumnName("UG_DFLT_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UgEffFrmDt)
                    .HasColumnName("UG_Eff_FRM_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.UgEffToDt)
                    .HasColumnName("UG_Eff_TO_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.UgGroupCode)
                    .IsRequired()
                    .HasColumnName("UG_Group_Code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UgGroupName)
                    .IsRequired()
                    .HasColumnName("UG_Group_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UgModBy)
                    .HasColumnName("UG_Mod_By")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UgModDt)
                    .HasColumnName("UG_Mod_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UgRemarks)
                    .HasColumnName("UG_Remarks")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UgSubmitSts).HasColumnName("UG_SUBMIT_STS");

                entity.Property(e => e.UgUsrPkId).HasColumnName("UG_USR_PK_Id");

                entity.HasOne(d => d.UgUsrPk)
                    .WithMany(p => p.RmUserGroup)
                    .HasForeignKey(d => d.UgUsrPkId)
                    .HasConstraintName("FK__RM_User_G__UG_US__72910220");
            });

            modelBuilder.Entity<RmUserGroupRights>(entity =>
            {
                entity.HasKey(e => e.UgrPkId)
                    .HasName("PK__RM_User___76A9E1958BFD7B3A");

                entity.ToTable("RM_User_Group_Rights");

                entity.Property(e => e.UgrPkId).HasColumnName("UGR_PK_id");

                entity.Property(e => e.UgrActiveYn)
                    .IsRequired()
                    .HasColumnName("UGR_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UgrCrBy)
                    .HasColumnName("UGR_CR_By")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UgrCrDt)
                    .HasColumnName("UGR_CR_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UgrModBy)
                    .HasColumnName("UGR_Mod_By")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UgrModDt)
                    .HasColumnName("UGR_Mod_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UgrRemarks)
                    .HasColumnName("UGR_Remarks")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UgrRightLevel).HasColumnName("UGR_Right_level");

                entity.Property(e => e.UgrRightsCode)
                    .HasColumnName("UGR_Rights_Code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UgrSubmitSts).HasColumnName("UGR_SUBMIT_STS");
            });

            modelBuilder.Entity<RmUserNotification>(entity =>
            {
                entity.HasKey(e => e.RmNotPk);

                entity.ToTable("RM_UserNotification");

                entity.Property(e => e.RmNotPk).HasColumnName("RM_NOT_PK");

                entity.Property(e => e.RmNotCrBy)
                    .IsRequired()
                    .HasColumnName("RM_NOT_CrBy")
                    .HasMaxLength(50);

                entity.Property(e => e.RmNotGroup)
                    .IsRequired()
                    .HasColumnName("RM_NOT_Group")
                    .HasMaxLength(50);

                entity.Property(e => e.RmNotMessage)
                    .IsRequired()
                    .HasColumnName("RM_NOT_Message")
                    .HasMaxLength(250);

                entity.Property(e => e.RmNotOn)
                    .HasColumnName("RM_NOT_On")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RmNotUrl)
                    .IsRequired()
                    .HasColumnName("RM_NOT_URL")
                    .HasMaxLength(500);

                entity.Property(e => e.RmNotUserId)
                    .IsRequired()
                    .HasColumnName("RM_NOT_UserID")
                    .HasMaxLength(250);

                entity.Property(e => e.RmNotViewed).HasColumnName("RM_Not_Viewed");
            });

            modelBuilder.Entity<RmUsers>(entity =>
            {
                entity.HasKey(e => e.UsrPkId)
                    .HasName("PK__RM_USERS__69FEDA60FFEA56B5");

                entity.ToTable("RM_USERS");

                entity.Property(e => e.UsrPkId).HasColumnName("USR_PK_Id");

                entity.Property(e => e.UsrActiveYn)
                    .IsRequired()
                    .HasColumnName("USR_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UsrCompanyName)
                    .HasColumnName("USR_CompanyName")
                    .HasMaxLength(500);

                entity.Property(e => e.UsrContactNo)
                    .HasColumnName("USR_ContactNo")
                    .HasMaxLength(100);

                entity.Property(e => e.UsrContrPkId).HasColumnName("USR_CONTR_PK_ID");

                entity.Property(e => e.UsrCrBy)
                    .HasColumnName("USR_CR_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsrCrDt)
                    .HasColumnName("USR_CR_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsrDepartment)
                    .HasColumnName("USR_Department")
                    .HasMaxLength(500);

                entity.Property(e => e.UsrDfltUserRole).HasColumnName("USR_DFLT_UserRole");

                entity.Property(e => e.UsrEmail)
                    .HasColumnName("USR_Email")
                    .HasMaxLength(500);

                entity.Property(e => e.UsrForceRstPwd).HasColumnName("USR_Force_Rst_PWD");

                entity.Property(e => e.UsrIsDisabled).HasColumnName("USR_IsDisabled");

                entity.Property(e => e.UsrLockedUntil)
                    .HasColumnName("USR_LockedUntil")
                    .HasColumnType("datetime");

                entity.Property(e => e.UsrLoginDate)
                    .HasColumnName("USR_LoginDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.UsrModBy)
                    .HasColumnName("USR_Mod_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsrModDt)
                    .HasColumnName("USR_Mod_DT")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsrPassword)
                    .HasColumnName("USR_Password")
                    .HasMaxLength(500);

                entity.Property(e => e.UsrPasswordExpiry)
                    .HasColumnName("USR_PasswordExpiry")
                    .HasColumnType("datetime");

                entity.Property(e => e.UsrPosition)
                    .HasColumnName("USR_Position")
                    .HasMaxLength(500);

                entity.Property(e => e.UsrReportingUsrPkId).HasColumnName("USR_Reporting_USR_PK_ID");

                entity.Property(e => e.UsrRetryCount).HasColumnName("USR_RetryCount");

                entity.Property(e => e.UsrSign).HasColumnName("USR_Sign");

                entity.Property(e => e.UsrSubmitSts).HasColumnName("USR_SUBMIT_STS");

                entity.Property(e => e.UsrUgDfltYn).HasColumnName("USR_UG_DFLT_YN");

                entity.Property(e => e.UsrUserName)
                    .HasColumnName("USR_UserName")
                    .HasMaxLength(500);

                entity.Property(e => e.UsrUserid)
                    .HasColumnName("USR_Userid")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RmUvModuleGroupFieldRights>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RM_uv_ModuleGroupFieldRights");

                entity.Property(e => e.GroupCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.MgfrCreatedBy)
                    .IsRequired()
                    .HasColumnName("MGFR_CreatedBy")
                    .HasMaxLength(200);

                entity.Property(e => e.MgfrCreatedOn)
                    .HasColumnName("MGFR_CreatedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.MgfrFieldName)
                    .IsRequired()
                    .HasColumnName("MGFR_FieldName")
                    .HasMaxLength(100);

                entity.Property(e => e.MgfrIsDisabled).HasColumnName("MGFR_IsDisabled");

                entity.Property(e => e.MgfrIsHide).HasColumnName("MGFR_IsHide");

                entity.Property(e => e.MgfrModifiedBy)
                    .IsRequired()
                    .HasColumnName("MGFR_ModifiedBy")
                    .HasMaxLength(200);

                entity.Property(e => e.MgfrModifiedOn)
                    .HasColumnName("MGFR_ModifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.MgfrPkId).HasColumnName("MGFR_PkId");

                entity.Property(e => e.ModPkId).HasColumnName("Mod_PkId");
            });

            modelBuilder.Entity<RmUvModuleGroupRights>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RM_uv_ModuleGroupRights");

                entity.Property(e => e.DvIsAdd).HasColumnName("DV_IsAdd");

                entity.Property(e => e.DvIsDelete).HasColumnName("DV_IsDelete");

                entity.Property(e => e.DvIsModify).HasColumnName("DV_IsModify");

                entity.Property(e => e.DvIsView).HasColumnName("DV_IsView");

                entity.Property(e => e.GroupCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.MgrCreatedBy)
                    .IsRequired()
                    .HasColumnName("MGR_CreatedBy")
                    .HasMaxLength(200);

                entity.Property(e => e.MgrCreatedOn)
                    .HasColumnName("MGR_CreatedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.MgrModifiedBy)
                    .IsRequired()
                    .HasColumnName("MGR_ModifiedBy")
                    .HasMaxLength(200);

                entity.Property(e => e.MgrModifiedOn)
                    .HasColumnName("MGR_ModifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.MgrPkId).HasColumnName("MGR_PkId");

                entity.Property(e => e.ModPkId).HasColumnName("Mod_PkId");

                entity.Property(e => e.PcIsAdd).HasColumnName("PC_IsAdd");

                entity.Property(e => e.PcIsDelete).HasColumnName("PC_IsDelete");

                entity.Property(e => e.PcIsModify).HasColumnName("PC_IsModify");

                entity.Property(e => e.PcIsView).HasColumnName("PC_IsView");
            });

            modelBuilder.Entity<RmWarImageDtl>(entity =>
            {
                entity.HasKey(e => e.FwarPkRefNo)
                    .HasName("PK__RM_WAR_i__B72C77917F68D8DC");

                entity.ToTable("RM_WAR_image_DTL");

                entity.Property(e => e.FwarPkRefNo).HasColumnName("FWAR_PK_Ref_No");

                entity.Property(e => e.FwarActiveYn)
                    .IsRequired()
                    .HasColumnName("FWAR_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FwarCrBy)
                    .HasColumnName("FWAR_CR_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FwarCrDt)
                    .HasColumnName("FWAR_CR_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwarFddPkRefNo).HasColumnName("FWAR_FDD_PK_Ref_No");

                entity.Property(e => e.FwarFxhPkRefNo).HasColumnName("FWAR_FXH_PK_Ref_No");

                entity.Property(e => e.FwarImageFilenameSys)
                    .HasColumnName("FWAR_Image_Filename_Sys")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FwarImageFilenameUpload)
                    .HasColumnName("FWAR_Image_Filename_Upload")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FwarImageSrno).HasColumnName("FWAR_Image_SRNO");

                entity.Property(e => e.FwarImageTypeCode)
                    .HasColumnName("FWAR_Image_Type_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FwarImageUserFilename)
                    .HasColumnName("FWAR_image_user_filename")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FwarModBy)
                    .HasColumnName("FWAR_Mod_By")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FwarModDt)
                    .HasColumnName("FWAR_Mod_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FwarSubmitSts).HasColumnName("FWAR_SUBMIT_STS");

                entity.HasOne(d => d.FwarFddPkRefNoNavigation)
                    .WithMany(p => p.RmWarImageDtl)
                    .HasForeignKey(d => d.FwarFddPkRefNo)
                    .HasConstraintName("FK__RM_WAR_im__FWAR___408F9238");

                entity.HasOne(d => d.FwarFxhPkRefNoNavigation)
                    .WithMany(p => p.RmWarImageDtl)
                    .HasForeignKey(d => d.FwarFxhPkRefNo)
                    .HasConstraintName("FK__RM_WAR_im__FWAR___4183B671");
            });

            modelBuilder.Entity<RmWeekLookup>(entity =>
            {
                entity.HasKey(e => e.ClkPkRefNo)
                    .HasName("PK__RM_Week___8642B1A4A008F3F5");

                entity.ToTable("RM_Week_Lookup");

                entity.Property(e => e.ClkPkRefNo).HasColumnName("CLK_PK_Ref_No");

                entity.Property(e => e.ClkMonth).HasColumnName("CLK_Month");

                entity.Property(e => e.ClkQuarter).HasColumnName("CLK_Quarter");

                entity.Property(e => e.ClkWeekNo).HasColumnName("CLK_Week_No");

                entity.Property(e => e.ClkYear).HasColumnName("CLK_Year");
            });

            modelBuilder.Entity<TestColumns>(entity =>
            {
                entity.HasKey(e => e.Column1)
                    .HasName("PK__TestColu__1AA08F1C09E1A05F");

                entity.Property(e => e.Column1)
                    .HasColumnName("Column_1")
                    .ValueGeneratedNever();

                entity.Property(e => e.Column2).HasColumnName("Column_2");

                entity.Property(e => e.Column3).HasColumnName("Column_3");

                entity.Property(e => e.Column4).HasColumnName("Column_4");
            });

            modelBuilder.Entity<UvwSearchData>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("uvw_SearchData");

                entity.Property(e => e.Display)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Hid).HasColumnName("HID");

                entity.Property(e => e.RefId)
                    .HasColumnName("RefID")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(63)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
