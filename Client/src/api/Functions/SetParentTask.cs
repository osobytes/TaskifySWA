namespace Taskify.Api.Functions
{
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Azure.WebJobs;
  using Microsoft.Azure.WebJobs.Extensions.Http;
  using Microsoft.Extensions.Logging;
  using System;
  using System.Security.Claims;
  using System.Threading.Tasks;
  using System.Web.Http;
  using TaskifyAPI.Managers;
  using TaskifyAPI.Dtos;

  public class SetParentTask : BaseFunction
  {
    private readonly ITaskifyManager Manager;
    public SetParentTask(ITaskifyManager manager)
    {
      Manager = manager;
    }

    [FunctionName("SetParentTask")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "task/parent")] HttpRequest req,
        ILogger log,
        ClaimsPrincipal claims)
    {
      try
      {
        var dto = await ParseBodyAsync<SetParentTaskDto>(req);
        req.AddUserIdTelemetry(claims);
        var result = await Manager.SetParentAsync(dto);
        return new OkObjectResult(result);
      }
      catch (Exception ex)
      {
        log.LogError("Exception: {Message}", ex.Message);
        return new ExceptionResult(ex, true);
      }
    }
  }
}
