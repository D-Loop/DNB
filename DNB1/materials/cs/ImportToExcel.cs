using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using System.Threading.Tasks;
using NbrbAPI.Models;
using System.Collections.ObjectModel;

namespace DNB1.materials.cs
{
    public class ImportToExcel
    {


        public void SaveExcelData(ObservableCollection<Currency> Data, string FileName,string per)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Валюты");
            string[] charm = { "A", "B", "C", "D", "E" };

            foreach (string v in charm)
            {
                worksheet.Cell(v + 1).Style.Font.Bold = true;
                worksheet.Cell(v + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                worksheet.Cell(v + 1).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;
            }
            worksheet.Cell("A" + 1).Value = "Период обновления данных";
            worksheet.Cell("B" + 1).Value = "Цифровой код";
            worksheet.Cell("C" + 1).Value = "Буквенный код";
            worksheet.Cell("D" + 1).Value = "Наименование(на русском)";
            worksheet.Cell("E" + 1).Value = "Наименование(на английском)";
            int row = 2;


            foreach (var data in Data)
            {

                worksheet.Cell("A" + row).Value = data.Cur_PeriodicityString;
                worksheet.Cell("A" + row).Style.Fill.BackgroundColor = XLColor.White;
                worksheet.Cell("A" + row).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;
                worksheet.Cell("B" + row).Value = data.Cur_Code;
                worksheet.Cell("B" + row).Style.Fill.BackgroundColor = XLColor.White;
                worksheet.Cell("B" + row).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;
                worksheet.Cell("C" + row).Value = data.Cur_Abbreviation;
                worksheet.Cell("C" + row).Style.Fill.BackgroundColor = XLColor.White;
                worksheet.Cell("C" + row).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;
                worksheet.Cell("D" + row).Value = data.Cur_Name;
                worksheet.Cell("D" + row).Style.Fill.BackgroundColor = XLColor.White;
                worksheet.Cell("D" + row).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;
                worksheet.Cell("E" + row).Value = data.Cur_Name_Eng;
                worksheet.Cell("E" + row).Style.Fill.BackgroundColor = XLColor.White;
                worksheet.Cell("E" + row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                row++;
            }
            worksheet.Range("A1:E1").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            worksheet.Range("A1:E1").Style.Border.InsideBorder = XLBorderStyleValues.Thick;
            worksheet.Range("A2:E" + row).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            worksheet.Range("A2:E"+ row).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            worksheet.Cell("A" + row).Value = "Всего валют в промежутке: "+per;
            worksheet.Cell("E" + row).Value = "" + row + " шт.";
            worksheet.Range("A" + row + ":E" + row).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            worksheet.Range("A" + row + ":E" + row).Style.Border.InsideBorder = XLBorderStyleValues.Thick;
            worksheet.Range("A" + row + ":D" + row).Row(1).Merge();
            worksheet.Range("A" + row + ":E" + row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(@"" + FileName + ".xlsx");
        }

        public void SaveSecondExcelData(ObservableCollection<Rate> Data, string FileName)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Динамика");
            string[] charm = { "A", "B", "C", "D"};

            foreach (string v in charm)
            {
                worksheet.Cell(v + 1).Style.Font.Bold = true;
                worksheet.Cell(v + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                worksheet.Cell(v + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            worksheet.Cell("A" + 1).Value = "Дата";
            worksheet.Cell("B" + 1).Value = "Буквенный код";
            worksheet.Cell("C" + 1).Value = "Наименование(на русском)";
            worksheet.Cell("D" + 1).Value = "Курс";
            int row = 2;


            foreach (var data in Data)
            {

                worksheet.Cell("A" + row).Value = data.Date;
                worksheet.Cell("A" + row).Style.Fill.BackgroundColor = XLColor.White;
                worksheet.Cell("A" + row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell("B" + row).Value = data.Cur_Abbreviation;
                worksheet.Cell("B" + row).Style.Fill.BackgroundColor = XLColor.White;
                worksheet.Cell("B" + row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell("C" + row).Value = data.Cur_Name;
                worksheet.Cell("C" + row).Style.Fill.BackgroundColor = XLColor.White;
                worksheet.Cell("C" + row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell("D" + row).Value = data.Cur_OfficialRate;
                worksheet.Cell("D" + row).Style.Fill.BackgroundColor = XLColor.White;
                worksheet.Cell("D" + row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                row++;
            }
            row--;
            worksheet.Range("A1:D1").Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            worksheet.Range("A1:D1").Style.Border.InsideBorder = XLBorderStyleValues.Thick;
            worksheet.Range("A2:D" + row).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            worksheet.Range("A2:D" + row).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(@"" + FileName + ".xlsx");
        }


    }
}
