using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.Repository.Interfaces;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormAImageService : IFormAImageService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        public FormAImageService(IRepositoryUnit repoUnit, IMapper mapper)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> DectivateAssetImage(int assetImgId)
        {
            return await _repoUnit.FormAImgDtlRepository.DectivateAssetImage(assetImgId);
        }


        public async Task<List<FormAImageListRequestDTO>> GetAllImageByAssetPK(int assetPK)
        {
            List<FormAImageListRequestDTO> imageList = new List<FormAImageListRequestDTO>();
            try
            {
                var images = await _repoUnit.FormAImgDtlRepository.GetAllImageByAssetPK(assetPK).ConfigureAwait(false);
                foreach (var image in images)
                {
                    imageList.Add(_mapper.Map<FormAImageListRequestDTO>(image));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return imageList;
        }


        public async Task<int> LastInsertedSRNO(int hederId, string type)
        {
            return await _repoUnit.FormAImgDtlRepository.LastInsertedSRNO(hederId, type);
        }

        public async Task<int> SaveImageDtl(List<FormAImageListRequestDTO> imageList)
        {
            int rowsAffected;
            try
            {
                var imageListDtl = new List<RmFormaImageDtl>();

                foreach (var list in imageList)
                {
                    imageListDtl.Add(_mapper.Map<RmFormaImageDtl>(list));
                }

                _repoUnit.FormAImgDtlRepository.Create(imageListDtl);

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
