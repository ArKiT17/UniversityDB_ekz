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
    /// Логика взаимодействия для ChangeStudent.xaml
    /// </summary>
    public partial class ChangeStudent : Page
    {
        public ChangeStudent()
        {
            InitializeComponent();
        }

		private void Page_loaded(object sender, RoutedEventArgs e) {
			name.Text = Buffer.selectedStudent.Name;
			surname.Text = Buffer.selectedStudent.Surname;
			age.Text = Buffer.selectedStudent.Age.ToString();
		}

		private void Back_btn_click(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
		}

		private void SaveStudent_click(object sender, RoutedEventArgs e) {
			if (name.Text != "" && surname.Text != "" && Convert.ToInt32(age.Text) > -1) {
				StudentRepository.ChangeStudent(Buffer.selectedStudent.Id, name.Text, surname.Text, Convert.ToInt32(age.Text));
				MessageBox.Show("Студента відредаговано");
				name.Text = string.Empty;
				surname.Text = string.Empty;
				age.Text = string.Empty;
			}
			else
				MessageBox.Show("Заповніть всі поля");
		}

		private void text_changed(object sender, TextChangedEventArgs e) {
			try {
				if (name.Text != "" && surname.Text != "" && Convert.ToInt32(age.Text) > -1)
					button.IsEnabled = true;
				else
					button.IsEnabled = false;
			}
			catch {
				button.IsEnabled = false;
			}
		}
	}
}
