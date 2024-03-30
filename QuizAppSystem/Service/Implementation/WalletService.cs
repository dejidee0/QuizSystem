// WalletService.cs
using System;
using System.Threading.Tasks;
using AutoMapper;
using QuizAppSystem.DTO;
using QuizAppSystem.Models;
using QuizAppSystem.Repository.Interface;
using QuizAppSystem.Service.Interface;

namespace QuizAppSystem.Service.Implementation
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;

        public WalletService(IWalletRepository walletRepository, IMapper mapper)
        {
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<WalletDTO> GetWalletByIdAsync(string walletId)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(walletId);
            return wallet != null ? _mapper.Map<WalletDTO>(wallet) : null;
        }

        public async Task CreateWalletAsync(WalletDTO walletDTO)
        {
            var wallet = _mapper.Map<Wallet>(walletDTO);
            wallet.WalletId = GenerateWalletId(walletDTO.ParticipantId);
            await _walletRepository.CreateWalletAsync(wallet);
        }

        public async Task UpdateWalletAsync(string walletId, WalletDTO updatedWalletDTO)
        {
            var existingWallet = await _walletRepository.GetWalletByIdAsync(walletId);
            if (existingWallet != null)
            {
                _mapper.Map(updatedWalletDTO, existingWallet);
                await _walletRepository.UpdateWalletAsync(existingWallet);
            }
        }

        public async Task DeleteWalletAsync(string walletId)
        {
            await _walletRepository.DeleteWalletAsync(walletId);
        }

        private string GenerateWalletId(string participantId)
        {
            
            return $"{participantId}_{DateTime.UtcNow:yyyyMMddHHmmss}";
        }
    }
}
