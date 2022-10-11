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

  public class CreateTask : BaseFunction
  {
    private readonly ITaskifyManager Manager;
    public CreateTask(ITaskifyManager manager)
    {
      Manager = manager;
    }

    [FunctionName("CreateTask")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "task")] HttpRequest req,
        ILogger log)
    {
      try
      {
        var claims = AuthUtils.Parse(req);
        var task = await ParseBodyAsync<CreateNewTaskDto>(req);
        req.AddUserIdTelemetry(claims);
        StampTask(task, claims);
        var result = await Manager.CreateNewTaskAsync(task);
        return new OkObjectResult(result);
      }
      catch (Exception ex)
      {
        log.LogError("Exception: {Message}", ex.Message);
        return new ExceptionResult(ex, true);
      }
    }

    private static void StampTask(CreateNewTaskDto task, ClaimsPrincipal claims)
    {
      task.CreationDate = DateTime.UtcNow;
      task.CreatedBy = claims.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
    }
  }
}
