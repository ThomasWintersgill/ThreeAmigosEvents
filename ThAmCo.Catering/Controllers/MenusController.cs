using System;
using System.Collections;
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
            //get the menu
            var menu = _context.Menu.Find(id);
            if (menu == null)
            {
                return NotFound();
            }

            //store the food items, basically a sql query, select the fooditems from the menus that have an id that matches above
            var menuitems = from foodItems in _context.FoodItems
                            where foodItems.Menus.Any(m => m.MenuId == id)
                            select foodItems;

            //create a new menu dto
            ThAmCo.Catering.DTOs.MenuDTO menudto = new DTOs.MenuDTO();
            
            
            //set the menu dto variables to the variable from the menu that was input in the get request
            menudto.MenuName = menu.MenuName;
            menudto.MenuId = menu.MenuId;

           
            //// Getting a list of food items for that menu
            //create food items dto
            ThAmCo.Catering.DTOs.FoodItemsDTO foodItemsDTO = new DTOs.FoodItemsDTO();
            
            //create the composite/main dto
            ThAmCo.Catering.DTOs.MenuwithFoodItemDTO DTO = new DTOs.MenuwithFoodItemDTO();

            //set the dto menu to the menu
            DTO.menu = menudto;

            //put the food items into the main DTO
            DTO.FoodItems = menuitems.Select(item => new FoodItemsDTO
            {
                FoodItemId = item.FoodItemId,
                Description = item.Description,
                Price = item.UnitPrice

            }).ToList();

            return DTO;
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
