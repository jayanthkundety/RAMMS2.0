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

    public class FormJImgRepository : RepositoryBase<RmFormjImageDtl>, IFormJImgRepository
    {
        public FormJImgRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public List<RmFormjImageDtl> SaveFormAImageDtl(List<RmFormjImageDtl> rmFormJImageDtls)
        {

            foreach (var imgItem in rmFormJImageDtls)
            {
                try
                {
                    _context.RmFormjImageDtl.Add(imgItem);
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

        public async Task<List<RmFormjImageDtl>> GetAllImageByAssetPK(int dtlId)
        {
            var list = await _context.RmFormjImageDtl.Where(s => s.FjiFjdPkRefNo == dtlId && s.FjiActiveYn.Value).ToListAsync();
            return list;
        }

        public async Task<int> DectivateAssetImage(int assetImgId)
        {
            var img = await _context.RmFormjImageDtl.FirstOrDefaultAsync(s => s.FjiPkRefNo == assetImgId);
            if (img != null)
            {
                img.FjiActiveYn = false;
                 _context.Update(img);
                return await _context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> LastInsertedSRNO(int hederId, string type)
        {
            var id = await _context.RmFormjImageDtl.Where(s => s.FjiFjdPkRefNo == hederId && s.FjiActiveYn.Value && s.FjiImageTypeCode == type)
                .MaxAsync(s => s.FjiImageSrno);
            return id.GetValueOrDefault();
        }
    }
}
