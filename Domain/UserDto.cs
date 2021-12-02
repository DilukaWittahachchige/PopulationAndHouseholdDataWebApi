using System;

namespace Domain
{
    /// <summary>
    ///  User data domain object
    /// </summary>
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserEmailAddress { get; set; }
        public string UserNRIC { get; set; }
        public DateTime UserDateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUserId { get; set; }
        public int UpdatedUserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}