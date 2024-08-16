using BAL.DTOs;
using BAL.Utilities;
using BAL.Validations;
using DAL.Entities;
using DAL.Repository;
using Shared.Enums;

namespace BAL.Services
{
    public class RestaurantTableBookingServices
    {
        private readonly RestaurantTableRepository _restaurantTableRepository;

        public RestaurantTableBookingServices()
        {
            _restaurantTableRepository = new();
        }

        public async Task<List<RestaurantTableBookingDTO>> GetAllAsync()
        {
            var bookings = await _restaurantTableRepository.GetAllAsync();

            return bookings.Select(x => new RestaurantTableBookingDTO
            {
                Id = x.id,
                BookingDate = x.BookingDate,
                CustomerName = x.CustomerName,
                NoOfMembers = x.NoOfMembers,
                Email = x.Email,
                Phone = x.Phone,
                OccassionType = x.OccassionType,
                BookingTime = x.BookingTime,
                Discount = x.Discount,
                PaymentMode = x.PaymentMode,
                CouponCode = x.CouponCode,
                Status = x.Status,
                NoOfTable = x.NoOfTable
            }).ToList();
        }

        public async Task<RestaurantTableBooking> CreateBookingAsync(RestaurantTableBookingDTO booking)
        {
            int discount = 0;
            List<string> couponsList = (await _restaurantTableRepository.GetAllAsync()).Select(x => x.CouponCode).ToList();
            if (TableBookingValidations.IsValidCoupon(booking.CouponCode) && !couponsList.Contains(booking.CouponCode))
            {
                if (booking.PaymentMode == (TableBookingEnums.PaymentMode)1)
                {
                    discount = 10;
                }
                else if (booking.PaymentMode == (TableBookingEnums.PaymentMode)2)
                {
                    discount = 5;
                }
                else if (booking.PaymentMode == (TableBookingEnums.PaymentMode)3)
                {
                    discount = 2;
                }
            }

            var bookingEntity = new RestaurantTableBooking
            {
                BookingDate = booking.BookingDate,
                CustomerName = booking.CustomerName,
                NoOfMembers = booking.NoOfMembers,
                Email = booking.Email,
                Phone = booking.Phone,
                OccassionType = booking.OccassionType,
                BookingTime = booking.BookingTime,
                Discount = $"{discount}%",
                PaymentMode = booking.PaymentMode,
                CouponCode = booking.CouponCode,
                Status = booking.Status,
                NoOfTable = ((booking.NoOfMembers / 6) + 1)
            };
            await _restaurantTableRepository.CreateBookingAsync(bookingEntity);
            await SendBookingConfirmationEmail.SendEmailAsync(booking.Email, ((booking.NoOfMembers / 6) + 1).ToString(), (booking.OccassionType).ToString(), (booking.PaymentMode).ToString(), $"{discount}%", (booking.BookingTime).ToString());
            return bookingEntity;
        }

        public async Task<List<RestaurantTableBookingDTO>> ShowBookingsAsync()
        {
            var bookings = (await _restaurantTableRepository.GetAllAsync()).ToList();

            return bookings.Select(x => new RestaurantTableBookingDTO
            {
                Id = x.id,
                BookingDate = x.BookingDate,
                CustomerName = x.CustomerName.Length > 20 ? string.Concat(x.CustomerName.AsSpan(0, 20), "...") : x.CustomerName,
                NoOfMembers = x.NoOfMembers,
                Email = x.Email.Length > 20 ? string.Concat(x.Email.AsSpan(0, 20), "...") : x.Email,
                Phone = x.Phone,
                OccassionType = x.OccassionType,
                BookingTime = x.BookingTime,
                Discount = x.Discount,
                PaymentMode = x.PaymentMode,
                CouponCode = x.CouponCode,
                Status = x.Status,
                NoOfTable = x.NoOfTable
            }).ToList();
        }

        public async Task<RestaurantTableBookingDTO> CancelBookingAsync(RestaurantTableBookingDTO toCancelDto)
        {
            await _restaurantTableRepository.CancelBookingAsync(toCancelDto.Id);
            return toCancelDto;
        }

        public async Task<RestaurantTableBookingDTO> UpdateOccassionTypeAsync(RestaurantTableBookingDTO toUpdateOccassionDto, TableBookingEnums.OccassionType occassion)
        {
            await _restaurantTableRepository.UpdateOccassionTypeAsync(toUpdateOccassionDto.Id, occassion);
            return toUpdateOccassionDto;
        }

        public async Task<RestaurantTableBookingDTO> UpdateNumberOfMembersAsync(RestaurantTableBookingDTO toUpdateNumberOfMembersDto, int newNumberOfMembers, int newNumberOfTables)
        {
            await _restaurantTableRepository.UpdateNumberOfMembersAsync(toUpdateNumberOfMembersDto.Id, newNumberOfMembers, newNumberOfTables);
            return toUpdateNumberOfMembersDto;
        }
    }
}
