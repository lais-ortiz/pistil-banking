using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pistil.Banking.Api.Models.DTO;
using Pistil.Banking.Domain.Entities;
using Pistil.Banking.Services;
using System.Threading.Tasks;

namespace Pistil.Banking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountDetailsController : ControllerBase
    {
        private readonly ILogger<AccountDetailsController> _logger;
        private readonly IMapper _mapper;

        private readonly IServiceBase<AccountDetails> _accountDetailsService;

        public AccountDetailsController(
            ILogger<AccountDetailsController> logger,
            IMapper mapper,
            IServiceBase<AccountDetails> accountDetailsService
        )
        {
            _logger = logger;
            _mapper = mapper;
            _accountDetailsService = accountDetailsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long accountId)
        {
            var accountDetails = await _accountDetailsService.GetById(accountId);

            return Ok(_mapper.Map<AccountDetails, AccountDetailsDto>(accountDetails));
        }
    }
}
