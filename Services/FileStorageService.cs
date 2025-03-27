using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ProjectsMecsaSPA.Services
{
        public class FileStorageService
        {
            private readonly string _baseRoute = Path.Combine(Directory.GetCurrentDirectory(), "projectsdata");

            // Genera el directorio del proyecto si no existe
            public async Task GenerateProjectDirectoryAsync(int projectId)
            {
                var projectDirectory = Path.Combine(_baseRoute, projectId.ToString());
                if (!Directory.Exists(projectDirectory))
                {
                    Directory.CreateDirectory(projectDirectory);
                }
                await Task.CompletedTask;
            }

            // Genera el directorio de una factura dentro del proyecto
            public async Task GenerateBillDirectoryAsync(int projectId, string BillId)
            {
            try
            {
                Directory.CreateDirectory(Path.Combine(_baseRoute, projectId.ToString(), "bills"));
                var billDirectory = Path.Combine(_baseRoute, projectId.ToString(), "bills", BillId);
                if (!Directory.Exists(billDirectory))
                {
                    Directory.CreateDirectory(billDirectory);
                }
                await Task.CompletedTask;
            }
            catch (Exception e)
            {

                throw;
            }
            }

            // Subir archivos de la factura y devolver la ruta relativa
            public async Task<string> UploadBillsFilesAsync(int projectId, string BillId, IBrowserFile file)
            {
    
                // Generar directorios si no existen
                await GenerateProjectDirectoryAsync(projectId);
                await GenerateBillDirectoryAsync(projectId, BillId);

                // Directorio de la factura
                var billDirectory = Path.Combine(_baseRoute, projectId.ToString(), "bills", BillId);

              
                    var fileName = Path.GetFileName(file.Name);
                    var filePath = Path.Combine(billDirectory, fileName);

                    // Guardar el archivo en el directorio correspondiente
                    using (var stream = file.OpenReadStream())
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await stream.CopyToAsync(fileStream);
                        }
                    }

                    // Agregar la ruta relativa del archivo
                    var relativePath = Path.Combine("projectsdata", projectId.ToString(),"bills", BillId, fileName);

            return relativePath;
            }

            // Obtener los archivos de una factura
            public async Task<List<string>> GetBillsFilesAsync(int projectId, string BillId)
            {
                var billDirectory = Path.Combine(_baseRoute, projectId.ToString(), "bills", BillId);
                if (!Directory.Exists(billDirectory))
                {
                    return new List<string>();
                }

                var files = Directory.GetFiles(billDirectory);
                return files.Select(file => Path.Combine("projectsdata", projectId.ToString(), "bills", BillId, Path.GetFileName(file))).ToList();
            }
        }
    }
