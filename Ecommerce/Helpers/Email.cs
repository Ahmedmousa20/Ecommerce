using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Helpers
{
    public class Email
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string To { get; set; } //receiver email
    }
}
