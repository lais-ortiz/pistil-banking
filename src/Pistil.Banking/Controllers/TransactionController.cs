using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pistil.Banking.Api.Models.DTO;
using Pistil.Banking.Domain.Entities;
using Pistil.Banking.Domain.Interfaces.TransactionsService;
using System.Threading.Tasks;

namespace Pistil.Banking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IMapper _mapper;

        private readonly ITransactionsService _transactionsService;

        public TransactionController(
            ILogger<TransactionController> logger,
            IMapper mapper,
            ITransactionsService transactionsService
        )
        {
            _logger = logger;
            _mapper = mapper;
            _transactionsService = transactionsService;
        }

        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionDto dto)
        {
            var transaction = _mapper.Map<TransactionDto, Transaction>(dto);

            await _transactionsService.Deposit(transaction);

            return Ok();
        }

        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionDto dto)
        {
            var transaction = _mapper.Map<TransactionDto, Transaction>(dto);

            await _transactionsService.Withdraw(transaction);

            return Ok();
        }

        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransactionDto dto)
        {
            var transaction = _mapper.Map<TransactionDto, Transaction>(dto);

            await _transactionsService.Transfer(transaction);

            return Ok();
        }
    }
}
