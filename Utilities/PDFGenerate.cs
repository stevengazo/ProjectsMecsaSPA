using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using ProjectsMecsaSPA.Model;

public static class PDFGenerate
{
    public static async Task<byte[]> PDFProjectReport(Project project)
    {
        using (var memoryStream = new MemoryStream())
        {
            var document = new PdfDocument();
            var font = new XFont("Verdana", 12);

            // Primera página - Información del proyecto
            var page = document.AddPage();
            var graphics = XGraphics.FromPdfPage(page);
            graphics.DrawString($"Reporte del Proyecto: {project.Title}", new XFont("Verdana", 20), XBrushes.Black, new XRect(0, 40, page.Width, 0), XStringFormats.TopCenter);

            // Tabla para la información del proyecto
            var projectTable = new XTextFormatter(graphics);
            var yPos = 100;
            projectTable.DrawString($"Descripción: {project.Description}", font, XBrushes.Black, new XRect(40, yPos, page.Width - 80, 0));
            projectTable.DrawString($"Id: {project.ProjectId}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));
            projectTable.DrawString($"Número de Tarea: {project.TaskNumber}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));
            projectTable.DrawString($"Fecha de Creación: {project.CreationDate.ToString("dd/MM/yyyy")}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));
            projectTable.DrawString($"Orden de Compra: {project.OC}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));
            projectTable.DrawString($"Fecha orden de compra: {project.CreationDate.ToLongDateString()}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));
            projectTable.DrawString($"Estado: {project.State?.StateName}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));
            projectTable.DrawString($"Monto: {project.Amount.ToString("C2")}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));
            projectTable.DrawString($"Ubicación: {project.Ubication}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));
            projectTable.DrawString($"Provincia: {project.Province}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));
            projectTable.DrawString($"Tipo: {project.Type?.Name}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));
            projectTable.DrawString($"Estado: {project.State?.StateName}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));
            projectTable.DrawString($"Cliente: {project.Customer?.Name}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));
            projectTable.DrawString($"Cédula Cliente: {project.Customer?.DNI}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));
            projectTable.DrawString($"Vendedor: {project.Seller?.SellerName}", font, XBrushes.Black, new XRect(40, yPos += 20, page.Width - 80, 0));

            // Segunda página - Facturas
            if (project.Bills != null && project.Bills.Any())
            {
                var billsPage = document.AddPage();
                var billsGraphics = XGraphics.FromPdfPage(billsPage);

                billsGraphics.DrawString("Facturas", new XFont("Verdana", 20), XBrushes.Black, new XRect(0, 40, billsPage.Width, 0), XStringFormats.TopCenter);

                // Tabla para las facturas
                yPos = 100;
                foreach (var bill in project.Bills)
                {
                    billsGraphics.DrawString($"Factura N°: {bill.BillNumber}", font, XBrushes.Black, new XRect(40, yPos, billsPage.Width - 80, 0), XStringFormats.TopLeft);
                    billsGraphics.DrawString($"Importe: {bill.Amount.ToString("C2")} {bill.Currency}", font, XBrushes.Black, new XRect(40, yPos += 20, billsPage.Width - 80, 0), XStringFormats.TopLeft);
                    billsGraphics.DrawString($"Fecha de Factura: {bill.BillDate.ToString("dd/MM/yyyy")}", font, XBrushes.Black, new XRect(40, yPos += 20, billsPage.Width - 80, 0), XStringFormats.TopLeft);
                    billsGraphics.DrawString($"Autor: {bill.Author}", font, XBrushes.Black, new XRect(40, yPos += 20, billsPage.Width - 80, 0), XStringFormats.TopLeft);
                    billsGraphics.DrawString($"Información: {bill.Note}", font, XBrushes.Black, new XRect(40, yPos += 20, billsPage.Width - 80, 0), XStringFormats.TopLeft);

                    yPos += 40; // Espacio entre facturas
                }
            }

            // Guardar el documento en el MemoryStream
            document.Save(memoryStream);

            // Retornar el PDF en formato de bytes
            return memoryStream.ToArray();
        }
    }
}
