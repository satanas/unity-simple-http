using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

namespace SimpleHTTP {
	
	public class RequestBody {
		private string contentType;
		private byte[] body;

		RequestBody(string contentType, byte[] body) {
			this.contentType = contentType;
			this.body = body;
		}

		public static RequestBody From(string value) {
			byte[] bodyRaw = Encoding.UTF8.GetBytes(value.ToCharArray());
			return new RequestBody ("application/x-www-form-urlencoded", bodyRaw);
		}

		public static RequestBody From<T>(T value) {
			byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(value).ToCharArray());
			return new RequestBody ("application/json", bodyRaw);
		}
			
		public static RequestBody From(FormData form) {
			// https://answers.unity.com/questions/1354080/unitywebrequestpost-and-multipartform-data-not-for.html

			List<IMultipartFormSection> formData = form.MultipartForm ();

			// generate a boundary then convert the form to byte[]
			byte[] boundary = UnityWebRequest.GenerateBoundary();
			byte[] formSections = UnityWebRequest.SerializeFormSections(formData, boundary);
			// my termination string consisting of CRLF--{boundary}--
			byte[] terminate = Encoding.UTF8.GetBytes(String.Concat("\r\n--", Encoding.UTF8.GetString(boundary), "--"));
			// Make complete body from the two byte arrays
			byte[] bodyRaw = new byte[formSections.Length + terminate.Length];
			Buffer.BlockCopy(formSections, 0, bodyRaw, 0, formSections.Length);
			Buffer.BlockCopy(terminate, 0, bodyRaw, formSections.Length, terminate.Length);
			// Set the content type
			string contentType = String.Concat("multipart/form-data; boundary=", Encoding.UTF8.GetString(boundary));
			return new RequestBody (contentType, bodyRaw);
		}

		[System.Obsolete("WWWForm is obsolete. Use List<IMultipartFormSection> instead")]
		public static RequestBody From(WWWForm formData) {
			return new RequestBody ("application/x-www-form-urlencoded", formData.data);
		}

		public string ContentType() {
			return contentType;
		}

		public byte[] Body() {
			return body;
		}
	}
}
