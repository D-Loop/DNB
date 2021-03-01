using DNB1.materials.cs;
using System.Windows;
using System.Windows.Forms;

namespace DNB1.materials.xaml
{
    /// <summary>
    /// Логика взаимодействия для FN.xaml
    /// </summary>
    public partial class FN2 : Window
    {
        public SecondPage m;
        
        public FN2(SecondPage secondPage)
        {
            m = secondPage;
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            m.nameFile = NameFile.Text;


                new ImportToExcel().SaveSecondExcelData(m.lrate, m.FBD.SelectedPath + @"\" + NameFile.Text);
            Close();

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}
