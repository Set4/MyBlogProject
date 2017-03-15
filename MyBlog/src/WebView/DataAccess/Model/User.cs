using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlog.DataAccess.Model
{

    public class User:ModelBase
    {
        [Required, MaxLength(20)]
        public string UserName { get; set; }

        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
      
        public DateTime DateRegistration { get; set; }

        public StateUser State { get; set; }

        public List<RoleCollection> Role { get; set; }

        public List<PrizeCollection> Prize { get; set; }

        public string ImagePath { get; set; }

        public Security Security { get; set; }

        public Access Access { get; set; }

        public UserSettings Settings { get; set; }
    }

    public enum StateUser
    {
        Public = 0,
        Removed = 1,
        Blocked = 2
    }


    public class Security: ModelBase
    {
        string _password;
        [Required]
        public string Password
        { get
            {
                return Other.HashGenerator.GetSha256(_password);
            }

            set
            {
                _password = value;
            }
        }
    }

    public class Access: ModelBase
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string AUTHToken { get; set; }
    }
}
