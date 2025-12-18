

using ClosedXML.Excel;
using ProjectsMecsaSPA.Model;
using static ProjectsMecsaSPA.Pages.Bill.BillsListPage;

namespace ProjectsMecsaSPA.Utilities
{
    /// <summary>
    /// Proporciona métodos utilitarios para la generación de archivos Excel
    /// relacionados con ofertas y proyectos utilizando ClosedXML.
    /// </summary>
    public static class XmlsGenerate
    {
        #region Offers
        /// <summary>
        /// Genera un archivo Excel con el listado de ofertas.
        /// </summary>
        /// <param name="offers">
        /// Colección de ofertas a exportar. Cada oferta se representa como una fila
        /// con información general, requisitos técnicos y montos.
        /// </param>
        /// <returns>
        /// Un arreglo de bytes que representa el archivo Excel generado.
        /// </returns>
        public static async Task<byte[]> GenerateFileOffers(IEnumerable<Offer> offers)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Offers");

            // Encabezados de las columnas
            worksheet.Cell(1, 1).Value = "OfferId";
            worksheet.Cell(1, 2).Value = "Fecha de Registro";
            worksheet.Cell(1, 3).Value = "Cliente";
            worksheet.Cell(1, 4).Value = "Descripción";
            worksheet.Cell(1, 5).Value = "Tipo de Contacto";
            worksheet.Cell(1, 6).Value = "Tipo";
            worksheet.Cell(1, 7).Value = "Estado";
            worksheet.Cell(1, 8).Value = "DDCE Requerido";
            worksheet.Cell(1, 9).Value = "Protección contra Rayos Requerida";
            worksheet.Cell(1, 10).Value = "Torre Requerida";
            worksheet.Cell(1, 11).Value = "SPAT Requerido";
            worksheet.Cell(1, 12).Value = "Protector de Sobretensión Requerido";
            worksheet.Cell(1, 13).Value = "Otro Requerido";
            worksheet.Cell(1, 14).Value = "Monto";
            worksheet.Cell(1, 15).Value = "Cotizado Por";
            worksheet.Cell(1, 16).Value = "Autor";
            worksheet.Cell(1, 17).Value = "AuthorId";
            worksheet.Cell(1, 18).Value = "Responsable";
            worksheet.Cell(1, 19).Value = "ResponsibleId";

            // Llenar los datos
            int row = 2;
            foreach (var offer in offers)
            {
                worksheet.Cell(row, 1).Value = offer.OfferId;
                worksheet.Cell(row, 2).Value = offer.Creation;
                worksheet.Cell(row, 3).Value = offer.Customer;
                worksheet.Cell(row, 4).Value = offer.Description;
                worksheet.Cell(row, 5).Value = offer.ContactType;
                worksheet.Cell(row, 6).Value = offer.Type;
                worksheet.Cell(row, 7).Value = offer.State;
                worksheet.Cell(row, 8).Value = offer.RequiredDDCE ? "Posee DDCE" : "";
                worksheet.Cell(row, 9).Value = offer.RequiredLightningStrike ? "Posee Protección contra Rayos" : "";
                worksheet.Cell(row, 10).Value = offer.RequireTower ? "Posee Torre" : "";
                worksheet.Cell(row, 11).Value = offer.RequireSPAT ? "Posee SPAT" : "";
                worksheet.Cell(row, 12).Value = offer.RequireSurgeProtector ? "Posee Protector de Sobretensión" : "";
                worksheet.Cell(row, 13).Value = offer.RequireOther ? "Posee Otros" : "";
                worksheet.Cell(row, 14).Value = offer.Amount;
                worksheet.Cell(row, 15).Value = offer.CalculateBy;
                worksheet.Cell(row, 16).Value = offer.Author;
                worksheet.Cell(row, 17).Value = offer.AuthorId;
                worksheet.Cell(row, 18).Value = offer.Responsible;
                worksheet.Cell(row, 19).Value = offer.ResponsibleId;

                row++;
            }

            // Definir el rango de la tabla
            var range = worksheet.Range(1, 1, row - 1, 19);

            // Crear la tabla
            var table = range.CreateTable();

            // Asignar un nombre a la tabla
            table.Name = "OffersTable";

            // Aplicar un estilo a la tabla
            table.Theme = XLTableTheme.TableStyleMedium9;

            // Ajustar el ancho de las columnas
            worksheet.Columns().AdjustToContents();

            // Guardar el archivo en un array de bytes
            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return memoryStream.ToArray();
        }

        #endregion

        #region Projects

        /// <summary>
        /// Genera un archivo Excel con un resumen de proyectos y su información de facturación.
        /// </summary>
        /// <param name="projects">
        /// Colección de proyectos con información resumida de montos, facturas y estado.
        /// </param>
        /// <returns>
        /// Un arreglo de bytes que representa el archivo Excel generado.
        /// </returns>
        public static async Task<byte[]> GenerateFileProjects(IEnumerable<ProjectBillSummary> projects)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Projects");

            // Encabezados de las columnas
            worksheet.Cell(1, 1).Value = "ProjectId";
            worksheet.Cell(1, 2).Value = "ProjectName";
            worksheet.Cell(1, 3).Value = "CreationDate";
            worksheet.Cell(1, 4).Value = "ProjectAmount";
            worksheet.Cell(1, 5).Value = "BillsTotalAmount";
            worksheet.Cell(1, 6).Value = "TaskNumber";
            worksheet.Cell(1, 7).Value = "Bill Mark";
            worksheet.Cell(1, 8).Value = "Status";

            // Llenar los datos
            int row = 2;
            foreach (var project in projects)
            {
                worksheet.Cell(row, 1).Value = project.ProjectId;
                worksheet.Cell(row, 2).Value = project.ProjectName;
                worksheet.Cell(row, 3).Value = project.CreationDate;
                worksheet.Cell(row, 4).Value = project.ProjectAmount;
                worksheet.Cell(row, 5).Value = project.BillsTotalAmount;
                worksheet.Cell(row, 6).Value = project.TaskNumber;
                worksheet.Cell(row, 7).Value = project.billMark;
                worksheet.Cell(row, 8).Value = project.status;

                row++;
            }

            // Ajustar el ancho de las columnas
            worksheet.Columns().AdjustToContents();

            // Guardar el archivo en un array de bytes
            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Genera un archivo Excel con el listado completo de proyectos.
        /// </summary>
        /// <param name="projects">
        /// Lista de proyectos a exportar.
        /// </param>
        /// <returns>
        /// Un arreglo de bytes que representa el archivo Excel generado.
        /// </returns>
        public static async Task<byte[]> GenerateFileProjects(List<Project> projects)
        {
            return await GenerateFileProjects(projects.AsEnumerable());
        }

        /// <summary>
        /// Genera un archivo Excel con información detallada de proyectos.
        /// </summary>
        /// <param name="projects">
        /// Colección de proyectos que incluye datos generales, cliente, estado,
        /// montos, moneda y condiciones del proyecto.
        /// </param>
        /// <returns>
        /// Un arreglo de bytes que representa el archivo Excel generado.
        /// </returns>
        public static async Task<byte[]> GenerateFileProjects(IEnumerable<Project> projects)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Projects");

            // Encabezados de las columnas
            worksheet.Cell(1, 1).Value = "ProjectId";
            worksheet.Cell(1, 2).Value = "Titulo";
            worksheet.Cell(1, 3).Value = "Descripcion";
            worksheet.Cell(1, 4).Value = "Tarea";
            worksheet.Cell(1, 5).Value = "Registro";
            worksheet.Cell(1, 6).Value = "Tipo";
            worksheet.Cell(1, 7).Value = "Cliente";
            worksheet.Cell(1, 8).Value = "Ubicación";
            worksheet.Cell(1, 9).Value = "Monto";
            worksheet.Cell(1, 10).Value = "Orden Compra";
            worksheet.Cell(1, 11).Value = "Fecha OC";
            worksheet.Cell(1, 12).Value = "Marcado como Borrado";
            worksheet.Cell(1, 13).Value = "Marcado como Completo";
            worksheet.Cell(1, 14).Value = "Marcado como Facturado";
            worksheet.Cell(1, 15).Value = "Vendedor";
            worksheet.Cell(1, 16).Value = "Estado";
            worksheet.Cell(1, 17).Value = "Moneda";
            worksheet.Cell(1, 18).Value = "Tipo Cambio";
            worksheet.Cell(1, 19).Value = "Provincia";

            // Llenar los datos
            int row = 2;
            foreach (var project in projects)
            {
                worksheet.Cell(row, 1).Value = project.ProjectId;
                worksheet.Cell(row, 2).Value = project.Title;
                worksheet.Cell(row, 3).Value = project.Description;
                worksheet.Cell(row, 4).Value = project.TaskNumber;
                worksheet.Cell(row, 5).Value = project.CreationDate;
                worksheet.Cell(row, 6).Value = project.Type?.Name;
                worksheet.Cell(row, 7).Value = project.Customer?.Name;
                worksheet.Cell(row, 8).Value = project.Ubication;
                worksheet.Cell(row, 9).Value = project.Amount;
                worksheet.Cell(row, 10).Value = project.OC;
                worksheet.Cell(row, 11).Value = project.OCDate.ToLongDateString();
                worksheet.Cell(row, 12).Value = project.IsDeleted;
                worksheet.Cell(row, 13).Value = project.IsCompleted;
                worksheet.Cell(row, 14).Value = project.Isfactured;
                worksheet.Cell(row, 15).Value = project.Seller?.SellerName;
                worksheet.Cell(row, 16).Value = project.State?.StateName;
                worksheet.Cell(row, 17).Value = project.CurrencyType;
                worksheet.Cell(row, 18).Value = project.TypeOfChange;
                worksheet.Cell(row, 19).Value = project.Province;

                row++;
            }

            // Ajustar el ancho de las columnas
            worksheet.Columns().AdjustToContents();

            // Guardar el archivo en un array de bytes
            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return memoryStream.ToArray();
        }

        #endregion


        #region Weeks

        public static async Task<byte[]> GenerateWeekResumeAsync(IEnumerable<Employee> employees, IEnumerable<Schedule> schedules)
        {
            // Define the current week (starting from Monday)
            var currentMonday = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday);
            var weekDays = Enumerable.Range(0, 7).Select(i => currentMonday.AddDays(i)).ToList();

            // Create the Excel file
            using var workbook = new XLWorkbook();
            workbook.Author = "SPA - Projects";

            // Create the worksheet
            var worksheet = workbook.Worksheets.Add("Semana");

            // Title: Week of [currentMonday] to [currentMonday + 6]
            worksheet.Cell(1, 1).Value = $"Semana del {currentMonday:dd MMM yyyy} al {currentMonday.AddDays(6):dd MMM yyyy}";
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 14;

            // Add table headers (days of the week)
            worksheet.Cell(3, 1).Value = "Empleado";  // Employee header
            for (int i = 0; i < 7; i++)
            {
                var day = weekDays[i];
                worksheet.Cell(3, i + 2).Value = $"{day:dddd}\n{day:dd/MM}";
                worksheet.Cell(3, i + 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                worksheet.Cell(3, i + 2).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                worksheet.Cell(3, i + 2).Style.Font.Bold = true;
            }

            // Fill the table rows with employees' schedules
            int row = 4;
            foreach (var employee in employees)
            {
                // Get the schedules for the employee during the week
                var employeeSchedules = schedules
                    .Where(s => s.SchEmpls.Any(se => se.EmployeeId == employee.EmployeeId))
                    .ToList();

                worksheet.Cell(row, 1).Value = $"{employee.FirstName} {employee.LastName}";
                if (employee.Position == "Tecnico")
                {
                    worksheet.Cell(row, 1).Style.Font.Italic = true; // Mark technician
                }

                // Fill in each day for the employee
                for (int i = 0; i < 7; i++)
                {
                    var day = weekDays[i];
                    var daySchedules = employeeSchedules
                        .Where(s => s.Start.Date <= day.Date && s.End.Date >= day.Date)
                        .ToList();

                    var cell = worksheet.Cell(row, i + 2);
                    if (!daySchedules.Any())
                    {
                        cell.Value = "-";  // No schedule
                        cell.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                        cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    }
                    else
                    {
                        // Loop through the schedules for this day and display the details
                        foreach (var schedule in daySchedules)
                        {

                            var projectTitle = schedule.Project?.Title ?? "No Project";
                            var projectId = schedule.Project?.ProjectId ;
                            var province = schedule.Project?.Province ?? "N/A";
                            var projectType = schedule.Project?.Type?.Name ?? "N/A";
                            var car = schedule.Car ?? "N/A";

                            cell.Value = $"Proyecto: {projectId} - {projectTitle}";
                           
                            cell.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
                            cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                            // Add project details in the next lines (simulated)
                            cell.Style.Alignment.SetWrapText(true);
                            cell.Value += $"\nProvincia: {province}\nTipo: {projectType}\nAuto: {car}";
                        }
                    }

                    // Adjust column width to fit content
                    worksheet.Columns().AdjustToContents();
                }
                row++;
            }

            // Save the file in a memory stream
            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);

            return memoryStream.ToArray();
        }

        #endregion

    }
}
