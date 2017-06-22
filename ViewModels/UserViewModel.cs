using Learner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learner.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public IEnumerable<User> Users { get; set; }
        public User User { get; set; }
    }
}