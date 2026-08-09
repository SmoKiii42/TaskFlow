using System.Net.Mail;
using TaskFlow.Core;
using TaskFlow.Data;
using TaskFlow.Models;

namespace TaskFlow.Новая_папка1.TaskFlow
{
    internal class ServisUser
    {
        private readonly AppDbContext _context;
        public ServisUser()
        {
            _context = new AppDbContext();
        }


        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public bool UserExists()
        {
            return _context.Users.Any();

        }
         
        public bool EmailExists(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }

        public void DeleteUser(int userId)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null) return;

            _context.Users.Remove(user);
            _context.SaveChanges();
        }


        public bool CheckEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
        public bool CorrectEmail(string email)
        {
            try
            {
                MailAddress address = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool CheckPassword(string password)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Password == password);

            if (user == null)
            {
                return false;
            }
            else
                return user.Password == password;
        }


        public User? Login(string email, string password)
        {
            User? user = _context.Users
                .FirstOrDefault(u => u.Email == email);

            if (user == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            CurrentSession.CurrentUser = user;

            return user;
        }

    }
}
