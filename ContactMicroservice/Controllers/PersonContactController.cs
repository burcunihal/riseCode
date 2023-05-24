using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactMicroservice.Data;
using ContactMicroservice.Models;
using ContactMicroservice.DTOs;
using Confluent.Kafka;
using System.Net;
using System.Text.Json;

namespace ContactMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonContactController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string bootstrapServers = "localhost:29092";
        private readonly string topic = "test";
        public PersonContactController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AddContactToPerson")]
        public async Task<ActionResult<Person>> AddContactInfoToPerson(Guid personId, ContactInfoDto contactInfoDto)
        {
            var existingPerson = _context.People.Include(p => p.ContactInfos).FirstOrDefault(w => w.UUID == personId);
            if (existingPerson == null)
                return NoContent();
            ContactInfo contactInfo = new ContactInfo();
            if (string.IsNullOrEmpty(contactInfoDto.Content))
            {
                return NoContent();
            }
            contactInfo.Content = contactInfoDto.Content;
            contactInfo.Type = contactInfoDto.Type;
            _context.ContactInfos.Add(contactInfo);
            _context.SaveChanges();
            existingPerson.ContactInfos.Add(contactInfo);
            _context.SaveChanges();
            return Ok();
        }
        // GET: api/PersonContact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            return await _context.People.Include(p => p.ContactInfos).ToListAsync();
        }

        // GET: api/PersonContact/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }
   
        // PUT: api/PersonContact/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            if (id != person.UUID)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PersonContact
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(PersonDto personDto)
        {
            var person = new Person();
            List<ContactInfo> contacts = new List<ContactInfo>();
            if (personDto.ContactInfoDto != null)
            {
                foreach (var ci in personDto.ContactInfoDto)
                {
                    contacts.Add(new ContactInfo
                    {
                        Type = ci.Type,
                        Content = ci.Content
                    });
                }
            }

            _context.ContactInfos.AddRange(contacts);
            await _context.SaveChangesAsync();
            person.Name = personDto.Name;
            person.Surname = personDto.Surname;
            if(string.IsNullOrEmpty(personDto.Location)){
                person.Location = "Ankara";
            }else {
                person.Location = personDto.Location;
            }
            person.ContactInfos = contacts;
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            string message = JsonSerializer.Serialize(person);

            return CreatedAtAction("GetPerson", new { id = person.UUID }, person);
        }

        // DELETE: api/PersonContact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Route("DeletePersonContact")]
        public async Task<IActionResult> DeletePersonContactInfo(Guid personId, Guid contactd)
        {

            var existingPerson = _context.People.Include(p => p.ContactInfos).FirstOrDefault(w => w.UUID == personId);
            if (existingPerson == null)
                return NoContent();


            var existingContactInfo = existingPerson.ContactInfos.FirstOrDefault(w => w.UUID == contactd);

            if (existingContactInfo == null)
                return NoContent();

            existingPerson.ContactInfos.Remove(existingContactInfo);
            _context.SaveChanges();
            return Ok();


        }


        private bool PersonExists(Guid id)
        {
            return _context.People.Any(e => e.UUID == id);
        }

     

    }
}
