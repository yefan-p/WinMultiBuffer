using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MultiBuffer.WebApi.DataModels
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Token { get; set; }

        public ICollection<BufferItem> Buffers { get; set; }
    }
}
