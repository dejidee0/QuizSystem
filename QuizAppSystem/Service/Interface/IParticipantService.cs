// IParticipantService.cs
using System;
using System.Collections.Generic;
using QuizAppSystem.DTO;

namespace QuizAppSystem.Service.Interface
{
    public interface IParticipantService
    {
        IEnumerable<ParticipantDTO> GetAllParticipants();
        ParticipantDTO GetParticipantById(Guid participantId);
        void CreateParticipant(ParticipantDTO participantDTO);
        void UpdateParticipant(Guid id, ParticipantDTO updatedParticipantDTO);
        void DeleteParticipant(Guid participantId);

    }
}
