using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet.Api.Rest.Models;

namespace Projet.Api.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremierController : ControllerBase
    {

        private readonly DatabaseContext _context;

        public PremierController(DatabaseContext context)
        {
            //dependencies injection
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<ItemModel>>> Get()
        {


            var result = await _context.ItemModels.ToListAsync();
            return Ok(result);

            //return Ok(items);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<ItemModel>> Get(string name)
        {
            var item = await _context.ItemModels.FindAsync(name);
            if(item == null) return BadRequest("Pas d'item trouvé !");

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<List<ItemModel>>> Post(ItemModel model)
        {
            _context.ItemModels.Add(model);
            await _context.SaveChangesAsync();

            //items.Add(model);
            return CreatedAtAction("POST", model);
        }

        [HttpPut]
        public async Task<ActionResult<ItemModel>> Put(string name, ItemModel model)
        {
            if (model.Name != name) return BadRequest("L'élément recherché n'éxiste pas");

            var item = await _context.ItemModels.FindAsync(name);
            if (item == null) return BadRequest("Pas d'item trouvé !");

            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete]
        public async Task<ActionResult<ItemModel>> Delete(string name)
        {
            var item = await _context.ItemModels.FindAsync(name);
            if (item == null) return BadRequest("Pas d'item trouvé !");

            _context.ItemModels.Remove(item);

            await _context.SaveChangesAsync();

            return Ok(item);
        }

    }
}
