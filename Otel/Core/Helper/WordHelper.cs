using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace Otel.Core.Helper
{
    public class WordHelper
    {
        private FileInfo fileInfo;

        public WordHelper(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentNullException("Template file not found");
            }
            var temp = new FileInfo(filePath);
            fileInfo = temp.CopyTo(filePath.Replace("Marking_template.docx", "Marking_template_copy.docx"), true);
        }

        public void CreateCheck(Dictionary<string, string> informationPairs)
        {

            var app = new Word.Application();

            Object file = fileInfo.FullName;

            Object missing = Type.Missing;

            app.Documents.Open(file);

            foreach (var item in informationPairs)
            {
                Word.Find find = app.Selection.Find;
                find.Text = item.Key;
                find.Replacement.Text = item.Value;

                Object wrap = Word.WdFindWrap.wdFindContinue;
                Object replace = Word.WdReplace.wdReplaceAll;

                find.Execute(FindText: Type.Missing,
                    MatchCase: false,
                    MatchWholeWord: false,
                    MatchWildcards: false,
                    MatchSoundsLike: missing,
                    MatchAllWordForms: false,
                    Forward: true,
                    Wrap: wrap,
                    Format: false,
                    ReplaceWith: missing,
                    Replace: replace);
            }

            Object newFilePath = Path.Combine(fileInfo.DirectoryName, DateTime.Now.Date.ToString("yyyyMMdd HHmmss") + fileInfo.Name).Replace(".docx", ".pdf").Replace("check_template", informationPairs["{FIO}"]);

            try
            {
                app.ActiveDocument.SaveAs(newFilePath, Word.WdSaveFormat.wdFormatPDF);
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                app.ActiveDocument.Close();
                app.Quit();
            }
           

        }
    }
}
