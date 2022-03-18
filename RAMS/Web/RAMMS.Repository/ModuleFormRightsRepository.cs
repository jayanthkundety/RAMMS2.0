using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace RAMMS.Repository
{
    public class ModuleFormRightsRepository : RepositoryBase<RmModuleRightByForm>, IModuleFormRightsRepository
    {

        public ModuleFormRightsRepository(RAMMSContext context) : base(context) { _context = context ?? throw new ArgumentNullException(nameof(context)); }


        public   IList<RmModuleRightByForm> GetIWRightsAll(int UserID)
        {
            //select RM_Group_User.RmGroupsUgPkId from RM_Group_User where RmUsersUsrPkId =100000015
            //select* from RM_Module_Right_By_Form where MFR_Ug_PkId in (7)

            return (from s in _context.RmGroupUser where s.RmUsersUsrPkId == (int)UserID
             join  uug in _context.RmModuleRightByForm on s.RmGroupsUgPkId equals  uug.MfrUgPkId
              select uug).ToListAsync().Result;

            //var query =  (from s in _context.RmModuleRightByForm where s.MfrActiveYn == true
            //                   let uug = _context.RmGroupUser.FirstOrDefault(u => u.RmUsersUsrPkId == UserID)
            //                   join u in _context.RmModuleRightByForm on uug.UsrGpkid equals u.MfrUgPkId 
            //                   join m in _context.RmModule on s.MfrModPkId equals m.ModPkId
            //                   join g in _context.RmGroup on s.MfrUgPkId equals g.UgPkId
            //                   join f in _context.RmModuleForms on s.MfrMfPkId equals f.MfPkId
            //                   select s).ToList();

            //return query;



        }
    }
}
