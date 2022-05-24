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
using PSM_Libary.model;
namespace PSM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        ExcelAdapter excelAdapter = new ExcelAdapter();
        JSONAdapter JSONAdapter = new JSONAdapter();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Datenladen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string filename = txtBox_Entry.Text;
            
            if(filename == "tblFamilyMembers.csv")
            {
                excelAdapter.ReadfamilyMemberExcel(filename);
                MessageBox.Show("Die Daten wurden eingelesen");
            }
            if (filename == "tblBericht.csv")
            {
                excelAdapter.ReadReportExcel(filename);
                MessageBox.Show("Die Daten wurden eingelesen");
            }
            if (filename == "tblAuftrag.csv")
            {
                excelAdapter.ReadOrderExcel(filename);
                MessageBox.Show("Die Daten wurden eingelesen");
            }
            if (filename == "tblPersonal.csv")
            {
                excelAdapter.ReadPersonalExcel(filename);
                MessageBox.Show("Die Daten wurden eingelesen");
            }
            if (filename == "Taetigkeiten.json")
            {
                JSONAdapter.ReadData(filename);
                MessageBox.Show("Die Daten wurden eingelesen");
            }
        }
    }
}