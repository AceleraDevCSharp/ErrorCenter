using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ErrorCenter.WebAPI.Models;

namespace ErrorCenter.WebAPI.Controllers {
  [ApiController]
  [Route("v1/sessions")]
  public class SessionsController : ControllerBase {
    [HttpPost]
    [Route("")]
    public async Task<ActionResult<Session>> Create([FromBody] LoginInfo login) {
      return new Session();
    }
  }
}