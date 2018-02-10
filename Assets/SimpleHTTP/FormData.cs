using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace SimpleHTTP {
	public class FormData {

		private List<IMultipartFormSection> formData;

		public FormData() {
			formData = new List<IMultipartFormSection> ();
		}

		public FormData AddField(string name, string value) {
			formData.Add (new MultipartFormDataSection (name, value));
			return this;
		}

		public FormData AddFile(string name, byte[] data) {
			formData.Add (new MultipartFormFileSection (name, data));
			return this;
		}

		public List<IMultipartFormSection> MultipartForm() {
			return formData;
		}
	}
}
