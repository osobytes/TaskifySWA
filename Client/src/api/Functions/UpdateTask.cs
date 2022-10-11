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
  using System.Collections.Generic;
  using Taskify.Data.Models;

  public class UpdateTask : BaseFunction
  {
    private readonly ITaskifyManager Manager;
    public UpdateTask(ITaskifyManager manager)
    {
      Manager = manager;
    }

    [FunctionName("UpdateTask")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "task")] HttpRequest req,
        ILogger log)
    {
      try
      {
        var claims = AuthUtils.Parse(req);
        var task = await ParseBodyAsync<UpdateTaskDto>(req);
        var existingTask = await Manager.Fetch(task.Key);
        if(!HasUpdatePermissions(existingTask, claims))
        {
          return new UnauthorizedResult();
        }
        req.AddUserIdTelemetry(claims);
        var result = await Manager.UpdateTaskAsync(task);
        return new OkObjectResult(result);
      }
      catch (KeyNotFoundException)
      {
        return new NotFoundResult();
      }
      catch (Exception ex)
      {
        log.LogError("Exception: {Message}", ex.Message);
        return new ExceptionResult(ex, true);
      }
    }

    private static bool HasUpdatePermissions(TaskModel task, ClaimsPrincipal claims)
    {
      var requestUser = claims.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
      return task.CreatedBy == requestUser;
    }
  }
}
