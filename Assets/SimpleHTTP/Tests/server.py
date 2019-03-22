from http.server import HTTPServer, BaseHTTPRequestHandler
from io import BytesIO

class SimpleHTTPServer(BaseHTTPRequestHandler):

    def do_POST(self):
        print('HTTP POST:')
        print('Authorization: ' + self.headers['Authorization'])
        print('Content-Type: ' + self.headers['Content-Type'])
        print('X-Api-Version: ' + self.headers['X-Api-Version'])

        content_length = int(self.headers['Content-Length'])
        body = self.rfile.read(content_length)
        print('Body: ' + body.decode('utf-8'))

        self.send_response(200)
        self.end_headers()
        response = BytesIO()
        response.write(str.encode('Ok'))
        self.wfile.write(response.getvalue())

print('Serving in port 8000...')
httpd = HTTPServer(('localhost', 8000), SimpleHTTPServer)
httpd.serve_forever()

