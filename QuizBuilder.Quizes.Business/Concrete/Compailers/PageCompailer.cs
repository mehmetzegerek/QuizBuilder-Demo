using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.Business.Abstract;
using QuizBuilder.Quizes.Entities.Concrete;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Image = System.Drawing.Image;
using XImage = Xceed.Document.NET.Image;

namespace QuizBuilder.Quizes.Business.Concrete.Compailers
{
    public class PageCompailer:IPageCompailer
    {
        private QuestionCompiler _qCompailer = new QuestionCompiler();
        public string GetPath(string path, string fileName)
        {
            string fullPath = string.Format(@"{0}\{1}.docx", path, fileName);
            return fullPath;
        }

        private MemoryStream GetImage(byte[] imageDataBytes)
        {
            

            MemoryStream stream = new MemoryStream(imageDataBytes);
            Image img = Image.FromStream(stream);
            return stream;

        }

        public void Create20QuestionsPage(string path,string quizTitle,string logoPath,List<Soru> questionList)
        {

            string file = path;

            string title = quizTitle;

            List<Soru> questionsWithNonePic = new List<Soru>();
            List<Soru> questionsWithPic = new List<Soru>();


            using (var document = DocX.Create(file))
            {

                document.MarginRight = 20f;
                document.MarginLeft = 20f;
                document.MarginTop = 20f;
                document.MarginBottom = 20f;

                var Image = document.AddImage(logoPath);

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
                secondPageT.SetTableCellMargin(TableCellMarginType.top, 5d);

                for (int j = 0; j < firstPageT.RowCount; j++)
                    firstPageT.Rows[j].Height = 115d;

                for (int i = 0; i < secondPageT.RowCount; i++)
                    secondPageT.Rows[i].Height = 190d;

                //t.Rows[0].Cells[0].Paragraphs[0].Append("as");
                firstPageT.SetWidths(new float[] { 800f, 800f }, true);
                secondPageT.SetWidths(new float[] { 800f, 800f }, true);


                foreach (Soru soru in questionList)
                {
                    if (soru.Gorsel == null && soru.Klasik == false && !(questionsWithNonePic.Count>11))
                    {
                        questionsWithNonePic.Add(soru);
                    }
                    else
                    {
                        questionsWithPic.Add(soru);
                    }
                }




                int q = 0;
                for (int i = 0; i < firstPageT.ColumnCount; i++)
                {
                    for (int j = 0; j < firstPageT.RowCount; j++)
                    {
                        Paragraph question = firstPageT.Rows[j].Cells[i].Paragraphs[0].Append(_qCompailer.FormatWithNonePictureStylish(questionsWithNonePic[q],Convert.ToInt16(q+1)));
                        question.IndentationBefore = 10f;
                        q += 1;
                    }
                }


                

                q = 0;
                for (int i = 0; i < secondPageT.RowCount; i++)
                {
                    for (int j = 0; j < secondPageT.ColumnCount; j++)
                    {
                       

                        if (questionsWithPic[q].Klasik == false && questionsWithPic[q].Gorsel != null)
                        {
                            XImage img = document.AddImage(GetImage(questionsWithPic[q].Gorsel));
                            Picture p = img.CreatePicture();
                            Paragraph questionw = secondPageT.Rows[i].Cells[j].Paragraphs[0].AppendPicture(p).Append("\n");
                            
                            questionw.Append(_qCompailer.FormatWithNonePictureStylish(questionsWithPic[q], Convert.ToInt16(q + 13)));
                            questionw.IndentationBefore = 10f;
                        }
                        else if (questionsWithPic[q].Gorsel == null && questionsWithPic[q].Klasik == false)
                        {
                            Paragraph question = secondPageT.Rows[i].Cells[j].Paragraphs[0].Append(_qCompailer.FormatWithNonePictureStylish(questionsWithNonePic[q], Convert.ToInt16(q + 13)));
                            question.IndentationBefore = 10f;
                        }
                        else if (questionsWithPic[q].Gorsel == null && questionsWithPic[q].Klasik == true)
                        {
                            Paragraph questionw = secondPageT.Rows[i].Cells[j].Paragraphs[0].Append(_qCompailer.FormatWithPictureClassic(questionsWithPic[q], Convert.ToInt16(q + 13)));
                            questionw.IndentationBefore = 10f;
                        }
                        else
                        {
                            XImage img = document.AddImage(GetImage(questionsWithPic[q].Gorsel));
                            Picture p = img.CreatePicture();
                            Paragraph questionw = secondPageT.Rows[i].Cells[j].Paragraphs[0].AppendPicture(p).Append("\n");
                            questionw.Append(_qCompailer.FormatWithPictureClassic(questionsWithPic[q], Convert.ToInt16(q + 13)));
                            questionw.IndentationBefore = 10f;

                        }


                        q += 1;
                    }
                }










                //t.SetWidthsPercentage(new float[]{50,50},1600f);



                Border border = new Border();
                border.Tcbs = BorderStyle.Tcbs_thickThinSmallGap;
                border.Color = Color.Green;
                border.Size = BorderSize.five;

                firstPageT.SetBorder(TableBorderType.InsideH, border);

                // Add the table to the document
                document.InsertTable(firstPageT);
                document.InsertParagraph("");
                document.InsertTable(secondPageT);

                // Save the changes to the document
                document.Save();
            }
        }

        
    }
}
