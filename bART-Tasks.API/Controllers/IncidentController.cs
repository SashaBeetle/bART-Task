using AutoMapper;
using bART_Task.Core.Entities;
using bART_Task.EF.Interfaces;
using bART_Task.EF.Repositories;
using bART_Tasks.API.Mapping.DTOs;
using bART_Tasks.API.Mapping.Models;
using Microsoft.AspNetCore.Mvc;

namespace bART_Tasks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IMapper _mapper;
        public IncidentController(IIncidentRepository incidentRepository, IMapper mapper)
        {
            _incidentRepository = incidentRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncident(IncidentRequest request)
        {
            Incident incidentDto = new Incident()
            {
                Description = request.IncidentDescription,
                Account = new Account()
                {
                    Name = request.AccountName,
                    Contacts = new List<Contact>(){
                        new Contact(){
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            Email = request.Email,
                        }
                    }
                }
            };

            try
            {
                Incident createdIncident = await _incidentRepository.CreateIncidentAsync(incidentDto, request.Email);

                return Ok(_mapper.Map<IncidentDTO>(createdIncident));
            }
            catch (AccountNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetIncidents()
        {
            var incidents = await _incidentRepository.GetIncidentsAsync();

            return Ok(_mapper.Map<List<IncidentDTO>>(incidents));
        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetIncident(string Id)
        {
            var incident = await _incidentRepository.GetIncidentAsync(Id);

            return Ok(_mapper.Map<IncidentDTO>(incident));
        }
    }
}