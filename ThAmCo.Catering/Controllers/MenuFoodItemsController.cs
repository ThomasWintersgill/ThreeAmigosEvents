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
    public class MenuFoodItemsController : ControllerBase
    {
        private readonly CateringContext _context;

        public MenuFoodItemsController(CateringContext context)
        {
            _context = context;
        }

        // GET: api/MenuFoodItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuFoodItem>>> GetMenuFoodItems()
        {
            return await _context.MenuFoodItems.ToListAsync();
        }

        // GET: api/MenuFoodItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuFoodItem>> GetMenuFoodItem(int id)
        {
            var menuFoodItem = await _context.MenuFoodItems.FindAsync(id);

            if (menuFoodItem == null)
            {
                return NotFound();
            }

            return menuFoodItem;
        }

        // GET: api/Menus/5
        //Menu with food items included
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuwithFoodItemDTO>> GetMenuWithFood(int id)
        {
            //get the menu
            var menu = _context.Menu.Find(id);
            if (menu == null)
            {
                return NotFound();
            }

            //store the food items, basically a sql query, select the fooditems from the menus that have an id that matches above
            //comparing this to barrys demo-- this is all done in a static method
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
            ThAmCo.Catering.DTOs.FoodItemDTO foodItemsDTO = new DTOs.FoodItemDTO();

            //create the composite/main dto
            ThAmCo.Catering.DTOs.MenuwithFoodItemDTO DTO = new DTOs.MenuwithFoodItemDTO();

            //set the dto menu to the menu
            DTO.menu = menudto;

            //put the food items into the main DTO, this DTO has a list property that is populated
            DTO.FoodItems = menuitems.Select(item => new FoodItemDTO
            {
                Title = item.Title,
                FoodItemId = item.FoodItemId,
                Description = item.Description,
                Price = item.UnitPrice

            }).ToList();

            return DTO;
        }

        // PUT: api/MenuFoodItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuFoodItem(int id, MenuFoodItem menuFoodItem)
        {
            if (id != menuFoodItem.FoodItemId)
            {
                return BadRequest();
            }

            _context.Entry(menuFoodItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuFoodItemExists(id))
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

        // POST: api/MenuFoodItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuFoodItem>> PostMenuFoodItem(MenuFoodItem menuFoodItem)
        {
            _context.MenuFoodItems.Add(menuFoodItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MenuFoodItemExists(menuFoodItem.FoodItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMenuFoodItem", new { id = menuFoodItem.FoodItemId }, menuFoodItem);
        }

        // DELETE: api/MenuFoodItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuFoodItem(int id)
        {
            var menuFoodItem = await _context.MenuFoodItems.FindAsync(id);
            if (menuFoodItem == null)
            {
                return NotFound();
            }

            _context.MenuFoodItems.Remove(menuFoodItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Remove FoodItem from a menu
        [HttpDelete()]
        public async Task<IActionResult> RemoveFoodItem(int menuId, int FoodItemId)
        {
            if (menuId == null || FoodItemId == null || _context.MenuFoodItems == null)
            {
                return NotFound();
            }

            var MenuFoodITems = await _context.MenuFoodItems

                .FirstOrDefaultAsync(m => m.MenuId == menuId && m.FoodItemId == FoodItemId);
            if (MenuFoodITems == null)
            {
                return NotFound();
            }

            _context.MenuFoodItems.Remove(MenuFoodITems);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool MenuFoodItemExists(int id)
        {
            return _context.MenuFoodItems.Any(e => e.FoodItemId == id);
        }
    }
}
