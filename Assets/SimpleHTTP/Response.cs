using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace SimpleHTTP {
	public class Response {
		private long status;
		private string body;
		private byte[] rawBody;

		public Response(long status, string body, byte[] rawBody) {
			this.status = status;
			this.body = body;
			this.rawBody = rawBody;
		}

		public T To<T>() {
			return JsonUtility.FromJson<T> (body);
		}

		public long Status() {
			return status;
		}

		public string Body() {
			return body;
		}

		public byte[] RawBody() {
			return rawBody;
		}

		public bool IsOK() {
			return status >= 200 && status < 300;
		}

		public string ToString() {
			return "status: " + status.ToString () + " - response: " + body.ToString ();
		}

		public static Response From(UnityWebRequest www) {
			return new Response (www.responseCode, www.downloadHandler.text, www.downloadHandler.data);
		}
	}
}
