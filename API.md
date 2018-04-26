# SimpleHTTP Full API Reference

## Request

Class to build an HTTP request.

### Public Methods

#### Request(string url)

Creates a new `Request` object. Receives a URL as param.

#### Request Url(string url)

Sets the URL of the `Request` object. Returns the updated `Request`, so this method is chainable.

#### Request Method(string url)

Sets the URL of the `Request` object. Returns the updated `Request`, so this method is chainable.

#### Request AddHeader(string name, string value)

#### Request RemoveHeader(string name)

#### Request Timeout(int timeout)

#### Request Get()

#### Request Post(RequestBody body)

#### 
