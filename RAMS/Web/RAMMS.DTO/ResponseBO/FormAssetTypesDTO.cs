using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    /// <summary>
    /// <returns>IList<Dictionary<string, List<Dictionary<string, string>>>> --> Collection of key values, Key --> DdlTypeCode, values --> Collection of DdlPkRefNo, DdlTypeDesc and DdlTypeValue </returns>
    /// </summary>
    public class FormAssetTypesDTO : Dictionary<string, List<Dictionary<string, string>>>
    {
        
    }    
}
