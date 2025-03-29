using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace SimpleHTTP {
	
	public class Request {
		private string url;
		private string method;
		private Dictionary<string, string> headers;
		private RequestBody body;
		private Response response;
		private int timeout;
		private UnityWebRequest www;

		public Request(string url) {
			this.method = "GET";
			this.url = url;
			this.body = null;
			this.response = null;
			this.timeout = 0;
			this.headers = new Dictionary<string, string> ();
		}

		public Request Url(string url) {
			this.url = url;
			return this;
		}

		public Request Method(string method, RequestBody body) {
			if (method == null) throw new NullReferenceException ("method cannot be null");
			if (method.Length == 0)	throw new InvalidOperationException ("method cannot be empty");

			this.method = method;
			this.body = body;
			return this;
		}

		public Request AddHeader(string name, string value) {
			this.headers.Add (name, value);
			return this;
		}

		public Request RemoveHeader(string name) {
			this.headers.Remove (name);
			return this;
		}

		public Request Timeout(int timeout) {
			this.timeout = timeout;
			return this;
		}

		public int Timeout() {
			return timeout;
		}

		public Request Get() {
			Method (UnityWebRequest.kHttpVerbGET, null);
			return this;
		}

		public Request Post(RequestBody body) {
			Method (UnityWebRequest.kHttpVerbPOST, body);
			return this;
		}

		public Request Put(RequestBody body) {
			Method (UnityWebRequest.kHttpVerbPUT, body);
			return this;
		}

		public Request Delete() {
			Method (UnityWebRequest.kHttpVerbDELETE, null);
			return this;
		}

		public string Url() {
			return url;
		}

		public string Method() {
			return method;
		}

		public RequestBody Body() {
			return body;
		}

		public Dictionary<string, string> Headers() {
			return headers;
		}

		public Response Response() {
			return response;
		}

		public void Abort() {
			if (www != null) {
				www.Abort ();
			}
		}

		public IEnumerator Send() {
			// Employing `using` will ensure that the UnityWebRequest is properly cleaned in case of uncaught exceptions
			using (www = new UnityWebRequest (Url (), Method ())) {

				www.timeout = timeout;

				if (body != null) {
					UploadHandler uploader = new UploadHandlerRaw (body.Body ());
					uploader.contentType = body.ContentType ();
					www.uploadHandler = uploader;
				}
					
				if (headers != null) {
					foreach (KeyValuePair<string, string> header in headers) {
						www.SetRequestHeader (header.Key, header.Value);
					}
				}

				www.downloadHandler = new DownloadHandlerBuffer ();

				yield return www.SendWebRequest();

				if (www.result == UnityWebRequest.Result.ConnectionError) {
					response = new Response(www.error);
				} else {
					response = new Response(www.responseCode, www.downloadHandler.text, www.downloadHandler.data);
				}
			}
		}
	}
}