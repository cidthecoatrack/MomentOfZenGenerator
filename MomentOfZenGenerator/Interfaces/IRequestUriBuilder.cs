using System;

namespace MomentOfZenGenerator.Interfaces
{
    public interface IRequestUriBuilder
    {
        String BuildRequestUrl(String search);
    }
}