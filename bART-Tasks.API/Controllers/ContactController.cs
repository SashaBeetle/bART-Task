using AutoMapper;
using bART_Task.Core.Entities;
using bART_Task.EF.Interfaces;
using bART_Tasks.API.Mapping.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace bART_Tasks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactController(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _contactRepository.GetContactsAsync();

            return Ok(_mapper.Map<List<ContactDTO>>(contacts));
        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetContact(int Id)
        {
            var contact = await _contactRepository.GetContactAsync(Id);

            return Ok(_mapper.Map<ContactDTO>(contact));
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(ContactDTO contactDto)
        {
            var createdContact = await _contactRepository.CreateContactAsync(_mapper.Map<Contact>(contactDto));

            return Ok(_mapper.Map<ContactDTO>(createdContact));
        }
    }
}
