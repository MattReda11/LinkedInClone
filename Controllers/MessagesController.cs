using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkedInClone.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LinkedInClone.Controllers
{
    public class MessagesController : Controller
    {
        private readonly AppDbContext _context;

        private readonly ILogger<MessagesController> _logger;

        public MessagesController(ILogger<MessagesController> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ActionResult> Conversations()
        {
            var username = User.Identity.Name;
            var user = _context.Users.Where(u => u.UserName == username).FirstOrDefault();

            return View(await _context.Messages.Where(m => m.SentBy == user || m.ReceivedUser == user).Include("ReceivedUser").Include("SentBy").ToListAsync());
        }

    }
}