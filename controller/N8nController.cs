using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace openiddictAPI.controller
{
    public class N8nController : ControllerBase
    {
      private readonly  IHttpClientFactory _httpClientFactory;
         public N8nController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("~/trigger-n8n")]
        public async Task<IActionResult> TriggerN8n()
        {
            var client = _httpClientFactory.CreateClient();

            var payload = new
            {
                message = "openiddictAPI'den n8n'e tetikleme",
                source = "openiddictAPI",
                timestamp = DateTime.UtcNow
            };

            var response = await client.PostAsJsonAsync(
                "http://localhost:5678/webhook-test/138a7a9e-0d3b-4f02-bb02-f5730b077c47",
                payload
            );

            var responseBody = await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                n8nStatusCode = (int)response.StatusCode,
                n8nResponse = responseBody
            });
        }
    }
    }
