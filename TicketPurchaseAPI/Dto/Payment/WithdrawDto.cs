namespace TicketPurchaseAPI.Dto.Payment
{
    public class WithdrawDto
    {
        public string account_bank {  get; set; }
        public string account_number { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
        public string narration { get; set; }

    }
}
