using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.MiniAspNet
{
    public static class Extensions
    {
        public static T Get<T>(this IFeatureCollection features) =>
            features.TryGetValue(typeof(T), out var value) ? (T)value : default(T);

        public static IFeatureCollection Set<T>(this IFeatureCollection features, T feature)
        {
            features[typeof(T)] = feature;
            return features;
        }

        public static Task WriteAsync(this HttpResponse response, string contents)
        {
            var buffer = Encoding.UTF8.GetBytes(contents);
            return response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
