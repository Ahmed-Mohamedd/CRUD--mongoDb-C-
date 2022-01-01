using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbdemo
{
    public class Users
    {
        public Guid id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public DateTime dateTime { get; set; }
        public int phoneNumber { get; set; }
        public string Gender { get; set; }
        public byte [] img { get; set; } // mongo doesn't support to hold image directly so we should store at as byte array .

    }
}
