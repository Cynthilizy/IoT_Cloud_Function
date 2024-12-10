using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public class HttpTrigger1
    {
        private readonly ILogger<HttpTrigger1> _logger;

        public HttpTrigger1(ILogger<HttpTrigger1> logger)
        {
            _logger = logger;
        }

        [Function("HttpTrigger1")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Read request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            InputData? data = JsonConvert.DeserializeObject<InputData>(requestBody);

            // Use default values if any of the fields are missing
            string name = data?.Name ?? "Guest";
            int a = data?.A ?? 0;
            int b = data?.B ?? 0;

            // Calculate the sum of a and b
            int sum = a + b;

            // Replace [Your Name] with your actual name
            string responseMessage = $"Cynthia: Hello {name}. The sum of the numbers {a} and {b} is {sum}.";
            return new OkObjectResult(responseMessage);
        }
    }

    public class InputData
    {
        public string? Name { get; set; }
        public int? A { get; set; }
        public int? B { get; set; }
    }
}
