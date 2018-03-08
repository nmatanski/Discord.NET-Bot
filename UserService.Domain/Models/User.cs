using CommonHelpers.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Models
{
    public sealed class User
    {
        private CustomId _id;

        public User() { }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public User(string Username, CustomId id = null, string imgUrl = null,
            DateTime? dateCreated = null, DateTime? dateChanged = null)
        {
            this.Username = Username;
            ImgUrl = imgUrl ?? string.Empty;
            DateCreated = dateCreated ?? DateTime.Now;
            DateChanged = dateChanged ?? DateTime.MinValue;
            _id = id ?? new CustomId();
        }
        public User(string username, string email, string password, Role role, string validationCode = null,
            bool isEmailConfirmed = false, CustomId id = null, string imgUrl = null, DateTime? dateChanged = null)
        {
            Username = username;
            Email = email;
            Password = password;
            UserRole = role;
            ValidationCode = validationCode;
            IsEmailConfirmed = isEmailConfirmed;
            DateCreated = DateTime.Now;
            ImgUrl = imgUrl ?? string.Empty;
            DateChanged = dateChanged ?? DateTime.MinValue;
            _id = id ?? new CustomId();
        }

        [Key]
        public string Id
        {
            get { return _id.ToString(); }
            private set { _id = new CustomId(new Guid(value)); }
        }

        public string Username { get; private set; }

        [DisplayName("Avatar")]
        public string ImgUrl { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        [DisplayName("Role")]
        public Role UserRole { get; set; }

        [DisplayName("Validation Code")]
        public string ValidationCode { get; private set; }

        [DisplayName("Email Confirmed")]
        public bool IsEmailConfirmed { get; private set; }

        [DisplayName("Date Created")]
        public DateTime? DateCreated { get; private set; }

        [DisplayName("Date Changed")]
        public DateTime? DateChanged { get; private set; }

    }
}
