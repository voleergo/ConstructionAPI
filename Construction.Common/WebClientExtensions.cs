using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Construction.Common
{
    public static class WebClientExtensions
    {
        public static HttpClient AddTokenToHeader(this HttpClient cl, string token)
        {
            cl.DefaultRequestHeaders.Clear();           
            //int timeoutSec = 90;
            //cl.Timeout = new TimeSpan(0, 0, timeoutSec);
            string contentType = "application/json";
            cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            cl.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));
            var userAgent = "d-fens HttpClient";
            cl.DefaultRequestHeaders.Add("User-Agent", userAgent);
            return cl;
        }
    }
}
