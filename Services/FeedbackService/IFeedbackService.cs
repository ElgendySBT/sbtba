using SBTBackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Services.FeedbackService
{
    public interface IFeedbackService
    {
        Task<IReadOnlyList<FeedbackMessage>> GetFeedbackMessage();
        Task<FeedbackMessage> GetFeedbackMessage(int id);
        Task<FeedbackMessage> UpdateFeedbackMessage(FeedbackMessage feedbackMessage);
        Task<FeedbackMessage> CreateFeedbackMessage(FeedbackMessage feedbackMessage);
        Task<bool> DeleteFeedbackMessage(int id);
    }
}
