using System.Collections.Generic;
using UnityEngine;

namespace SimpleHTTP.Interfaces {
    public interface IHttpResponse {
        int Status { get; }
        string Content { get; }
        byte[] RawBody { get; }
        string Error { get; }
        Dictionary<string, string> Headers { get; }
        
        public virtual T Data<T>() {
            return JsonUtility.FromJson<T>(Content);
        }
    }
}
