using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMS.Repository;

namespace RAMMS.Repository
{
    public class FormHImgRepository : RepositoryBase<RmFormhImageDtl>
    {
        public FormHImgRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public List<RmFormaImageDtl> SaveFormAImageDtl(List<RmFormhImageDtl> rmFormAImageDtls)
        {

            foreach (var imgItem in rmFormAImageDtls)
            {
                try
                {
                    _context.RmFormhImageDtl.Add(imgItem);
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
            _context.SaveChanges();
            return null;
        }

        public async Task<List<RmFormhImageDtl>> GetAllImageByAssetPK(int assetPK)
        {
            var list = await _context.RmFormhImageDtl.Where(s => s.FhiFhhPkRefNo == assetPK && s.FhiActiveYn.Value).ToListAsync();
            return list;
        }

        public async Task<int> DectivateAssetImage(int assetImgId)
        {
            var img = await _context.RmFormhImageDtl.FirstOrDefaultAsync(s => s.FhiPkRefNo == assetImgId);
            if (img != null)
            {
                img.FhiActiveYn = false;
                return await _context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> LastInsertedSRNO(int hederId, string type)
        {
            var id = await _context.RmFormhImageDtl.Where(s => s.FhiFhhPkRefNo == hederId && s.FhiActiveYn.Value && s.FhiImageTypeCode == type)
                .MaxAsync(s => s.FhiImageSrno);
            return id.GetValueOrDefault();
        }
    }
}
