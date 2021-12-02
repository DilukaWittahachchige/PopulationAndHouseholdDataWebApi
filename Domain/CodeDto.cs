using System;

namespace Domain
{
    /// <summary>
    ///  This class need for maintain metadata - Application common codes 
    /// </summary>
    public class CodeDto
    {
        public int CodeId { get; set; }
        public string CodeName { get; set; }
        public string CodeValue { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUserId { get; set; }
        public int UpdatedUserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}