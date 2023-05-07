using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
	/// Логика взаимодействия для Start.xaml
	/// </summary>
	public partial class Start : Page {
		public Start() {
			InitializeComponent();
		}

		private void Courses_Click(object sender, RoutedEventArgs e) {
			NavigationService.Navigate(new CoursesMain());
		}

		private void Students_Click(object sender, RoutedEventArgs e) {
			NavigationService.Navigate(new StudentsMain());
		}

		private void ExportCSV_Click(object sender, RoutedEventArgs e) {
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Файл CSV|*.csv";
			if (saveFileDialog.ShowDialog() == true) {
				using (var writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8)) {
					var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ua-UA")) {
						HasHeaderRecord = false,
						Delimiter = ";"
					};
					using (var csv = new CsvWriter(writer, csvConfig)) {
						try {
							csv.WriteRecords(CourseRepository.GetCourses());
							csv.WriteRecords(StudentRepository.GetStudents());
							MessageBox.Show("Дані успішно експортовано");
						}
						catch (ConfigurationException ex) {
							MessageBox.Show("Помилка збереження!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
						}
					}
				}
			}
        }

		private void ExportExcel_Click(object sender, RoutedEventArgs e) {

		}
	}
}
