using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using SimpleHTTP;
using System.Text;
using System.Linq;
using UnityEngine.Networking;
using NSubstitute;

public class ResponseTest {

	private byte[] rawBody = Encoding.UTF8.GetBytes ("Foo Bar");
	private string body = "Hello World";

	// Subject under test
	private Response response;

	[SetUp]
	public void SetUp() {
		
	}

	[TearDown]
	public void TearDown() {
	}

	[Test]
	public void TestConstructorAndGetters() {
		// Execution
		response = new Response (200, body, rawBody);

		// Expected
		Assert.That (response.Status () == 200);
		Assert.That (response.Body () == body);
		Assert.That (response.RawBody ().SequenceEqual(rawBody));
	}

	[Test]
	public void TestIsOkSuccessfulRequest() {
		// Conditions
		int statusCode = 200;

		// Execution
		response = new Response (statusCode, body, rawBody);

		// Expected
		Assert.IsTrue (response.IsOK());
	}

	[Test]
	public void TestIsOkClientError() {
		// Conditions
		int statusCode = 400;

		// Execution
		response = new Response (statusCode, body, rawBody);

		// Expected
		Assert.IsFalse(response.IsOK());
	}

	[Test]
	public void TestIsOkServerError() {
		// Conditions
		int statusCode = 500;

		// Execution
		response = new Response (statusCode, body, rawBody);

		// Expected
		Assert.IsFalse(response.IsOK());
	}

	[Test]
	public void TestIsOkInformationalResponse() {
		// Conditions
		int statusCode = 100;

		// Execution
		response = new Response (statusCode, body, rawBody);

		// Expected
		Assert.IsFalse(response.IsOK());
	}

	[Test]
	public void TestSerialization() {
		// Conditions
		string title = "My Awesome Post";
		string body = "Lorem Ipsum";
		int userId = 12345;

		string json = "{\"title\": \"" + title + "\", \"body\": \"" + body + "\", \"userId\": " + userId + "}";

		// Execution
		response = new Response(200, json, null);
		Post post = response.To<Post>();

		// Expected
		Assert.That(post.title == title);
		Assert.That (post.body == body);
		Assert.That (post.userId == userId);
	}

	[Test]
	public void TestFrom() {
		// Conditions
		DownloadHandler dlHandler = Substitute.For<DownloadHandler> ();
		dlHandler.data.Returns (rawBody);
		dlHandler.text.Returns ("Foo Bar");

		UnityWebRequest request = new UnityWebRequest ("http://127.0.0.1");
		request.downloadHandler = dlHandler;

		// Execution
		response = Response.From(request);

		Debug.Log (response.Status ());
		// Expected
		// FIXME: Test statusCode
		//Assert.That (response.Status () == 201);
		Assert.That (response.Body () == "Foo Bar");
		Assert.That (response.RawBody ().SequenceEqual(rawBody));
	}
}

[System.Serializable]
public class Post {
	public string title;
	public string body;
	public int userId;

	public Post(string title, string body, int userId) {
		this.title = title;
		this.body = body;
		this.userId = userId;
	}
}