using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using AAT;

var ConnectionString = "";
var sql = "SELECT TOP 1000000 * FROM received WHERE status = 1 ORDER BY re_ref";

IEnumerable<IConfigurationSection> sqlNodes =
    Program.Configuration.GetSection("ConnectionStrings")
        .GetSection("SqlNodes")
        .GetChildren();
// changed all declarations to implicit declarations
//changed List to ConcurrentBag<T> to allow safe concurrent access by multiple threads without the need for explicit locks.
var results = new ConcurrentBag<received>();

if (sqlNodes.Any())
{
    //  added parallelOptions so the use of resources is controlled 
    var parallelOptions = new ParallelOptions{MaxDegreeOfParallelism = 5};

    await Parallel.ForEachAsync(sqlNodes,parallelOptions, async(node, token) =>
    {
        // i assume DBQuery is a  Custom class with method query, so will change to async method to ensure that the method is truly asynchronous and doesn't rely on blocking I/O operations.
      
        var result = await DBQuery<received>.QueryAsync(node.Value, sql);
        // add the lock to ensure thread safety when adding to the list
        foreach (var rec in result)
        {
            results.Add(rec);
        }
    });
    //change insert to use bulk insert 
    if (results.Any())
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("rt_msisdn", typeof(string)); 
        dataTable.Columns.Add("rt_message", typeof(string)); 

        foreach (var rec in results)
        {
            dataTable.Rows.Add(rec.re_fromnum, rec.re_message);
        }
    
        var tableReceived = "received";
   
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        using var bulkCopy = new SqlBulkCopy(connection);
        bulkCopy.DestinationTableName = tableReceived;
        bulkCopy.BatchSize = 1000; // Adjust batch size as needed
        bulkCopy.BulkCopyTimeout = 600; // Adjust timeout as needed

        // Map DataTable columns to target table columns
        bulkCopy.ColumnMappings.Add("rt_msisdn", "rt_msisdn");
        bulkCopy.ColumnMappings.Add("rt_message", "rt_message");

       await bulkCopy.WriteToServerAsync(dataTable);
    }
}

