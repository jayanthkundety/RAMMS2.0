using System;
using System.Collections.Generic;
using System.Text;
using RAMMS.Common;
//using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;
using System.Threading.Tasks;
using RAMS.Repository;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace RAMMS.Business.ServiceProvider
{
    public interface IFormABO
    {
        //int GetFormABO(RmFormAHdr _RmFormAHdr, RmFormADtl _RmFormADtl);
        //List<RmFormaImageDtl> SaveFormAImageBO(List<RmFormaImageDtl> _RmFormaImageDtl);
        //List<RmFormADtl> GetGridBO(RmFormADtl _RmFormADtl);
        //List<RmFormAHdr> GetGridHdrBO(RmFormAHdr _RmFormAHdr);
    }
    public class FormABO : IFormABO
    {
        //private readonly IFormAProvider _FormAProvider;
        public FormABO()
        {
            //_FormAProvider = _FormAProd;
        }
    //    public int GetFormABO(RmFormAHdr _RmFormAHdr, RmFormADtl _RmFormADtl)
    //    {
    //        int _formAHdr = _FormAProvider.SaveFormAHdrProd(_RmFormAHdr);
    //        int _formADtl = _FormAProvider.SaveFormADtlProd(_RmFormADtl);
    //        // throw new NotImplementedException();
    //        return _formAHdr;
    //    }
    //    public List<RmFormaImageDtl> SaveFormAImageBO(List<RmFormaImageDtl> _RmFormaImageDtl)
    //    {
    //        var _formAImage = _FormAProvider.SaveFormAImageProd(_RmFormaImageDtl);
    //        // throw new NotImplementedException();
    //        return _formAImage;
    //    }
    //    public List<RmFormADtl> GetGridBO(RmFormADtl _RmFormADtl)
    //    {
    //        var _GetGrid = _FormAProvider.GetGridProd(_RmFormADtl);

    //        return _GetGrid;
    //    }
    //    public List<RmFormAHdr> GetGridHdrBO(RmFormAHdr _RmFormAHdr)
    //    {
    //        var _GetGrid = _FormAProvider.GetGridHdrProd(_RmFormAHdr);

    //        return _GetGrid.ToList();
    //    }
    }

}
