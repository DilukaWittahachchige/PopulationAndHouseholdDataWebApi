using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models
{
    /// <summary>
    ///  UserType enum
    /// </summary>
    public enum UserType { Admin, Coordinator, Guest, Supervisor }

    /// <summary>
    /// User Entity
    /// </summary>
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public UserType Type { get; set; }
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
