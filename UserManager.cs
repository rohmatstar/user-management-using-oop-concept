using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class UserManager
    {
        private List<User> users;
        private int nextID;
        private bool isPasswordHidden;

        public UserManager()
        {
            users = new List<User>();
            nextID = 1;
            isPasswordHidden = true;
        }

        public bool CreateUser(string firstName, string lastName, string password)
        {
            if (ValidateUser(firstName, lastName, password))
            {
                int userID = nextID;
                User newUser = new User(userID, firstName, lastName, password);
                users.Add(newUser);
                nextID++;
                return true;
            }
            return false;
        }


        public void ShowUsers()
        {
            Console.WriteLine("==SHOW USER==");

            foreach (User user in users)
            {
                string password = isPasswordHidden ? user.GetHiddenPassword() : user.Password;
                Console.WriteLine("========================");
                Console.WriteLine($"ID          : {user.ID}");
                Console.WriteLine($"Name        : {user.GetName()}");
                Console.WriteLine($"Username    : {user.GetUsername()}");
                Console.WriteLine($"Password    : {password}");
                Console.WriteLine("========================");
            }
        }

        public void SearchUsers(string searchName)
        {   
            foreach (User user in users)
            {
                if (user.FirstName.Contains(searchName, StringComparison.OrdinalIgnoreCase) ||
                    user.LastName.Contains(searchName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("========================");
                    Console.WriteLine($"ID          : {user.ID}");
                    Console.WriteLine($"Name        : {user.GetName()}");
                    Console.WriteLine($"Username    : {user.GetUsername()}");
                    Console.WriteLine($"Password    : {user.Password}");
                    Console.WriteLine("========================");
                }
            }
        }

        public bool LoginUser(string username, string password)
        {
            foreach (User user in users)
            {
                if (user.GetUsername().Equals(username, StringComparison.OrdinalIgnoreCase) &&
                    user.Password.Equals(password))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidatePassword(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }

            bool hasUppercase = false;
            bool hasLowercase = false;
            bool hasSymbol = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUppercase = true;
                }
                else if (char.IsLower(c))
                {
                    hasLowercase = true;
                }
                else if (char.IsNumber(c) || char.IsSymbol(c) || char.IsPunctuation(c))
                {
                    hasSymbol = true;
                }
            }

            return hasUppercase && hasLowercase && hasSymbol;
        }

        private bool ValidateUser(string firstName, string lastName, string password)
        {
            if (firstName.Length < 2 || lastName.Length < 2 || !ValidatePassword(password))
            {
                return false;
            }
            return true;
        }


        public bool UpdateUser(int userID, string firstName, string lastName, string password)
        {
            User user = users.Find(u => u.ID == userID);
            if (user != null)
            {
                if (ValidateUser(firstName, lastName, password))
                {
                    user.UpdateUser(firstName, lastName, password);
                    return true;
                }
            }
            return false;
        }

        public bool DeleteUser(int userID)
        {
            User user = users.Find(u => u.ID == userID);
            if (user != null)
            {
                users.Remove(user);
                return true;
            }
            return false;
        }

        public User GetUserByID(int userID)
        {
            return users.Find(u => u.ID == userID);
        }

        public void TogglePasswordVisibility()
        {
            isPasswordHidden = !isPasswordHidden;
        }



    }
}
