using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApp.MiniAspNet
{
    public interface IFeatureCollection : IDictionary<Type, object>
    {

    }

    public class FeatureCollection : Dictionary<Type, object>, IFeatureCollection
    {

    }

    public interface IHttpRequestFeature
    {
        Uri Url { get; }

        NameValueCollection Headers { get; }

        Stream Body { get; }
    }

    public interface IHttpResponseFeature
    {
        int StatusCode { get; set; }

        NameValueCollection Headers { get; }

        Stream Body { get; }
    }

    public class HttpListenerFeature : IHttpRequestFeature, IHttpResponseFeature
    {
        private readonly HttpListenerContext _context;

        public HttpListenerFeature(HttpListenerContext context)
        {
            _context = context;
        }

        NameValueCollection IHttpRequestFeature.Headers => _context.Request.Headers;

        Stream IHttpRequestFeature.Body => _context.Request.InputStream;

        Uri IHttpRequestFeature.Url => _context.Request.Url;



        int IHttpResponseFeature.StatusCode
        {
            get => _context.Response.StatusCode;
            set => _context.Response.StatusCode = value;
        }

        NameValueCollection IHttpResponseFeature.Headers => _context.Response.Headers;

        Stream IHttpResponseFeature.Body => _context.Response.OutputStream;

    }
}
