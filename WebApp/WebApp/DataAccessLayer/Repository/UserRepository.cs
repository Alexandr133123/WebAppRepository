using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.DB;
using WebApp.DataAccessLayer.IRepository;
using WebApp.DataAccessLayer.Model;

namespace WebApp.DataAccessLayer.Repository
{
    public class UserRepository: IUserRepository
    {
        private ApplicationContext db;

        public UserRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public List<Users> GetAllUsers()
        {
            List<Users> users = db.User.ToList();
            foreach(Users user in users)
            {
                byte[] passwordB64 = System.Convert.FromBase64String(user.Password);
                user.Password = System.Text.ASCIIEncoding.ASCII.GetString(passwordB64);
            }
            return users;
        }
        public void CreateUser(string username, string password)
        {
            Users user = new Users();
            user.Login = username;
            byte[] passwordBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(password);
            user.Password = System.Convert.ToBase64String(passwordBytes);
            db.Add(user);
            db.SaveChanges();
        }
    }
}
