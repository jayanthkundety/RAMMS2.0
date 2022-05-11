// Decompiled with JetBrains decompiler
// Type: RAMMS.Business.ServiceProvider.Interfaces.ISectionService
// Assembly: RAMMS.Business.ServiceProvider, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2E3E52CD-370B-4DC8-B38A-DF785010DD3C
// Assembly location: F:\Mohan\Avows\RAMS_WEB_PRD\RAMMS.Business.ServiceProvider.dll

using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
  public interface ISectionService
  {
    long LastSectionInsertedNo();

    Task<SectionRequestDTO> GetSectionById(int id);

    Task<int> SaveSection(SectionRequestDTO model);

    Task<bool> RemoveSection(int id);

    Task<PagingResult<SectionRequestDTO>> GetSectionList(
      FilteredPagingDefinition<SectionRequestDTO> filterOptions);

    List<SelectListItem> GetList(string div, string rmu);
  }
}
