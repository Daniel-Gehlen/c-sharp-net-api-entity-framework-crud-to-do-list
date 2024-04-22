using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrailApiChallenge.Context;
using TrailApiChallenge.Models;
using System.Linq;
using System;

namespace TrailApiChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly OrganizerContext _context;

        public TaskController(OrganizerContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var tasks = _context.Tasks.ToList();
            return Ok(tasks);
        }

        [HttpGet("GetByTitle")]
        public IActionResult GetByTitle(string title)
        {
            var tasks = _context.Tasks.Where(x => x.Title.Contains(title)).ToList();
            return Ok(tasks);
        }

        [HttpGet("GetByDate")]
        public IActionResult GetByDate(DateTime date)
        {
            var tasks = _context.Tasks.Where(x => x.Date.Date == date.Date).ToList();
            return Ok(tasks);
        }

        [HttpGet("GetByStatus/{status}")]
        public IActionResult GetByStatus(EnumTaskStatus status)
        {
            var tasks = _context.Tasks.Where(x => x.Status == status).ToList();
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            if (task.Date == DateTime.MinValue)
                return BadRequest(new { Error = "Task date cannot be empty" });

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskItem task)
        {
            var taskDb = _context.Tasks.Find(id);

            if (taskDb == null)
                return NotFound();

            if (task.Date == DateTime.MinValue)
                return BadRequest(new { Error = "Task date cannot be empty" });

            taskDb.Title = task.Title;
            taskDb.Description = task.Description;
            taskDb.Date = task.Date;
            taskDb.Status = task.Status;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var taskDb = _context.Tasks.Find(id);

            if (taskDb == null)
                return NotFound();

            _context.Tasks.Remove(taskDb);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
