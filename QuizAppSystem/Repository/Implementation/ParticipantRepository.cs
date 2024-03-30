using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuizAppSystem.Models;
using QuizAppSystem.Repository.Interface;

namespace QuizAppSystem.Repository.Implementation
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly QuizAppDbContext _dbContext;

        public ParticipantRepository(QuizAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Participant> GetAllParticipants()
        {
            return _dbContext.Participants.ToList();
        }

        public Participant GetParticipantById(Guid participantId)
        {
            return _dbContext.Participants.Find(participantId);
        }

        public void CreateParticipant(Participant participant)
        {
            _dbContext.Participants.Add(participant);
            _dbContext.SaveChanges();
        }

        public void UpdateParticipant(Guid participantId, Participant updatedParticipant)
        {
            var participant = _dbContext.Participants.Find(participantId);

            if (participant != null && updatedParticipant != null)
            {
                // Update participant properties
                participant.FirstName = updatedParticipant.FirstName;
                participant.LastName = updatedParticipant.LastName;
                participant.Email = updatedParticipant.Email;
                participant.RegistrationDate = updatedParticipant.RegistrationDate;
                participant.StartedAt = updatedParticipant.StartedAt;
                participant.CompletedAt = updatedParticipant.CompletedAt;
                participant.IsSuccessful = updatedParticipant.IsSuccessful;
                participant.IsCompleted = updatedParticipant.IsCompleted;
                participant.PIN = updatedParticipant.PIN;

                _dbContext.SaveChanges();
            }
        }

        public void DeleteParticipant(Guid participantId)
        {
            var participant = _dbContext.Participants.Find(participantId);

            if (participant != null)
            {
                _dbContext.Participants.Remove(participant);
                _dbContext.SaveChanges();
            }
        }
    }
}
