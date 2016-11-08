using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelCore
{
    public class AuthorizationLogic
    {
        int numberAttempts;
        string CAPCTHA;

        DataBaseContext db;

        public AuthorizationLogic(DataBaseContext dbContext)
        {
            db = dbContext;
            numberAttempts = 0;
        }

        public string  Authorization()
        {

        }


        private bool Proverka(string email, string password, string capctha = null)
        {
            //
            if (numberAttempts >= 3)
            {
                //proverka capchi
                return false;
            }

            //poisk emal
            //naiden da/net
            //vozvrashaer iduser

            //poisk po idUser i proverka parola
        }


        private string GetAuthToken(int idUser)
        {
            //generachia i save v bd token`a

        }

        public User Autondification(string AuthToken)
        {
            //poisk po topkeny idUsera-> pousk usera po id
        }
    }
}
