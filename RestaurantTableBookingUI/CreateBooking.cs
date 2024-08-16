using BAL.DTOs;
using BAL.Services;
using BAL.Validations;
using Shared.Constants;
using Shared.Enums;

namespace RestaurantTableBookingUI
{
    public class CreateBooking
    {
        public static async Task CreateBookingAsync(RestaurantTableBookingServices services)
        {
            bool flag = false;

            string bookingDate = string.Empty;
            while (!flag)
            {
                await Console.Out.WriteAsync($"\t{RestaurantTableBookingConstants.askDate}");
                bookingDate = Console.ReadLine() ?? string.Empty;
                flag = TableBookingValidations.IsValidDate(bookingDate);
                if (!flag) { Console.WriteLine("\tInvalid input. Please enter Date again."); }
            }
            flag = false;

            string customerName = string.Empty;
            while (!flag)
            {
                await Console.Out.WriteAsync($"\t{RestaurantTableBookingConstants.askName}");
                customerName = Console.ReadLine() ?? string.Empty;
                flag = TableBookingValidations.IsValidName(customerName, 3, 100);
                if (!flag) { Console.WriteLine("\tInvalid input. Please enter Name again."); }
            }
            flag = false;

            int noOfMembers = 0;
            while (!flag)
            {
                await Console.Out.WriteAsync($"\t{RestaurantTableBookingConstants.askNoOfMembers}");
                int.TryParse(Console.ReadLine(), out noOfMembers);
                flag = TableBookingValidations.IsValidNumber(noOfMembers, 1, 28);
                if (!flag) { Console.WriteLine("\tInvalid input. Please enter Number of Members again."); }
            }
            flag = false;

            string customerEmail = string.Empty;
            while (!flag)
            {
                await Console.Out.WriteAsync($"\t{RestaurantTableBookingConstants.askEmail}");
                customerEmail = Console.ReadLine() ?? string.Empty;
                flag = TableBookingValidations.IsValidEmail(customerEmail);
                if (!flag) { Console.WriteLine("\tInvalid input. Please enter Email again."); }
            }
            flag = false;

            await Console.Out.WriteAsync($"\t{RestaurantTableBookingConstants.askPhone}");
            long.TryParse(Console.ReadLine(), out long customerPhone);
            TableBookingValidations.IsValidPhoneNumber(customerPhone);
            if (!TableBookingValidations.IsValidPhoneNumber(customerPhone)) { Console.WriteLine("\tPhone Number must have 10 digits. Not more, not less."); }


            TableBookingEnums.OccassionType occassion = TableBookingEnums.OccassionType.OtherOccassion;
            while (!flag)
            {
                await Console.Out.WriteAsync($"\n\t{RestaurantTableBookingConstants.askOccassion}");
                char occassionTypes = Console.ReadKey().KeyChar;

                switch (occassionTypes)
                {
                    case 'B':
                        occassion = TableBookingEnums.OccassionType.BirthDay;
                        break;
                    case 'b':
                        occassion = TableBookingEnums.OccassionType.BirthDay;
                        break;
                    case 'A':
                        occassion = TableBookingEnums.OccassionType.Anniversary;
                        break;
                    case 'a':
                        occassion = TableBookingEnums.OccassionType.Anniversary;
                        break;
                    case 'R':
                        occassion = TableBookingEnums.OccassionType.Retirement;
                        break;
                    case 'r':
                        occassion = TableBookingEnums.OccassionType.Retirement;
                        break;
                    case 'O':
                        occassion = TableBookingEnums.OccassionType.OtherOccassion;
                        break;
                    case 'o':
                        occassion = TableBookingEnums.OccassionType.OtherOccassion;
                        break;
                    default:
                        await Console.Out.WriteAsync("\n\tEnter a valid Option.");
                        break;
                }
                flag = TableBookingValidations.IsValidOccassionType(occassionTypes);
                if (!flag) { Console.WriteLine("\tInvalid input. Please enter Occassion again."); }
            }
            flag = false;

            TableBookingEnums.BookingTime bookingTime = TableBookingEnums.BookingTime.Breakfast;
            while (!flag)
            {
                await Console.Out.WriteAsync($"\n\t{RestaurantTableBookingConstants.askbookingTime}");
                char bookingTimes = Console.ReadKey().KeyChar;

                switch (bookingTimes)
                {
                    case 'B':
                        bookingTime = TableBookingEnums.BookingTime.Breakfast;
                        break;
                    case 'b':
                        bookingTime = TableBookingEnums.BookingTime.Breakfast;
                        break;
                    case 'L':
                        bookingTime = TableBookingEnums.BookingTime.Lunch;
                        break;
                    case 'l':
                        bookingTime = TableBookingEnums.BookingTime.Lunch;
                        break;
                    case 'D':
                        bookingTime = TableBookingEnums.BookingTime.Dinner;
                        break;
                    case 'd':
                        bookingTime = TableBookingEnums.BookingTime.Dinner;
                        break;
                    default:
                        await Console.Out.WriteLineAsync("\n\tEnter a Valid Option");
                        break;
                }
                flag = TableBookingValidations.IsValidBookingType(bookingTimes);
                if (!flag) { Console.WriteLine("\tInvalid input. Please enter Booking Time again."); }
            }
            flag = false;

            TableBookingEnums.PaymentMode paymentMode = TableBookingEnums.PaymentMode.Cash;
            while (!flag)
            {
                await Console.Out.WriteAsync($"\n\t{RestaurantTableBookingConstants.askpaymentMode}");
                string paymentModes = Console.ReadLine() ?? string.Empty;

                switch (paymentModes)
                {
                    case "C":
                        paymentMode = TableBookingEnums.PaymentMode.Cash;
                        break;
                    case "c":
                        paymentMode = TableBookingEnums.PaymentMode.Cash;
                        break;
                    case "CC":
                        paymentMode = TableBookingEnums.PaymentMode.CreditCard;
                        break;
                    case "cc":
                        paymentMode = TableBookingEnums.PaymentMode.CreditCard;
                        break;
                    case "G":
                        paymentMode = TableBookingEnums.PaymentMode.Gpay;
                        break;
                    case "g":
                        paymentMode = TableBookingEnums.PaymentMode.Gpay;
                        break;
                    default:
                        await Console.Out.WriteLineAsync("\n\tEnter a valid Option.");
                        break;
                }
                flag = TableBookingValidations.IsValidPaymentMode(paymentModes);
                if (!flag) { Console.WriteLine("\tInvalid input. Please enter Payment Mode again."); }
            }
            flag = false;

            await Console.Out.WriteAsync($"\n\t{RestaurantTableBookingConstants.askcouponCode}");
            string couponCodes = Console.ReadLine() ?? string.Empty;
            TableBookingValidations.IsValidCoupon(couponCodes);
            if (!TableBookingValidations.IsValidCoupon(couponCodes)) { Console.WriteLine("\n\tInvalid Coupon."); }

            TableBookingEnums.Status statuss = TableBookingEnums.Status.Booked;
            while (!flag)
            {
                await Console.Out.WriteAsync($"\t{RestaurantTableBookingConstants.askStatus}");
                char status = Console.ReadKey().KeyChar;

                statuss = status switch
                {
                    'B' => TableBookingEnums.Status.Booked,
                    'b' => TableBookingEnums.Status.Booked,
                    'C' => TableBookingEnums.Status.Cancelled,
                    'c' => TableBookingEnums.Status.Cancelled,
                    _ => TableBookingEnums.Status.Booked,
                };
                flag = true;
            }

            var bookingDto = new RestaurantTableBookingDTO
            {
                BookingDate = bookingDate,
                CustomerName = customerName,
                Email = customerEmail,
                Phone = customerPhone,
                NoOfMembers = noOfMembers,
                OccassionType = occassion,
                BookingTime = bookingTime,
                PaymentMode = paymentMode,
                CouponCode = couponCodes,
                Status = statuss,
            };
            await services.CreateBookingAsync(bookingDto);
            await Console.Out.WriteLineAsync("\n\tBooking has been saved. An email is also being sent. \n\tPlease wait for the Email to show up in your mailbox.");
        }
    }
}
