using BAL.DTOs;
using BAL.Services;
using BAL.Utilities;
using Shared.Constants;

namespace RestaurantTableBookingUI
{
    public class Login
    {
        private static readonly UsersServices _usersServices = new ();
        private static readonly RestaurantTableBookingServices _services = new ();

        public static async Task UserLoginAsync()
        {
            List<string> emailList = (await _usersServices.GetAllAsync()).Select(x => x.Email).ToList();
            List<string> passwordList = (await _usersServices.GetAllAsync()).Select(x => x.Password).ToList();

            Console.Write($"\n\t{UserConstants.authenticateEmail}");
            string userName = Console.ReadLine() ?? string.Empty;

            if (emailList.Contains(userName))
            {
                int counter = 0;
                int index = emailList.IndexOf(userName);
                while (counter != 2)
                {
                    Console.Write($"\t{UserConstants.authenticatePassword}");
                    string password = Console.ReadLine() ?? string.Empty;

                    if (passwordList[index] == password)
                    {
                        Console.WriteLine("\n\tAuthenticated!!\n");
                        await ShowMenu(_services);
                    }
                    else
                    {
                        Console.WriteLine("\n\tPassword Incorrect!!");
                        counter++;

                        if (counter == 2)
                        {
                            await Console.Out.WriteLineAsync($"\tYou have entered wrong password for {counter} times\n\tYou will be sent to Login Page again.");
                            await SendWarningEmail.SendEmailAsync();
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("\n\tUsername Incorrect!!");
                await UserLoginAsync();
            }
        }

        public static async Task ShowMenu(RestaurantTableBookingServices _services)
        {

            string check = string.Empty;
            do
            {
                Console.WriteLine("\n\t1 - Booking \n\t2 - Update \n\t3 - View All Booking \n\t4 - Cancel Booking \n\t5 - Go Back");

                Console.Write("\n\tEnter your choice: ");
                int.TryParse(Console.ReadLine(), out int choice);

                switch (choice)
                {
                    case 1:
                        await CreateBooking.CreateBookingAsync(_services);
                        break;
                    case 2:
                        await Program.UpdateBookingAsync(_services);
                        break;
                    case 3:
                        await Console.Out.WriteAsync("\n\tEnter the Date ");
                        string inputDate = Console.ReadLine() ?? string.Empty;
                        List<RestaurantTableBookingDTO> bookings = (await _services.ShowBookingsAsync()).Where(x => x.BookingDate == inputDate).ToList();
                        Program.ShowBookings(bookings);
                        break;
                    case 4:
                        await Program.CancelBooking(_services);
                        break;
                    case 5:
                        await Program.HomePageAsync();
                        check = "quit";
                        break;
                } 
            } while (check != "quit");
        }
    }
}
