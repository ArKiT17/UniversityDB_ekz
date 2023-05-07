using Dapper;
using Microsoft.Data.SqlClient;
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

namespace ekz.Pages
{
    /// <summary>
    /// Логика взаимодействия для CoursesMain.xaml
    /// </summary>
    public partial class CoursesMain : Page {
		public CoursesMain() {
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e) {
			search.Text = string.Empty;
			CoursesGrid.ItemsSource = CourseRepository.GetCourses();
		}
		
		private void Back_btn_click(object sender, RoutedEventArgs e) {
			NavigationService.GoBack();
        }

		private void AddCourse_click(object sender, RoutedEventArgs e) {
			NavigationService.Navigate(new AddCourse());
		}

		private void ChangeCourse_click(object sender, RoutedEventArgs e) {
			if (CoursesGrid.SelectedIndex != -1) {
				using (var connection = new SqlConnection(Buffer.connectionString)) {
					Buffer.selectedCourse = (CoursesGrid.ItemsSource as List<Course>)[CoursesGrid.SelectedIndex];
					NavigationService.Navigate(new ChangeCourse());
				}
			}
			else
				MessageBox.Show("Виберіть курс");
		}

		private void DeleteCourse_click(object sender, RoutedEventArgs e) {
			if (CoursesGrid.SelectedIndex != -1) {
				if (MessageBox.Show("Ви впевнені, що хочете видалити цей курс?\nВидалення курсу розірве всі прив'язки студентів до нього!",
					"Попередження", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
					Buffer.selectedCourse = (CoursesGrid.ItemsSource as List<Course>)[CoursesGrid.SelectedIndex];
					if (CourseRepository.DeleteCourse(Buffer.selectedCourse.Id))
						CoursesGrid.ItemsSource = CourseRepository.GetCourses();
				}
			}
			else
				MessageBox.Show("Виберіть курс");
		}

		private void LinkCourse_click(object sender, RoutedEventArgs e) {
			if (CoursesGrid.SelectedIndex != -1) {
				Buffer.selectedCourse = (CoursesGrid.ItemsSource as List<Course>)[CoursesGrid.SelectedIndex];
				Buffer.selectedStudent = null;
				Buffer.selectedItemIndex = CoursesGrid.SelectedIndex;
				NavigationService.Navigate(new LinkStudentToCourse());
			}
			else
				MessageBox.Show("Виберіть курс");
		}

		private void search_textChanged(object sender, TextChangedEventArgs e) {
			if (search.Text != string.Empty) {
				List<Course> filteredCourses = new List<Course>();
				foreach (Course tmp in CourseRepository.GetCourses())
					if (tmp.Name.ToLower().Contains(search.Text) || tmp.Teacher.ToLower().Contains(search.Text))
						filteredCourses.Add(tmp);
				CoursesGrid.ItemsSource = filteredCourses;
			}
			else
				CoursesGrid.ItemsSource = CourseRepository.GetCourses();
		}

		private void CoursesGrid_doubleClick(object sender, MouseButtonEventArgs e) {
			if (CoursesGrid.SelectedItem != null) {
				Buffer.selectedCourse = (CoursesGrid.ItemsSource as List<Course>)[CoursesGrid.SelectedIndex];
				Buffer.selectedStudent = null;
				Buffer.selectedItemIndex = CoursesGrid.SelectedIndex;
				NavigationService.Navigate(new CourseInfo());
			}
		}
	}
}
