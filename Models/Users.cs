using System.Collections.Generic;

namespace ExampleGrid.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string OtherID { get; set; }

        public IEnumerable<Users> GetUsers()
        {
            return new List<Users>() { new Users { Id = 101, OtherID = "123576" } };
        }
    }
}