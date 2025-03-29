using System.Linq;
using System.Text;
using NUnit.Framework;
using SimpleHTTP;

public class ResponseTest {
    private byte[] rawBody = Encoding.UTF8.GetBytes("Foo Bar");
    private string body = "Hello World";

    // Subject under test
    private Response response;

    [Test]
    public void TestConstructorAndGetters() {
        // Execution
        response = new Response(200, body, rawBody);

        // Expected
        Assert.That(response.Status() == 200);
        Assert.That(response.Body() == body);
        Assert.That(response.RawBody().SequenceEqual(rawBody));
        Assert.That(response.Error() == null);
    }

    [Test]
    public void TestIsOkSuccessfulRequest() {
        // Conditions
        int statusCode = 200;

        // Execution
        response = new Response(statusCode, body, rawBody);

        // Expected
        Assert.IsTrue(response.IsOK());
    }

    [Test]
    public void TestIsOkClientError() {
        // Conditions
        int statusCode = 400;

        // Execution
        response = new Response(statusCode, body, rawBody);

        // Expected
        Assert.IsFalse(response.IsOK());
    }

    [Test]
    public void TestIsOkServerError() {
        // Conditions
        int statusCode = 500;

        // Execution
        response = new Response(statusCode, body, rawBody);

        // Expected
        Assert.IsFalse(response.IsOK());
    }

    [Test]
    public void TestIsOkInformationalResponse() {
        // Conditions
        int statusCode = 100;

        // Execution
        response = new Response(statusCode, body, rawBody);

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
        BlogPost post = response.To<BlogPost>();

        // Expected
        Assert.That(post.title == title);
        Assert.That(post.body == body);
        Assert.That(post.userId == userId);
    }
}
