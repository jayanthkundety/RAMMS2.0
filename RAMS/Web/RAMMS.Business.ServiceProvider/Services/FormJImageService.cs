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
    public class FormJImageService : IFormJImageService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        public FormJImageService(IRepositoryUnit repoUnit, IMapper mapper)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> DectivateAssetImage(int assetImgId)
        {
            return await _repoUnit.FormJImgDtlRepository.DectivateAssetImage(assetImgId);
        }


        public async Task<List<FormJImageListRequestDTO>> GetAllImageByAssetPK(int assetPK)
        {
            List<FormJImageListRequestDTO> imageList = new List<FormJImageListRequestDTO>();
            try
            {
                var images = await _repoUnit.FormJImgDtlRepository.GetAllImageByAssetPK(assetPK).ConfigureAwait(false);
                foreach (var image in images)
                {
                    imageList.Add(_mapper.Map<FormJImageListRequestDTO>(image));
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
            return await _repoUnit.FormJImgDtlRepository.LastInsertedSRNO(hederId, type);
        }

        public async Task<int> SaveImageDtl(List<FormJImageListRequestDTO> imageList)
        {
            int rowsAffected;
            try
            {
                var imageListDtl = new List<RmFormjImageDtl>();

                foreach (var list in imageList)
                {
                    imageListDtl.Add(_mapper.Map<RmFormjImageDtl>(list));
                }

                _repoUnit.FormJImgDtlRepository.Create(imageListDtl);

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
