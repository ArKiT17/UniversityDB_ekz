using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Spreadsheet;
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
	public partial class Start : System.Windows.Controls.Page {
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
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Книга Excel|*.xlsx";
			if (saveFileDialog.ShowDialog() == true) {
				try {
					var workbook = new XLWorkbook();
					var sheetCourses = workbook.Worksheets.Add("Courses");
					var courses = CourseRepository.GetCourses();
					sheetCourses.Cell(1, 1).SetValue("Id");
					sheetCourses.Cell(1, 2).SetValue("Курс");
					sheetCourses.Cell(1, 3).SetValue("Викладач");
					for (int i = 2; i < courses.Count + 2; i++) {
						sheetCourses.Cell(i, 1).SetValue(courses[i - 2].Id);
						sheetCourses.Cell(i, 2).SetValue(courses[i - 2].Name);
						sheetCourses.Cell(i, 3).SetValue(courses[i - 2].Teacher);
					}

					var sheetStudents = workbook.Worksheets.Add("Students");
					var students = StudentRepository.GetStudents();
					sheetStudents.Cell(1, 1).SetValue("Id");
					sheetStudents.Cell(1, 2).SetValue("Ім'я студента");
					sheetStudents.Cell(1, 3).SetValue("Прізвище студента");
					sheetStudents.Cell(1, 4).SetValue("Вік студента");
					for (int i = 2; i < students.Count + 2; i++) {
						sheetStudents.Cell(i, 1).SetValue(students[i - 2].Id);
						sheetStudents.Cell(i, 2).SetValue(students[i - 2].Name);
						sheetStudents.Cell(i, 3).SetValue(students[i - 2].Surname);
						sheetStudents.Cell(i, 4).SetValue(students[i - 2].Age);
					}
					workbook.SaveAs(saveFileDialog.FileName);
					MessageBox.Show("Дані успішно експортовано");
				}
				catch {
					MessageBox.Show("Помилка збереження!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
	}
}