using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;


using ElectronicObject = Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Models.ElectronicObject;
using System.Security.Cryptography;
using System.Windows.Controls;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services
{
    public static class ObjectListToWord
    {
        private static object missing = System.Reflection.Missing.Value;

        public static void CreateWordFile(List<ElectronicObject> electronicObjects)
        {
            object Visible = true;
            object start1 = 0;
            object end1 = 0;
            Word.Application WordApp = new Word.Application();
            Word.Document document = WordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            object start = 0, end = 0;
            document.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
            document.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4;
            document.PageSetup.BottomMargin = 14.2f;
            document.PageSetup.TopMargin = 49.6f;
            document.PageSetup.LeftMargin = 28.35f;
            document.PageSetup.RightMargin = 26.95f;

            Word.Range range = document.Range(ref start, ref end);

            range.InsertBefore("\nMinisterul Educației\nUniversitatea Transilvania din Brașov");
            range.Font.Name = "Calibri";
            range.Font.Size = 11;
            range.InsertParagraphAfter();
            range.InsertParagraphAfter();
            range.SetRange(range.End, range.End);

            range.InsertBefore("PROCES-VERBAL\n");
            range.Font.Size = 14;
            range.Font.Bold = 1;
            range.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
            range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            range.SetRange(range.End, range.End);

            range.InsertBefore("de propunere de scoatere din funcțiune" +
                " / declasarea și casarea obiectelor de inventar\n");
            range.Font.Size = 11;
            range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            range.SetRange(range.End, range.End);

            range.InsertBefore("aflate în gestiunea......................." +
                ".........................................................." +
                ".........................................................." +
                "..........................................................." +
                "..........................................\r\nCauzele degradării" +
                "............................................................" +
                "............................................................" +
                "............................................................" +
                ".............................................................\r\n");
            range.Font.Size = 11;
            range.InsertParagraphAfter();
            range.InsertParagraphAfter();
            range.SetRange(range.End, range.End);

            range.Tables.Add(document.Paragraphs[9].Range, 1, 12, ref missing, ref missing);
            Word.Table tbl = document.Tables[1];
            
            tbl.Range.Font.Size = 11;
            tbl.Columns.DistributeWidth();
            tbl.Borders.Enable = 1;
            tbl.Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            tbl.Cell(1, 1).Range.Text = "Nr. crt.";
            tbl.Cell(1, 2).Range.Text = "Nr. de Cod / N.O.";
            tbl.Cell(1, 3).Range.Text = "Denumirea obiectelor de inventar";
            tbl.Cell(1, 4).Range.Text = "U/M";
            tbl.Cell(1, 5).Range.Text = "Cantitate";
            tbl.Cell(1, 6).Range.Text = "Preț";
            tbl.Cell(1, 7).Range.Text = "Valoare";
            tbl.Cell(1, 8).Range.Text = "Data intrării în gestiune";
            tbl.Cell(1, 9).Range.Text = "Durata de serviciu";
            tbl.Cell(1, 10).Range.Text = "Starea în care se găsește";
            tbl.Cell(1, 11).Merge(tbl.Cell(1, 12));
            tbl.Cell(1, 11).Range.Text = "Nume, prenume, semnătură utilizator";

            int currentItem = 1;
            double totalSum = 0;
            foreach (var electronicObject in electronicObjects)
            {
                tbl.Rows.Add(ref missing);
                tbl.Rows.Last.Cells[1].Range.Text = currentItem.ToString();
                tbl.Rows.Last.Cells[2].Range.Text = electronicObject.Code + "/" + electronicObject.Order;
                tbl.Rows.Last.Cells[3].Range.Text = electronicObject.Name;
                tbl.Rows.Last.Cells[4].Range.Text = "Bucată";
                tbl.Rows.Last.Cells[5].Range.Text = "1";
                tbl.Rows.Last.Cells[6].Range.Text = electronicObject.Price;
                tbl.Rows.Last.Cells[7].Range.Text = electronicObject.Price;
                tbl.Rows.Last.Cells[10].Range.Text = "Defect";


                tbl.Rows.Last.Cells[8].Range.Text = electronicObject.Date;

                totalSum += double.Parse(electronicObject.Price);
                ++currentItem;
            }

            range.SetRange(range.End, range.End);

            range.InsertBefore("  Data..............................." +
                "                                                     " +
                "                                                 ");
            range.Font.Size = 11;
            range.SetRange(range.End, range.End);

            range.InsertBefore($"Total valoare: {Math.Round(totalSum, 3)}\n");
            range.Font.Size = 12;
            range.Font.Bold = 1;
            range.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
            range.SetRange(range.End, range.End);

            range.InsertBefore("                                           " +
                "  Decan / Director,                                       " +
                "                                                          " +
                "  Director de departament /                               " +
                "                                    Gestionar,\r\n        " +
                "                                                          " +
                "                                                          " +
                "                                                   " +
                "şef structură administrativă,                     \r\n    " +
                "                                        Comisia de inventariere" +
                " (nume, prenume, semnătură):\r\n");
            range.Font.Size = 11;
            range.SetRange(range.End, range.End);


            range.ListFormat.ApplyNumberDefault();
            range.ListFormat.ListIndent();
            range.ParagraphFormat.LeftIndent = 109.5f;

            int listSize = 3;
            for (int i = 0; i < listSize; i++)
            {
                string bulletItem = "........................" +
                    "............................         .....................";
                if (i < listSize) 
                    bulletItem += "\n";
                range.InsertAfter(bulletItem);
            }
            range.SetRange(range.End, range.End);

            range.ListFormat.RemoveNumbers();
            range.InsertAfter("F01 – PS 6.6-01/ed. 1, rev.0");

            try { document.Save(); }
            catch (Exception ex) { }
        }
    }
}
