using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public class FormW1Repository : RepositoryBase<RmIwFormW1>, IFormW1Repository
    {
        public FormW1Repository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
     
        public int SaveFormW1(RmIwFormW1 FormW1)
        {
            try
            {
                _context.Entry<RmIwFormW1>(FormW1).State = FormW1.Fw1PkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return FormW1.Fw1PkRefNo;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public async Task<RmIwFormW1> FindFormW1ByID(int Id)
        {
            return await _context.RmIwFormW1.Where(x => x.Fw1PkRefNo == Id && x.Fw1ActiveYn == true).FirstOrDefaultAsync();
        }

        public Task<List<RmIwformImage>> GetImagelist(string IwRefNo)
        {
            return _context.RmIwformImage.Where(x => x.FiwiFw1IwRefNo == IwRefNo && x.FiwiActiveYn == true).ToListAsync();
        }

        public async Task<int> GetImageId(string iwRefNo, string type)
        {
            int? result = await _context.RmIwformImage.Where(x => x.FiwiFw1IwRefNo == iwRefNo && x.FiwiImageTypeCode == type).Select(x => x.FiwiImageSrno).MaxAsync();
            return result.HasValue ? result.Value : 0;
        }

        public async Task<int> GetImageIdByW1Id(int formW1Id, string type)
        {
            int? result = await _context.RmIwformImage.Where(x => x.FiwiFw1PkRefNo == formW1Id && x.FiwiImageTypeCode == type).Select(x => x.FiwiImageSrno).MaxAsync();
            return result.HasValue ? result.Value : 0;
        }

        public void SaveImage(IEnumerable<RmIwformImage> image)
        {
            _context.RmIwformImage.AddRange(image);
        }

        //public async Task<RmDivRmuSecMaster> GetDDl()
        //{
        //  //  return await _context.RoadmasterRepository.FindAsync(x => x.RdmActiveYn == true, x => new CSelectListItem() { Text = x.RdmRdCode + "-" + x.RdmRdName, Value = x.RdmPkRefNo.ToString(), CValue = x.RdmRmuCode, Item1 = x.RdmRdName, PKId = x.RdmPkRefNo, Code = x.RdmRdCode, Item2 = x.RdmSecCode.ToString(), Item3 = (x.RdmLengthPaved + x.RdmLengthUnpaved).ToString(), FromKm = (int)x.RdmFrmCh, FromM = x.RdmFrmChDeci.ToString(), ToKm = (int)x.RdmToCh, ToM = x.RdmToChDeci.ToString() });
        //  //  return await _context.RmDivRmuSecMaster.Where(x => x.RdsmActiveYn == true).Select(x => new RAMMS.Business.ServiceProvider.CSelectListItem() { Text = x. + "-" + x.RdmRdName, Value = x.RdmPkRefNo.ToString(), CValue = x.RdmRmuCode, Item1 = x.RdmRdName, PKId = x.RdmPkRefNo, Code = x.RdmRdCode, Item2 = x.RdmSecCode.ToString(), Item3 = (x.RdmLengthPaved + x.RdmLengthUnpaved).ToString(), FromKm = (int)x.RdmFrmCh, FromM = x.RdmFrmChDeci.ToString(), ToKm = (int)x.RdmToCh, ToM = x.RdmToChDeci.ToString() });
        //}


        public Task<RmIwformImage> GetImageById(int imageId)
        {
            return _context.RmIwformImage.Where(x => x.FiwiPkRefNo == imageId).FirstOrDefaultAsync();
        }

        public void UpdateImage(RmIwformImage image)
        {
            _context.Set<RmIwformImage>().Attach(image);
            _context.Entry(image).State = EntityState.Modified;
        }

    }
}
