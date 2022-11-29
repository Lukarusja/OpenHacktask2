[FunctionName("HttpTriggerCSharp")]
public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
    HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string productId = req.Query["productId"];

    string requestBody = String.Empty;
    using (StreamReader streamReader =  new  StreamReader(req.Body))
    {
        requestBody = await streamReader.ReadToEndAsync();
    }
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    productId = productId ?? data?.productId;

    return productId != null
        ? (ActionResult)new OkObjectResult($"The product name for your product id {productId} is Starfruit Explosion")
        : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
}
