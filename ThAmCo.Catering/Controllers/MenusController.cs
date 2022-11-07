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
    public class MenusController : ControllerBase
    {
        private readonly CateringContext _context;

        public MenusController(CateringContext context)
        {
            _context = context;
        }

        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenu()
        {
            return await _context.Menu.ToListAsync();
            //ThAmCo.Catering.DTOs.MenuDTO menudto = new DTOs.MenuDTO();
            //ThAmCo.Catering.DTOs.FoodItemsDTO menuItem = new DTOs.FoodItemsDTO();
            //ThAmCo.Catering.DTOs.MenuwithFoodItemDTO DTO = new DTOs.MenuwithFoodItemDTO();

            //var mydata = await _context.Menu
            //    .Include(f => f.FoodItems)
            //    .ToListAsync();

            //menudto.MenuName = mydata;

            //return null;
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuwithFoodItemDTO>> GetMenu(int id)
        {
            var menuitems = from foodItems in _context.FoodItems
                            where foodItems.Menus.Any(m => m.MenuId == id)
                            select foodItems;

            var menu = _context.Menu.Find(id);

   

            ThAmCo.Catering.DTOs.MenuDTO menudto = new DTOs.MenuDTO();
            //// constructing the Menu DTO
            menudto.MenuName = menu.MenuName;
            menudto.MenuId = menu.MenuId;

            //// Getting a list of food items for that menue
            ThAmCo.Catering.DTOs.FoodItemsDTO foodItemsDTO = new DTOs.FoodItemsDTO();

           

            //ThAmCo.Catering.DTOs.MenuwithFoodItemDTO DTO = new DTOs.MenuwithFoodItemDTO();






            //DTO.menu = menudto;
            //DTO.FoodItems = foodDto;

            //if (menu == null)
            //{
            //    return NotFound();
            //}

            return null;
        }

        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, Menu menu)
        {
            if (id != menu.MenuId)
            {
                return BadRequest();
            }

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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

        // POST: api/Menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {
            _context.Menu.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenu", new { id = menu.MenuId }, menu);
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.MenuId == id);
        }
    }
}
