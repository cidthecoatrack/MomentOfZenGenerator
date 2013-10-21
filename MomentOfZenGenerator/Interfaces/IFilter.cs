using System;
using System.Collections.Generic;

namespace MomentOfZenGenerator.Interfaces
{
    public interface IFilter
    {
        IEnumerable<String> GetVideoUrlsLessThanOneMinuteLong(String searchWord);
    }
}