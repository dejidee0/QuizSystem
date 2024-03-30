// ParticipantService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using QuizAppSystem.DTO;
using QuizAppSystem.Models;
using QuizAppSystem.Repository.Interface;
using QuizAppSystem.Service.Interface;

namespace QuizAppSystem.Service.Implementation
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IWalletService _walletService;

        public ParticipantService(IParticipantRepository participantRepository, IWalletService walletService)
        {
            _participantRepository = participantRepository;
            _walletService = walletService;
        }

        public IEnumerable<ParticipantDTO> GetAllParticipants()
        {
            var participants = _participantRepository.GetAllParticipants();
            return participants.Select(MapToDTO);
        }

        public ParticipantDTO GetParticipantById(Guid participantId)
        {
            var participant = _participantRepository.GetParticipantById(participantId);
            return participant != null ? MapToDTO(participant) : null;
        }

        public void CreateParticipant(ParticipantDTO participant)
        {
            _participantRepository.CreateParticipant(MapToEntity(participant));
        }

        public void UpdateParticipant(Guid participantId, ParticipantDTO updatedParticipant)
        {
            _participantRepository.UpdateParticipant(participantId, MapToEntity(updatedParticipant));
        }

        public void DeleteParticipant(Guid participantId)
        {
            _participantRepository.DeleteParticipant(participantId);
        }

        private ParticipantDTO MapToDTO(Participant participant)
        {
            return new ParticipantDTO
            {
                Id = participant.Id,
                FirstName = participant.FirstName,
                LastName = participant.LastName,
                Email = participant.Email,
                RegistrationDate = participant.RegistrationDate,
                StartedAt = participant.StartedAt,
                CompletedAt = participant.CompletedAt,
                IsSuccessful = participant.IsSuccessful,
                IsCompleted = participant.IsCompleted,
                PIN = participant.PIN
            };
        }

        private Participant MapToEntity(ParticipantDTO participantDTO)
        {
            return new Participant
            {
                Id = participantDTO.Id,
                FirstName = participantDTO.FirstName,
                LastName = participantDTO.LastName,
                Email = participantDTO.Email,
                RegistrationDate = participantDTO.RegistrationDate,
                StartedAt = participantDTO.StartedAt,
                CompletedAt = participantDTO.CompletedAt,
                IsSuccessful = participantDTO.IsSuccessful,
                IsCompleted = participantDTO.IsCompleted,
                PIN = participantDTO.PIN
            };
        }
    }
}
