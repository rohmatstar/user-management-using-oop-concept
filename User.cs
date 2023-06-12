using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class User
    {
        public int ID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Password { get; private set; }

        public User(int id, string firstName, string lastName, string password)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

        public string GetName()
        {
            return $"{FirstName} {LastName}";
        }

        public string GetUsername()
        {
            return $"{FirstName.Substring(0, 2)}{LastName.Substring(0, 2)}";
        }

        public string GetHiddenPassword()
        {
            return new string('*', Password.Length);
        }

        public void UpdateUser(string firstName, string lastName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

    }

}
