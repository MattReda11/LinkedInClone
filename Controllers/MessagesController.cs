using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkedInClone.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using LinkedInClone.Models;

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
            var user = _context.AppUsers.Where(u => u.UserName == username).FirstOrDefault();

            return View(await _context.Conversations.Where(c => c.StartedBy == user || c.ReceivedBy == user).Include("ReceivedBy").Include("StartedBy").Include("Messages").Include("Messages.SentBy").Include("Messages.ReceivedBy").ToListAsync());
        }

        public IActionResult Create()
        {
            Conversation conversation = new Conversation();
            return PartialView("_NewConversation", conversation);
        }

        public async Task<IActionResult> CreateNewConvo(string Receiver)
        {
            var username = User.Identity.Name;
            var user = _context.AppUsers.Where(u => u.UserName == username).FirstOrDefault();
            var user2 = _context.AppUsers.Where(u => u.FullName == Receiver).FirstOrDefault();

            if (user2 == null)
            {
                TempData["generalInfo"] = $"Sorry {Receiver} does not exist as a user.";
                return RedirectToAction(nameof(Conversations));
            }

            var connection = _context.Connections.Where(c => c.AccountOwner == user && c.Friend == user2 && c.Accepted == true).FirstOrDefault();

            var connection2 = _context.Connections.Where(c => c.AccountOwner == user2 && c.Friend == user && c.Accepted == true).FirstOrDefault();

            if (connection == null && connection2 == null)
            {
                TempData["generalInfo"] = $"Sorry you and {Receiver} are not connected, you cannot start a conversation with them!";
                return RedirectToAction(nameof(Conversations));
            }

            var conversation = new Conversation { StartedBy = user, ReceivedBy = user2, CreatedDate = DateTime.Now };

            var checkConversation = _context.Conversations.Where(c => c.StartedBy == user && c.ReceivedBy == user2).FirstOrDefault();
            var checkConversation2 = _context.Conversations.Where(c => c.StartedBy == user2 && c.ReceivedBy == user).FirstOrDefault();

            if (checkConversation != null || checkConversation2 != null)
            {
                TempData["generalInfo"] = $"Conversation with {Receiver} already exists.";

                return RedirectToAction(nameof(Conversations));
            }
            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();

            TempData["generalInfo"] = $"Conversation with {Receiver} has been started.";

            return RedirectToAction(nameof(Conversations));
        }

        public async Task<IActionResult> AddConversationRecruiter(string id)
        {
            var username = User.Identity.Name;
            var user = _context.Recruiters.Where(u => u.UserName == username).FirstOrDefault();
            var user2 = _context.AppUsers.Find(id);

            var conversation = new Conversation { StartedBy = user, ReceivedBy = user2, CreatedDate = DateTime.Now };

            var checkConversation = _context.Conversations.Where(c => c.StartedBy == user && c.ReceivedBy == user2).FirstOrDefault();
            var checkConversation2 = _context.Conversations.Where(c => c.StartedBy == user2 && c.ReceivedBy == user).FirstOrDefault();

            if (checkConversation != null || checkConversation2 != null)
            {
                TempData["generalInfo"] = $"Conversation with {user2.FullName} already exists.";

                return RedirectToAction(nameof(Conversations));
            }

            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();


            TempData["generalInfo"] = $"Conversation with {user2.FullName} has been started.";

            return RedirectToAction(nameof(Conversations));
        }

        public IActionResult Message(int id)
        {
            Conversation conversation = _context.Conversations.Where(c => c.Id == id).Include("StartedBy").Include("ReceivedBy").Include("Messages").Include("Messages.SentBy").Include("Messages.ReceivedBy").FirstOrDefault();

            return PartialView("_Messages", conversation);
        }

        public async Task<IActionResult> NewMessage(string id, string MessageContent, int conversationId)
        {
            var username = User.Identity.Name;
            var user = _context.AppUsers.Where(u => u.UserName == username).FirstOrDefault();
            var user2 = _context.AppUsers.Find(id);

            var conversation = _context.Conversations.Find(conversationId);

            var newMessage = new Message { Content = MessageContent, SentBy = user, ReceivedBy = user2, Conversation = conversation, SentDate = DateTime.Now };

            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Conversations));
        }

    }
}