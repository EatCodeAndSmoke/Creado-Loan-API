using System;
using System.Threading.Tasks;
using CredoLoan.Api.Filters;
using CredoLoan.Business;
using CredoLoan.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CredoLoan.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoansController : ControllerBase {

        private readonly ILoanApplicationService _service;

        public LoansController(ILoanApplicationService service) {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpGet("all/{clientId}")]
        public async Task<IActionResult> GetAll(Guid clientId) {
            return Ok(await _service.GetByClientIdAsync(clientId));
        }

        [HttpPost("{clientId}")]
        [ValidateModel]
        public async Task<IActionResult> Add([FromBody]LoanApplicationModel loanApplication, Guid clientId) {
            var result = await _service.RegisterApplicationAsync(loanApplication, clientId);
            return result != null ? (IActionResult)Ok(result) : BadRequest();
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> Put([FromBody] LoanApplicationModel loanApplication) {
            int status = await _service.UpdateApplicationAsync(loanApplication);
            return status != 1 ? (IActionResult)BadRequest() : NoContent();
        }
    }
}
