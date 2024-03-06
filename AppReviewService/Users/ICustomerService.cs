using AppReviewDomain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReviewService.Users
{
    public interface IUserservice
    {
        #region For check token
        Task<bool> IsExistUserByEmailOrPhoneNumberAsync(string iEmailOrPhone);

        Task<bool> IsExistUserByIdAsync(int iUserId);

        Task<bool> IsExistGuestUserAsync(string iUserGuid);
        #endregion

        Task<User> GetUserByIdAsync(int id);

        Task<User> GetUserByUserNameOrEmailAsync(string iUserName);

        Task<IEnumerable<User>> GetAllUsersAsync(bool isAll);
    }
}
