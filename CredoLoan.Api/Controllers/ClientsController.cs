using System.Threading.Tasks;
using CredoLoan.Api.Filters;
using CredoLoan.Api.Tokens;
using CredoLoan.Business;
using CredoLoan.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CredoLoan.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase {

        private readonly IClientService _clientService;
        private readonly IJwtTokenManager _tekonManager;

        public ClientsController(IClientService clientService, IJwtTokenManager tekonManager) {
            _clientService = clientService;
            _tekonManager = tekonManager;
        }

        [HttpPost("auth")]
        [ValidateModel]
        public async Task<IActionResult> Authenticate([FromBody] ClientAuthModel authModel) {
            var client = await _clientService.AuthenticateAsync(authModel);
            if (client == null)
                return Unauthorized();

            string access_token = _tekonManager.GenerateAccessToken(client.Id);
            return Ok(new { client, access_token });
        }

        [HttpPost("register")]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] ClientAddOrUpdateModel clientModel) {
            //throw new System.Exception("sadasdasd");
            string refresh_token = _tekonManager.GenerateRefreshToken();
            var registeredClient = await _clientService.RegisterAsync(clientModel, refresh_token);
            string access_token = _tekonManager.GenerateAccessToken(registeredClient.Id);
            return Ok(new { client = registeredClient, access_token, refresh_token });
        }

        [HttpPut("update")]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] ClientAddOrUpdateModel clientModel) {
            var updated = await _clientService.UpdateClientAsync(clientModel);
            return updated ? (IActionResult)Ok() : BadRequest();
        }

        [HttpPost("refresh-token")]
        [ValidateModel]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel refreshTokenModel) {
            try {
                string access_token = await _tekonManager.ValidateRefreshTokenAsync(refreshTokenModel.AccessToken, refreshTokenModel.RefreshToken);
                return Ok(new { access_token });
            } catch (SecurityTokenException ex) {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
