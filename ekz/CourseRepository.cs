using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ekz {
	internal class CourseRepository {
		public static bool AddCourse(string name, string teacher) {
			using (var connection = new SqlConnection(Buffer.connectionString))
				if (connection.Execute($"insert into Courses ([Name], Teacher) values ('{name}', '{teacher}')") > 0)
					return true;
				else
					return false;
		}
		public static bool ChangeCourse(int id, string name, string teacher) {
			using (var connection = new SqlConnection(Buffer.connectionString))
				if (connection.Execute($"update Courses set [Name] = '{name}', Teacher = '{teacher}' where Id = {id}") > 0)
					return true;
				else
					return false;
		}
		public static bool DeleteCourse(int id) {
			using (var connection = new SqlConnection(Buffer.connectionString))
				if (connection.Execute($"delete from Courses where Id = {id}") > 0)
					return true;
				else
					return false;
		}
		public static List<Course> GetCourses() {
			using (var connection = new SqlConnection(Buffer.connectionString))
				return connection.Query<Course>("select * from Courses").ToList();
		}
		public static int Link(int studentId, int courseId) {
			using (var connection = new SqlConnection(Buffer.connectionString)) {
				if (connection.Query($"select * from StudentsCourses where StudentsId = {studentId} and CoursesId = {courseId}").ToList().Count == 0)
					return connection.Execute($"insert into StudentsCourses(StudentsId, CoursesId) values ({studentId}, {courseId})");
				else
					return 0;
			}
		}
	}
}
