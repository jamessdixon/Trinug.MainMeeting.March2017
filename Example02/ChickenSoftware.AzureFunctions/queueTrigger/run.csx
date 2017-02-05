#r "Newtonsoft.Json"
#r "Microsoft.WindowsAzure.Storage"

using System;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

public class Person: TableEntity
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string address { get; set; }
}


public static void Run(string queueItem, ICollector<Person> outTable)
{
    var person = JsonConvert.DeserializeObject<Person>(queueItem);
    person.PartitionKey = "Trinug";
    person.RowKey = Guid.NewGuid().ToString();
    outTable.Add(person);
}