using System;
using System.Collections.Generic;
using QuizAppSystem.Models;
using QuizAppSystem.Models.Enums;
using System.Threading.Tasks;
using QuizAppSystem.DTO;

namespace QuizAppSystem.Service.Interface
{
    public interface IWalletService
    {
        Task<WalletDTO> GetWalletByIdAsync(string walletId);
        Task CreateWalletAsync(WalletDTO walletDTO);
        Task UpdateWalletAsync(string walletId, WalletDTO updatedWalletDTO);
        Task DeleteWalletAsync(string walletId);


    }
}
