using FoodyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace FoodyAPI.Data
{
    public class DbController
    {
        private static APIContext _context;
        public DbController(APIContext context)
        {
            _context = context;
        }




        public async Task<IEnumerable<User>> GetAllUsers()
        {
            //cause Bazaka told me to use async methods
            string bazaka = "BAZAKA";
            using (StringReader reader = new StringReader(bazaka))
            {
                var result = await reader.ReadToEndAsync();
            }
            List<User> list = new List<User>();

            foreach (var item in _context.Users)
            {
                list.Add(item);
            }
            return list;

        }



        public virtual async Task<User> GetUserById(string id)
        {
            //cause Bazaka told me to use async methods
            string bazaka = "BAZAKA";
            using (StringReader reader = new StringReader(bazaka))
            {
                var result = await reader.ReadToEndAsync();
            }

            var _users = _context.Users
                .Include(user => user.Favourite)
                .Include(user => user.Statistics)
                .ToList();

            var result1 = _users.FirstOrDefault(u => u.ID == id);

            if (result1 != null)
            {
                return result1;
            }
            else { return null; }

        }

        public async Task<User> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> AddProduct(string id, Product product)
        {

            var user1 = await GetUserById(id);
            product.User = user1;

            user1.Favourite.Add(product);

            await _context.SaveChangesAsync();

            
            return user1;
        }


        public async Task<User> RemoveProduct(string id, Product product)
        {
            var found = await GetUserById(id);
            try
            {
                found.Favourite.Remove(product);
            }
            catch
            {

                return null;
            }
            await _context.SaveChangesAsync();
            return found;
        }


        public async Task<User> RemoveUser(string id)
        {
            var user = await GetUserById(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<DayIntake> NewConsume(User user, double callories, DateTime day)
        {
            DayIntake take = new DayIntake();
            take.user = user;
            take.Date = day;
            take.DayCalories = 0;

            return take;
        }

        public async Task<User> ConsumeProduct(string id, double callories, DateTime day)
        {
            var user = await GetUserById(id);
            

            var Intakes = user.Statistics as List<DayIntake>;
            int lenght = Intakes.Count;

            if (lenght == 0)
            {
                var take = await NewConsume(user, callories, day);
                take.DayCalories += callories;
                user.Statistics.Add(take);
                
            }
            else if (lenght > 0)
            {
                if (Intakes[lenght-1].Date != day)
                {
                    var take = await NewConsume(user, callories, day);
                    take.DayCalories += callories;
                    user.Statistics.Add(take);
                }
                else if (Intakes[lenght - 1].Date == day)
                {
                    var intake = Intakes[lenght - 1];
                    intake.DayCalories += callories;
                }

            }


            await _context.SaveChangesAsync();

            return user;
        }
    }
}
