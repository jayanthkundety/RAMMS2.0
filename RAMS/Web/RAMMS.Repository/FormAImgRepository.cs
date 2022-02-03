using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    
    public class FormAImgRepository: RepositoryBase<RmFormaImageDtl>, IFormAImgRepository
    {
        public FormAImgRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public List<RmFormaImageDtl> SaveFormAImageDtl(List<RmFormaImageDtl> rmFormAImageDtls)
        {

            foreach (var imgItem in rmFormAImageDtls)
            {
                try
                {
                    _context.RmFormaImageDtl.Add(imgItem);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            _context.SaveChanges();
            return null;
        }
        public IQueryable<RmFormADtl> GetGridData(RmFormADtl rmFormADtl)
        {
            IQueryable<RmFormADtl> gridData;
            gridData = _context.RmFormADtl
                .AsNoTracking();

            return gridData;
        }

        public async Task<List<RmFormaImageDtl>> GetAllImageByAssetPK(int dtlId)
        {
            var list = await _context.RmFormaImageDtl.Where(s => s.FaiFadPkRefNo == dtlId && s.FaiActiveYn.Value).ToListAsync();
            return list;
        }

        public async Task<int> DectivateAssetImage(int assetImgId)
        {
            var img = await _context.RmFormaImageDtl.FirstOrDefaultAsync(s => s.FaiPkRefNo == assetImgId);
            if(img!=null)
            {
                img.FaiActiveYn = false;
               return await _context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> LastInsertedSRNO(int hederId, string type)
        {
            var id = await _context.RmFormaImageDtl.Where(s => s.FaiFadPkRefNo == hederId && s.FaiActiveYn.Value && s.FaiImageTypeCode == type)
                .MaxAsync(s => s.FaiImageSrno);
            return id.GetValueOrDefault();
        }
    }
}
