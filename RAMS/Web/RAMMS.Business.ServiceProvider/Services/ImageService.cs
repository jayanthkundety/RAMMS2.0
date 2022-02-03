using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.Repository.Interfaces;

namespace RAMMS.Business.ServiceProvider.Services
{
   public  class ImageService : IImageService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        public ImageService(IRepositoryUnit repoUnit, IMapper mapper)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> SaveImageDtl(List<ImageListRequestDTO> imagelist)
        {
            int rowsAffected;
            try
            {
                var imagelistdtl = new List<RmAssetImageDtl>();

                foreach (var list in imagelist)
                {
                    imagelistdtl.Add(_mapper.Map<RmAssetImageDtl>(list));
                }

                _repoUnit.RmAssetImgDtlRepository.Create(imagelistdtl);

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
