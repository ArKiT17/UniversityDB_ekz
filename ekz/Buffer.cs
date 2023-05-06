using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ekz
{
    internal class Buffer
    {
        public static string connectionString = @"Data Source = ASUS-TUF\SQLEXPRESS; Initial Catalog = UniversityDB; Trusted_Connection=True; Encrypt=False";
        public static Course? selectedCourse;
        public static Student? selectedStudent;
        public static int selectedItemIndex;
    }
}
