using System;
using System.Collections.Generic;
using System.Linq;
using SOATApiReact.Model;

namespace SOATApiReact.Data
{
    public class UserRepository : IUserRepo
    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }
        public void CreateUser(User user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));
            context.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));
            context.Users.Remove(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        public User GetUserByDocument(int document)
        {
            return this.context.Users.FirstOrDefault(u => u.Document == document);
        }

        public bool SaveChanges()
        {
            return (this.context.SaveChanges() >= 0);
        }

        public void UpdateUser(User user)
        {
            //Nothing
        }
    }
}