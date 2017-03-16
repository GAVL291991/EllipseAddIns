﻿namespace EllipseQueryLoaderExcelAddIn
{
    partial class RibbonEllipse : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonEllipse()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabEllipse = this.Factory.CreateRibbonTab();
            this.grpEllipseQueryLoaderExcelAddIn = this.Factory.CreateRibbonGroup();
            this.btnFormatSheet = this.Factory.CreateRibbonButton();
            this.drpEnviroment = this.Factory.CreateRibbonDropDown();
            this.menuActions = this.Factory.CreateRibbonMenu();
            this.btnExecuteQuery = this.Factory.CreateRibbonButton();
            this.btnReadFromText = this.Factory.CreateRibbonButton();
            this.btnReadFromFile = this.Factory.CreateRibbonButton();
            this.btnStopThread = this.Factory.CreateRibbonButton();
            this.btnCleanSheet = this.Factory.CreateRibbonButton();
            this.cbIgnoreOperators = this.Factory.CreateRibbonCheckBox();
            this.tabEllipse.SuspendLayout();
            this.grpEllipseQueryLoaderExcelAddIn.SuspendLayout();
            // 
            // tabEllipse
            // 
            this.tabEllipse.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tabEllipse.Groups.Add(this.grpEllipseQueryLoaderExcelAddIn);
            this.tabEllipse.Label = "ELLIPSE 8";
            this.tabEllipse.Name = "tabEllipse";
            // 
            // grpEllipseQueryLoaderExcelAddIn
            // 
            this.grpEllipseQueryLoaderExcelAddIn.Items.Add(this.btnFormatSheet);
            this.grpEllipseQueryLoaderExcelAddIn.Items.Add(this.drpEnviroment);
            this.grpEllipseQueryLoaderExcelAddIn.Items.Add(this.menuActions);
            this.grpEllipseQueryLoaderExcelAddIn.Label = "QueryLoader v.1.0.0";
            this.grpEllipseQueryLoaderExcelAddIn.Name = "grpEllipseQueryLoaderExcelAddIn";
            // 
            // btnFormatSheet
            // 
            this.btnFormatSheet.Label = "&Formatear Hoja";
            this.btnFormatSheet.Name = "btnFormatSheet";
            this.btnFormatSheet.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFormatSheet_Click);
            // 
            // drpEnviroment
            // 
            this.drpEnviroment.Label = "&Env.";
            this.drpEnviroment.Name = "drpEnviroment";
            // 
            // menuActions
            // 
            this.menuActions.Items.Add(this.btnExecuteQuery);
            this.menuActions.Items.Add(this.btnReadFromText);
            this.menuActions.Items.Add(this.btnReadFromFile);
            this.menuActions.Items.Add(this.cbIgnoreOperators);
            this.menuActions.Items.Add(this.btnStopThread);
            this.menuActions.Items.Add(this.btnCleanSheet);
            this.menuActions.Label = "&Acciones";
            this.menuActions.Name = "menuActions";
            // 
            // btnExecuteQuery
            // 
            this.btnExecuteQuery.Label = "&Ejecutar Consulta";
            this.btnExecuteQuery.Name = "btnExecuteQuery";
            this.btnExecuteQuery.ShowImage = true;
            this.btnExecuteQuery.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnExecuteQuery_Click);
            // 
            // btnReadFromText
            // 
            this.btnReadFromText.Label = "Cargar de &Celda";
            this.btnReadFromText.Name = "btnReadFromText";
            this.btnReadFromText.ShowImage = true;
            this.btnReadFromText.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnReadFromText_Click);
            // 
            // btnReadFromFile
            // 
            this.btnReadFromFile.Label = "Cargar de &Archivo";
            this.btnReadFromFile.Name = "btnReadFromFile";
            this.btnReadFromFile.ShowImage = true;
            this.btnReadFromFile.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnReadFromFile_Click);
            // 
            // btnStopThread
            // 
            this.btnStopThread.Label = "&Detener Proceso";
            this.btnStopThread.Name = "btnStopThread";
            this.btnStopThread.ShowImage = true;
            this.btnStopThread.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnStopThread_Click);
            // 
            // btnCleanSheet
            // 
            this.btnCleanSheet.Label = "&Limpiar Consulta";
            this.btnCleanSheet.Name = "btnCleanSheet";
            this.btnCleanSheet.ShowImage = true;
            this.btnCleanSheet.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnCleanSheet_Click);
            // 
            // cbIgnoreOperators
            // 
            this.cbIgnoreOperators.Checked = true;
            this.cbIgnoreOperators.Label = "&Ignorar Operadores";
            this.cbIgnoreOperators.Name = "cbIgnoreOperators";
            // 
            // RibbonEllipse
            // 
            this.Name = "RibbonEllipse";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tabEllipse);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonEllipse_Load);
            this.tabEllipse.ResumeLayout(false);
            this.tabEllipse.PerformLayout();
            this.grpEllipseQueryLoaderExcelAddIn.ResumeLayout(false);
            this.grpEllipseQueryLoaderExcelAddIn.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabEllipse;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpEllipseQueryLoaderExcelAddIn;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFormatSheet;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown drpEnviroment;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuActions;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnExecuteQuery;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnStopThread;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnCleanSheet;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnReadFromText;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnReadFromFile;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox cbIgnoreOperators;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonEllipse RibbonEllipse
        {
            get { return this.GetRibbon<RibbonEllipse>(); }
        }
    }
}
