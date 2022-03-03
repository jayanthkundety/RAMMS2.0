using System;
using System.Collections.Generic;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class DivisionRequestDTO
    {
        [MapTo("DivPkRefNo")] public int PkRefNo { get; set; }
        [MapTo("DivCode")] public string Code { get; set; }
        [MapTo("DivName")] public string Name { get; set; }
        [MapTo("DivIsActive")] public bool Isactive { get; set; }

       public List<Division> Divisions { get; set; }

        public List<ServiceProvider> ServiceProviders { get; set; }
    }

    public struct Division
    {
        public string Code;
        public string Name;
        public string Adress1;
        public string Adress2;
        public string Adress3;
        public string Phone;
        public string Fax;
        public string ZipCode;
    }

    public struct ServiceProvider
    {
        public string Code;
        public string Name;
        public string Adress1;
        public string Adress2;
        public string Adress3;
        public string Phone;
        public string Fax;
        public string ZipCode;
    }
}
