using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SampleProject.Tools.Http;

public class HttpClientFactory : IDisposable
{
    private static readonly Dictionary<string, HttpClient> Instances =
        new Dictionary<string, HttpClient>();

    public static HttpClient Create()
    {
        var client = HttpClientFactory.Create();

        client.BaseAddress = new Uri("http://localhost:5000");

        return client;
    }

    public void Dispose()
    {
    }
}