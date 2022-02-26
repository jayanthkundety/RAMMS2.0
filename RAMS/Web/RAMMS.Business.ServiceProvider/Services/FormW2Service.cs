using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormW2Service : IFormW2Service
    {
        private readonly IFormW2Repository _repo;
        private readonly IFormW2FcemRepository _repoFCEM;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormW2Service(IRepositoryUnit repoUnit, IFormW2Repository repo, IMapper mapper, ISecurity security, IProcessService process, IFormW2FcemRepository repoFCEM)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _repoFCEM = repoFCEM ?? throw new ArgumentNullException(nameof(repoFCEM));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
        }

        public async Task<int> DeActivateImage(int imageId)
        {
            int rowsAffected;
            try
            {
                var domainModelforW2 = await _repoUnit.FormW2Repository.GetImageById(imageId);
                domainModelforW2.Fw2iActiveYn = false;
                _repoUnit.FormW2Repository.UpdateImage(domainModelforW2);

                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<FormW2ResponseDTO> FindW2ByID(int id)
        {
            RmIwFormW2 formW2 = await _repo.FindW2ByID(id);
            return _mapper.Map<FormW2ResponseDTO>(formW2);
        }

        public async Task<List<FormIWImageResponseDTO>> GetImageList(int formW2Id)
        {
            List<FormIWImageResponseDTO> images = new List<FormIWImageResponseDTO>();
            try
            {
                var getList = await _repoUnit.FormW2Repository.GetImagelist(formW2Id);
                foreach (var listItem in getList)
                {
                    images.Add(_mapper.Map<FormIWImageResponseDTO>(listItem));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return images;
        }

        public async Task<int> LastInsertedIMAGENO(int hederId, string type)
        {
            int imageCt = await _repoUnit.FormW2Repository.GetImageId(hederId, type);
            return imageCt;
        }

        public async Task<int> Save(FormW2ResponseDTO formW2BO)
        {
            FormW2ResponseDTO formW2Response;
            try
            {
                var domainModelFormW2 = _mapper.Map<RmIwFormW2>(formW2BO);
                domainModelFormW2.Fw2PkRefNo = 0;
                domainModelFormW2.Fw2ActiveYn = true;
                //domainModelFormW2 = UpdateStatus(domainModelFormW2);
                var entity = _repoUnit.FormW2Repository.CreateReturnEntity(domainModelFormW2);
                formW2Response = _mapper.Map<FormW2ResponseDTO>(entity);
                return formW2Response.PkRefNo;

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> SaveImage(List<FormIWImageResponseDTO> image)
        {
            int rowsAffected;
            try
            {
                var domainModelFormW2 = new List<RmIwFormW2Image>();

                foreach (var list in image)
                {
                    domainModelFormW2.Add(_mapper.Map<RmIwFormW2Image>(list));
                }
                _repoUnit.FormW2Repository.SaveImage(domainModelFormW2);
                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> Update(FormW2ResponseDTO formW2Bo)
        {
            int rowsAffected;
            try
            {
                var domainModelformW2 = _mapper.Map<RmIwFormW2>(formW2Bo);
                domainModelformW2.Fw2PkRefNo = formW2Bo.PkRefNo;
                domainModelformW2.Fw2ActiveYn = true;
                //domainModelformW2 = UpdateStatus(domainModelformW2);
                _repoUnit.FormW2Repository.Update(domainModelformW2);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> UpdateFormW2Signature(FormW2ResponseDTO formW2DTO)
        {
            int rowsAffected = 2;
            try
            {

                /*   var getHeader = await _repoUnit.FormW2Repository.GetByIdAsync(formW2DTO.PkRefNo);
                   getHeader.fFwSubmitSts = formW2DTO.SubmitSts;
                   getHeader.FdhSignAgrdSo = formW2DTO.SignAgrdSo ?? getHeader.FdhSignAgrdSo ?? null;
                   getHeader.FdhSignPrcdSo = formW2DTO.SignPrcdSo ?? getHeader.FdhSignPrcdSo ?? null;
                   getHeader.FdhSignPrp = formW2DTO.ReportedBySign ?? getHeader.FdhSignPrp ?? null;
                   getHeader.FdhSignVer = formW2DTO.SignVer ?? getHeader.FdhSignVer ?? null;
                   getHeader.FdhSignVerSo = formW2DTO.SignVerSo ?? getHeader.FdhSignVerSo ?? null;
                   getHeader.FdhSignVet = formW2DTO.SignVet ?? getHeader.FdhSignVet ?? null;

                   getHeader.FdhUseridAgrdSo = formW2DTO.UseridAgrdSo ?? getHeader.FdhUseridAgrdSo ?? null;
                   getHeader.FdhUsernameAgrdSo = formW2DTO.UseridVer ?? getHeader.FdhUsernameAgrdSo ?? null;
                   getHeader.FdhDesignationAgrdSo = formW2DTO.DesignationAgrdSo ?? getHeader.FdhDesignationAgrdSo ?? null;
                   getHeader.FdhDtAgrdSo = formW2DTO.DtAgrdSo ?? getHeader.FdhDtAgrdSo ?? null;

                   getHeader.FdhUseridPrcdSo = formW2DTO.UseridPrcdSo ?? getHeader.FdhUseridPrcdSo ?? null;
                   getHeader.FdhUsernamePrcdSo = formW2DTO.UsernamePrcdSo ?? getHeader.FdhUsernamePrcdSo ?? null;
                   getHeader.FdhDesignationPrcdSo = formW2DTO.DesignationPrcdSo ?? getHeader.FdhDesignationPrcdSo ?? null;
                   getHeader.FdhDtPrcdSo = formW2DTO.DtPrcdSo ?? getHeader.FdhDtPrcdSo ?? null;

                   if (formW2DTO.ReportedByUserId != null)
                   {
                       getHeader.FdhUseridPrp = formW2DTO.ReportedByUserId.ToString() ?? getHeader.FdhUseridPrp ?? null;
                   }
                   getHeader.FdhUsernamePrp = formW2DTO.ReportedByUsername ?? getHeader.FdhUsernamePrp ?? null;
                   getHeader.FdhDesignationPrp = formW2DTO.ReportedByDesignation ?? getHeader.FdhDesignationPrp ?? null;
                   getHeader.FdhDtPrp = formW2DTO.DateReported ?? getHeader.FdhDtPrp ?? null;

                   getHeader.FdhUseridVer = formW2DTO.UseridVer ?? getHeader.FdhUseridVer ?? null;
                   getHeader.FdhUsernameVer = formW2DTO.UsernameVer ?? getHeader.FdhUsernameVer ?? null;
                   getHeader.FdhDesignationVer = formW2DTO.DesignationVer ?? getHeader.FdhDesignationVer ?? null;
                   getHeader.FdhDtVer = formW2DTO.DtVer ?? getHeader.FdhDtVer ?? null;

                   getHeader.FdhUseridVerSo = formW2DTO.UseridVerSo ?? getHeader.FdhUseridVerSo ?? null;
                   getHeader.FdhUsernameVerSo = formW2DTO.UsernameVerSo ?? getHeader.FdhUsernameVerSo ?? null;
                   getHeader.FdhDesignationVerSo = formW2DTO.DesignationVerSo ?? getHeader.FdhDesignationVerSo ?? null;
                   getHeader.FdhDtVerSo = formW2DTO.DtVerSo ?? getHeader.FdhDtVerSo ?? null;

                   getHeader.FdhUseridVet = formW2DTO.UseridVet ?? getHeader.FdhUseridVet ?? null;
                   getHeader.FdhUsernameVet = formW2DTO.UsernameVet ?? getHeader.FdhUsernameVet ?? null;
                   getHeader.FdhDesignationVet = formW2DTO.DesignationVet ?? getHeader.FdhDesignationVet ?? null;
                   getHeader.FdhDtVet = formW2DTO.DtVet ?? getHeader.FdhDtVet ?? null;


                   var formD = _mapper.Map<RmFormDHdr>(getHeader);


                   _repoUnit.FormDRepository.Update(formD);*/


                //rowsAffected = 2;// await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                //await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public async Task<IEnumerable<CSelectListItem>> GetFormW1DDL()
        {
            return await _repoUnit.FormW1Repository.FindAsync(m => m.Fw1ActiveYn == true , x => new CSelectListItem() { Text = x.Fw1IwRefNo, Value = x.Fw1PkRefNo.ToString() });
        }

        public async Task<List<FormW1ResponseDTO>> GetFormW1List()
        {
            List<FormW1ResponseDTO> images = new List<FormW1ResponseDTO>();
            try
            {
                var getList = await _repoUnit.FormW2Repository.GetFormW1List();
                foreach (var listItem in getList)
                {
                    images.Add(_mapper.Map<FormW1ResponseDTO>(listItem));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return images;
        }
        public async Task<FormW1ResponseDTO> GetFormW1ById(int formW1Id)
        {
            RmIwFormW1 formW1 = await _repo.GetFormW1ById(formW1Id);
            return _mapper.Map<FormW1ResponseDTO>(formW1);
        }

        public async Task<FormW1ResponseDTO> GetFormW1ByRoadCode(string roadCode)
        {
            RmIwFormW1 formW1 = await _repo.GetFormW1ByRoadCode(roadCode);
            return _mapper.Map<FormW1ResponseDTO>(formW1);
        }


        public async Task<IEnumerable> GetRoadCodesByRMU(string rmu)
        {
            try
            {
                var codes = await _repoUnit.FormW2Repository.GetRoadCodesByRMU(rmu);

                return codes.OrderBy(s => s.RdmPkRefNo).Select(x => new 
                {
                    Value = x.RdmRdCode.ToString(),
                    Text = x.RdmRdCode + "-" + x.RdmRdName.ToString(),
                    CValue = x.RdmRmuCode,
                    Item1 = x.RdmRdName,
                    PKId = x.RdmPkRefNo,
                    Code = x.RdmRdCode,
                    Item2 = x.RdmSecCode.ToString(),
                    Item3 = (x.RdmLengthPaved + x.RdmLengthUnpaved).ToString(),
                    FromKm = (int)x.RdmFrmCh,
                    FromM = x.RdmFrmChDeci.ToString(),
                    ToKm = (int)x.RdmToCh,
                    ToM = x.RdmToChDeci.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }

        }


        public async Task<PagingResult<FormIWResponseDTO>> GetFilteredFormIWGrid(FilteredPagingDefinition<FormIWSearchGridDTO> filterOptions)
        {
            PagingResult<FormIWResponseDTO> result = new PagingResult<FormIWResponseDTO>();

            List<FormIWResponseDTO> formIWList = new List<FormIWResponseDTO>();
            try
            {

                var filteredRecords = await _repoUnit.FormW2Repository.GetFilteredRecordList(filterOptions).ConfigureAwait(false);

                result.TotalRecords = await _repoUnit.FormW2Repository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                result.PageResult = filteredRecords;

                result.PageNo = filterOptions.StartPageNo;
                result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return result;
        }

        #region FCEM

        public async Task<int> SaveFCEM(FormW2FECMResponseDTO formW2BO)
        {
            FormW2FECMResponseDTO formW2Response;
            try
            {
                var domainModelFcem = _mapper.Map<RmIwFormW2Fecm>(formW2BO);
                domainModelFcem.FecmPkRefNo = 0;
                var entity = _repoUnit.FormW2FcemRepository.CreateReturnEntity(domainModelFcem);
                formW2Response = _mapper.Map<FormW2FECMResponseDTO>(entity);
                return formW2Response.PkRefNo;

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> UpdateFCEM(FormW2FECMResponseDTO formW2Bo)
        {
            int rowsAffected;
            try
            {
                var domainModelformW2 = _mapper.Map<RmIwFormW2Fecm>(formW2Bo);
                domainModelformW2.FecmPkRefNo = formW2Bo.PkRefNo;
                domainModelformW2.FecmActiveYn = true;
                //domainModelformW2 = UpdateStatus(domainModelformW2);
                _repoUnit.FormW2FcemRepository.Update(domainModelformW2);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<FormW2FECMResponseDTO> FindFCEM2ByW2ID(int Id)
        {
            RmIwFormW2Fecm formW2 = await _repoFCEM.FindFCEM2ByW2ID(Id);
            return _mapper.Map<FormW2FECMResponseDTO>(formW2);
        }

        public async Task<FormW2FECMResponseDTO> FindFCEM2ByID(int Id)
        {
            RmIwFormW2Fecm formW2 = await _repoFCEM.FindFCEM2ByID(Id);
            return _mapper.Map<FormW2FECMResponseDTO>(formW2);
        }


        #endregion
    }
}
