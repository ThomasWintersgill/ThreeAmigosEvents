﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Controllers
{
    public class StaffingsController : Controller
    {
        private readonly EventsDbContext _context;

        public StaffingsController(EventsDbContext context)
        {
            _context = context;
        }

        // GET: Staffings
        public async Task<IActionResult> Index()
        {
            var eventsDbContext = _context.staffing.Include(s => s.Event).Include(s => s.Staff);
            return View(await eventsDbContext.ToListAsync());
        }

        // GET: Staffings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.staffing == null)
            {
                return NotFound();
            }

            var staffing = await _context.staffing
                .Include(s => s.Event)
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffing == null)
            {
                return NotFound();
            }

            return View(staffing);
        }

        // GET: Staffings/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId");
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId");
            return View();
        }

        // POST: Staffings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,EventId")] Staffing staffing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", staffing.EventId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", staffing.StaffId);
            return View(staffing);
        }

        // GET: Staffings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.staffing == null)
            {
                return NotFound();
            }

            var staffing = await _context.staffing.FindAsync(id);
            if (staffing == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", staffing.EventId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", staffing.StaffId);
            return View(staffing);
        }

        // POST: Staffings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,EventId")] Staffing staffing)
        {
            if (id != staffing.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffingExists(staffing.StaffId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", staffing.EventId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", staffing.StaffId);
            return View(staffing);
        }

        // GET: Staffings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.staffing == null)
            {
                return NotFound();
            }

            var staffing = await _context.staffing
                .Include(s => s.Event)
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffing == null)
            {
                return NotFound();
            }

            return View(staffing);
        }

        // POST: Staffings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.staffing == null)
            {
                return Problem("Entity set 'EventsDbContext.staffing'  is null.");
            }
            var staffing = await _context.staffing.FindAsync(id);
            if (staffing != null)
            {
                _context.staffing.Remove(staffing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffingExists(int id)
        {
          return _context.staffing.Any(e => e.StaffId == id);
        }
    }
}
