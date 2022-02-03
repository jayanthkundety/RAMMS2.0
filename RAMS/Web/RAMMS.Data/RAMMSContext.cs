using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using RAMMS.Domain.Models;

namespace RAMMS.Data
{
    public class RAMMSContext : DbContext
    {
        public RAMMSContext(DbContextOptions options) : base(options) { }

        public RAMMSContext()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>(eb =>
            //{
            //    //eb.HasKey(x => x.Pk);
            //    eb.HasNoKey();
            //});
            //modelBuilder.Entity<UserDetail>(eb =>
            //{
            //    //eb.HasKey(x => x.Pk);
            //    eb.HasNoKey();
            //});
            //modelBuilder.Entity<UserGroup>(eb =>
            //{
            //    //eb.HasKey(x => x.Pk);
            //    eb.HasNoKey();

            //});


            modelBuilder.Entity<RmDdLookup>(entity =>
            {
                entity.HasKey(e => e.DdlPkRefNo)
                    .HasName("PK__RM_DD_LO__C0F32C374D78F971");

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
            // OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<RmFormAHdr>(entity =>
            {
                entity.HasKey(e => e.FahPkRefNo)
                    .HasName("PK__RM_FormA__797F99D609527BB0");

                entity.ToTable("RM_FormA_HDR");

                entity.Property(e => e.FahPkRefNo).HasColumnName("FAH_PK_Ref_No");

                entity.Property(e => e.FahActiveYn)
                    .IsRequired()
                    .HasColumnName("FAH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FahAssetGroupCode)
                    .HasColumnName("FAH_Asset_Group_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

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

                entity.Property(e => e.FahSignPrp).HasColumnName("FAH_SIgn_PRP");

                entity.Property(e => e.FahSignVer).HasColumnName("FAH_SIgn_VER");

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

                entity.Property(e => e.FahYyyyMm).HasColumnName("FAH_YYYY_MM");
            });

            modelBuilder.Entity<RmFormADtl>(entity =>
            {
                entity.HasKey(e => e.FadPkRefNo)
                    .HasName("PK__RM_FormA__312C1ECD59C43622");

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
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FadDefCodeCl)
                    .HasColumnName("FAD_Def_code_CL")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FadDefCodeLhs)
                    .HasColumnName("FAD_Def_code_LHS")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FadDefCodeRhs)
                    .HasColumnName("FAD_Def_code_RHS")
                    .HasMaxLength(16)
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

                entity.Property(e => e.FadFrmChDeci).HasColumnName("FAD_FRM_CH_Deci");

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
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FadRemarks)
                    .HasColumnName("FAD_Remarks")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FadRt).HasColumnName("FAD_RT");

                entity.Property(e => e.FadSftPs).HasColumnName("FAD_SFT_PS");

                entity.Property(e => e.FadSftWis).HasColumnName("FAD_SFT_WIS");

                entity.Property(e => e.FadSiteRef)
                    .HasColumnName("FAD_Site_Ref")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FadSrno).HasColumnName("FAD_SRNO");

                entity.Property(e => e.FadSubmitSts).HasColumnName("FAD_SUBMIT_STS");

                entity.Property(e => e.FadToCh).HasColumnName("FAD_To_CH");

                entity.Property(e => e.FadToChDeci).HasColumnName("FAD_To_CH_Deci");

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
                    .HasConstraintName("FK__RM_FormA___FAD_F__534D60F1");
            });
            modelBuilder.Entity<RmFormHHdr>(entity =>
            {
                entity.HasKey(e => e.FhhPkRefNo)
                    .HasName("PK__RM_FormH__E221D2BF5FA96394");

                entity.ToTable("RM_FormH_HDR");

                entity.Property(e => e.FhhPkRefNo).HasColumnName("FHH_PK_Ref_No");

                entity.Property(e => e.FhhActiveYn)
                    .IsRequired()
                    .HasColumnName("FHH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FhhAssetGroupCode)
                    .HasColumnName("FHH_Asset_Group_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.FhhAssetId)
                    .HasColumnName("FHH_Asset_ID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

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

                entity.Property(e => e.FhhDtSubAuth)
                    .HasColumnName("FHH_DT_SUB_AUTH")
                    .HasColumnType("datetime");

                entity.Property(e => e.FhhDtVer)
                    .HasColumnName("FHH_DT_VER")
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

                entity.Property(e => e.FhhSignPrp).HasColumnName("FHH_SIgn_PRP");

                entity.Property(e => e.FhhSignRcvdAuth).HasColumnName("FHH_SIgn_RCVD_AUTH");

                entity.Property(e => e.FhhSignVer).HasColumnName("FHH_SIgn_VER");

                entity.Property(e => e.FhhSignVetAuth).HasColumnName("FHH_SIgn_VET_AUTH");

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
                    .HasConstraintName("FK__RM_FormH___FHH_F__6B24EA82");

                //entity.HasOne(d => d.FhhFjdPkRefNoNavigation)
                //    .WithMany(p => p.RmFormHHdr)
                //    .HasForeignKey(d => d.FhhFjdPkRefNo)
                //    .HasConstraintName("FK__RM_FormH___FHH_F__6C190EBB");
            });

            modelBuilder.Entity<RmFormJDtl>(entity =>
            {
                entity.HasKey(e => e.FjdPkRefNo)
                    .HasName("PK__RM_FormJ__CAC96AA87A635154");

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

                entity.Property(e => e.FjdFrmChDeci).HasColumnName("FJD_FRM_CH_Deci");

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
                    .HasMaxLength(16)
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

                entity.Property(e => e.FjdToChDeci).HasColumnName("FJD_To_CH_Deci");

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
                    .HasConstraintName("FK__RM_FormJ___FJD_F__60A75C0F");
            });
            modelBuilder.Entity<RmFormJHdr>(entity =>
            {
                entity.HasKey(e => e.FjhPkRefNo)
                    .HasName("PK__RM_FormJ__8E84C363B6AC82E5");

                entity.ToTable("RM_FormJ_HDR");

                entity.Property(e => e.FjhPkRefNo).HasColumnName("FJH_PK_Ref_No");

                entity.Property(e => e.FjhActiveYn)
                    .IsRequired()
                    .HasColumnName("FJH_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FjhAssetGroupCode)
                    .HasColumnName("FJH_Asset_Group_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

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

                entity.Property(e => e.FjhSignPrp).HasColumnName("FJH_SIgn_PRP");

                entity.Property(e => e.FjhSignVer).HasColumnName("FJH_SIgn_VER");

                entity.Property(e => e.FjhSignVet).HasColumnName("FJH_SIgn_VET");

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

                entity.Property(e => e.FjhYyyyMm).HasColumnName("FJH_YYYY_MM");
            });
            modelBuilder.Entity<RmFormaImageDtl>(entity =>
            {
                entity.HasKey(e => e.FaiPkRefNo)
                    .HasName("PK__RM_FORMA__F68F136645E7C048");

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
                    .HasMaxLength(16)
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
                    .HasConstraintName("FK__RM_FORMA___FAI_F__5812160E");
            });
            modelBuilder.Entity<RmFormhImageDtl>(entity =>
            {
                entity.HasKey(e => e.FhiPkRefNo)
                    .HasName("PK__RM_FORMH__9D78178595556E64");

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
                    .HasMaxLength(16)
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
                    .HasConstraintName("FK__RM_FORMH___FHI_F__70DDC3D8");
            });
            modelBuilder.Entity<RmFormjImageDtl>(entity =>
            {
                entity.HasKey(e => e.FjiPkRefNo)
                    .HasName("PK__RM_FORMJ__D85FB9EBA3101BE5");

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
                    .HasMaxLength(16)
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
                    .HasConstraintName("FK__RM_FORMJ___FJI_F__656C112C");
            });
            modelBuilder.Entity<RmAllassetInventory>(entity =>
            {
                entity.HasKey(e => e.AiPkRefNo)
                  .HasName("PK__RM_Allas__76B9B0FC33795F71");

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
                  .HasMaxLength(16)
                  .IsUnicode(false);

                entity.Property(e => e.AiAssetId)
                  .HasColumnName("AI_Asset_ID")
                  .HasMaxLength(100)
                  .IsUnicode(false);

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
                  .HasMaxLength(16)
                  .IsUnicode(false);

                entity.Property(e => e.AiDivCode)
                  .IsRequired()
                  .HasColumnName("AI_Div_Code")
                  .HasMaxLength(16)
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
                  .HasMaxLength(1)
                  .IsUnicode(false);

                entity.Property(e => e.AiFinRdLevel).HasColumnName("AI_Fin_Rd_Level");

                entity.Property(e => e.AiFrmCh).HasColumnName("AI_FRM_CH");

                entity.Property(e => e.AiFrmChDeci).HasColumnName("AI_FRM_CH_Deci");

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

                entity.Property(e => e.AiHeight).HasColumnName("AI_Height");

                entity.Property(e => e.AiIntelLevel).HasColumnName("AI_Intel_Level");

                entity.Property(e => e.AiIntelStruc)
                  .HasColumnName("AI_Intel_Struc")
                  .HasMaxLength(16)
                  .IsUnicode(false);

                entity.Property(e => e.AiLaneCnt).HasColumnName("AI_Lane_Cnt");

                entity.Property(e => e.AiLaneNo)
                  .HasColumnName("AI_Lane_No")
                  .HasMaxLength(150)
                  .IsUnicode(false);

                entity.Property(e => e.AiLength).HasColumnName("AI_Length");

                entity.Property(e => e.AiLengthSpan).HasColumnName("AI_length_Span");

                entity.Property(e => e.AiLocCh).HasColumnName("AI_Loc_CH");

                entity.Property(e => e.AiMaintainedBy)
                  .HasColumnName("AI_maintained_By")
                  .HasMaxLength(16)
                  .IsUnicode(false);

                entity.Property(e => e.AiMaterial)
                  .HasColumnName("AI_Material")
                  .HasMaxLength(16)
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
                  .HasMaxLength(16)
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

                entity.Property(e => e.AiRiverName)
                  .HasColumnName("AI_River_Name")
                  .HasMaxLength(250)
                  .IsUnicode(false);

                entity.Property(e => e.AiRmuCode)
                  .IsRequired()
                  .HasColumnName("AI_RMU_Code")
                  .HasMaxLength(16)
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
                  .HasMaxLength(16)
                  .IsUnicode(false);

                entity.Property(e => e.AiStrucSuper)
                  .HasColumnName("AI_Struc_Super")
                  .HasMaxLength(100)
                  .IsUnicode(false);

                entity.Property(e => e.AiSubmitSts).HasColumnName("AI_SUBMIT_STS");

                entity.Property(e => e.AiTier).HasColumnName("AI_Tier");

                entity.Property(e => e.AiToCh).HasColumnName("AI_To_CH");

                entity.Property(e => e.AiToChDeci).HasColumnName("AI_To_CH_Deci");

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
                  .HasConstraintName("FK__RM_Allass__AI_RD__4316F928");
            });
            modelBuilder.Entity<RmAssetImageDtl>(entity =>
            {
                entity.HasKey(e => e.AidPkRefNo)
                    .HasName("PK__RM_Asset__E2BB224759304E87");

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
                    .HasMaxLength(16)
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
                    .HasConstraintName("FK__RM_Asset___AID_A__47DBAE45");
            });
            modelBuilder.Entity<RmRoadMaster>(entity =>
            {
                entity.HasKey(e => e.RdmPkRefNo)
                    .HasName("PK__RM_Road___D2C3D8CD85DA3224");

                entity.ToTable("RM_Road_Master");

                entity.Property(e => e.RdmPkRefNo).HasColumnName("RDM_PK_Ref_No");

                entity.Property(e => e.RdmActiveYn)
                    .IsRequired()
                    .HasColumnName("RDM_Active_YN")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RdmCrBy)
                    .HasColumnName("RDM_CR_By")
                    .HasMaxLength(16)
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
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RdmLengthUnpaved)
                    .HasColumnName("RDM_Length_Unpaved")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RdmModBy)
                    .HasColumnName("RDM_Mod_By")
                    .HasMaxLength(16)
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

                entity.Property(e => e.RdmSecCode)
                    .IsRequired()
                    .HasColumnName("RDM_Sec_Code")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.RdmToCh).HasColumnName("RDM_To_CH");

                entity.Property(e => e.RdmToChDeci).HasColumnName("RDM_To_CH_Deci");

                entity.Property(e => e.RdmToLoc)
                    .HasColumnName("RDM_TO_Loc")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

        }



        //entities
        //public DbSet<User> User { get; set; }
        //public DbSet<UserDetail> UserDetail { get; set; }
        //public DbSet<UserGroup> UserGroup { get; set; }
        // public virtual DbSet<RmDdLookup> RmDdLookup { get; set; }

        //public virtual DbSet<RmAccUcuImageDtl> RmAccUcuImageDtl { get; set; }
        public virtual DbSet<RmAllassetInventory> RmAllassetInventory { get; set; }
        //public virtual DbSet<RmAssetDefectCode> RmAssetDefectCode { get; set; }
        //public virtual DbSet<RmAssetGroupType> RmAssetGroupType { get; set; }
        public virtual DbSet<RmAssetImageDtl> RmAssetImageDtl { get; set; }
        public virtual DbSet<RmDdLookup> RmDdLookup { get; set; }
        //public virtual DbSet<RmDivRmuSecMaster> RmDivRmuSecMaster { get; set; }
        public virtual DbSet<RmFormADtl> RmFormADtl { get; set; }
        public virtual DbSet<RmFormAHdr> RmFormAHdr { get; set; }
        // public virtual DbSet<RmFormDDtl> RmFormDDtl { get; set; }
        //public virtual DbSet<RmFormDEquipDtl> RmFormDEquipDtl { get; set; }
        //public virtual DbSet<RmFormDHdr> RmFormDHdr { get; set; }
        //public virtual DbSet<RmFormDLabourDtl> RmFormDLabourDtl { get; set; }
        //public virtual DbSet<RmFormDMaterialDtl> RmFormDMaterialDtl { get; set; }
        //public virtual DbSet<RmFormHHdr> RmFormHHdr { get; set; }
        //public virtual DbSet<RmFormJDtl> RmFormJDtl { get; set; }
        //public virtual DbSet<RmFormJHdr> RmFormJHdr { get; set; }
        //public virtual DbSet<RmFormXHdr> RmFormXHdr { get; set; }
        public virtual DbSet<RmFormaImageDtl> RmFormaImageDtl { get; set; }
        //public virtual DbSet<RmFormhImageDtl> RmFormhImageDtl { get; set; }
        //public virtual DbSet<RmFormjImageDtl> RmFormjImageDtl { get; set; }
        public virtual DbSet<RmRoadMaster> RmRoadMaster { get; set; }
        //public virtual DbSet<RmWarImageDtl> RmWarImageDtl { get; set; }

    }
}
