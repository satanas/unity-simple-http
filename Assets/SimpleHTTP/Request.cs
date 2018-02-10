using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

namespace SimpleHTTP {
	
	public class Request {
		private string url;
		private string method;
		private Dictionary<string, string> headers;
		private RequestBody body;
		private int timeout;

		public Request(string url) {
			this.method = "GET";
			this.url = url;
			this.body = null;
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

		public int Timeout() {
			return timeout;
		}
	}
}