using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Common.Extensions;
using RAMMS.Common.RefNumber;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public class FormW2Repository : RepositoryBase<RmIwFormW2>, IFormW2Repository
    {
        public FormW2Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public RmIwFormW2 SaveFormW2(RmIwFormW2 formW2, bool updateSubmit)
        {
            return formW2;
        }

        public async Task<RmIwFormW2> FindW2ByID(int Id)
        {
            return await _context.RmIwFormW2.Include(x => x.Fw2Fw1RefNoNavigation).Where(x => x.Fw2PkRefNo  == Id && x.Fw2ActiveYn == true && x.Fw2Fw1RefNoNavigation.Fw1PkRefNo == x.Fw2Fw1RefNo).FirstOrDefaultAsync();
        }

        public void SaveImage(IEnumerable<RmIwFormW2Image> image)
        {
            _context.RmIwFormW2Image.AddRange(image);
        }

        public void UpdateImage(RmIwFormW2Image image)
        {
            _context.Set<RmIwFormW2Image>().Attach(image);
            _context.Entry(image).State = EntityState.Modified;
        }

        public Task<List<RmIwFormW2Image>> GetImagelist(int formW2Id)
        {
            return _context.RmIwFormW2Image.Where(x => x.Fw2iFw2RefNo == formW2Id && x.Fw2iActiveYn == true).ToListAsync();
        }

        public Task<RmIwFormW2Image> GetImageById(int imageId)
        {
            return _context.RmIwFormW2Image.Where(x => x.Fw2iPkRefNo == imageId).FirstOrDefaultAsync();
        }

        public async Task<int> GetImageId(int formW2Id, string type)
        {
            int? result = await _context.RmIwFormW2Image.Where(x => x.Fw2iFw2RefNo == formW2Id && x.Fw2iImageTypeCode == type).Select(x => x.Fw2iImageSrno).MaxAsync();
            return result.HasValue ? result.Value : 0;
        }

        public async Task<List<RmIwFormW1>> GetFormW1List()
        {
            return await _context.RmIwFormW1.Where(x => x.Fw1ActiveYn == true).ToListAsync();
        }

        public async Task<RmIwFormW1> GetFormW1ById(int formW1Id)
        {
            return await _context.RmIwFormW1.Where(x => x.Fw1ActiveYn == true && x.Fw1PkRefNo == formW1Id).FirstOrDefaultAsync();
        }

    }
}
