using UnityEngine;

namespace SimpleHTTP {
    public class Response {
        private long status;
        private string body;
        private byte[] rawBody;
        private string error;

        public Response(long status, string body, byte[] rawBody) {
            this.status = status;
            this.body = body;
            this.rawBody = rawBody;
            this.error = null;
        }

        public Response(string error) {
            this.error = error;
        }

        public T To<T>() {
            return JsonUtility.FromJson<T>(body);
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
            return status >= 200 && status < 300 && error == null;
        }

        public string Error() {
            return error;
        }

        public override string ToString() {
            return "status: " + status.ToString() + " - response: " + body.ToString();
        }
    }
}
