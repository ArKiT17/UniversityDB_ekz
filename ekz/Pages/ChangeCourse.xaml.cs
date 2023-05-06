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
	/// Логика взаимодействия для ChangeCourse.xaml
	/// </summary>
	public partial class ChangeCourse : Page {
		public ChangeCourse() {
			InitializeComponent();
		}

		private void Back_btn_click(object sender, RoutedEventArgs e) {
			NavigationService.GoBack();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e) {
			name.Text = Buffer.selectedCourse.Name;
			teacher.Text = Buffer.selectedCourse.Teacher;
		}

		private void SaveCourse_click(object sender, RoutedEventArgs e) {
			if (name.Text != "" && teacher.Text != "") {
				CourseRepository.ChangeCourse(Buffer.selectedCourse.Id, name.Text, teacher.Text);
				MessageBox.Show("Курс відредаговано");
				name.Text = string.Empty;
				teacher.Text = string.Empty;
			}
			else
				MessageBox.Show("Заповніть всі поля");
		}

		private void text_changed(object sender, TextChangedEventArgs e) {
			if (name.Text != "" && teacher.Text != "")
				button.IsEnabled = true;
			else
				button.IsEnabled = false;
		}
	}
}
