namespace TicketPurchaseAPI.Dto.Payment
{
    public class WithdrawDto
    {
        public string account_bank {  get; set; }
        public string account_number { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string narration { get; set; }

    }
}
