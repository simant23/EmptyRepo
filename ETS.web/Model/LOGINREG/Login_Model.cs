using System.ComponentModel.DataAnnotations;

namespace ETSystem.Model.LOGINREG
{
    public class LOGIN_Model
    {
        public string? EmailId { get; set; }

        public string? Password { get; set; }

        public string? Type { get; set; }

        //public ICollection<ChatRoom> ChatRooms { get; set; }
    }
}
