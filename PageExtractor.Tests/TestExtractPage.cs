using System;
using System.IO;
using EO.Pdf;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PageExtractor.Tests
{
    [TestClass]
    public class TestExtractPage
    {
        [TestMethod]
        public void TestCreatePages()
        {
            var pdfFileName = @"c:\code\PdfTranslator\Solution1\PdfReader.Tests\Iosephi_Scaligeri_Opus_de_emendatione_te.pdf";
            var newFileName = @"Iosephi_Scaligeri_Opus_de_emendatione_te.pdf";
            var doc = new PdfDocument(pdfFileName);

            //Extract 3 pages, starting from the second page (page index is zero based)
            for (int i = 0; i < doc.Pages.Count; i++)
            {
                doc = new PdfDocument(pdfFileName);
                PdfDocument doc2 = doc.Clone(i, 1);

                if (!Directory.Exists("Extracted"))
                {
                    Directory.CreateDirectory("Extracted");
                }
                //Save the extracted pages to a new file
                doc2.Save(Path.Combine("Extracted", "Page" + i + "_" + newFileName));
            }
        }
    }
}
