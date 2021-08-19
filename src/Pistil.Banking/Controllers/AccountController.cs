using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pistil.Banking.Api.Models.DTO;
using Pistil.Banking.Domain.Entities;
using Pistil.Banking.Domain.Interfaces.AccountService;
using Pistil.Banking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pistil.Banking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        private readonly IServiceBase<Account> _accountService;

        public AccountController(
            ILogger<AccountController> logger,
            IMapper mapper,
            IServiceBase<Account> accountService
        )
        {
            _logger = logger;
            _mapper = mapper;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long accountId)
        {
            var account = await _accountService.GetById(accountId);

            return Ok(_mapper.Map<Account, AccountDto>(account));
        }
    }
}
