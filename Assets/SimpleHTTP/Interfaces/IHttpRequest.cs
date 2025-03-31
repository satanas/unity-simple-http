using System.Collections;

namespace SimpleHTTP.Interfaces {
    public interface IHttpRequest {
        IHttpRequest SetMethod(string method);
        IHttpRequest AddHeader(string name, string value);
        IHttpRequest AddQueryParameter(string key, string value);
        IHttpRequest SetTimeout(int seconds);
        IHttpRequest SetStringBody(string body);
        IHttpRequest SetJsonBody<T>(T body);
        IHttpRequest AddFile(string name, byte[] data, string fileName, string contentType);
        IHttpResponse GetResponse();
        IEnumerator Send();
        void Abort();
    }
}
