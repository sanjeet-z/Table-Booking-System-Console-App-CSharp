namespace Shared.Enums
{
    public class TableBookingEnums
    {
        public enum OccassionType
        {
            BirthDay = 1,
            Anniversary,
            Retirement,
            OtherOccassion
        }

        public enum BookingTime
        {
            Breakfast = 1,
            Lunch,
            Dinner
        }

        public enum PaymentMode
        {
            Cash = 1,
            CreditCard,
            Gpay
        }

        public enum Status
        {
            Booked = 1,
            Cancelled
        }
    }
}
