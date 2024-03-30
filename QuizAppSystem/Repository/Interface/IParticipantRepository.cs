using System;
using System.Collections.Generic;
using QuizAppSystem.Models;

namespace QuizAppSystem.Repository.Interface
{
    public interface IParticipantRepository
    {
        IEnumerable<Participant> GetAllParticipants();
        Participant GetParticipantById(Guid participantId);
        void CreateParticipant(Participant participant);
        void UpdateParticipant(Guid participantId, Participant updatedParticipant);
        void DeleteParticipant(Guid participantId);
    }
}
