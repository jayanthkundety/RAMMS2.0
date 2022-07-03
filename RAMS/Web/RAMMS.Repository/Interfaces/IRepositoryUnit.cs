using RAMMS.Domain.Models;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IRepositoryUnit
    {
        public FormARepository FormARepository { get; }

        public FormXRepository FormXRepository { get; }

        public FormN1Repository FormN1Repository { get; }

        public FormN2Repository FormN2Repository { get; }

        public FormQa2Repository FormQa2Repository { get; }

        public FormQa2DtlRepository FormQa2DtlRepository { get; }

        public FormDRepository FormDRepository { get; }

        public FormDLabourRepository FormDLabourRepository { get; }

        public FormDMaterialRepository FormDMaterialRepository { get; }

        public FormDEduipmentRepository FormDEquipmentRepository { get; }

        public FormDDtlRepository FormDDtlRepository { get; }

        public FormADtlRepository FormADtlRepository { get; }
        public AssetRepository AllAssetRepository { get; }
        public RoadmasterRepository RoadmasterRepository { get; }
        public DDLookupRepository DDLookUpRepository { get; }
        public UserRepository UserRepository { get; }
        public RmAssetImgDtlRepository RmAssetImgDtlRepository { get; }
        public IFormAImgRepository FormAImgDtlRepository { get; }
        public IFormJImgRepository FormJImgDtlRepository { get; }
        public FormHRepository FormHRepository { get; }
        public FormJRepository FormJRepository { get; }
        public FormHImgRepository FormHImgRepository { get; }
        public FormS2Repository FormS2Repository { get; }
        public FormS2DetailRepository FormS2DetailRepository { get; }

        //S2 Days Schedule
        public FormS2DayScheduleRepository FormS2DayScheduleRepository { get; }

        //IW Forms
        public FormWCRepository FormWCRepository { get; }
        public FormWGRepository FormWGRepository { get; }
        public FormW2FcemRepository FormW2FcemRepository { get; }
        public FormW1Repository FormW1Repository { get; }
        public FormW2Repository FormW2Repository { get; }
        public FormWDRepository FormWDRepository { get; }
        public FormWNRepository FormWNRepository { get; }

        public ModuleFormRightsRepository ModuleFormRightsRepository { get; }

        //End IW Forms

        //MAM - VForms
        public FormV2Repository FormV2Repository { get; }
        public FormV2LabourRepository FormV2LabourRepository { get; }
        public FormV2EquipmentRepository FormV2EquipmentRepository { get; }
        public FormV2MaterialRepository FormV2MaterialRepository { get; }

        public FormQa1Repository FormQa1Repository { get; } 
        //End Vforms


        public FormG1Repository FormG1Repository { get; }

        public FormR1R2Repository FormR1Repository { get; }

        FormS2QuarterDtlRepository FormS2QuarterDtlRepository { get; }
        CalendarRepository CalendarRepository { get; }
        IFormF2Repository FormF2Repository { get; }
        IFormF2DetailRepository FormF2DetailRepository { get; }
        IFormB1B2HeaderRepository FormB1B2HeaderRepository { get; }
        IFormB1B2DetailRepository FormB1B2DetailRepository { get; }
        IFormB1B2ImgRepository FormB1B2ImgRepository { get; }

        IFormS1Repository formS1Repository { get; }
        IFormFDRepository FormFDRepository { get; }
        IFormF4Repository formF4Repository { get; }
        IFormF5Repository formF5Repository { get; }
        IFormFSHeaderRepository FormFSHeaderRepository { get; }
        IFormFSDetailRepository FormFSDetailRepository { get; }
        IFormC1C2Repository formC1C2Repository { get; }
        IModuleGroupRepository ModuleGroupRepository { get; }
        IAuditTransactionRepository AuditTransactionRepository { get; }
        IAuditActionRepository AuditActionRepository { get; }

        //MAM
        public FormV1Repository FormV1Repository { get; }

        public FormF3Repository FormF3Repository { get; }
        public FormF1Repository FormF1Repository { get; }


        int Commit();
        Task<int> CommitAsync();
        void Rollback();
        Task RollbackAsync();
        RAMMSContext _context { get; }
        IDivisonRepository DivisonRepository { get; }
        IRMURepository RMURepository { get; }

        IDivRmuSectionRepository DivRmuSectionRepository { get; }
        ISectionRepository SectionRepository { get; }
        IRoadRepository RoadRepository { get; }
    }
}
