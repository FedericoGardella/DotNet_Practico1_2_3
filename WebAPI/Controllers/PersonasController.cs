using BL.BLs;
using BL.IBLs;
using Microsoft.AspNetCore.Mvc;
using Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IBL_Personas bl;

        public PersonasController(IBL_Personas _bl)
        {
            bl = _bl;
        }

        // GET: api/<PersonasController>
        [HttpGet]
        public IEnumerable<Persona> Get()
        {
            return bl.GetPersonas();
        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}")]
        public Persona Get(long id)
        {
            return bl.GetPersona(id);
        }

        // POST api/<PersonasController>
        [HttpPost]
        public void Post([FromBody] Persona value)
        {
            bl.AddPersona(value);
        }

        // PUT api/<PersonasController>/5
        [HttpPut("{id}")]
        public void Put(Persona value)
        {
            bl.UpdatePersona(value);
        }

        // DELETE api/<PersonasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            bl.DeletePersona(id);
        }
    }
}
