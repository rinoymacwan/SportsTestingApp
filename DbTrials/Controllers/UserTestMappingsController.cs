﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbTrials.Models;

namespace DbTrials.Controllers
{
    public class UserTestMappingsController : Controller
    {
        private readonly DbTrialsContext _context;

        public UserTestMappingsController(DbTrialsContext context)
        {
            _context = context;
        }

        // GET: UserTestMappings
        public async Task<IActionResult> Index()
        {
            var dbTrialsContext = _context.UserTestMapping.Include(u => u.Test).Include(u => u.User);
            return View(await dbTrialsContext.ToListAsync());
        }

        // GET: UserTestMappings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTestMapping = await _context.UserTestMapping
                .Include(u => u.Test)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTestMapping == null)
            {
                return NotFound();
            }

            return View(userTestMapping);
        }

        // GET: UserTestMappings/Create
        public IActionResult Create()
        {
            ViewData["TId"] = new SelectList(_context.Test, "Id", "Id");
            ViewData["UId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: UserTestMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Distance,Time,TId,UId")] UserTestMapping userTestMapping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTestMapping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TId"] = new SelectList(_context.Test, "Id", "Id", userTestMapping.TId);
            ViewData["UId"] = new SelectList(_context.User, "Id", "Id", userTestMapping.UId);
            return View(userTestMapping);
        }

        // GET: UserTestMappings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTestMapping = await _context.UserTestMapping.FindAsync(id);
            if (userTestMapping == null)
            {
                return NotFound();
            }
            ViewData["TId"] = new SelectList(_context.Test, "Id", "Id", userTestMapping.TId);
            ViewData["UId"] = new SelectList(_context.User, "Id", "Id", userTestMapping.UId);
            return View(userTestMapping);
        }

        // POST: UserTestMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Distance,Time,TId,UId")] UserTestMapping userTestMapping)
        {
            if (id != userTestMapping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTestMapping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTestMappingExists(userTestMapping.Id))
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
            ViewData["TId"] = new SelectList(_context.Test, "Id", "Id", userTestMapping.TId);
            ViewData["UId"] = new SelectList(_context.User, "Id", "Id", userTestMapping.UId);
            return View(userTestMapping);
        }

        // GET: UserTestMappings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTestMapping = await _context.UserTestMapping
                .Include(u => u.Test)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTestMapping == null)
            {
                return NotFound();
            }

            return View(userTestMapping);
        }

        // POST: UserTestMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userTestMapping = await _context.UserTestMapping.FindAsync(id);
            _context.UserTestMapping.Remove(userTestMapping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTestMappingExists(int id)
        {
            return _context.UserTestMapping.Any(e => e.Id == id);
        }
    }
}
