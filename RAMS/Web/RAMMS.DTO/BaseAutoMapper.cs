using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RAMMS.DTO.Profiles;

namespace RAMMS.DTO
{
    public static class BaseAutoMapper
    {
        public static void InitAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(FormC1C2Profile),
                typeof(FormFCProfile),
                typeof(FormFDProfile)
                );
        }
    }
}
