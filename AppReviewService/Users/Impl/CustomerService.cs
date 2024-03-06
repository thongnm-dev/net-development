using Net.Data;
using Net.Data.Repository;
using AppReviewDomain.Users;
using Net.Core.Utils;
using Net.Caching;
using AppReviewService.Users.Caching;

namespace AppReviewService.Users
{
    internal class Userservice : IUserservice
    {
        #region Field
        private readonly IStaticCacheManager _staticCacheManager;

        private readonly IReadRepository<User> _userRepository;

        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public Userservice(IReadRepository<User> userRepository, IUnitOfWork unitOfWork, IStaticCacheManager staticCacheManager)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _staticCacheManager = staticCacheManager;
        }
        #endregion

        #region For check token
        public Task<bool> IsExistUserByEmailOrPhoneNumberAsync(string iEmailOrPhone)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistUserByIdAsync(int iUserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistGuestUserAsync(string iUserGuid)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Get User Info

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = _userRepository.GetByIdAsync(id);

            //return user;
            return await Task.FromResult(new User());
        }

        public async Task<User> GetUserByUserNameOrEmailAsync(string iUserName)
        {
            if (Utils.IsPhoneNumber(iUserName))
            {
                var user = _userRepository.GetAllAsync(query =>
                {
                    return from q in query
                           where q.PhoneNumber == iUserName
                           select q;
                }, cache => _staticCacheManager.PrepareKeyForDefaultCache(NopUserServicesDefaults.UserByUserName, iUserName));
            }
            else if (Utils.IsEmail(iUserName))
            {
                var user = _userRepository.GetAllAsync(query =>
                {
                    return from q in query
                           where q.Email == iUserName
                           select q;
                }, cache => _staticCacheManager.PrepareKeyForDefaultCache(NopUserServicesDefaults.UserByEmail, iUserName));
            }
            else
            {
                var user = _userRepository.GetAllAsync(query =>
                {
                    return from q in query
                           where q.Username == iUserName
                           select q;
                }, cache => _staticCacheManager.PrepareKeyForDefaultCache(NopUserServicesDefaults.AllCacheKey));
            }
            return await Task.FromResult(new User());
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(bool isAll)
        {
            var Users = new List<User>();

            return await Task.FromResult(Users);
        }
        #endregion

        #region Storage User

        /// <summary>
        /// Insert new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task CreateUser(User user)
        {
            using (var trans = await _unitOfWork.CreateTransactionAsync())
            {
                try
                {
                    await _unitOfWork.Repository<User>().CreateAsync(user);

                    await trans.CommitAsync();
                }
                catch (Exception ex)
                {
                    await trans.RollbackAsync();
                }
            }

            await Task.CompletedTask;
        }


        #endregion
    }
}
