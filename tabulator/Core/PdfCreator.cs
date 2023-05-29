using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using tabulator.MVVM.Models;
using tabulator.MVVM.Views.UserViews;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace tabulator.Core
{
    public struct RaportStats
    {
        public string equipmentName;
        public string employeeName;
        public string employeeSurname;

        public string roomNumber;
        public string faculty;
        public string department;

        public string available;
        public string notInUse;
        public string destroyed;
    }

    public struct OutputItemData
    {
        public string itemName;
        public string itemDescription;
        public string owner;
        public string room;
        public string site;
        public string state;
    }

    public class PdfCreator
    {
        Paragraph newLine = new Paragraph("\n");
        public void Generate(List<OutputItemData> eqList, RaportStats stats)
        {
            Document document = new Document(new Rectangle(PageSize.A4.Rotate()));

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save PDF";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string outputPath = saveFileDialog.FileName;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));
                document.Open();

                string currentDateTimeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Paragraph title = new Paragraph("University equipment raport", new Font(Font.FontFamily.HELVETICA, 22, Font.BOLD));
                document.Add(title);

                Paragraph date = new Paragraph(currentDateTimeString, new Font(Font.FontFamily.HELVETICA, 12, Font.ITALIC));
                document.Add(date);
                AddStatsParagraph(stats, document);
                

                document.Add(newLine);

                float[] columnWidths = { 1f, 1f, 1f, 1f, 1f, 1f };
                PdfPTable table = new PdfPTable(columnWidths);
                table.WidthPercentage = 100;

                table.AddCell("Item Name");
                table.AddCell("Item description");
                table.AddCell("Owner");
                table.AddCell("Room");
                table.AddCell("Site");
                table.AddCell("State");

                foreach (OutputItemData item in eqList)
                {
                    table.AddCell(item.itemName);
                    table.AddCell(item.itemDescription);
                    table.AddCell(item.owner);
                    table.AddCell(item.room);
                    table.AddCell(item.site);
                    table.AddCell(item.state);
                }

                document.Add(table);

                document.Close();
            }
        }
        public string equipmentName;
        public string employeeName;
        public string employeeSurname;

        public string roomNumber;
        public string faculty;
        public string department;

        public string available;
        public string notInUse;
        public string destroyed;
        private void AddStatsParagraph(RaportStats stats, Document doc)
        {
            Paragraph title = new Paragraph("Applied filters: ", new Font(Font.FontFamily.HELVETICA, 15, Font.BOLDITALIC));
            title.SpacingAfter = 4f;
            doc.Add(title);
            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;

            CreateCell(table, $"Item: {stats.equipmentName}");
            CreateCell(table, $"Name: {stats.employeeName}");
            CreateCell(table, $"Surname: {stats.employeeSurname}");

            CreateCell(table, $"Room: {stats.roomNumber}");
            CreateCell(table, $"Faculty: {stats.faculty}");
            CreateCell(table, $"Department: {stats.department}");            
            
            CreateCell(table, $"Is available: {stats.available}");
            CreateCell(table, $"Is not in use: {stats.notInUse}");
            CreateCell(table, $"Is destroyed: {stats.destroyed}");

            doc.Add(table);
        }

        private void CreateCell(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, new Font(Font.FontFamily.HELVETICA, 12, Font.ITALIC)));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
        }
    }
}
