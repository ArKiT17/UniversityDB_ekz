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
	/// Логика взаимодействия для LinkStudentToCourse.xaml
	/// </summary>
	public partial class LinkStudentToCourse : Page {
		public LinkStudentToCourse() {
			InitializeComponent();
		}

		private void Back_btn_click(object sender, RoutedEventArgs e) {
			NavigationService.GoBack();
        }

		private void Page_loaded(object sender, RoutedEventArgs e) {
			foreach (var item in CourseRepository.GetCourses())
				comboCourse.Items.Add(item);
			foreach (var item in StudentRepository.GetStudents())
				comboStudent.Items.Add(item);
			if (Buffer.selectedStudent != null)
				comboStudent.SelectedItem = comboStudent.Items[Buffer.selectedItemIndex];
			if (Buffer.selectedCourse != null)
				comboCourse.SelectedItem = comboCourse.Items[Buffer.selectedItemIndex];
		}

		private void selection_changed(object sender, SelectionChangedEventArgs e) {
			if (comboCourse.SelectedIndex != -1 && comboStudent.SelectedIndex != -1)
				button.IsEnabled = true;
			else
				button.IsEnabled = false;
		}

		private void SaveStudent_click(object sender, RoutedEventArgs e) {
			if (comboCourse.SelectedIndex != -1 && comboStudent.SelectedIndex != -1) {
				if (StudentRepository.Link((comboStudent.SelectedItem as Student).Id, (comboCourse.SelectedItem as Course).Id) != 0)
					MessageBox.Show("Студента додано до вибраного курса");
				else
					MessageBox.Show("Студент вже був записаний на цей курс");
			}
			else
				MessageBox.Show("Значення не вибрані");
		}
	}
}
