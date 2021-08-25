from http.server import HTTPServer, BaseHTTPRequestHandler
import json


class Serv(BaseHTTPRequestHandler):    
    def do_GET(self):
        x = json.loads('{"jumpDown":true, "jumpUp":true, "horizontal":5}')        
        
        self.send_response(200)
        
        self.end_headers()
        self.wfile.write(bytes(json.dumps(x), 'utf-8'))


httpd = HTTPServer(('localhost', 8080), Serv)
httpd.serve_forever()
