using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlog.DataAccess.Other
{
    public abstract class Filtr
    {
        public string Name { get; set; }
        public FilterType Type { get; set; }
    }

    public enum FilterType
    {
        SortFiltr,
        SearchFiltr
    }

    public class SortFiltr: Filtr
    {
        public SortType SortType { get; set; }
    }
    public enum SortType
    {
        DateINC,
        DateDEC,
        RetingBest,
        RetingBad,
        Popular
    }

    public class SearchFiltr: Filtr
    {
    }
}
