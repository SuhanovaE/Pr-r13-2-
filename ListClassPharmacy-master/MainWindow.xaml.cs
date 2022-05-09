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
using ListClass.Classes;
using System.IO;



namespace ListClass
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
       // List<Pharmacy> students = new List<Student>();
        public MainWindow()
        {
            InitializeComponent();
            ConnectHelper.students.Add(new Student("Соколов Т.Д.", "ИСП.19", 5, 5, 5, 5, 5));
            ConnectHelper.students.Add(new Student("Пирогов Н.Д.", "ССА.20", 5, 3, 2, 4, 4));
            ConnectHelper.students.Add(new Student("Иванов Г.И.", "ТМ.20", 3, 4, 3, 3, 5));
            ConnectHelper.students.Add(new Student("Понова В.Е.", "ИСП.18", 3, 4, 3, 3, 3));
            ConnectHelper.students.Add(new Student("Никова М.П.", "ИСП.20", 3, 5, 3, 4, 5));
            
            

            DtgListStudent.ItemsSource = ConnectHelper.students;
            //ConnectHelper.Students.Add(new Student("Соколов Т.Д.", "ИСП.19", 5, 5, 5, 5, 5));
            //ConnectHelper.Students.Add(new Student("Пирогов Н.Д.", "ССА.20", 5, 3, 2, 4, 5));
            //ConnectHelper.Students.Add(new Student("Иванов Г.Ив.", "ТМ.20", 4, 5, 3, 5, 5));
            //ConnectHelper.Students.Add(new Student("Пономарева В.Ег.", "ИСП.18", 4, 4, 4, 4, 5));
            //ConnectHelper.Students.Add(new Student("Николаева М.П.", "ИСП.20", 3, 5, 3, 4, 5));
            //ConnectHelper.Students.Add(new Student("Новикова Э.Дм.", "ССА.18", 4, 5, 5, 5, 4));
            //ConnectHelper.Students.Add(new Student("Васиьев Ил.М.", "К.20", 3, 5, 5, 5, 5));
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            DtgListStudent.ItemsSource = ConnectHelper.students.ToList();
            DtgListStudent.SelectedIndex = -1;
            int num = ConnectHelper.students.Count();
            CountStudent.Text += num.ToString();

        }
        /// <summary>
        /// сортировка по алфавиту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbUp_Checked(object sender, RoutedEventArgs e)
        {
            DtgListStudent.ItemsSource = ConnectHelper.students.OrderBy(x=>x.NameStudent).ToList();
        }
        /// <summary>
        /// сортировка в обратном порядке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbDown_Checked(object sender, RoutedEventArgs e)
        {
            DtgListStudent.ItemsSource = ConnectHelper.students.OrderByDescending(x => x.NameStudent).ToList();
        }
        /// <summary>
        /// поиск по названию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DtgListStudent.ItemsSource = ConnectHelper.students.Where(x => 
                x.NameStudent.ToLower().Contains(TxtSearch.Text.ToLower())).ToList();
        }

       /// <summary>
       /// фильтр по количеству
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void CmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (CmbFiltr.SelectedIndex == 0)
            {
                DtgListStudent.ItemsSource = ConnectHelper.students.Where(x =>
                    (x.CountGymnastic+x.CountChemistry + x.CountAlgebra + x.CountLiterature + x.CountPhysics)/5 > 4).ToList();
                MessageBox.Show("Мало отличников!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                if (CmbFiltr.SelectedIndex == 1)
            {
                DtgListStudent.ItemsSource = ConnectHelper.students.Where(x =>
                   (x.CountGymnastic + x.CountChemistry + x.CountAlgebra + x.CountLiterature + x.CountPhysics) / 5 <= 3.5).ToList();
                MessageBox.Show("Следует подтянуть учеников",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
            else
            {
                DtgListStudent.ItemsSource = ConnectHelper.students.Where(x =>
                    (x.CountGymnastic + x.CountChemistry + x.CountAlgebra + x.CountLiterature + x.CountPhysics) / 5 > 3.5 && 
                    (x.CountGymnastic + x.CountChemistry + x.CountAlgebra + x.CountLiterature + x.CountPhysics) / 5 <= 4).ToList();
                MessageBox.Show("Нужно провести беседу",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddPreparate windowAdd = new WindowAddPreparate();
            windowAdd.ShowDialog();
        }

       

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            DtgListStudent.ItemsSource = null;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowAddPreparate windowAdd = new WindowAddPreparate((sender as Button).DataContext as Student);
            windowAdd.ShowDialog();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var resMessage = MessageBox.Show("Удалить запись?", "Подтверждение",
               MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resMessage == MessageBoxResult.Yes)
            {
                int ind = DtgListStudent.SelectedIndex;
                ConnectHelper.students.RemoveAt(ind);
                DtgListStudent.ItemsSource = ConnectHelper.students.ToList();
                ConnectHelper.SaveListToFile(@"ListStudents.txt");
            }
        }

       
    }
}
