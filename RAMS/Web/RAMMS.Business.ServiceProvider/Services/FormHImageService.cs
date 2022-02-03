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
    public class FormHImageService : IFormHImageService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        public FormHImageService(IRepositoryUnit repoUnit, IMapper mapper)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> DectivateAssetImage(int assetImgId)
        {
            return await _repoUnit.FormHImgRepository.DectivateAssetImage(assetImgId);
        }


        public async Task<List<FormHImageListRequestDTO>> GetAllImageByAssetPK(int assetPK)
        {
            List<FormHImageListRequestDTO> imageList = new List<FormHImageListRequestDTO>();
            try
            {
                var images = await _repoUnit.FormHImgRepository.GetAllImageByAssetPK(assetPK).ConfigureAwait(false);
                foreach (var image in images)
                {
                    imageList.Add(_mapper.Map<FormHImageListRequestDTO>(image));
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
            return await _repoUnit.FormHImgRepository.LastInsertedSRNO(hederId, type);
        }

        public async Task<int> SaveImageDtl(List<FormHImageListRequestDTO> imageList)
        {
            int rowsAffected;
            try
            {
                var imageListDtl = new List<RmFormhImageDtl>();

                foreach (var list in imageList)
                {
                    imageListDtl.Add(_mapper.Map<RmFormhImageDtl>(list));
                }

                _repoUnit.FormHImgRepository.Create(imageListDtl);

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
