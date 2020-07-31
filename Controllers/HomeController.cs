using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Snowflake.Data.Client;
using snowflakescratch.Models;

namespace snowflakescratch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private const string SnowflakeUser = "MARTEZKILLENS";
        private const string SnowflakePassword = ;
        private const string SnowflakeAccount = "vm57568";
        private const string SnowflakeWarehouse = "COMPUTE_WH";
        private const string SnowflakeDatabase = "SNOWFLAKE_SAMPLE_DATA";
        private const string SnowflakeSchema = "TPCH_SF001";
        private const string SnowflakeRole = "SYSADMIN";
        private const string SnowflakeHost = "vm57568.west-us-2.azure.snowflakecomputing.com";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            await using var connection = new SnowflakeDbConnection
            {
                
                ConnectionString = $"scheme=https;host={SnowflakeHost};port=443;account={SnowflakeAccount};role={SnowflakeRole};db={SnowflakeDatabase};schema={SnowflakeSchema};warehouse={SnowflakeWarehouse};user={SnowflakeUser};password={SnowflakePassword};"
            };

            Console.WriteLine(connection.ConnectionString);

            var results = new List<Supplier>();

            await connection.OpenAsync(cancellationTokenSource.Token);
            await using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM TPCH_SF001.SUPPLIER";
                await using var reader = await command.ExecuteReaderAsync(cancellationTokenSource.Token);

                while (await reader.ReadAsync(cancellationTokenSource.Token))
                {
                    var supplier = new Supplier 
                    {
                        Name = reader.GetString("S_NAME"),
                        Address = reader.GetString("S_ADDRESS")
                    };

                    results.Add(supplier);
                    Console.WriteLine($"Name: {supplier.Name} \t Address: {supplier.Address}");
                }

                await reader.CloseAsync();
            }
            await connection.CloseAsync();

            ViewBag.Host = SnowflakeHost;
            ViewBag.Account = SnowflakeAccount;
            ViewBag.User = SnowflakeUser;
            ViewBag.Warehouse = SnowflakeWarehouse;
            ViewBag.Database = SnowflakeDatabase;
            ViewBag.Schema = SnowflakeSchema;
            ViewBag.Role = SnowflakeRole;
            return View(results);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
