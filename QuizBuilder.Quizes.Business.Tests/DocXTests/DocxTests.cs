using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using QuizBuilder.Quizes.Business.Concrete.Compailers;
using QuizBuilder.Quizes.Entities.Concrete;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Font = Xceed.Document.NET.Font;
using Image = System.Drawing.Image;
using Word = Microsoft.Office.Interop.Word;
using XImage = Xceed.Document.NET.Image;

namespace QuizBuilder.Quizes.Business.Tests.DocXTests
{
    [TestClass]
    public class DocxTests
    {
        [TestMethod]
        public void DocX_creates_document()
        {
            string fileName = @"E:\TestDocs\exempleWord.docx";

            var doc = DocX.Create(fileName);

            doc.InsertParagraph("Hello Word");

            doc.Save();

            //Process.Start("WINWORD.EXE", fileName);


        }

        [TestMethod]
        public void Test()
        {
            List<Soru> questionList = new List<Soru>
            {
                new Soru(){Gorsel = new byte[]{1,2,3},Metin = "görselli"},
                new Soru(){Gorsel = null,Metin = "göörselsiz"}
            };
            List<Soru> questionsWithNonePic = new List<Soru>();
            List<Soru> questionsWithPic = new List<Soru>();

            foreach (Soru soru in questionList)
            {
                if (soru.Gorsel == null)
                {
                    questionsWithNonePic.Add(soru);
                }
                else
                {
                    questionsWithPic.Add(soru);
                }
            }
        }

        public MemoryStream GetImage()
        {
            byte[] imageDataBytes = File.ReadAllBytes(@"C:\Users\Mehmet\Desktop\right-arroww.png");

            MemoryStream stream = new MemoryStream(imageDataBytes);
            Image img = Image.FromStream(stream);
            return stream;

        }

        [TestMethod]
        public void DocX_Sends_Picture()
        {
            string fileName = @"E:\TestDocs\exempleWord.docx";
            var doc = DocX.Create(fileName);
            
            //create picture
            XImage img = doc.AddImage(GetImage());
            Picture p = img.CreatePicture();

            //create a new paragraph
            Paragraph par = doc.InsertParagraph("Arrow Picture");
            par.AppendPicture(p);
            doc.Save();
        }

        [TestMethod]
        public void Test_Page()
        {
            string fileName = @"E:\TestDocs\exempleWord.docx";
            string title = "Ankara Üniversitesi 2.Güz dönemi Programlama Vizesi";
            string paragraph = "" + "S1) asflkasfşkasşflkasşldaf" + 
                               Environment.NewLine + Environment.NewLine + "A) alkfjaksfja" +
                               Environment.NewLine + "B) lakflaskfişaslfşasilfas" +
                               Environment.NewLine + "C) kajsflalskjflkasjflkaf" + 
                               Environment.NewLine + "D) alkfjaskffjlkasjfas";

            //title format

            Formatting titleFormatting = new Formatting();
            // font family
            titleFormatting.FontFamily = new Font("Century Gothic");
            // font size
            titleFormatting.Size = 18D;
            titleFormatting.Position = 2; // kurc
            titleFormatting.FontColor = Color.Black;
            titleFormatting.UnderlineColor = Color.Bisque;

            // paragraph format
            Formatting paraFormatting = new Formatting();
            paraFormatting.FontFamily = new Font("Century Gothic");
            paraFormatting.Size = 10D;
            paraFormatting.Spacing = 2;
            

            var doc = DocX.Create(fileName);
            Paragraph paragraphTitle = doc.InsertParagraph(title, false, titleFormatting);
            paragraphTitle.Alignment = Alignment.center;
            paragraphTitle.LineSpacingAfter = 10f;
            paragraphTitle.SpacingAfter(40d);
            
            //paragraphTitle.LineSpacingAfter = 10f;

            doc.InsertParagraph(paragraph, false, paraFormatting);
            doc.Save();
            

        }

        [TestMethod]
        public void improvedPage()
        {
            string file = @"E:\TestDocs\exempleWord.docx";

            using (var document = DocX.Create(file))
            {
                // document margin
                document.MarginLeft = 10f;
                
                
                document.Save();
            }
        }

        [TestMethod]
        public void TablesAndCells()
        {
            string file = @"E:\TestDocs\exempleWord.docx";

            using (var document = DocX.Create(file))
            {
                // Section 1
                // Set Page Orientation to Landscape.
                document.Sections[0].PageLayout.Orientation = Orientation.Landscape;
                // Add paragraphs in section 1.
                document.InsertParagraph("This is the first page in Landscape format.");
                // Add a section break as a page break to end section 1.
                // The new section properties will be based on last section properties.
                document.InsertSectionPageBreak();
                // Section 2
                // Set Page Orientation to Portrait.
                document.Sections[1].PageLayout.Orientation = Orientation.Portrait;
                // Add paragraphs in section 2.
                document.InsertParagraph("This is the second page in Portrait format.");
                // Add a section break as a page break to end section 2.
                // The new section properties will be based on last section properties.
                document.InsertSectionPageBreak();
                // Section 3
                // Set Page Orientation to Landscape.
                document.Sections[2].PageLayout.Orientation = Orientation.Landscape;
                // Add paragraphs in section 3.
                document.InsertParagraph("This is the third page in Landscape format.");
                document.Save();
            }
        }


        public string makeQuestionCase(int questionNum,string question,string ch1,string ch2,string ch3,string ch4)
        {
             string paragraph = "" + $"S{questionNum}) {question}"  +
                               Environment.NewLine + Environment.NewLine + $"A) {ch1}" +
                               Environment.NewLine + $"B) {ch2}" +
                               Environment.NewLine + $"C) {ch3}" +
                               Environment.NewLine + $"D) {ch4}";
             return paragraph;
        }

        [TestMethod]
        public void Create_Table()
        {
            string file = @"E:\TestDocs\exempleWord.docx";
            
            string title = "Ankara Üniversitesi 2.Güz dönemi Programlama Vizesi";

            

            using (var document = DocX.Create(file))
            {

                document.MarginRight = 20f;
                document.MarginLeft = 20f;
                document.MarginTop = 20f;
                document.MarginBottom = 20f;

                var Image = document.AddImage(@"C:\Users\Mehmet\Desktop\uniLogo.png");

                var picture = Image.CreatePicture();



                // title
                var headerTable = document.AddTable(1, 2);
                headerTable.Rows[0].Cells[0].Width = 100d;
                headerTable.Rows[0].Cells[1].Width = 700d;
                
                
                Paragraph header = headerTable.Rows[0].Cells[1].Paragraphs[0].Append(title);
                header.Alignment = Alignment.center;
                header.Color(Color.Black);
                header.FontSize(16d);
                header.SpacingAfter(25d);
                header.SpacingBefore(15d);
                header.IndentationAfter = 100f;
                
                
                Paragraph studentInfo = header.InsertParagraphAfterSelf("Öğrenci No :");
                studentInfo.Alignment = Alignment.right;
                studentInfo.IndentationAfter = 80f;
                



                headerTable.Design = TableDesign.None;
                headerTable.Rows[0].Cells[0].Paragraphs[0].AppendPicture(picture);
                //headerTable.SetWidthsPercentage(new float[]{7,93},1600f);

                document.InsertTable(headerTable);
                var space = document.InsertParagraph("  ");


                // title end


                // Create a table (initial size of 3 rows and 2 columns).
                var firstPageT = document.AddTable(6, 2);
                var secondPageT = document.AddTable(4, 2);
                firstPageT.Design = TableDesign.None;
                secondPageT.Design = TableDesign.None;

                firstPageT.SetTableCellMargin(TableCellMarginType.top, 5d);
                secondPageT.SetTableCellMargin(TableCellMarginType.top,5d);

                for (int j = 0; j < firstPageT.RowCount; j++)
                    firstPageT.Rows[j].Height = 115d;

                for (int i = 0; i < secondPageT.RowCount; i++)
                    secondPageT.Rows[i].Height = 190d;

                //t.Rows[0].Cells[0].Paragraphs[0].Append("as");
                firstPageT.SetWidths(new float[] { 800f, 800f }, true);
                secondPageT.SetWidths(new float[] { 800f, 800f }, true);

                int q = 1;
                for (int i = 0; i < firstPageT.ColumnCount; i++)
                {
                    for (int j = 0; j < firstPageT.RowCount; j++)
                    {
                        Paragraph question =  firstPageT.Rows[j].Cells[i].Paragraphs[0].Append(makeQuestionCase(q, "Hangisi a içerir bu bir metinsel olarak uzun soru deneme amaçlı yapıldı alt satıra geçmeli", "bu şıkta a yok",
                            "bu şıkta a var", "bu şıkta b var", "bu şıkta b yok"));
                        question.IndentationBefore = 10f;
                        q += 1;
                    }
                }
                

                XImage img = document.AddImage(GetImage());
                Picture p = img.CreatePicture();


                for (int i = 0; i < secondPageT.RowCount; i++)
                {
                    for (int j = 0; j < secondPageT.ColumnCount; j++)
                    {

                        Paragraph questionw = secondPageT.Rows[i].Cells[j].Paragraphs[0].AppendPicture(p).Append("\n");

                        questionw.Append(makeQuestionCase(q,
                            "Hangisi a içerir bu bir metinsel olarak uzun soru deneme amaçlı yapıldı alt satıra geçmeli",
                            "bu şıkta a yok",
                            "bu şıkta a var", "bu şıkta b var", "bu şıkta b yok"));
                        questionw.IndentationBefore = 10f;
                        q += 1;
                    }
                }


                







                //t.SetWidthsPercentage(new float[]{50,50},1600f);



                Border border = new Border();
                border.Tcbs = BorderStyle.Tcbs_thickThinSmallGap;
                border.Color = Color.Green;
                border.Size = BorderSize.five;
                
                firstPageT.SetBorder(TableBorderType.InsideH,border);

                // Add the table to the document
                document.InsertTable(firstPageT);
                document.InsertParagraph("");
                document.InsertTable(secondPageT);

                // Save the changes to the document
                document.Save();
            }
        }

        [TestMethod]
        public void Create_list()
        {
            string file = @"E:\TestDocs\exempleWord.docx";

            using (var document = DocX.Create(file))
            {
                // Add a numbered list where the first ListItem is starting with number 1.
                var numberedList = document.AddList("Berries", 0, ListItemType.Numbered, 1);
                // Add Sub-items(level 1) to the preceding ListItem.
                document.AddListItem(numberedList, "Strawberries", 1);
                document.AddListItem(numberedList, "Blueberries", 1);
                document.AddListItem(numberedList, "Raspberries", 1);
                
                
                // Add an item (level 0)
                document.AddListItem(numberedList, "Banana");
                // Add an item (level 0)
                document.AddListItem(numberedList, "Apple");
                // Add Sub-items(level 1) to the preceding ListItem.
                document.AddListItem(numberedList, "Red", 1);
                document.AddListItem(numberedList, "Green", 1);
                document.AddListItem(numberedList, "Yellow", 1);
                // Add a bulleted list with its first item.
                var bulletedList = document.AddList("Canada", 0, ListItemType.Bulleted);
                // Add Sub-items(level 1) to the preceding ListItem.
                document.AddListItem(bulletedList, "Toronto", 1);
                document.AddListItem(bulletedList, "Montreal", 1);
                // Add an item (level 0)
                document.AddListItem(bulletedList, "Brazil");
                // Add an item (level 0)
                document.AddListItem(bulletedList, "USA");
                // Add Sub-items(level 1) to the preceding ListItem.
                document.AddListItem(bulletedList, "New York", 1);
                // Add Sub-items(level 2) to the preceding ListItem.
                document.AddListItem(bulletedList, "Brooklyn", 2);
                document.AddListItem(bulletedList, "Manhattan", 2);
                document.AddListItem(bulletedList, "Los Angeles", 1);
                document.AddListItem(bulletedList, "Miami", 1);
                // Add an item (level 0)
                document.AddListItem(bulletedList, "France");
                // Add Sub-items(level 1) to the preceding ListItem.
                document.AddListItem(bulletedList, "Paris", 1);
                // Insert the lists into the document.
                document.InsertParagraph("This is a Numbered List:\n");
                document.InsertList(numberedList);
                document.InsertParagraph().SpacingAfter(40d);
                document.InsertParagraph("This is a Bulleted List:\n");
                document.InsertList(bulletedList, new Xceed.Document.NET.Font("Cooper Black"), 15);
                document.Save();
            }
        }

        [TestMethod]
        public void CompilerTests()
        {
            PageCompailer compailer = new PageCompailer();
            //compailer.CreateDocument(@"C:\Users\Mehmet\Desktop\sorular","deneme");
        }
    }


}
