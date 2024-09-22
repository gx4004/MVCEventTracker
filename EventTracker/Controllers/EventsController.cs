using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EventTracker.Models;

namespace EventTracker.Controllers;

public class EventsController : Controller
{
    private static List<EventModel> eventList = new List<EventModel>
    {
        new EventModel
        {
            Id = 1,
            Title = "Sample Event 1",
            Description = "This is the first sample event.",
            Date = DateTime.UtcNow.AddDays(7)
        },
        new EventModel
        {
            Id = 2,
            Title = "Sample Event 2",
            Description = "This is the second sample event.",
            Date = DateTime.UtcNow.AddMonths(1)
        }
    };

    public IActionResult List()
    {
        return View(eventList);
    }

    public IActionResult Details(int id)
    {
        var eventItem = eventList.Find(a => a.Id == id);
        if (eventItem is null)
        {
            return NotFound();
        }

        return View(eventItem);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(EventModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = eventList.Count + 1;
            eventList.Add(model);
            return RedirectToAction("List");
        }

        return View(model);

    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var eventItem = eventList.Find(e => e.Id == id);
        if (eventItem == null)
        {
            return NotFound();
        }
        return View(eventItem);
    }

    [HttpPost]
    public IActionResult Edit(EventModel model)

    {
        var eventItem = eventList.Find(e => e.Id == model.Id);
        if (eventItem != null)
        {
            eventItem.Title = model.Title;
            eventItem.Description = model.Description;
            eventItem.Date = model.Date;
            return RedirectToAction("List");
        }

        return NotFound();
    }

    public IActionResult Delete(int id)
    {
        var eventItem = eventList.Find(a => a.Id == id);
        if (eventItem != null)
        {
            eventList.Remove(eventItem);
            return RedirectToAction("List");
        }

        return NotFound();
    }







}