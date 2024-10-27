using Microsoft.AspNetCore.Identity;
using TicketPurchaseAPI.Data;
using TicketPurchaseAPI.Interface;
using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        public PaymentRepository(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<TicketPayment> CreatePaymentAsync(string user, int ticketId, decimal amount)
        {

            var appUser = await _userManager.FindByNameAsync(user);
            var newPayment = new TicketPayment
            {
                PaymentStatus = TicketPayment.Status.Pending,
                CreatedAt = DateTime.UtcNow,
                UserId = appUser.Id,
                TicketId = ticketId,
                Amount = amount
            };

            await _context.AddAsync(newPayment);
            await _context.SaveChangesAsync();
            return newPayment;
        }

        public Task<TicketPayment> GetPaymentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TicketPayment>> GetPaymentsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
