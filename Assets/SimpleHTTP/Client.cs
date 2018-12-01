using System.Collections;
using System.Collections.Generic;
using System;

namespace SimpleHTTP {
	public class Client {
		private Response response;
		private string error;
		private Request request;

		public Client() {
			error = null;
			response = null;
		}

		public IEnumerator Send(Request www) {
			this.request = www;

			yield return request.Send ();

			response = request.Response ();
			error = response.Error (); // Backward compatibility
		}

		public void Abort() {
			if (request != null) {
				request.Abort ();
			}
		}

		[Obsolete("IsSuccessful() is deprecated. Please, use Response() to get the response and check response.IsOK()")]
		public bool IsSuccessful() {
			return (response != null && response.IsOK());
		}

		[Obsolete("Error() is deprecated. Please, use Response() to get the response and check response.Error()")]
		public string Error() {
			return error;
		}

		public Response Response() {
			return response;
		}
	}
}
