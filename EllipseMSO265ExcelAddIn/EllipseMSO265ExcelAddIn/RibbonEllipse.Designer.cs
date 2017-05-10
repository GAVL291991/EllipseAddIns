﻿using System.ComponentModel;
using Microsoft.Office.Tools.Ribbon;

namespace EllipseMSO265ExcelAddIn
{
    partial class RibbonEllipse : RibbonBase
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private IContainer components = null;

        public RibbonEllipse()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabEllipse = this.Factory.CreateRibbonTab();
            this.grpMSO265 = this.Factory.CreateRibbonGroup();
            this.menuFormats = this.Factory.CreateRibbonMenu();
            this.btnFormatCesantias = this.Factory.CreateRibbonButton();
            this.drpEnviroment = this.Factory.CreateRibbonDropDown();
            this.menuActions = this.Factory.CreateRibbonMenu();
            this.btnReloadParameters = this.Factory.CreateRibbonButton();
            this.btnValidate = this.Factory.CreateRibbonButton();
            this.btnLoad = this.Factory.CreateRibbonButton();
            this.box1 = this.Factory.CreateRibbonBox();
            this.btnAbout = this.Factory.CreateRibbonButton();
            this.tabEllipse.SuspendLayout();
            this.grpMSO265.SuspendLayout();
            this.box1.SuspendLayout();
            // 
            // tabEllipse
            // 
            this.tabEllipse.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tabEllipse.Groups.Add(this.grpMSO265);
            this.tabEllipse.Label = "ELLIPSE 8";
            this.tabEllipse.Name = "tabEllipse";
            // 
            // grpMSO265
            // 
            this.grpMSO265.Items.Add(this.box1);
            this.grpMSO265.Items.Add(this.drpEnviroment);
            this.grpMSO265.Items.Add(this.menuActions);
            this.grpMSO265.Label = "MSO265";
            this.grpMSO265.Name = "grpMSO265";
            // 
            // menuFormats
            // 
            this.menuFormats.Items.Add(this.btnFormatCesantias);
            this.menuFormats.Label = "Formatos";
            this.menuFormats.Name = "menuFormats";
            // 
            // btnFormatCesantias
            // 
            this.btnFormatCesantias.Label = "Formato Cesantias";
            this.btnFormatCesantias.Name = "btnFormatCesantias";
            this.btnFormatCesantias.ShowImage = true;
            this.btnFormatCesantias.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFormatCesantias_Click);
            // 
            // drpEnviroment
            // 
            this.drpEnviroment.Label = "Env.";
            this.drpEnviroment.Name = "drpEnviroment";
            // 
            // menuActions
            // 
            this.menuActions.Items.Add(this.btnReloadParameters);
            this.menuActions.Items.Add(this.btnValidate);
            this.menuActions.Items.Add(this.btnLoad);
            this.menuActions.Label = "Acciones";
            this.menuActions.Name = "menuActions";
            // 
            // btnReloadParameters
            // 
            this.btnReloadParameters.Label = "Recargar Parametros";
            this.btnReloadParameters.Name = "btnReloadParameters";
            this.btnReloadParameters.ShowImage = true;
            this.btnReloadParameters.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnReloadParameters_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.Label = "Validar";
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.ShowImage = true;
            this.btnValidate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnValidate_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Label = "Cargar Datos";
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.ShowImage = true;
            this.btnLoad.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLoad_Click);
            // 
            // box1
            // 
            this.box1.Items.Add(this.menuFormats);
            this.box1.Items.Add(this.btnAbout);
            this.box1.Name = "box1";
            // 
            // btnAbout
            // 
            this.btnAbout.Label = "?";
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAbout_Click);
            // 
            // RibbonEllipse
            // 
            this.Name = "RibbonEllipse";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tabEllipse);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonEllipse_Load);
            this.tabEllipse.ResumeLayout(false);
            this.tabEllipse.PerformLayout();
            this.grpMSO265.ResumeLayout(false);
            this.grpMSO265.PerformLayout();
            this.box1.ResumeLayout(false);
            this.box1.PerformLayout();

        }

        #endregion

        internal RibbonTab tabEllipse;
        internal RibbonGroup grpMSO265;
        internal RibbonMenu menuFormats;
        internal RibbonDropDown drpEnviroment;
        internal RibbonMenu menuActions;
        internal RibbonButton btnReloadParameters;
        internal RibbonButton btnFormatCesantias;
        internal RibbonButton btnValidate;
        internal RibbonButton btnLoad;
        internal RibbonBox box1;
        internal RibbonButton btnAbout;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonEllipse RibbonEllipse
        {
            get { return this.GetRibbon<RibbonEllipse>(); }
        }
    }
}