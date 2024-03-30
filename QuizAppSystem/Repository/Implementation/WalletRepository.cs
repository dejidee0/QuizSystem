// WalletRepository.cs
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizAppSystem.Models;
using QuizAppSystem.Repository.Interface;

namespace QuizAppSystem.Repository.Implementation
{
    public class WalletRepository : IWalletRepository
    {
        private readonly QuizAppDbContext _context;

        public WalletRepository(QuizAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Wallet> GetWalletByIdAsync(string walletId)
        {
            return await _context.Wallets.FirstOrDefaultAsync(w => w.WalletId == walletId);
        }

        public async Task CreateWalletAsync(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWalletAsync(Wallet wallet)
        {
            _context.Entry(wallet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWalletAsync(string walletId)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.WalletId == walletId);
            if (wallet != null)
            {
                _context.Wallets.Remove(wallet);
                await _context.SaveChangesAsync();
            }
        }
    }
}
