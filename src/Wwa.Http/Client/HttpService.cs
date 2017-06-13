// Copyright 2017 (c) [Denis Da Silva]. All rights reserved.
// See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Wwa.Core.Collections;
using Wwa.Core.Http;
using Wwa.Http.Extensions;

namespace Wwa.Http.Client
{
    public class HttpService : IHttpService
	{
		#region Properties

		public string BaseUrl { get; set; }

		public IDictionary<string, string> DefaultCookies { get; } = new WeakDictionary<string, string>();

        #endregion

        #region Public Methods

        async public Task<StringBody> Get(string url)
		{
			var content = string.Empty;
			var bodyResponse = new StringBody();
			
			using (var client = CreateClient(url))
            {
                var response = await client.GetAsync(url);

                if (response.Content != null)
                {
                    content = await response.Content.ReadAsStringAsync();
                }

				Trace(url, content, string.Empty);

				if (!response.IsSuccessStatusCode)
                    throw new HttpException(response.ReasonPhrase, response.StatusCode, content);

				var cookies = response.GetCookies();
				bodyResponse = new StringBody(content, cookies);
			}
			
            return bodyResponse;
		}

		async public Task<StringBody> Post(string url, string json)
		{
			const string JSON_MEDIA_TYPE = "application/json";

			var content = string.Empty;
			var bodyResponse = new StringBody();

			using (var client = CreateClient(url))
			{
				client.AcceptJson();

				var body = new StringContent(json);
				body.Headers.ContentType = new MediaTypeHeaderValue(JSON_MEDIA_TYPE);
				var response = await client.PostAsync(url, body);

				if (response.Content != null)
				{
					content = await response.Content.ReadAsStringAsync();
				}

				Trace(url, content, json);

				if (!response.IsSuccessStatusCode)
					throw new HttpException(response.ReasonPhrase, response.StatusCode, content);

				var cookies = response.GetCookies();
				bodyResponse = new StringBody(content, cookies);
			}

			return bodyResponse;
		}

		async public Task<StringBody> Post(string url, IDictionary<string, string> form)
		{
			if (form == null)
				form = new Dictionary<string, string>();

			var bodyResponse = new StringBody();

			using (var client = CreateClient(url))
			{
				client.AcceptJson();

				var body = new FormUrlEncodedContent(form);
				var response = await client.PostAsync(url, body);

				string content = await response?.Content?.ReadAsStringAsync();
				
				Trace(url, content, form.ToString());

				if (!response.IsSuccessStatusCode)
					throw new HttpException(response.ReasonPhrase, response.StatusCode, content);

				var cookies = response.GetCookies();
				bodyResponse = new StringBody(content, cookies);
			}

			return bodyResponse;
		}

		async public Task<StringBody> Put(string url, string json)
        {
            const string JSON_MEDIA_TYPE = "application/json";

            var content = string.Empty;
            var bodyResponse = new StringBody();

            using (var client = CreateClient(url))
            {
				client.AcceptJson();

				var body = new StringContent(json);
                body.Headers.ContentType = new MediaTypeHeaderValue(JSON_MEDIA_TYPE);
                var response = await client.PutAsync(url, body);

                if (response.Content != null)
                {
                    content = await response.Content.ReadAsStringAsync();
                }

                Trace(url, content, json);

                if (!response.IsSuccessStatusCode)
                    throw new HttpException(response.ReasonPhrase, response.StatusCode, content);

                var cookies = response.GetCookies();
                bodyResponse = new StringBody(content, cookies);
            }

            return bodyResponse;
        }

        async public Task<StringBody> Delete(string url)
        {
            var content = string.Empty;
            var bodyResponse = new StringBody();

            using (var client = CreateClient(url))
            {
                var response = await client.DeleteAsync(url);

                if (response.Content != null)
                {
                    content = await response.Content.ReadAsStringAsync();
                }

                Trace(url, content, string.Empty);

                if (!response.IsSuccessStatusCode)
                    throw new HttpException(response.ReasonPhrase, response.StatusCode, content);

                var cookies = response.GetCookies();
                bodyResponse = new StringBody(content, cookies);
            }

            return bodyResponse;
        }

        #endregion

        #region Private Methods

        HttpClient CreateClient(string url)
		{
			Uri uri = null;

			if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
			{
				if (string.IsNullOrWhiteSpace(BaseUrl))
					throw new ArgumentNullException("BaseUrl");

				uri = new Uri(BaseUrl);
			}
			
			var cookieContainer = new CookieContainer();
			
			if (DefaultCookies?.Any() ?? false)
			{
				foreach (var cookie in DefaultCookies)
				{
					cookieContainer.Add(uri, new Cookie(cookie.Key, cookie.Value));
				}
			}

			var handler = new HttpClientHandler
			{
				CookieContainer = cookieContainer
			};

			var client = new HttpClient(handler)
			{
				BaseAddress = uri
			};
			
			return client;
		}

		void Trace(string url, string response, string request, [CallerMemberName] string method = null)
		{
#if DEBUG
			if (!Debugger.IsAttached)
				return;

			string line = new string('-', 80);

			Debug.WriteLine(line);
			Debug.WriteLine("DATE: {0}", DateTime.Now);
			Debug.WriteLine("URL: {0}", url);
			Debug.WriteLine("HTTP METHOD: {0}", method?.ToUpper());
			Debug.WriteLine("REQUEST COOKIES: {0}", DefaultCookies);
			Debug.WriteLine(string.Empty);

			if (!string.IsNullOrWhiteSpace(request))
			{
				Debug.WriteLine("REQUEST:");
				Debug.WriteLine(request);
				Debug.WriteLine(line);
			}

			Debug.WriteLine("RESPONSE:");
			Debug.WriteLine(response);
			Debug.WriteLine(line);
#endif
		}

		#endregion
	}
}
