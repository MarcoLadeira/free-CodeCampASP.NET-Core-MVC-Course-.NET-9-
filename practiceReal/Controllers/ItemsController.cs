using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using practiceReal.Data;
using practiceReal.Models;

namespace practiceReal.Controllers
{
    public class ItemsController : Controller
    {

        //CRUD
        //variable to store the context and to use in all the methods
        private readonly MyAppContext _context;
        public ItemsController(MyAppContext context)
        {
            _context = context;
        }

        //start writing methods
        //async means asynchronous meaning this method will run without stopping the rest of the program ,
        //basically instead of running line by line it can run along other methods
        public async Task<IActionResult> Index()
        {
            var items = await _context.Items
                .Include(s => s.SerialNumber)
                .Include(c => c.Category)
                .Include(ic => ic.ItemClients)
                .ThenInclude(c => c.Client)
                .ToListAsync();
            return View(items);
        }


        //CREATE METHODS
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,CategoryId")] Item item)
        {
            if(ModelState.IsValid)
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }



        //EDIT METHODS
        //2 get actions for post and get
        public async Task<IActionResult> Edit(int id) 
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            // var item = await _context.Items.FindAsync(id);
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,CategoryId")] Item item)
        {

            if(ModelState.IsValid)
            {
                _context.Items.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }


        //Delete methods
        public async Task<IActionResult>Delete(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if(item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }



    }
}
