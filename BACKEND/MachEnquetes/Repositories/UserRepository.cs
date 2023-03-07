using MachEnquetes.Entities;
using MachEnquetes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MachEnquetes.Repositories
{
    public class UserRepository : Controller
    {
        private MachEnquetesContext MachEnquetesContext;

        public UserRepository(MachEnquetesContext machEnquetesContext)
        {
            this.MachEnquetesContext = machEnquetesContext;
        }

        public async Task<ObjectResult> GetAll()
        {
            try
            {
                var objects = await MachEnquetesContext.Users.ToListAsync();
                if (objects.Count == 0)
                    return StatusCode(204, objects);

                return StatusCode(200, objects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public async Task<ObjectResult> GetById(int id)
        {
            try
            {
                var users = await MachEnquetesContext.Users.Where(s => s.Id == id).ToListAsync();
                if (users == null || users.Count == 0)
                    return StatusCode(404, users);

                return StatusCode(200, users.First());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public async Task<ObjectResult> GetByEmail(string email)
        {
            try
            {
                var user = MachEnquetesContext.Users.Where(s => s.Email == email);
                if (user == null || user.Count() == 0)
                    return StatusCode(404, user);

                return StatusCode(200, user.First());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public async Task<ObjectResult> Create(User user)
        {
            try
            {
                var foundUser = await GetByEmail(user.Email);
                if (foundUser.StatusCode == 404)
                {
                    MachEnquetesContext.Users.AddAsync(user);
                    await MachEnquetesContext.SaveChangesAsync();
                    return StatusCode(201, user);
                }

                return StatusCode(406, new { Message = "Already exists " });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public async Task<ObjectResult> Update(int id, User updatedUser)
        {
            try
            {
                var foundUser = MachEnquetesContext.Users.Where(s => s.Id == id);

                if (foundUser == null || foundUser.Count() == 0)
                    return StatusCode(404, null);

                var user = foundUser.First();
                user.FullName = updatedUser.FullName;
                user.Password = updatedUser.Password;
                user.DateBirth = updatedUser.DateBirth;
                user.LastModifiedDate = DateTime.UtcNow;
                MachEnquetesContext.Users.Update(user);
                await MachEnquetesContext.SaveChangesAsync();

                return StatusCode(200, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public async Task<ObjectResult> DeleteById(int id)
        {
            try
            {
                var foundUser = await GetById(id);
                if (foundUser.StatusCode == 200)
                {
                    _ = MachEnquetesContext.Users.Remove((User)foundUser.Value);
                    await MachEnquetesContext.SaveChangesAsync();

                    return StatusCode(200, null);
                }

                return StatusCode(404, new { Message = "User not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public async Task<ObjectResult> DeleteAll()
        {
            try
            {
                var users = MachEnquetesContext.Users;
                foreach (var item in users)
                {
                    MachEnquetesContext.Users.Remove(item);
                };
                await MachEnquetesContext.SaveChangesAsync();

                return StatusCode(204, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public async Task<ObjectResult> Login(User user)
        {
            var foundUser = MachEnquetesContext.Users.Where(s => s.Email == user.Email && s.Password == user.Password);

            if (foundUser == null || foundUser.Count() == 0)
                return StatusCode(404, user);

            return StatusCode(200, user);
        }

    }
}
