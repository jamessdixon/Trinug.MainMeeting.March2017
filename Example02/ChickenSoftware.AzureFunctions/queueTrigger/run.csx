using System;
using System.Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

public class Person{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string address { get; set; }
}


public static void Run(string queueItem, ICollector<Person> outTable)
{
    var person = JsonConvert.SerializeObject<Person>(queueItem);
    outTable.Add(person);
}