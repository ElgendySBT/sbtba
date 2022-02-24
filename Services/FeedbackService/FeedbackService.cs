using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SBTBackEnd.Data;
using SBTBackEnd.Entities;
using SBTBackEnd.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly DataContext _context;

        public FeedbackService(DataContext context)
        {
            _context = context;
        }

        public async Task<FeedbackMessage> CreateFeedbackMessage(FeedbackMessage feedbackMessage)
        {
            var message = new FeedbackMessage
            {
                Message = feedbackMessage.Message
            };
                await _context.FeedbackMessages.AddAsync(feedbackMessage);
                await _context.SaveChangesAsync();

            return message;
                
        }

        public async Task<bool> DeleteFeedbackMessage(int id)
        {
            var feedbackMessage = await _context.FeedbackMessages.FindAsync(id);
            if (feedbackMessage == null)
            {
                return false;
            }

             _context.FeedbackMessages.Remove(feedbackMessage);
            await _context.SaveChangesAsync();

            return true ;
        }

        public async Task<IReadOnlyList<FeedbackMessage>> GetFeedbackMessage()
        {
            return await _context.FeedbackMessages.ToListAsync();
        }

        public async Task<FeedbackMessage> GetFeedbackMessage(int id)
        {
            var feedbackMessage = await _context.FeedbackMessages.FindAsync(id);

            if (feedbackMessage == null)
            {
                return null;
            }

            return feedbackMessage;
        }

        public async Task<FeedbackMessage> UpdateFeedbackMessage(FeedbackMessage feedbackMessage)
        {
            var message =  _context.FeedbackMessages.Attach(feedbackMessage);
            message.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return feedbackMessage;

        }

        private bool FeedbackMessageExists(int id)
        {
            return _context.FeedbackMessages.Any(e => e.Id == id);
        }
    }
}
