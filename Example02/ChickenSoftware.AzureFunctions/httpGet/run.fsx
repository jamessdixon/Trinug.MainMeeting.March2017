#r "System.Net.Http"

open System.Net
open System.Net.Http

let Run(req: HttpRequestMessage) =
    let currentDateTime = DateTime.Now.ToString()
    req.CreateResponse(HttpStatusCode.OK, sprintf "{%s}" currentDateTime)