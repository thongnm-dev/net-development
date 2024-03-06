using Net.Core;

namespace AppReviewDomain.Users
{
    public class User : BaseEntity
    {
        public User()
        {
            UserGuid = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the user GUID
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone
        /// </summary>
        public string PhoneNumber { get; set; }


    }
}
