using ic.backend.precotex.web.Service.Services.Implementacion.HelpCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI = SixLabors.ImageSharp;
using SI2 = SixLabors.ImageSharp.PixelFormats;
using SI3 = SixLabors.ImageSharp.Processing;
using SI4 = SixLabors.Fonts;
using SI5 = SixLabors.ImageSharp.Drawing;

using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing.Processors.Text;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using Org.BouncyCastle.Crypto.Prng;
using ic.backend.precotex.web.Service.common;

namespace ic.backend.precotex.web.Service.Services.HelpCommon
{
    public class GenerateImageDinamycService : IGenerateImageDinamycService
    {
        public async Task<ServiceResponse<byte[]>> GenerarImagen(string titulo, string colorHex, string iconoPath,
                                    string area, string persona, string fecha, string hora, string tipo)
        {
            int width = 600;
            int height = 480;

            using var image = new SI.Image<SI2.Rgba32>(width, height);

            // Fondo dinámico
            var fondo = SI.Color.ParseHex(colorHex);
            image.Mutate(x => x.Fill(fondo));

            //Agegamos el icono 
            string vPathIcon = string.Empty;
            if (tipo == "1")
            {
                vPathIcon = "https://gestion.precotex.com:444/ubicaciones/api/TxRetiroRepuestos/getImagenDesdeBackEnd?imageId=icon_alert_red.png";
            }
            else if (tipo == "2")
            {
                vPathIcon = "https://gestion.precotex.com:444/ubicaciones/api/TxRetiroRepuestos/getImagenDesdeBackEnd?imageId=icon_alert_yellow.png";
            }

            using var httpClient = new HttpClient(); 
            using var response = await httpClient.GetAsync(vPathIcon);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync(); 
            using var icono = Image.Load(stream);

            //Redimensiona Icono
            icono.Mutate(x => x.Resize(75, 75));

            //Dibuja icono
            image.Mutate(ctx => ctx.DrawImage(icono, new SI.Point(250, 10), 1f));

            // Fuentes
            var fontTitle = SI4.SystemFonts.CreateFont("Arial", 36, SI4.FontStyle.Bold);
            var fontLabel = SI4.SystemFonts.CreateFont("Arial", 20, SI4.FontStyle.Bold); // títulos
            var fontValue = SI4.SystemFonts.CreateFont("Arial", 24);                     // valores

            // Título principal
            image.Mutate(ctx => ctx.DrawText(titulo, fontTitle, SI.Color.White, new SI.PointF(100, 100)));

            // Cuadro blanco interno
            var rect = new SI.Rectangle(20, 150, width - 40, height - 185);
            float cornerRadius = 25f;

            //var roundedRect = new RectangularPolygon(rect.X, rect.Y, rect.Width, rect.Height);

            // Crear figura compuesta con esquinas redondeadas
            var path = new PathCollection( 
                new EllipsePolygon(rect.Left + cornerRadius, rect.Top + cornerRadius, cornerRadius), // sup izq
                new EllipsePolygon(rect.Right - cornerRadius, rect.Top + cornerRadius, cornerRadius), // sup der
                new EllipsePolygon(rect.Right - cornerRadius, rect.Bottom - cornerRadius, cornerRadius), // inf der
                new EllipsePolygon(rect.Left + cornerRadius, rect.Bottom - cornerRadius, cornerRadius), // inf izq
                new RectangularPolygon(rect.Left + cornerRadius, rect.Top, rect.Width - 2 * cornerRadius, rect.Height), // centro horizontal
                new RectangularPolygon(rect.Left, rect.Top + cornerRadius, rect.Width, rect.Height - 2 * cornerRadius) // centro vertical
            );
           
            image.Mutate(ctx =>
            {
                ctx.Fill(SI.Color.FromRgb(255, 255, 252), path);
                //ctx.Draw(SI.Color.Black, 2, rect);
            });

            // Campos dinámicos: título arriba, valor abajo
            float startX = 50;
            float startY = 160;
            float spacingY = 70; // espacio vertical entre bloques

            /*DESDE NUEVO CAMBIO*/

            var bloques = new[] { 
                new { Titulo = "Área", Valor = area },
                new { Titulo = "Persona", Valor = persona }, 
                new { Titulo = "Fecha", Valor = fecha },
                new { Titulo = "Hora", Valor = hora } 
            };

            var fontLabelContent = SI4.SystemFonts.CreateFont("Arial", 15, SI4.FontStyle.Bold); // títulos

            image.Mutate(ctx =>
            {
                for (int i = 0; i < bloques.Length; i++)
                {
                    float cornerRadiusZ = 15f;
                    float y = startY + i * spacingY;

                    // Definir rectángulo para el bloque  , utlizamos el ancho general - 
                    var rect = new RectangleF(startX, y, width - 100, 65);

                    // Sombrita rojiza suave a la izquierda
                    //var sombra = new RectangleF(rect.X - 6, rect.Y, 8, rect.Height);
                    // Path para la sombra (desplazada a la izquierda)
                    var sombraPath = new PathCollection( 
                        new EllipsePolygon(rect.Left - 6 + cornerRadiusZ, rect.Top + cornerRadiusZ, cornerRadiusZ), 
                        new EllipsePolygon(rect.Right - 6 - cornerRadiusZ, rect.Top + cornerRadiusZ, cornerRadiusZ), 
                        new EllipsePolygon(rect.Right - 6 - cornerRadiusZ, rect.Bottom - cornerRadiusZ, cornerRadiusZ), 
                        new EllipsePolygon(rect.Left - 6 + cornerRadiusZ, rect.Bottom - cornerRadiusZ, cornerRadiusZ), 
                        new RectangularPolygon(rect.Left - 6 + cornerRadiusZ, rect.Top, rect.Width - 2 * cornerRadiusZ, rect.Height), 
                        new RectangularPolygon(rect.Left - 6, rect.Top + cornerRadiusZ, rect.Width, rect.Height - 2 * cornerRadiusZ) 
                        );
                    ctx.Fill(SI.Color.FromRgb(194, 36, 35), sombraPath); // rojo translúcido

                    
                    var path = new PathCollection(
                        new EllipsePolygon(rect.Left + cornerRadiusZ, rect.Top + cornerRadiusZ, cornerRadiusZ), // sup izq
                        new EllipsePolygon(rect.Right - cornerRadiusZ, rect.Top + cornerRadiusZ, cornerRadiusZ), // sup der
                        new EllipsePolygon(rect.Right - cornerRadiusZ, rect.Bottom - cornerRadiusZ, cornerRadiusZ), // inf der
                        new EllipsePolygon(rect.Left + cornerRadiusZ, rect.Bottom - cornerRadiusZ, cornerRadiusZ), // inf izq
                        new RectangularPolygon(rect.Left + cornerRadiusZ, rect.Top, rect.Width - 2 * cornerRadiusZ, rect.Height), // centro horizontal
                        new RectangularPolygon(rect.Left, rect.Top + cornerRadiusZ, rect.Width, rect.Height - 2 * cornerRadiusZ) // centro vertical
                    );

                    // Dibujar cuadro blanco encima
                    ctx.Fill(SI.Color.FromRgb(245,245,245), path);
                    //ctx.Draw(SI.Color.Black, 1, rect);

                    // Texto dentro del cuadro
                    ctx.DrawText(bloques[i].Titulo, fontLabelContent, SI.Color.Red, new PointF(rect.X + 10, rect.Y + 15)); 
                    ctx.DrawText(bloques[i].Valor, fontValue, SI.Color.Black, new PointF(rect.X + 10, rect.Y + 30));

                }
            });


            /*HASTA AQUI*/

            // Exportar a memoria
            using var ms = new MemoryStream();
            await image.SaveAsPngAsync(ms);

            // Retornar envuelto en ServiceResponse<byte[]>
            return new ServiceResponse<byte[]> { Element = ms.ToArray(), Success = true, Message = "Imagen correcto" };

        }


    }
}
