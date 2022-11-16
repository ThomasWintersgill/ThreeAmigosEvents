using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;
using ThAmCo.Catering.DTOs;

namespace ThAmCo.Catering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly CateringContext _context;

        public FoodItemsController(CateringContext context)
        {
            _context = context;
            
        }

        // GET: api/FoodItems
        [HttpGet]
        public IQueryable<FoodItemDTO> GetFoodItems()
        {

            var food = from fi in _context.FoodItems
                       select new FoodItemDTO
                       {
                           FoodItemId = fi.FoodItemId,
                           Description = fi.Description,
                           Price = fi.UnitPrice
                       };

            return food;
        }

        // GET: api/FoodItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodItemDTO>> GetFoodItem(int id)
        {
            var foodItem = _context.FoodItems.Find(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            FoodItemDTO DTO = new FoodItemDTO();
            DTO.FoodItemId = foodItem.FoodItemId;
            DTO.Description = foodItem.Description;
            DTO.Price = foodItem.UnitPrice;

            return DTO;
        }

        // PUT: api/FoodItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodItem(int id, FoodItem foodItem)
        {
            if (id != foodItem.FoodItemId)
            {
                return BadRequest();
            }

            _context.Entry(foodItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!FoodItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    var error = ex.Message;
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FoodItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodItem>> PostFoodItem(FoodItem foodItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.FoodItems.Add(foodItem);
            await _context.SaveChangesAsync();

            //need to take a look at automapper for this install-package AutoMapper
            FoodItemDTO DTO = new FoodItemDTO();
            DTO.FoodItemId = foodItem.FoodItemId;
            DTO.Description = foodItem.Description;
            DTO.Price = foodItem.UnitPrice;

            return CreatedAtAction("GetFoodItem", new { id = foodItem.FoodItemId }, DTO);
        }

        // DELETE: api/FoodItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodItem(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodItemExists(int id)
        {
            return _context.FoodItems.Any(e => e.FoodItemId == id);
        }
    }
}
