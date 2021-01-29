using System.Collections.Generic;
using SOATApiReact.Model;

namespace SOATApiReact.Data
{
    public interface IUserRepo
    {
        bool SaveChanges();
        IEnumerable<User> GetAllUsers();
        void CreateUser(User user);
        User GetUserByDocument(int document);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}