﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using EllipseCommonsClassLibrary;
using EllipseCommonsClassLibrary.Classes;
using EllipseCommonsClassLibrary.Connections;
using EllipseCommonsClassLibrary.Constants;
using EllipseJobsClassLibrary;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;
using Application = Microsoft.Office.Interop.Excel.Application;

// ReSharper disable UseNullPropagation
// ReSharper disable UseStringInterpolation
// ReSharper disable UseIndexedProperty
// ReSharper disable SuggestVarOrType_Elsewhere

namespace EllipsePlannerExcelAddIn
{
    public partial class RibbonEllipse
    {
        private ExcelStyleCells _cells;
        private readonly EllipseFunctions _eFunctions = new EllipseFunctions();
        private Application _excelApp;
        private readonly FormAuthenticate _frmAuth = new FormAuthenticate();
        private Thread _thread;

        //Hojas
        private const string ValidationSheetName = "Validacion";
        private const string ResourcesSheetName = "Recursos Planeados";
        private const string EllipseResourcesSheetName = "Recursos Estimados";

        //Tablas
        private const string TableJobResources = "JobResources";
        private const string TableEllipseResources = "EllipseResources";
        private const string TableIndicator = "Indicator";
        private const string TablePsoftResources = "PsoftResources";

        //Titulos
        private const int TitleRowResources = 8;
        private const int TitleRowEllipse = 6;

        //Columnas de Resultado
        private const int ResultColumnResources = 12;
        private const int ResultColumnEllipse = 5;

        private void RibbonEllipse_Load(object sender, RibbonUIEventArgs e)
        {
            _excelApp = Globals.ThisAddIn.Application;
            var enviroments = Environments.GetEnviromentList();
            foreach (var env in enviroments)
            {
                var item = Factory.CreateRibbonDropDownItem();
                item.Label = env;
                drpEnviroment.Items.Add(item);
            }
        }

        private void btnAbout_Click(object sender, RibbonControlEventArgs e)
        {
            new AboutBoxExcelAddIn().ShowDialog();
        }

        private void btnStopThread_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (_thread != null && _thread.IsAlive)
                    _thread.Abort();
                if (_cells != null) _cells.SetCursorDefault();
            }
            catch (ThreadAbortException ex)
            {
                MessageBox.Show(@"Se ha detenido el proceso. " + ex.Message);
            }
        }

        private void btnFormatSheet_Click(object sender, RibbonControlEventArgs e)
        {
            FormatSheet();
        }

        private void FormatSheet()
        {

            try
            {
                _excelApp = Globals.ThisAddIn.Application;
                _eFunctions.SetDBSettings(drpEnviroment.SelectedItem.Label);

                _excelApp.Workbooks.Add();
                while (_excelApp.ActiveWorkbook.Sheets.Count < 3)
                    _excelApp.ActiveWorkbook.Worksheets.Add();

                if (_cells == null)
                    _cells = new ExcelStyleCells(_excelApp);

                _cells.SetCursorWait();

                //hoja de validación
                _cells.CreateNewWorksheet(ValidationSheetName);


                //hoja 1
                _excelApp.ActiveWorkbook.ActiveSheet.Name = ResourcesSheetName;
                var districtList = Districts.GetDistrictList();

                var searchCriteriaList = SearchFieldCriteriaType.GetSearchFieldCriteriaTypes().Select(g => g.Value).ToList();
                var workGroupList = Groups.GetWorkGroupList().Select(g => g.Name).ToList();

                _cells.GetCell("A3").Value = "DISTRITO";
                _cells.GetCell("B3").Value = Districts.DefaultDistrict;

                _cells.SetValidationList(_cells.GetCell("B3"), districtList, ValidationSheetName, 1);

                _cells.GetCell("A4").Value = SearchFieldCriteriaType.WorkGroup.Value;
                _cells.GetCell("A4").AddComment("--ÁREA GERENCIAL/SUPERINTENDENCIA--\n" +
                    "INST: IMIS, MINA\n" +
                    "" + ManagementArea.ManejoDeCarbon.Key + ": " + QuarterMasters.Ferrocarril.Key + ", " + QuarterMasters.PuertoBolivar.Key + ", " + QuarterMasters.PlantasDeCarbon.Key + "\n" +
                    "" + ManagementArea.Mantenimiento.Key + ": MINA\n" +
                    "" + ManagementArea.SoporteOperacion.Key + ": ENERGIA, LIVIANOS, MEDIANOS, GRUAS, ENERGIA");
                _cells.GetCell("A4").Comment.Shape.TextFrame.AutoSize = true;

                _cells.GetCell("A5").Value = "Trabajos Adicionales";

                var aditionalJobsLis = new List<string> { "Backlog", "Unscheduled", "Backlog and Unscheduled", "Backlog Only", "Unscheduled Only", "Backlog and Unscheduled Only" };

                _cells.SetValidationList(_cells.GetCell("A4"), searchCriteriaList, ValidationSheetName, 2);
                _cells.SetValidationList(_cells.GetCell("B4"), workGroupList, ValidationSheetName, 3, false);
                _cells.SetValidationList(_cells.GetCell("B5"), aditionalJobsLis, ValidationSheetName, 4, false);

                _cells.GetRange("A3", "A5").Style = _cells.GetStyle(StyleConstants.Option);
                _cells.GetRange("B3", "B5").Style = _cells.GetStyle(StyleConstants.Select);

                _cells.GetCell("C3").Value = "FECHA";
                _cells.GetCell("D3").Value = SearchDateCriteriaType.PlannedStart.Value;
                _cells.GetCell("C4").Value = "DESDE";
                _cells.GetCell("D4").Value = string.Format("{0:0000}", DateTime.Now.Year) + "0101";
                _cells.GetCell("D4").AddComment("YYYYMMDD");
                _cells.GetCell("C5").Value = "HASTA";
                _cells.GetCell("D5").Value = string.Format("{0:0000}", DateTime.Now.Year) + string.Format("{0:00}", DateTime.Now.Month) + string.Format("{0:00}", DateTime.Now.Day);
                _cells.GetCell("D5").AddComment("YYYYMMDD");
                _cells.GetRange("C3", "C5").Style = _cells.GetStyle(StyleConstants.Option);
                _cells.GetRange("D3", "D5").Style = _cells.GetStyle(StyleConstants.Select);

                _cells.GetCell("A1").Value = "CERREJÓN";
                _cells.GetCell("A1").Style = StyleConstants.HeaderDefault;
                _cells.MergeCells("A1", "B2");
                _cells.GetCell("C1").Value = "RECURSOS NECESARIOS - ELLIPSE 8";
                _cells.GetCell("C1").Style = StyleConstants.HeaderDefault;
                _cells.MergeCells("C1", "J2");
                _cells.GetCell("K1").Value = "OBLIGATORIO";
                _cells.GetCell("K1").Style = StyleConstants.TitleRequired;
                _cells.GetCell("K2").Value = "OPCIONAL";
                _cells.GetCell("K2").Style = StyleConstants.TitleOptional;
                _cells.GetCell("K3").Value = "INFORMATIVO";
                _cells.GetCell("K3").Style = StyleConstants.TitleInformation;
                _cells.GetCell("K4").Value = "ACCIÓN A REALIZAR";
                _cells.GetCell("K4").Style = StyleConstants.TitleAction;

                _cells.GetCell(1, TitleRowResources).Value = "Grupo";
                _cells.GetCell(2, TitleRowResources).Value = "Equipo";
                _cells.GetCell(3, TitleRowResources).Value = "Eq Desc";
                _cells.GetCell(4, TitleRowResources).Value = "MST";
                _cells.GetCell(5, TitleRowResources).Value = "Referencia";
                _cells.GetCell(6, TitleRowResources).Value = "Ref Desc";
                _cells.GetCell(7, TitleRowResources).Value = "Tarea";
                _cells.GetCell(8, TitleRowResources).Value = "Recurso";
                _cells.GetCell(9, TitleRowResources).Value = "Horas Estimadas";
                _cells.GetCell(10, TitleRowResources).Value = "Horas Reales";
                _cells.GetCell(11, TitleRowResources).Value = "Horas restantes";
                _cells.GetCell(12, TitleRowResources).Value = "Fecha Planeada";
                _cells.GetRange(1, TitleRowResources, ResultColumnResources, TitleRowResources).Style = StyleConstants.TitleInformation;
                _cells.FormatAsTable(_cells.GetRange(1, TitleRowResources, ResultColumnResources, TitleRowResources + 1), TableJobResources);
                _cells.GetRange(1, TitleRowResources, ResultColumnResources, TitleRowResources + 1).NumberFormat = NumberFormatConstants.Text;
                _cells.GetRange(12, TitleRowResources, 12, TitleRowResources + 1).NumberFormat = NumberFormatConstants.Date;

                //hoja 2
                _excelApp.ActiveWorkbook.Sheets.get_Item(2).Activate();
                _excelApp.ActiveWorkbook.ActiveSheet.Name = EllipseResourcesSheetName;

                _cells.GetCell("A1").Value = "CERREJÓN";
                _cells.GetCell("A1").Style = StyleConstants.HeaderDefault;
                _cells.MergeCells("A1", "B2");
                _cells.GetCell("C1").Value = "RECURSOS ELLIPSE - ELLIPSE 8";
                _cells.GetCell("C1").Style = StyleConstants.HeaderDefault;
                _cells.MergeCells("C1", "J2");
                _cells.GetCell("K1").Value = "OBLIGATORIO";
                _cells.GetCell("K1").Style = StyleConstants.TitleRequired;
                _cells.GetCell("K2").Value = "OPCIONAL";
                _cells.GetCell("K2").Style = StyleConstants.TitleOptional;
                _cells.GetCell("K3").Value = "INFORMATIVO";
                _cells.GetCell("K3").Style = StyleConstants.TitleInformation;
                _cells.GetCell("K4").Value = "ACCIÓN A REALIZAR";
                _cells.GetCell("K4").Style = StyleConstants.TitleAction;

                _cells.GetCell(1, TitleRowEllipse).Value = "Grupo";
                _cells.GetCell(2, TitleRowEllipse).Value = "Recurso";
                _cells.GetCell(3, TitleRowEllipse).Value = "Fecha";
                _cells.GetCell(4, TitleRowEllipse).Value = "Planeadas";
                _cells.GetCell(5, TitleRowEllipse).Value = "Disponibles";
                _cells.GetRange(1, TitleRowEllipse, ResultColumnEllipse, TitleRowEllipse).Style = StyleConstants.TitleInformation;
                _cells.FormatAsTable(_cells.GetRange(1, TitleRowEllipse, ResultColumnEllipse, TitleRowEllipse + 1), TableEllipseResources);
                _cells.GetRange(1, TitleRowEllipse, ResultColumnEllipse, TitleRowEllipse + 1).NumberFormat = NumberFormatConstants.Text;
                _cells.GetRange(3, TitleRowEllipse, 3, TitleRowEllipse + 1).NumberFormat = NumberFormatConstants.Date;

                _cells.GetCell(7, TitleRowEllipse).Value = "Grupo";
                _cells.GetCell(8, TitleRowEllipse).Value = "Fecha";
                _cells.GetCell(9, TitleRowEllipse).Value = "Horas Planeadas";
                _cells.GetCell(10, TitleRowEllipse).Value = "Horas Disponibles";
                _cells.GetCell(11, TitleRowEllipse).Value = "Indicador de Planeacion";
                _cells.GetRange(7, TitleRowEllipse, 11, TitleRowEllipse).Style = StyleConstants.TitleInformation;
                _cells.FormatAsTable(_cells.GetRange(7, TitleRowEllipse, 11, TitleRowEllipse + 1), TableIndicator);
                _cells.GetCell(7, TitleRowEllipse + 1).NumberFormat = NumberFormatConstants.Text;
                _cells.GetCell(8, TitleRowEllipse + 1).NumberFormat = NumberFormatConstants.Date;
                _cells.GetRange(9, TitleRowEllipse, 10, TitleRowEllipse + 1).NumberFormat = NumberFormatConstants.Text;
                _cells.GetCell(11, TitleRowEllipse + 1).NumberFormat = NumberFormatConstants.Percentage;

                _cells.GetCell(13, TitleRowEllipse).Value = "Grupo";
                _cells.GetCell(14, TitleRowEllipse).Value = "fecha";
                _cells.GetCell(15, TitleRowEllipse).Value = "Recurso";
                _cells.GetCell(16, TitleRowEllipse).Value = "Cantidad";
                _cells.GetCell(17, TitleRowEllipse).Value = "Estimado";
                _cells.GetCell(18, TitleRowEllipse).Value = "Disponible";
                _cells.GetRange(13, TitleRowEllipse, 18, TitleRowEllipse).Style = StyleConstants.TitleInformation;
                _cells.FormatAsTable(_cells.GetRange(13, TitleRowEllipse, 18, TitleRowEllipse + 1), TablePsoftResources);
                _cells.GetCell(13, TitleRowEllipse).NumberFormat = NumberFormatConstants.Text;
                _cells.GetCell(14, TitleRowEllipse).NumberFormat = NumberFormatConstants.Date;
                _cells.GetRange(15, TitleRowEllipse, 18, TitleRowEllipse + 1).NumberFormat = NumberFormatConstants.Text;

                _excelApp.ActiveWorkbook.Sheets[1].Select(Type.Missing);
            }
            catch (Exception ex)
            {
                Debugger.LogError("RibbonEllipse:FormatSheet()", "\n\rMessage:" + ex.Message + "\n\rSource:" + ex.Source + "\n\rStackTrace:" + ex.StackTrace);
                MessageBox.Show(@"Se ha producido un error al intentar crear el encabezado de la hoja. " + ex.Message);
            }
            finally
            {
                if (_cells != null) _cells.SetCursorDefault();
            }
        }

        private void btnReviewJobs_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (_excelApp.ActiveWorkbook.ActiveSheet.Name == ResourcesSheetName)
                {
                    //si ya hay un thread corriendo que no se ha detenido
                    if (_thread != null && _thread.IsAlive) return;
                    _frmAuth.StartPosition = FormStartPosition.CenterScreen;
                    _frmAuth.SelectedEnviroment = drpEnviroment.SelectedItem.Label;
                    if (_frmAuth.ShowDialog() != DialogResult.OK) return;
                    if (_thread != null && _thread.IsAlive) return;
                    _thread = new Thread(ReviewJobListPost);

                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start();
                }
                else
                    MessageBox.Show(@"La hoja de Excel seleccionada no tiene el formato válido para realizar la acción");
            }
            catch (Exception ex)
            {
                Debugger.LogError("RibbonEllipse.cs:ReviewJobList()", "\n\rMessage: " + ex.Message + "\n\rSource: " + ex.Source + "\n\rStackTrace: " + ex.StackTrace);
                MessageBox.Show(@"Se ha producido un error: " + ex.Message);
            }
        }

        private void ReviewJobListPost()
        {
            try
            {
                if (_cells == null)
                    _cells = new ExcelStyleCells(_excelApp);
                _cells.SetCursorWait();

                //hoja de Tareas
                _cells.ClearTableRange(TableJobResources);

                var urlServicePost = _eFunctions.GetServicesUrl(drpEnviroment.SelectedItem.Label, ServiceType.PostService);
                var searchCriteriaList = SearchFieldCriteriaType.GetSearchFieldCriteriaTypes();
                var district = _cells.GetEmptyIfNull(_cells.GetCell("B3").Value);
                var searchCriteriaKey1Text = _cells.GetEmptyIfNull(_cells.GetCell("A4").Value);
                var searchCriteriaValue1 = _cells.GetEmptyIfNull(_cells.GetCell("B4").Value);
                var dateInclude = _cells.GetEmptyIfNull(_cells.GetCell("B5").Value);
                var startDate = _cells.GetEmptyIfNull(_cells.GetCell("D4").Value);
                var endDate = _cells.GetEmptyIfNull(_cells.GetCell("D5").Value);
                var searchCriteriaKey1 = searchCriteriaList.FirstOrDefault(v => v.Value.Equals(searchCriteriaKey1Text)).Key;

                _eFunctions.SetPostService(_frmAuth.EllipseUser, _frmAuth.EllipsePswd, _frmAuth.EllipsePost, _frmAuth.EllipseDsct, urlServicePost);


                //consumo de servicio de msewts
                List<Jobs> ellipseJobs = JobActions.FetchJobsPost(_eFunctions, district, dateInclude, searchCriteriaKey1, searchCriteriaValue1, startDate, endDate);

                //consulta sobre tabla de peoplesoft
                List<LabourResources> psoftResources = JobActions.GetPsoftResources(district, searchCriteriaKey1, searchCriteriaValue1, startDate, endDate);

                //recursos planeados ellipse agrupados por grupo/fecha/recurso
                var ellipseTotalresource = (from jobs in ellipseJobs from resources in jobs.LabourResourcesList select resources).GroupBy(l => new { l.WorkGroup, l.Date, l.ResourceCode })
                    .Select(cl => new LabourResources
                    {
                        WorkGroup = cl.First().WorkGroup,
                        ResourceCode = cl.First().ResourceCode,
                        Date = cl.First().Date,
                        Quantity = cl.Sum(c => c.Quantity),
                        EstimatedLabourHours = cl.Sum(c => c.EstimatedLabourHours),
                        RealLabourHours = cl.Sum(c => c.RealLabourHours)
                    }).ToList();

                //union de tareas planeadas y horas disponibles por grupo/recurso/fecha
                var totalDailyResources = ellipseTotalresource.Union(psoftResources).GroupBy(a => new { a.WorkGroup, a.Date, a.ResourceCode }).Select(cl => new LabourResources
                {
                    WorkGroup = cl.First().WorkGroup,
                    ResourceCode = cl.First().ResourceCode,
                    Date = cl.First().Date,
                    EstimatedLabourHours = cl.Sum(c => c.EstimatedLabourHours),
                    RealLabourHours = cl.Sum(c => c.RealLabourHours),
                    AvailableLabourHours = cl.Sum(c => c.AvailableLabourHours)
                }).ToList();

                //Recursos disponibles agrupados por grupo/recurso
                var totalWeeklyResources = (from r in totalDailyResources select r).GroupBy(l => new { l.WorkGroup, l.ResourceCode })
                    .Select(cl => new LabourResources
                    {
                        WorkGroup = cl.First().WorkGroup,
                        Date = DateTime.ParseExact(startDate, "yyyyMMdd", CultureInfo.InvariantCulture),
                        ResourceCode = cl.First().ResourceCode,
                        Quantity = cl.Average(c => c.Quantity),
                        AvailableLabourHours = cl.Sum(c => c.AvailableLabourHours),
                        EstimatedLabourHours = cl.Sum(c => c.EstimatedLabourHours),
                        RealLabourHours = cl.Sum(c => c.RealLabourHours)
                    }).ToList();

                //variable para el calculo del indicador de disponible/planeado
                var plannerIndicator = (from r in totalDailyResources select r).GroupBy(l => new { l.WorkGroup })
                    .Select(cl => new LabourResources
                    {
                        WorkGroup = cl.First().WorkGroup,
                        Date = DateTime.ParseExact(startDate, "yyyyMMdd", CultureInfo.InvariantCulture),
                        EstimatedLabourHours = cl.Sum(c => c.EstimatedLabourHours),
                        RealLabourHours = cl.Sum(c => c.RealLabourHours),
                        AvailableLabourHours = cl.Sum(c => c.AvailableLabourHours)
                    }).ToList();

                //LLenado de tablas
                var i = TitleRowResources + 1;
                foreach (var j in ellipseJobs)
                {
                    if (j.LabourResourcesList.Count > 0)
                    {
                        foreach (var r in j.LabourResourcesList)
                        {
                            _cells.GetCell(1, i).Value = j.WorkGroup;
                            _cells.GetCell(2, i).Value = j.EquipNo;
                            _cells.GetCell(3, i).Value = j.ItemName1;
                            _cells.GetCell(4, i).Value = j.MaintSchTask;
                            _cells.GetCell(5, i).Value = j.WorkOrder ?? j.StdJobNo;
                            _cells.GetCell(6, i).Value = j.WoTaskNo ?? j.StdJobTask;
                            _cells.GetCell(7, i).Value = j.WoDesc;
                            _cells.GetCell(8, i).Value = r.ResourceCode;
                            _cells.GetCell(9, i).Value = r.EstimatedLabourHours;
                            _cells.GetCell(10, i).Value = r.RealLabourHours;
                            _cells.GetCell(11, i).Value = r.EstimatedLabourHours - r.RealLabourHours;
                            _cells.GetCell(12, i).Value = j.PlanStrDate;
                            i++;
                        }
                    }
                    else
                    {
                        _cells.GetCell(1, i).Value = j.WorkGroup;
                        _cells.GetCell(2, i).Value = j.EquipNo;
                        _cells.GetCell(3, i).Value = j.ItemName1;
                        _cells.GetCell(4, i).Value = j.MaintSchTask;
                        _cells.GetCell(5, i).Value = j.WorkOrder ?? j.StdJobNo;
                        _cells.GetCell(6, i).Value = j.WoTaskNo ?? j.StdJobTask;
                        _cells.GetCell(7, i).Value = j.WoDesc;
                        _cells.GetCell(12, i).Value = j.PlanStrDate;
                    }
                }
                _excelApp.ActiveWorkbook.ActiveSheet.Cells.Columns.AutoFit();

                //hoja de ellipse
                _excelApp.ActiveWorkbook.Sheets.get_Item(2).Activate();
                i = TitleRowEllipse + 1;
                foreach (var r in totalDailyResources)
                {
                    _cells.GetCell(1, i).Value = r.WorkGroup;
                    _cells.GetCell(2, i).Value = r.ResourceCode;
                    _cells.GetCell(3, i).Value = r.Date;
                    _cells.GetCell(4, i).Value = r.EstimatedLabourHours - r.RealLabourHours;
                    _cells.GetCell(5, i).Value = r.AvailableLabourHours;
                    i++;
                }

                i = TitleRowEllipse + 1;
                foreach (var r in plannerIndicator)
                {
                    _cells.GetCell(7, i).Value = r.WorkGroup;
                    _cells.GetCell(8, i).Value = r.Date;
                    _cells.GetCell(9, i).Value = r.EstimatedLabourHours - r.RealLabourHours;
                    _cells.GetCell(10, i).Value = r.AvailableLabourHours;
                    _cells.GetCell(11, i).Value = (r.EstimatedLabourHours - r.RealLabourHours) / r.AvailableLabourHours;
                    i++;
                }

                i = TitleRowEllipse + 1;
                foreach (var r in totalWeeklyResources)
                {
                    _cells.GetCell(13, i).Value = r.WorkGroup;
                    _cells.GetCell(14, i).Value = r.Date;
                    _cells.GetCell(15, i).Value = r.ResourceCode;
                    _cells.GetCell(16, i).Value = r.Quantity;
                    _cells.GetCell(17, i).Value = r.EstimatedLabourHours - r.RealLabourHours;
                    _cells.GetCell(18, i).Value = r.AvailableLabourHours;
                    i++;
                }

                // Add chart.
                var charts = _excelApp.ActiveWorkbook.ActiveSheet.ChartObjects() as ChartObjects;
                if (charts != null)
                {
                    var chartObject = charts.Add(60, 10, 300, 300);
                    var chart = chartObject.Chart;

                    // Set chart range.
                    var range = _cells.GetRange(1, TitleRowEllipse, ResultColumnEllipse, TitleRowEllipse + totalDailyResources.Count);
                    chart.SetSourceData(range);

                    // Set chart properties.
                    chart.ChartType = XlChartType.xlColumnClustered;
                    chart.ChartWizard(range);
                }
                _excelApp.ActiveWorkbook.ActiveSheet.Cells.Columns.AutoFit();


                _excelApp.ActiveWorkbook.Sheets[1].Select(Type.Missing);

                if (_cells != null) _cells.SetCursorDefault();
            }
            catch (Exception ex)
            {
                Debugger.LogError("RibbonEllipse:ReviewJobList()", "\n\rMessage:" + ex.Message + "\n\rSource:" + ex.Source + "\n\rStackTrace:" + ex.StackTrace);
                MessageBox.Show(@"Se ha producido un error al intentar ejecutar la funcion. " + ex.Message);
                if (_cells != null) _cells.SetCursorDefault();
            }
        }

        private void btnLoadData_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (_excelApp.ActiveWorkbook.ActiveSheet.Name == ResourcesSheetName)
                {
                    //si ya hay un thread corriendo que no se ha detenido
                    if (_thread != null && _thread.IsAlive) return;
                    _frmAuth.StartPosition = FormStartPosition.CenterScreen;
                    _frmAuth.SelectedEnviroment = drpEnviroment.SelectedItem.Label;
                    if (_frmAuth.ShowDialog() != DialogResult.OK) return;
                    if (_thread != null && _thread.IsAlive) return;
                    _thread = new Thread(LoadJobPlan);

                    _thread.SetApartmentState(ApartmentState.STA);
                    _thread.Start();
                }
                else
                    MessageBox.Show(@"La hoja de Excel seleccionada no tiene el formato válido para realizar la acción");
            }
            catch (Exception ex)
            {
                Debugger.LogError("RibbonEllipse.cs:ReviewJobList()", "\n\rMessage: " + ex.Message + "\n\rSource: " + ex.Source + "\n\rStackTrace: " + ex.StackTrace);
                MessageBox.Show(@"Se ha producido un error: " + ex.Message);
            }
        }

        private void LoadJobPlan()
        {
            try
            {
                if (_cells == null)
                    _cells = new ExcelStyleCells(_excelApp);
                _cells.SetCursorWait();

                //hoja de ellipse
                _excelApp.ActiveWorkbook.Sheets.get_Item(2).Activate();

                const int i = TitleRowEllipse + 1;
                var resourcesToSave = new List<LabourResources>();
                while (_cells.GetNullIfTrimmedEmpty(_cells.GetCell(13, i).Value) != null)
                {
                    resourcesToSave.Add(new LabourResources
                    {
                        WorkGroup = _cells.GetCell(13, i).Value.ToString(),
                        Date = DateTime.FromOADate(Convert.ToDouble(_cells.GetCell(14, i).Value)),
                        ResourceCode = _cells.GetCell(13, i).Value.ToString(),
                        Quantity = Convert.ToDouble(_cells.GetCell(13, i).Value),
                        EstimatedLabourHours = Convert.ToDouble(_cells.GetCell(13, i).Value),
                        AvailableLabourHours = Convert.ToDouble(_cells.GetCell(13, i).Value)
                    });
                }

                JobActions.SaveResources(resourcesToSave);


            }
            catch (Exception ex)
            {
                Debugger.LogError("RibbonEllipse:ReviewJobList()", "\n\rMessage:" + ex.Message + "\n\rSource:" + ex.Source + "\n\rStackTrace:" + ex.StackTrace);
                MessageBox.Show(@"Se ha producido un error al intentar ejecutar la funcion. " + ex.Message);
                if (_cells != null) _cells.SetCursorDefault();
            }
        }
    }
}
