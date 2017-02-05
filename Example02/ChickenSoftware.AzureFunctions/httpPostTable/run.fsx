#r "System.Net.Http"
#r "Microsoft.WindowsAzure.Storage"
#r "Newtonsoft.Json"

open System
open System.Net
open System.Net.Http
open Microsoft.WindowsAzure.Storage.Table
open Newtonsoft.Json
open Newtonsoft.Json.Linq

type Person() =
    inherit TableEntity()
    member val firstName: string = null with get, set    
    member val lastName: string = null with get, set    
    member val address: string = null with get, set    

let inline getValue (value:JToken) = 
    match isNull value with
    | false -> value.Value<string>()  
    | true -> ""

let hydrate data = 
    let json = JObject.Parse(data)
    let person = Person()
    person.PartitionKey <- "Functions"
    person.RowKey <- Guid.NewGuid().ToString()
    person.firstName <- getValue json.["firstName"]
    person.lastName <- getValue json.["lastName"] 
    person.address <- getValue json.["address"] 
    person

let isValid (person:Person) =
    [person.firstName; person.lastName; person.address]
    |> List.forall (not << String.IsNullOrEmpty)

let Run(req: HttpRequestMessage, outTable: ICollector<Person>, log: TraceWriter) =
    async {
        let! data = req.Content.ReadAsStringAsync() |> Async.AwaitTask
        let person = hydrate data
        match isValid person with
        | true -> outTable.Add(person)
                  return req.CreateResponse(HttpStatusCode.Created)
        | false -> return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass all of the values in the request body")
    } |> Async.StartAsTask

