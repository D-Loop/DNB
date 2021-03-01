using DNB1.materials.cs;
using System.Windows;
using System.Windows.Forms;

namespace DNB1.materials.xaml
{
    /// <summary>
    /// Логика взаимодействия для FN.xaml
    /// </summary>
    public partial class FN : Window
    {
        public MainPage m = new MainPage();
        private SecondPage m1;

        public SecondPage M1 { get => m1; set => m1 = value; }

        public FN(MainPage mainWindow)
        {
            m = mainWindow;
            InitializeComponent();
        }
        public FN(SecondPage secondPage)
        {
            M1 = secondPage;
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            m.nameFile = NameFile.Text;


                new ImportToExcel().SaveExcelData(m.currencys, m.FBD.SelectedPath + @"\" + NameFile.Text, m.From.Text + " | " + m.To.Text);
            Close();

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}
