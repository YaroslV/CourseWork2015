using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace course.ControllerCustomizations
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path):base(path)
        { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ?
                headers.ContentDisposition.FileName : "NoName";
            string endName = DateTime.Now.Date + name.Replace("\"" , string.Empty);

            return endName;
        }
    }
}