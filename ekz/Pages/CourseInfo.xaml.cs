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
	/// Логика взаимодействия для CourseInfo.xaml
	/// </summary>
	public partial class CourseInfo : Page {
		public CourseInfo() {
			InitializeComponent();
		}

		private void Page_loaded(object sender, RoutedEventArgs e) {
			courseName.Text = Buffer.selectedCourse.Name;
			StudentsGrid.ItemsSource = CourseRepository.StudentsFromCourse(Buffer.selectedCourse);
        }

		private void Back_btn_click(object sender, RoutedEventArgs e) {
			NavigationService.GoBack();
		}

		private void AddStudent_click(object sender, RoutedEventArgs e) {
			NavigationService.Navigate(new LinkStudentToCourse());
		}

		private void DeleteStudent_click(object sender, RoutedEventArgs e) {
			if (StudentsGrid.SelectedItem != null) {
				if (StudentRepository.DeleteStudentFromCourse(CourseRepository.StudentsFromCourse(Buffer.selectedCourse)[StudentsGrid.SelectedIndex], Buffer.selectedCourse))
					StudentsGrid.ItemsSource = CourseRepository.StudentsFromCourse(Buffer.selectedCourse);
				else
					MessageBox.Show("Помилка видалення");
			}
			else
				MessageBox.Show("Студента не вибрано");
		}
	}
}
