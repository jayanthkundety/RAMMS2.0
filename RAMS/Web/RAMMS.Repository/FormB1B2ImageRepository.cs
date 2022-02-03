using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;

namespace RAMMS.Repository
{
    public class FormB1B2ImageRepository : RepositoryBase<RmFormB1b2BrInsImage>, IFormB1B2ImgRepository
    {
        public FormB1B2ImageRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public List<RmFormB1b2BrInsImage> SaveImageDtl(List<RmFormB1b2BrInsImage> _RmFormB1b2BrInsImages)
        {

            foreach (var imgItem in _RmFormB1b2BrInsImages)
            {
                try
                {
                    _context.RmFormB1b2BrInsImage.Add(imgItem);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            _context.SaveChanges();
            return null;
        }

        public async Task<List<RmFormB1b2BrInsImage>> GetAllImageByHeaderId(int dtlId)
        {
            var list = await _context.RmFormB1b2BrInsImage.Where(s => s.FbriFbrihPkRefNo == dtlId && s.FbriActiveYn == true).ToListAsync();
            return list;
        }

        public async Task<int> DectivateAssetImage(int assetimgId)
        {
            var img = await _context.RmFormB1b2BrInsImage.FirstOrDefaultAsync(s => s.FbriPkRefNo == assetimgId);
            if (img != null)
            {
                img.FbriActiveYn
                    = false;
                _context.Update(img);
                return await _context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> LastInsertedSRNO(int hederid, string type)
        {
            var id = await _context.RmFormB1b2BrInsImage.Where(s => s.FbriFbrihPkRefNo == hederid && s.FbriActiveYn == true && s.FbriImageTypeCode == type)
                .MaxAsync(s => s.FbriImageSrno);
            return id.GetValueOrDefault();
        }

        public bool CheckImageExistforAllType(string[] lookup, int headerid)
        {
            var s = lookup.Where(s => !_context.RmFormB1b2BrInsImage.Any(i => i.FbriActiveYn == true && i.FbriFbrihPkRefNo == headerid && i.FbriImageTypeCode == s));
            if (s != null && s.Count() > 0)
            {
                return false;
            }
            return true;
        }
    }
}
