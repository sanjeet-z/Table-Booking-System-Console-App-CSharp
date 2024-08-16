using BAL.DTOs;
using BAL.Services;
using BAL.Validations;
using ConsoleTables;
using Shared.Constants;
using Shared.Enums;

namespace RestaurantTableBookingUI
{
    public class Program
    {
        static async Task Main(string[] args)
        { 
            await HomePageAsync();
        }

        public static async Task HomePageAsync()
        {
            string check = string.Empty;
            do
            {
                Console.Write($"\n\tPress 1 to Login \n\tPress 2 to Quit \n\n\tEnter your Choice:  ");
                int.TryParse(Console.ReadLine(), out int choice);

                switch (choice)
                {
                    case 1:
                        await Login.UserLoginAsync();
                        break;
                    case 2:
                        Environment.Exit(0);
                        check = "quit";
                        break;
                    default:
                        await Console.Out.WriteLineAsync("Enter appropriate choice please.");
                        break;
                }
            } while (check != "quit");
        }


        public static void ShowBookings(List<RestaurantTableBookingDTO> bookings)
        {
            var table = new ConsoleTable("Booking Id", "Booking Date", "Customer Name", "No of Members", "Mobile Number", "Occassion Type", "Booking Time", "Discount", "Payment Mode");

            foreach (var booking in bookings)
            {
                table.AddRow(booking.Id, booking.BookingDate, booking.CustomerName, booking.NoOfMembers, booking.Phone, booking.OccassionType, booking.BookingTime, booking.Discount, booking.PaymentMode);
            }
            Console.WriteLine();

            ConsoleTable.From<RestaurantTableBookingDTO>(bookings)
            .Configure(o => o.NumberAlignment = Alignment.Right)
            .Write(Format.Default);
        }

        public static async Task CancelBooking(RestaurantTableBookingServices services)
        {
            await Console.Out.WriteAsync("\n\tEnter the BookingId of Booking which you wanna Cancel: ");
            _ = int.TryParse(Console.ReadLine(), out int idInput);

            bool isPresent = (await services.GetAllAsync()).Where(x => x.Id == idInput).Any();
            
            if (isPresent)
            {
                var toCancelDto = new RestaurantTableBookingDTO
                {
                    Id = idInput,
                };

                await Console.Out.WriteAsync("\n\tAre you sure you want to Cancel the booking? \n\tType \"Y\" for Yes or \"N\" for No:  ");
                string confirmation = Console.ReadLine() ?? string.Empty;
                if (confirmation == "Y" || confirmation == "y")
                {
                    await services.CancelBookingAsync(toCancelDto);
                    await Console.Out.WriteLineAsync($"\n\tThe status of the Booking Id - {idInput} has been set to Cancelled now");
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("\n\tThis Id is not present in the table. You will be redirected to the menu page now.");
            }

        }

        public static async Task UpdateBookingAsync(RestaurantTableBookingServices services)
        {
            await Console.Out.WriteAsync("\n\tEnter the Id you want to update: ");
            _ = int.TryParse(Console.ReadLine(), out int id);
            
            bool isPresent = (await services.GetAllAsync()).Where(x => x.Id == id).Any();

            if (isPresent)
            {
                await UpdateNumberOfMembersAsync(services, id);
                await UpdateOccassionTypeAsync(services, id);
            }
            else
            {
                await Console.Out.WriteLineAsync("\n\tThis Id is not present in the table. You will be redirected to the menu page now.");
            }
        }

        public static async Task UpdateOccassionTypeAsync(RestaurantTableBookingServices services, int idInput)
        {
            bool flag = false;
            var toUpdateOccassionDto = new RestaurantTableBookingDTO
            {
                Id = idInput
            };
            TableBookingEnums.OccassionType occassion = TableBookingEnums.OccassionType.BirthDay;
            while (!flag)
            {
                await Console.Out.WriteAsync($"{RestaurantTableBookingConstants.askOccassion}");
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
                        await Console.Out.WriteLineAsync("\tYou have not entered a valid option.");
                        break;
                }
                flag = TableBookingValidations.IsValidOccassionType(occassionTypes);
            }
            await services.UpdateOccassionTypeAsync (toUpdateOccassionDto, occassion);
        }

        public static async Task UpdateNumberOfMembersAsync(RestaurantTableBookingServices services, int idInput)
        {
            bool flag = false;
            var toUpdateNumberOfMembersDto = new RestaurantTableBookingDTO
            {
                Id = idInput
            };

            int newNumberOfMembers = 0;
            while (!flag)
            {
                await Console.Out.WriteAsync("\n\tEnter the new Number of Members: ");
                _ = int.TryParse(Console.ReadLine(), out newNumberOfMembers);
                flag = TableBookingValidations.IsValidNumber(newNumberOfMembers, 1, 28);
                if (!flag) { Console.WriteLine("\tInvalid input. Please enter Number of Members again."); }
            }
            int newNumberOfTables = ((newNumberOfMembers / 6) + 1);

            await services.UpdateNumberOfMembersAsync(toUpdateNumberOfMembersDto, newNumberOfMembers, newNumberOfTables);
        }
    }
}