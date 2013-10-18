using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using MomentOfZenGenerator.Interfaces;

namespace MomentOfZenGenerator
{
    public class ResponseProvider : IResponseProvider
    {
        public String GetResponseContent(String uri)
        {
            var request = WebRequest.Create(uri);
            var response = request.GetResponse();

            var httpResponse = response as HttpWebResponse;
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new WebException(httpResponse.StatusCode.ToString());

            var content = String.Empty;

            using (var reader = new StreamReader(response.GetResponseStream()))
                content = reader.ReadToEnd();

            return content;
        }

        public T GetJsonResponseContent<T>(String uri)
        {
            var frequencyContent = GetResponseContent(uri);

            var javaScriptSerializer = new JavaScriptSerializer();
            var response = javaScriptSerializer.Deserialize(frequencyContent, typeof(T));

            return (T)response;
        }
    }
}