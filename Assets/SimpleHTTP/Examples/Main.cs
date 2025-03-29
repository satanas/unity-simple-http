using System.Collections;
using SimpleHTTP;
using TMPro;
using UnityEngine;

public class Main : MonoBehaviour {

	public TMP_Text errorText;
	public TMP_Text successText;

	private const string ValidURL = "https://jsonplaceholder.typicode.com/posts/";
	private const string InvalidURL = "https://jsonplaceholder.net/articles/";

	void Start () {
		errorText.text = "";
		successText.text = "";
	}

	IEnumerator Get(string baseUrl, int postId) {
		Request request = new Request (baseUrl + postId);

		Client http = new Client ();
		yield return http.Send (request);
		ProcessResult (http);
	}

	IEnumerator Post() {
		UserProfile user = new UserProfile (1, "admin", "value01", "value02");

		Request request = new Request (ValidURL)
			.AddHeader ("Authorization", "myID")
			.AddHeader ("Content-Type", "application/json")
			.AddHeader ("X-Api-Version", "1.0.0")
			.Post (RequestBody.From<UserProfile> (user));

		Client http = new Client ();
		yield return http.Send (request);
		ProcessResult (http);
	}

	IEnumerator PostWithFormData() {
		FormData formData = new FormData ()
			.AddField ("userId", "1")
			.AddField ("body", "Hey, another test")
			.AddField ("title", "Did I say test?");

		Request request = new Request (ValidURL)
			.Post (RequestBody.From(formData));

		Client http = new Client ();
		yield return http.Send (request);
		ProcessResult (http);
	}

	IEnumerator Put() {
		BlogPost post = new BlogPost ("Another Test", "This is another test", 1);

		Request request = new Request (ValidURL + "1")
			.Put (RequestBody.From<BlogPost> (post));

		Client http = new Client ();
		yield return http.Send (request);
		ProcessResult (http);
	}

	IEnumerator Delete() {
		Request request = new Request (ValidURL + "1")
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
		Response resp = http.Response ();
		if (resp.IsOK()) {
			successText.text = "status: " + resp.Status() + "\nbody: " + resp.Body();
		} else {
			errorText.text = "error: " + resp.Error();
		}
		StopCoroutine (ClearOutput ());
		StartCoroutine (ClearOutput ());
	}

	public void GetPost() {
		StartCoroutine (Get (ValidURL, 1));
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
		StartCoroutine (Get (ValidURL, 999));
	}

	public void GetInvalidUrl() {
		StartCoroutine (Get (InvalidURL, 1));
	}

	public void CreatePostWithFormData() {
		StartCoroutine (PostWithFormData ());
	}
}
