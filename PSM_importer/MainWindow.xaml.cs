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
using Microsoft.Win32;
using PSM_Libary.model;
namespace PSM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        ExcelAdapter excelAdapter;
        JSONAdapter jsonAdapter;
        OpenFileDialog openFileDialog;
        
        public MainWindow()
        {
            InitializeComponent();
            excelAdapter = new ExcelAdapter();
            jsonAdapter = new JSONAdapter();
            openFileDialog = new OpenFileDialog();
        }

        private void btn_OpenFile_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void btn_Datenladen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var filename = openFileDialog.FileName;
            
            switch (filename)
            {
                case null:
                    MessageBox.Show("Please select a File first!");
                    return;
                case "tblFamilyMembers.csv":
                    excelAdapter.ReadfamilyMemberExcel(filename);
                    break;
                case "tblBericht.csv":
                    excelAdapter.ReadReportExcel(filename);
                    break;
                case "tblAuftrag.csv":
                    excelAdapter.ReadOrderExcel(filename);
                    break;
                case "tblPersonal.csv":
                    excelAdapter.ReadPersonalExcel(filename);
                    break;
                case "Taetigkeiten.json":
                    jsonAdapter.ReadData(filename);
                    break;
                default:
                    MessageBox.Show("Please select a valid file");
                    return;
            }
            MessageBox.Show("Die Daten wurden eingelesen");
        }
    }
}