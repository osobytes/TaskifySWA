namespace Taskify.Api
{
  using Microsoft.ApplicationInsights.DataContracts;
  using Microsoft.AspNetCore.Http;
  using System.Security.Claims;
  internal static class TelemetryExtensions
  {
    internal static void AddTelemetryProperty(this HttpRequest req, string name, object value)
    {
      var requestTelemetry = req.HttpContext?.Features.Get<RequestTelemetry>();
      requestTelemetry?.Properties.Add(name, value.ToString());
    }

    internal static void AddUserIdTelemetry(this HttpRequest req, ClaimsPrincipal principal)
    {
      var userId = principal.FindFirst(ClaimTypes.NameIdentifier);
      req.AddTelemetryProperty(TelemetryEventNames.UserId, userId);
    }
  }

  public static class TelemetryEventNames
  {
    public const string UserId = nameof(UserId);
  }
}
