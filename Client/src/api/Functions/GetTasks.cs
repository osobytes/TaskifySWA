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
  public class GetTasks
  {
    private readonly ITaskifyManager Manager;
    public GetTasks(ITaskifyManager manager)
    {
      Manager = manager;
    }

    [FunctionName("GetTasks")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tasks")] HttpRequest req,
        ILogger log)
    {
      try
      {
        var claims = AuthUtils.Parse(req);
        req.AddUserIdTelemetry(claims);
        var result = await Manager.GetRootTasksAsync();
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
