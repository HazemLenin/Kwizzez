using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwizzez.Domain.Constants
{
    public static class Roles
    {
        public const string Admin = nameof(Admin);
        public const string Teacher = nameof(Teacher);
        public const string Student = nameof(Student);
        public static string[] GetRoles()
        {
            string[] roles = {
                Admin, Teacher, Student
            };

            return roles;
        }
    }
}
