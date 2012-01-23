﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using LateBindingApi.Core;
using Word = NetOffice.WordApi;
using NetOffice.WordApi.Enums;
using NetOffice.VBIDEApi.Enums;

namespace Example01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Initialize NetOffice
            LateBindingApi.Core.Factory.Initialize();

            // start word and turn off msg boxes
            Word.Application wordApplication = new Word.Application();
            wordApplication.DisplayAlerts = WdAlertLevel.wdAlertsNone;

            // add a new document
            Word.Document newDocument = wordApplication.Documents.Add();

            // insert some text
            wordApplication.Selection.TypeText("This text is written by NetOffice");

            wordApplication.Selection.HomeKey(WdUnits.wdLine, WdMovementType.wdExtend);
            wordApplication.Selection.Font.Color = WdColor.wdColorSeaGreen;
            wordApplication.Selection.Font.Bold = 1;
            wordApplication.Selection.Font.Size = 18;

            // save the document
            string fileExtension = GetDefaultExtension(wordApplication);
            object documentFile = string.Format("{0}\\Example01{1}", Application.StartupPath, fileExtension);
            newDocument.SaveAs(documentFile);

            // close word and dispose reference
            wordApplication.Quit();
            wordApplication.Dispose();

            FinishDialog fDialog = new FinishDialog("Document saved.", documentFile.ToString());
            fDialog.ShowDialog(this);
        }

        #region Helper

        /// <summary>
        /// returns the valid file extension for the instance. for example ".doc" or ".docx"
        /// </summary>
        /// <param name="application">the instance</param>
        /// <returns>the extension</returns>
        private static string GetDefaultExtension(Word.Application application)
        {
            double version = Convert.ToDouble(application.Version);
            if (version >= 120.00)
                return ".docx";
            else
                return ".doc";
        }

        #endregion
    }
}
