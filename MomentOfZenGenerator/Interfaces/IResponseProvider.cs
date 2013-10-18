using System;

namespace MomentOfZenGenerator.Interfaces
{
    public interface IResponseProvider
    {
        String GetResponseContent(String uri);
        T GetJsonResponseContent<T>(String uri);
    }
}