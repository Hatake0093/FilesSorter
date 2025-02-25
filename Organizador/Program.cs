using System;
using System.Collections.Generic;
using System.IO;

namespace Organizador
{
    internal class Program
    {
        static void Main()
        {
            string carpetaOrigen = @"D:\"; // Ahora busca archivos sueltos en D:
            string carpetaPendientes = @"D:\_PENDIENTES";

            var categorias = new Dictionary<string, string>
            {
                { ".pdf", "PDF" },
                { ".xlsx", "XLSX" },
                { ".csv", "XLSX" },
                { ".docx", "DOCX" },
                { ".png", "IMAGENES" },
                { ".svg", "IMAGENES" },
                { ".jpg", "IMAGENES" },
                { ".jpeg", "IMAGENES" },
                { ".mp4", "VIDEOS" },
                { ".mov", "VIDEOS" },
                { ".avi", "VIDEOS" },
                { ".zip", "COMPRIMIDOS" },
                { ".rar", "COMPRIMIDOS" },
                { ".exe", "EJECUTABLES" },
                { ".msi", "EJECUTABLES" }
            };

            try
            {
                MoverArchivosSueltos(carpetaOrigen, carpetaPendientes, categorias);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void MoverArchivosSueltos(string origen, string destinoBase, Dictionary<string, string> categorias)
        {
            if (!Directory.Exists(destinoBase))
                Directory.CreateDirectory(destinoBase);

            foreach (var archivo in Directory.GetFiles(origen))
            {

                try
                {
                    if ((File.GetAttributes(archivo) & FileAttributes.Hidden) == FileAttributes.Hidden)
                        continue; // Omitir archivos ocultos

                    var nombreArchivo = Path.GetFileName(archivo).ToLower();
                    var extension = Path.GetExtension(archivo).ToLower();
                    var destinoFinal = Path.Combine(destinoBase, "OTROS");

                    if (categorias.TryGetValue(extension, out var categoria))
                    {
                        destinoFinal = Path.Combine(destinoBase, categoria);
                    }

                    if (!Directory.Exists(destinoFinal))
                        Directory.CreateDirectory(destinoFinal);

                    var nuevoPath = Path.Combine(destinoFinal, Path.GetFileName(archivo));
                    File.Move(archivo, nuevoPath);

                    Console.WriteLine($"Movido: {nombreArchivo} -> {destinoFinal}");
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }
    }
}