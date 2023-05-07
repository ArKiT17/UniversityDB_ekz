using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ekz.Pages {
	/// <summary>
	/// Логика взаимодействия для StudentsInfo.xaml
	/// </summary>
	public partial class StudentsInfo : Page {
		public StudentsInfo() {
			InitializeComponent();
		}

		private void Page_loaded(object sender, RoutedEventArgs e) {
			studentName.Text = Buffer.selectedStudent.Name + " " + Buffer.selectedStudent.Surname;
			CoursesGrid.ItemsSource = StudentRepository.CoursesWhereStudent(Buffer.selectedStudent);
		}

		private void Back_btn_click(object sender, RoutedEventArgs e) {
			NavigationService.GoBack();
		}

		private void AddCourse_click(object sender, RoutedEventArgs e) {
			NavigationService.Navigate(new LinkStudentToCourse());
		}

		private void DeleteCourse_click(object sender, RoutedEventArgs e) {
			if (CoursesGrid.SelectedItem != null) {
				if (CourseRepository.DeleteStudentFromCourse(Buffer.selectedStudent, StudentRepository.CoursesWhereStudent(Buffer.selectedStudent)[CoursesGrid.SelectedIndex]))
					CoursesGrid.ItemsSource = StudentRepository.CoursesWhereStudent(Buffer.selectedStudent);
				else
					MessageBox.Show("Помилка видалення");
			}
			else
				MessageBox.Show("Курс не вибрано");
		}
	}
}
