using Shared.Enums;

namespace BAL.DTOs
{
    public class RestaurantTableBookingDTO
    {
        public int Id { get; set; }
        public string BookingDate { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public int NoOfMembers { get; set; }
        public string Email { get; set; } = string.Empty;
        public long Phone { get; set; }
        public TableBookingEnums.OccassionType OccassionType { get; set; }
        public TableBookingEnums.BookingTime BookingTime { get; set; }
        public TableBookingEnums.PaymentMode PaymentMode { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public string Discount { get; set; } = string.Empty;
        public TableBookingEnums.Status Status { get; set; }
        public int NoOfTable { get; set; }
    }
}
