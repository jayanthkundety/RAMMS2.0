using System;
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
using RAMMS.Repository.Interfaces;

namespace RAMMS.Business.ServiceProvider.Services
{

    public class FormW1Service : IFormW1Service
    {
        private readonly IFormW1Repository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormW1Service(IRepositoryUnit repoUnit, IFormW1Repository repo, IMapper mapper, ISecurity security, IProcessService process)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
            _repo = repo;
        }

        

         

        public async Task<FormW1ResponseDTO> FindFormW1ByID(int id)
        {
            RmIwFormW1 formW1 = await _repo.FindFormW1ByID(id);
            return _mapper.Map<FormW1ResponseDTO>(formW1);
        }

        public async Task<int> LastInsertedIMAGENO(int hederId, string type)
        {
            int imageCt = await _repoUnit.FormW1Repository.GetImageId(hederId, type);
            return imageCt;
        }
        public async Task<int> SaveFormW1(FormW1ResponseDTO FormW1)
        {
            FormW1ResponseDTO formW1Response;
            try
            {
                var domainModelFormW1 = _mapper.Map<RmIwFormW1>(FormW1);
                // domainModelFormW1 = UpdateStatus(domainModelFormW1);
                //  var entity = _repoUnit.FormW1Repository.SaveFormW1(domainModelFormW1);
                var entity = _repoUnit.FormW1Repository.CreateReturnEntity(domainModelFormW1);
                formW1Response = _mapper.Map<FormW1ResponseDTO>(entity);
                return formW1Response.PkRefNo;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<List<FormW1ImageResponseDTO>> GetImageList(int formW1Id)
        {
            List<FormW1ImageResponseDTO> images = new List<FormW1ImageResponseDTO>();
            try
            {
                var getList = await _repoUnit.FormW1Repository.GetImagelist(formW1Id);
                foreach (var listItem in getList)
                {
                    images.Add(_mapper.Map<FormW1ImageResponseDTO>(listItem));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return images;
        }


        public async Task<int> SaveImage(List<FormW1ImageResponseDTO> image)
        {
            int rowsAffected;
            try
            {
                var domainModelFormW1 = new List<RmIwFormW1Image>();

                foreach (var list in image)
                {
                    domainModelFormW1.Add(_mapper.Map<RmIwFormW1Image>(list));
                }
                _repoUnit.FormW1Repository.SaveImage(domainModelFormW1);
                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public async Task<int> Update(FormW1ResponseDTO FormW1)
        {
            int rowsAffected;
            try
            {
                var domainModelformW1 = _mapper.Map<RmIwFormW1>(FormW1);
                domainModelformW1.Fw1ActiveYn = true;
                //domainModelformW2 = UpdateStatus(domainModelformW2);
                _repoUnit.FormW1Repository.Update(domainModelformW1);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

    }
}
