using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Web.Data;
using AutoMapper;
using System.Runtime.ConstrainedExecution;

namespace LMS.Web.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LeaveTypesController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            if (_context.LeaveTypes == null)
            {
                return NotFound();
            }
            var LeaveTypes = _mapper.Map<List<LeaveTypeVM>>(await _context.LeaveTypes.ToListAsync());
            return View(LeaveTypes);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LeaveTypes == null)
            {
                return NotFound();
            }

            var leaveType = await _context.LeaveTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( LeaveTypeVM leaveTypeVM)
        {
            if (ModelState.IsValid)
            {

                if (LeaveTypeExists(leaveTypeVM.Name) == false)
                {

                    var leaveType = _mapper.Map<LeaveType>(leaveTypeVM);
                    _context.Add(leaveType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                else
                    ModelState.AddModelError(nameof(LeaveTypeVM.Name), "Leave Type already exists.");

            }
            else
            {
                int c = 0;
                int intErrorCount = 0;
                int intErrorToManage = 1;
                foreach (var key in ViewData.ModelState.Keys)
                {
                    if(key == "DefaultDays")
                    {
                        ModelState.Values.ElementAt(c).Errors.Clear();
                        ModelState.AddModelError(nameof(LeaveTypeVM.DefaultDays), "Days should be between 1 & 25");
                        intErrorCount++;
                    }
                    if (intErrorCount >= intErrorToManage)
                        break;
                    c++;
                }
            }
            return View(leaveTypeVM);
        }

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LeaveTypes == null)
            {
                return NotFound();
            }

            var leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            return View(leaveType);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,DefaultDays,ID,DeteCreated,DeteModified")] LeaveType leaveType)
        {
            if (id != leaveType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveTypeExists(leaveType.ID))
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
            return View(leaveType);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LeaveTypes == null)
            {
                return NotFound();
            }

            var leaveType = await _context.LeaveTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LeaveTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LeaveTypes'  is null.");
            }
            var leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType != null)
            {
                _context.LeaveTypes.Remove(leaveType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveTypeExists(int id)
        {
          return (_context.LeaveTypes?.Any(e => e.ID == id)).GetValueOrDefault();
        }
        private bool LeaveTypeExists(string name)
        {
            return (_context.LeaveTypes?.Any(e => e.Name == name)).GetValueOrDefault();
        }
        private bool CheckEmpty(int x)
        {
            //if (x == null)
            //    return false;
            //else 
            if (Convert.ToString(x) == "")
                return false;
            else if (Convert.ToString(x) == "0")
                return false;
            else
                return true;
        }

        //Action<string, bool> fnCheckEmpty = x =>
        //{
        //    if (x == null)
        //        return false;
        //    else if(x == "")
        //        return false;
        //    else
        //        return true;
        //}
    }
}
