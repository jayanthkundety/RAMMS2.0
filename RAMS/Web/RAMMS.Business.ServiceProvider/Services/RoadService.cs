// Decompiled with JetBrains decompiler
// Type: RAMMS.Business.ServiceProvider.Services.RoadService
// Assembly: RAMMS.Business.ServiceProvider, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2E3E52CD-370B-4DC8-B38A-DF785010DD3C
// Assembly location: F:\Mohan\Avows\RAMS_WEB_PRD\RAMMS.Business.ServiceProvider.dll

using AutoMapper;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.Repository.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class RoadService : IRoadService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;

        public RoadService(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security ?? throw new ArgumentNullException(nameof(security));
        }

        public long LastRoadInsertedNo()
        {
            RmRoadMaster rmRoadMaster = _repoUnit.RoadRepository.GetAll().OrderByDescending(s => s.RdmPkRefNo).FirstOrDefault<RmRoadMaster>();
            return rmRoadMaster != null ? rmRoadMaster.RdmPkRefNo : 0;
        }

        public async Task<RoadRequestDTO> GetRoadById(int id)
        {
            RmRoadMaster async = await _repoUnit.RoadRepository.FindAsync(s => s.RdmPkRefNo == id);
            return async != null ? _mapper.Map<RmRoadMaster, RoadRequestDTO>(async) : null;
        }

        public async Task<int> SaveRoad(RoadRequestDTO model)
        {
            int rdPkId = 0;
            try
            {
                RmRoadMaster form = _mapper.Map<RmRoadMaster>(model);
                if (form.RdmPkRefNo != 0)
                {
                    form.RdmModDt = new DateTime?(DateTime.UtcNow);
                    form.RdmModBy = _security.UserName;
                    _repoUnit.RoadRepository.Update(form);
                }
                else
                {
                    RmRoadMaster asyncNoTracking = await _repoUnit.RoadRepository.FindAsyncNoTracking(s => s.RdmRdCode == form.RdmRdCode);
                    if (asyncNoTracking != null)
                    {
                        form.RdmPkRefNo = asyncNoTracking.RdmPkRefNo;
                        form.RdmModDt = new DateTime?(DateTime.UtcNow);
                        form.RdmModBy = _security.UserName;
                        _repoUnit.RoadRepository.Update(form);
                    }
                    else
                    {
                        form.RdmCrDt = new DateTime?(DateTime.UtcNow);
                        form.RdmCrBy = _security.UserName;
                        form.RdmModDt = new DateTime?(DateTime.UtcNow);
                        form.RdmModBy = _security.UserName;
                        _repoUnit.RoadRepository.Create(form);
                    }
                }
                int num2 = await _repoUnit.CommitAsync();
                rdPkId = form.RdmPkRefNo;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return rdPkId;
        }

        public async Task<bool> RemoveRoad(int id) => _repoUnit.RoadRepository.Find(s => s.RdmPkRefNo == id) != null && await _repoUnit.CommitAsync() > 0;

        public async Task<GridWrapper<object>> GetRoadList(
          DataTableAjaxPostModel filterOptions)
        {
            return await _repoUnit.RoadRepository.GetFilteredRecordList(filterOptions);
        }
    }
}
