﻿using System;
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
            fileInfo = new FileInfo(filePath);
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

            Object newFilePath = Path.Combine(fileInfo.DirectoryName, DateTime.Now.Date.ToString("yyyyMMdd HHmmss") + fileInfo.Name);

            try
            {
                app.ActiveDocument.ExportAsFixedFormat(newFilePath.ToString(), Word.WdExportFormat.wdExportFormatPDF);
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