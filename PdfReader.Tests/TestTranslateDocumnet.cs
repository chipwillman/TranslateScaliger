using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Google.Cloud.Translation.V2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PageExtractor;
using TikaOnDotNet.TextExtraction;

namespace PdfReader.Tests
{
    [TestClass]
    public class TestTranslateDocumnet
    {
        [TestMethod]
        public void TestExtractTextAndWash()
        {
            var pageNumber = 14;
            for (int i = pageNumber; i < 970; i++)
            {
                var sourcePath = @"c:\code\PdfTranslator\Solution1\PageExtractor.Tests\bin\Debug\Extracted\";
                var pdfFilename = "Page" + i + "_Iosephi_Scaligeri_Opus_de_emendatione_te.pdf";
                var outFilename = "Page" + i.ToString("00#") + "_cleanedText_Iosephi_Scaligeri_Opus_de_emendatione_te.txt";
                var outDirectory = @"c:\code\PdfTranslator\Solution1\Output\Latin";
                var te = new TextExtractor();
                var textContents = te.Extract(Path.Combine(sourcePath, pdfFilename));

                Assert.IsNotNull(textContents);

                var pe = new PageFormatter() { OriginalText = textContents.Text };

                var correctedText = pe.FixCommonOCRErrors();

                Assert.IsFalse(correctedText.Contains(" fed "));

                File.WriteAllText(Path.Combine(outDirectory, outFilename), correctedText);
            }
        }

        [TestMethod]
        [Ignore]
        public void TestTranslateText()
        {
            var pages = System.IO.File.ReadAllLines("de_emendatione_temporum_extracted.txt");

            var translatedText = new List<string>();

            TranslationClient client = TranslationClient.Create();
            int pageNumber = 0;
            int startPage = 165;
            for (int i = startPage; i < pages.Length; i++)
            {
                var page = pages[i];
                try
                {
                    if (page.Length > 0)
                    {
                        //var response = client.TranslateText(page, "en");
                        //translatedText.Add(response.TranslatedText);
                        //System.IO.File.WriteAllText("de_emendatione_temporum_translated_" + pageNumber, response.TranslatedText);
                        //System.Threading.Thread.Sleep(3000);
                    }
                }
                catch (Exception)
                {

                }
            }

            System.IO.File.WriteAllLines("de_emendatione_temporum_translated_165_to_500.txt", translatedText.ToArray());
        }

        [TestMethod]
        public void TestTranslateScaliger()
        {
            var te = new TextExtractor();
            var pdfContents = te.Extract(@"Iosephi_Scaligeri_Opus_de_emendatione_te.pdf");

            Assert.IsNotNull(pdfContents);

            var lines = FormatLines(pdfContents);

            Assert.IsTrue(lines.Length > 10);

            var pages = new List<string>();

            var output = new StringBuilder();
            foreach(var line in lines)
            {
                if (output.Length + line.Length < 5000)
                {
                    output.Append(line);
                }
                else
                {
                    pages.Add(output.ToString());
                    output.Clear();
                }
            }
            if (output.Length > 0)
            {
                pages.Add(output.ToString());
            }

            Assert.IsNotNull(pages[0]);

            var translatedText = new List<string>();

            TranslationClient client = TranslationClient.Create();
            int startPage = 162;
            for (int i = startPage; i < pages.Count; i++)
            {
                var page = pages[i];
                try
                {
                    if (page.Length > 0)
                    {
                        //var response = client.TranslateText(page.Replace("fign","sign").Replace("ff", "ss").Replace("bf", "bs").Replace("fs", "ss").Replace("fc", "sc").Replace("ft", "st").Replace("fol", "sol"), "en");
                        //translatedText.Add(response.TranslatedText);
                        //System.IO.File.WriteAllText("Translation\\de_emendatione_temporum_translated_" + i, response.TranslatedText);
                        //System.Threading.Thread.Sleep(5000);
                    }
                }
                catch (Exception)
                {

                }
            }

            var stream = System.IO.File.AppendText("de_emendatione_temporum_translated.txt");
            for(int i = startPage; i < translatedText.Count; i++)
            {
                var page = translatedText[i];
                stream.WriteLine("Page " + (i + 1));
                stream.WriteLine();
                stream.WriteLine(page);
                stream.WriteLine();
            }
        }


        private string[] FormatLines(TextExtractionResult pdfContents)
        {
            var result = new List<string>();
            var pdfText = pdfContents.Text.Replace("\r\n\r\n", "@@NEWPARAGRAPH@@").Replace("-\r\n", "");

            var sb = new StringBuilder();
            var paragraphs = pdfText.Replace("@@NEWPARAGRAPH@@", "\n").Replace("\n\n", "").Split('\n');
            foreach (var paragraph in paragraphs)
            {
                var sentenceParts = paragraph.Split('.');
                for (int i = 0; i < sentenceParts.Length; i++)
                {
                    sb.Append(sentenceParts[i].Replace("-", ""));

                    if (sb.Length > 0)
                    {
                        result.Add(sb.ToString() + ".");
                        sb.Clear();
                    }
                }
                result.Add("\r\n");
            }
            return result.ToArray();
        }
    }
}
