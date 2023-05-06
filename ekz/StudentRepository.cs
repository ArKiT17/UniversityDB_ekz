using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ekz {
	internal class StudentRepository {
		public static bool AddStudent(string name, string surname, int age) {
			using (var connection = new SqlConnection(Buffer.connectionString))
				if (connection.Execute($"insert into Students ([Name], Surname, Age) values ('{name}', '{surname}', {age})") > 0)
					return true;
				else
					return false;
		}
		public static bool ChangeStudent(int id, string name, string surname, int age) {
			using (var connection = new SqlConnection(Buffer.connectionString))
				if (connection.Execute($"update Students set [Name] = '{name}', Surname = '{surname}', Age = {age} where Id = {id}") > 0)
					return true;
				else
					return false;
		}
		public static bool DeleteStudent(int id) {
			using (var connection = new SqlConnection(Buffer.connectionString))
				if (connection.Execute($"delete from Students where Id = {id}") > 0)
					return true;
				else
					return false;
		}
		public static List<Student> GetStudents() {
			using (var connection = new SqlConnection(Buffer.connectionString))
				return connection.Query<Student>("select * from Students").ToList();
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
