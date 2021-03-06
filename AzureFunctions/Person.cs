using Azure;
using Azure.Data.Tables;
using System;
using System.Text.Json.Serialization;

namespace AzureFunctions;

public class Person: ITableEntity
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("partitionkey")]
    public string PartitionKey { get; set; }

    [JsonPropertyName("rowkey")]
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    ETag ITableEntity.ETag { get; set; }
}

