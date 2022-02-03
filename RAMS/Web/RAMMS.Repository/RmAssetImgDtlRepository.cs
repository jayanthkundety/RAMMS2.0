using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RAMMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using RAMMS.Repository.Interfaces;

namespace RAMMS.Repository
{
    public interface IRmAssetImgRepository: IRepositoryBase<RmAssetImageDtl>
    {
        List<RmAssetImageDtl> SaveAssetImageDtl(List<RmAssetImageDtl> rmAssetImageDtls);

        IEnumerable<RmAssetImageDtl> GetUploadedImage();
    }
    public class RmAssetImgDtlRepository : RepositoryBase<RmAssetImageDtl>, IRmAssetImgRepository
    {
        public RmAssetImgDtlRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public List<RmAssetImageDtl> SaveAssetImageDtl(List<RmAssetImageDtl> rmAssetImageDtls)
        {

            foreach (var imgItem in rmAssetImageDtls)
            {
                try
                {
                    _context.RmAssetImageDtl.Add(imgItem);
                    _context.SaveChanges();

                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return null;
        }

        public IEnumerable<RmAssetImageDtl> GetUploadedImage()
        {
            IEnumerable<RmAssetImageDtl> gridData;
            gridData = _context.RmAssetImageDtl
                .AsNoTracking();

            return gridData.ToList();
        }

    }
}
