using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;

namespace DAL.Repository
{
    public class RestaurantTableRepository
    {
        private readonly RestaurantContext _context;
        public RestaurantTableRepository()
        {
            _context = new RestaurantContext();
        }

        public async Task<List<RestaurantTableBooking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<bool> CreateBookingAsync(RestaurantTableBooking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelBookingAsync(int idInput)
        {
            var booking = await _context.Bookings.FindAsync(idInput);
            if (booking == null)
            {
                return false;
            }

            booking.Status = (TableBookingEnums.Status)(TableBookingEnums.OccassionType)2;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOccassionTypeAsync(int idInput, TableBookingEnums.OccassionType occassion)
        {
            var booking = await _context.Bookings.FindAsync(idInput);
            if (booking == null)
            {
                return false;
            }

            booking.OccassionType = occassion;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateNumberOfMembersAsync(int idInput, int newNumberOfMembers, int newNumberOfTables)
        {
            var booking = await _context.Bookings.FindAsync(idInput);
            if (booking == null)
            {
                return false;
            }
            
            booking.NoOfMembers = newNumberOfMembers;
            booking.NoOfTable = newNumberOfTables;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
