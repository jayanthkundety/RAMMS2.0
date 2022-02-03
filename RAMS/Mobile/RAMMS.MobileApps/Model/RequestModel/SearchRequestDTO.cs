using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class SearchRequestDTO
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
        public Dictionary<string, string> filter { get; set; }
    }
    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }
    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }
    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
        public SortDirection SortOrder
        {
            get
            {
                return dir == "asc" ? SortDirection.Asc : SortDirection.Desc;
            }
        }
    }
    public enum SortDirection
    {
        Asc,
        Desc
    }
    public class GridWrapper<T>
    {
        public T data { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public int draw { get; set; }
    }
}