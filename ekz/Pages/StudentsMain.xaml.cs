using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
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
	/// Логика взаимодействия для StudentsMain.xaml
	/// </summary>
	public partial class StudentsMain : Page {
		public StudentsMain() {
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e) {
			search.Text = string.Empty;
			StudentsGrid.ItemsSource = StudentRepository.GetStudents();
		}

		private void Back_btn_click(object sender, RoutedEventArgs e) {
			NavigationService.GoBack();
        }

		private void AddStudent_click(object sender, RoutedEventArgs e) {
			NavigationService.Navigate(new AddStudent());
		}

		private void ChangeStudent_click(object sender, RoutedEventArgs e) {
			if (StudentsGrid.SelectedIndex != -1) {
				using (var connection = new SqlConnection(Buffer.connectionString)) {
					Buffer.selectedStudent = (StudentsGrid.ItemsSource as List<Student>)[StudentsGrid.SelectedIndex];
					NavigationService.Navigate(new ChangeStudent());
				}
			}
			else
				MessageBox.Show("Виберіть студента");
		}

		private void DeleteStudent_click(object sender, RoutedEventArgs e) {
			if (StudentsGrid.SelectedIndex != -1) {
				if (MessageBox.Show("Ви впевнені, що хочете видалити цього студента?",
					"Попередження", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
					Buffer.selectedStudent = (StudentsGrid.ItemsSource as List<Student>)[StudentsGrid.SelectedIndex];
					if (StudentRepository.DeleteStudent(Buffer.selectedStudent.Id))
						StudentsGrid.ItemsSource = StudentRepository.GetStudents();
				}
			}
			else
				MessageBox.Show("Виберіть студента");
		}

		private void LinkStudent_click(object sender, RoutedEventArgs e) {
			if (StudentsGrid.SelectedIndex != -1) {
				Buffer.selectedCourse = null;
				Buffer.selectedStudent = (StudentsGrid.ItemsSource as List<Student>)[StudentsGrid.SelectedIndex];
				Buffer.selectedItemIndex = StudentsGrid.SelectedIndex;
				NavigationService.Navigate(new LinkStudentToCourse());
			}
			else
				MessageBox.Show("Виберіть студента");
		}

		private void search_textChanged(object sender, TextChangedEventArgs e) {
			if (search.Text != string.Empty) {
				List<Student> filteredStudents = new List<Student>();
				foreach (Student tmp in StudentRepository.GetStudents())
					if (tmp.Surname.Contains(search.Text) || tmp.Name.Contains(search.Text))
						filteredStudents.Add(tmp);
				StudentsGrid.ItemsSource = filteredStudents;
			}
			else
				StudentsGrid.ItemsSource = StudentRepository.GetStudents();
		}
	}
}
