using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace project
{
    public class ShopMembershipProvider : MembershipProvider
    {
        public override bool EnablePasswordRetrieval => throw new NotImplementedException();

        public override bool EnablePasswordReset => throw new NotImplementedException();

        public override bool RequiresQuestionAndAnswer => throw new NotImplementedException();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override int MaxInvalidPasswordAttempts => throw new NotImplementedException();

        public override int PasswordAttemptWindow => throw new NotImplementedException();

        public override bool RequiresUniqueEmail => throw new NotImplementedException();

        public override MembershipPasswordFormat PasswordFormat => throw new NotImplementedException();

        public override int MinRequiredPasswordLength => throw new NotImplementedException();

        public override int MinRequiredNonAlphanumericCharacters => throw new NotImplementedException();

        public override string PasswordStrengthRegularExpression => throw new NotImplementedException();

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            if (username == "admin")
                return password == "admin";
            using(var context = new DatabaseDataContext())
            {
                var user = context.User.Where(x => x.username == username);
                if (context.User.Where(x => x.username == username).Count() > 0)
                    return getHashedPassword(password) == user.First().passwd;
            }
            return false;
        }

        public bool CreateShopUser(string username, string password, string address)
        {
            using (var context = new DatabaseDataContext())
            {
                var newUser = new User()
                {
                    adress = address,
                    username = username,
                    passwd = getHashedPassword(password),
                    id = context.User.OrderByDescending(x => x.id).First().id + 1,
                    role = (username == "admin") ? "admin" : "user"
                };
                if (context.User.Where(x => x.username == username).Count() > 0)
                    return false;
                context.User.InsertOnSubmit(newUser);
                context.SubmitChanges();
                return true;
            }
        }

        private string getHashedPassword(string raw)
        {
            const int noHashes = 5;
            const string salt = "asdadsa9bgi0qh17";
            using (var hasher = MD5.Create())
            {
                string toHash = raw + salt;
                var toHashBytes = Encoding.UTF8.GetBytes(toHash);
                for (int i = 0; i < noHashes; i++)
                {
                    toHashBytes = hasher.ComputeHash(toHashBytes);
                }
                return BitConverter.ToString(toHashBytes);
            }
        }
    }
}