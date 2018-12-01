using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace SimpleHTTP {
	public class Client {
		private Response response;
		private string error;
		private UnityWebRequest www;

		public Client() {
			error = null;
			response = null;
			www = null;
		}

		public IEnumerator Send(Request request) {
			// Employing `using` will ensure that the UnityWebRequest is properly cleaned in case of uncaught exceptions
			using (www = new UnityWebRequest (request.Url (), request.Method ())) {

				www.timeout = request.Timeout ();

				RequestBody body = request.Body ();
				if (body != null) {
					UploadHandler uploader = new UploadHandlerRaw (body.Body ());
					uploader.contentType = body.ContentType ();
					www.uploadHandler = uploader;
				}

				Dictionary<string, string> headers = request.Headers ();
				if (headers != null) {
					foreach (KeyValuePair<string, string> header in headers) {
						www.SetRequestHeader (header.Key, header.Value);
					}
				}

				www.downloadHandler = new DownloadHandlerBuffer ();

				yield return www.Send ();

				if (www.isNetworkError) {
					error = www.error;
				} else {
					response = SimpleHTTP.Response.From (www);
				}
			}
		}

		public void Abort() {
			www.Abort ();
		}

		public bool IsSuccessful() {
			return error == null;
		}

		public string Error() {
			return error;
		}

		public SimpleHTTP.Response Response() {
			return response;
		}
	}
}
