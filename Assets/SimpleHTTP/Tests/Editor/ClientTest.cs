using System.Collections;
using NSubstitute;
using NUnit.Framework;
using SimpleHTTP;

public class ClientTest {
    Request request;

    // Subject under test
    Client client;

    [SetUp]
    public void SetUp() {
        client = new Client();

        request = Substitute.For<Request>("http://127.0.0.1");
    }

    [Test]
    public void TestSend() {
        // Execution
        IEnumerator e = client.Send(request);
        e.MoveNext();

        // Expected
        request.Received(1).Send();
        request.Received(1).Response();
    }

    [Test]
    public void TestAbortNotInitialized() {
        // Execution
        client.Abort();

        // Expected
        request.Received(0).Abort();
    }

    [Test]
    public void TestAbort() {
        // Conditions
        IEnumerator e = client.Send(request);

        // Execution
        client.Abort();

        // Expected
        request.Received(1).Abort();
    }

    [Test]
    public void TestIsSuccessfulNotInitialized() {
        // Execution
        bool result = client.IsSuccessful();

        // Expected
        Assert.IsFalse(result);
    }

    [Test]
    public void TestIsSuccessful() {
        // Conditions
        Response successfulResponse = new Response(200, "hello", null);
        Response badResponse = new Response(400, "hello", null);

        // Execution
        ReflectionHelper.SetPrivateField(typeof(Client), client, "response", successfulResponse);
        bool successfulResult = client.IsSuccessful();

        ReflectionHelper.SetPrivateField(typeof(Client), client, "response", badResponse);
        bool badResult = client.IsSuccessful();

        // Expected
        Assert.IsTrue(successfulResult);
        Assert.IsFalse(badResult);
    }

    [Test]
    public void TestError() {
        // Conditions
        ReflectionHelper.SetPrivateField(typeof(Client), client, "error", "Oh, The humanity!");
        string error = client.Error();

        Assert.That(error, Is.EqualTo("Oh, The humanity!"));
    }

    [Test]
    public void TestResponse() {
        Response response = new Response(200, "hello", null);

        // Execution
        ReflectionHelper.SetPrivateField(typeof(Client), client, "response", response);

        Assert.That(client.Response(), Is.EqualTo(response));
    }
}
