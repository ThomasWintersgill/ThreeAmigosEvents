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
        public async Task<ActionResult<IEnumerable<MenuDTO>>> GetMenu()
        {
            var AllMenus = await _context.Menu.ToListAsync();

            var DTO = AllMenus.Select(item => new MenuDTO {
                MenuId = item.MenuId,
                MenuName = item.MenuName,
            }).ToList();

            return DTO;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDTO>> GetMenu(int id)
        {
            var menu = _context.Menu.Find(id);
            if (menu == null)
            {
                return NotFound();
            }

            MenuDTO DTO = new MenuDTO();
            DTO.MenuId = menu.MenuId;
            DTO.MenuName = menu.MenuName;
            DTO.DateCreated = menu.DateCreated;

            return DTO;
        }

        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, MenuDTO DTO)
        {
            Menu menu = new Menu()
            {
                MenuId = DTO.MenuId,
                MenuName = DTO.MenuName,
                DateCreated = DTO.DateCreated,
            };

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
        public async Task<ActionResult<MenuDTO>> PostMenu(MenuDTO menuDTO)
        {

            Menu menu = new Menu()
            {
                MenuId = menuDTO.MenuId,
                MenuName = menuDTO.MenuName,
                DateCreated = menuDTO.DateCreated
            };
         
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

        //public IActionResult AddFood(int menuId, int FoodItemId)
        //{

        //}

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.MenuId == id);
        }

       
    }
}
