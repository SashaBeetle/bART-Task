using AutoMapper;
using bART_Task.Core.Entities;
using bART_Task.EF.Interfaces;
using bART_Tasks.API.Mapping.DTOs;
using bART_Tasks.API.Mapping.Models;
using Microsoft.AspNetCore.Mvc;

namespace bART_Tasks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountController(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _accountRepository.GetAccountsAsync();

            return Ok(_mapper.Map<List<AccountDTO>>(accounts));
        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetAccount(int Id)
        {
            var account = await _accountRepository.GetAccountAsync(Id);

            return Ok(_mapper.Map<AccountDTO>(account));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountRequest accountDto)
        {
            var createdAccount = await _accountRepository.CreateAccountAsync(_mapper.Map<Account>(accountDto), accountDto.ContactId);

            return Ok(_mapper.Map<AccountDTO>(createdAccount));
        }
        [HttpPatch]
        public async Task<IActionResult> LinkToAccount(AccountLinkRequest accountLink)
        {
            var updatedAccount = await _accountRepository.LinkContactIntoAccount(accountLink.AccountId, accountLink.ContactId);

            return Ok(_mapper.Map<AccountDTO>(updatedAccount));
        }
    }
}
