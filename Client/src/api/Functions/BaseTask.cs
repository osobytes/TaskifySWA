using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Taskify.Api.Functions
{
  public class BaseFunction
  {
    protected static async Task<T> ParseBodyAsync<T>(HttpRequest req)
    {
      string? jsonBody = null;
      using (var sr = new StreamReader(req.Body, Encoding.UTF8))
      {
        jsonBody = await sr.ReadToEndAsync();
      }

      if (string.IsNullOrWhiteSpace(jsonBody))
      {
        throw new Exception("Invalid body");
      }

      return JsonConvert.DeserializeObject<T>(jsonBody)!;
    }
  }
}
