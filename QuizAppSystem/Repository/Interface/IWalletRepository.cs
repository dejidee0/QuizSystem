// IWalletRepository.cs
using System.Threading.Tasks;
using QuizAppSystem.Models;

namespace QuizAppSystem.Repository.Interface
{
    public interface IWalletRepository
    {
        Task<Wallet> GetWalletByIdAsync(string walletId);
        Task CreateWalletAsync(Wallet wallet);
        Task UpdateWalletAsync(Wallet wallet);
        Task DeleteWalletAsync(string walletId);
    }
}
