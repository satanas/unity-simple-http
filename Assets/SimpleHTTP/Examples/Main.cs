using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleHTTP;
using UnityEngine.UI;

public class Main : MonoBehaviour {

	private Text errorText;
	private Text successText;
	private string validURL = "https://jsonplaceholder.typicode.com/posts/";
	private string invalidURL = "https://jsonplaceholder.net/articles/";

	void Start () {
		errorText = GameObject.Find ("ErrorText").GetComponent<Text> ();
		successText = GameObject.Find ("SuccessText").GetComponent<Text> ();
	}

	IEnumerator Get(string baseUrl, int postId) {
		Request request = new Request (baseUrl + postId.ToString());

		Client http = new Client ();
		yield return http.Send (request);
		ProcessResult (http);
	}

	IEnumerator Post() {
		Post post = new Post ("Test", "This is a test", 1);

		Request request = new Request (validURL)
			.AddHeader ("Test-Header", "test")
			.Post (RequestBody.From<Post> (post));

		Client http = new Client ();
		yield return http.Send (request);
		ProcessResult (http);
	}

	IEnumerator PostWithFormData() {
		FormData formData = new FormData ()
			.AddField ("userId", "1")
			.AddField ("body", "Hey, another test")
			.AddField ("title", "Did I say test?");

		Request request = new Request (validURL)
			.Post (RequestBody.From(formData));

		Client http = new Client ();
		yield return http.Send (request);
		ProcessResult (http);
	}

	IEnumerator Put() {
		Post post = new Post ("Another Test", "This is another test", 1);

		Request request = new Request (validURL + "1")
			.Put (RequestBody.From<Post> (post));

		Client http = new Client ();
		yield return http.Send (request);
		ProcessResult (http);
	}

	IEnumerator Delete() {
		Request request = new Request (validURL + "1")
			.Delete ();

		Client http = new Client ();
		yield return http.Send (request);
		ProcessResult (http);
	}

	IEnumerator ClearOutput() {
		yield return new WaitForSeconds (2f);
		errorText.text = "";
		successText.text = "";
	}

	void ProcessResult(Client http) {
		if (http.IsSuccessful ()) {
			Response resp = http.Response ();
			successText.text = "status: " + resp.Status().ToString() + "\nbody: " + resp.Body();
		} else {
			errorText.text = "error: " + http.Error();
		}
		StopCoroutine (ClearOutput ());
		StartCoroutine (ClearOutput ());
	}

	public void GetPost() {
		StartCoroutine (Get (validURL, 1));
	}

	public void CreatePost() {
		StartCoroutine (Post ());
	}

	public void UpdatePost() {
		StartCoroutine (Put ());
	}

	public void DeletePost() {
		StartCoroutine (Delete ());
	}

	public void GetNonExistentPost() {
		StartCoroutine (Get (validURL, 999));
	}

	public void GetInvalidUrl() {
		StartCoroutine (Get (invalidURL, 1));
	}

	public void CreatePostWithFormData() {
		StartCoroutine (PostWithFormData ());
	}
}
