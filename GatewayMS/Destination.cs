// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GatewayMS
{
    /// <summary>
    /// This class contains the implementation of the destination for the gateway service
    /// </summary>
    public class Destination
    {
        static HttpClient client = new HttpClient();
        
        /// <summary>
        /// Gets or sets the destination path
        /// </summary>
        public string Path { get; set; }
        
        public Destination(string uri)
        {
            Path = uri;
        }

        private Destination()
        {
            Path = "/";
        }

        /// <summary>
        /// This method sends the request to the destination path
        /// </summary>
        /// <param name="request">The http request</param>
        public async Task<HttpResponseMessage> SendRequest(HttpRequest request)
        {
            string requestContent;
            using (Stream receiveStream = request.Body)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    requestContent = await readStream.ReadToEndAsync();
                }
            }

            using (var newRequest = new HttpRequestMessage(new HttpMethod(request.Method), CreateDestinationUri(request)))
            {
                foreach (var header in request.Headers.Keys)
                {
                    var value = request.Headers[header].First();
                    newRequest.Headers.TryAddWithoutValidation(header, value);
                }
                
                newRequest.Content = new StringContent(requestContent, Encoding.UTF8, request.ContentType);
                return await client.SendAsync(newRequest);
            }
        }

        private string CreateDestinationUri(HttpRequest request)
        {
            string requestPath = request.Path.ToString();
            string queryString = request.QueryString.ToString();

            string endpoint = "";
            string[] endpointSplit = requestPath.Substring(1).Split('/');

            for (int i = 1; i < endpointSplit.Length; i++)
            {
                endpoint += endpointSplit[i] + '/';
            }

            return Path + endpoint + queryString;
        }
    }
}