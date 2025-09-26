using ic.backend.precotex.web.Entity.Entities;
using ic.backend.precotex.web.Entity.Entities.Memorandum;
using ic.backend.precotex.web.Service.common;
using ic.backend.precotex.web.Service.Services.Implementacion.DDT;
using ic.backend.precotex.web.Service.Services.Implementacion.HelpCommon;
using iTextSharp.text;
using iTextSharp.text.pdf;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZXing.OneD;

namespace ic.backend.precotex.web.Service.Services.HelpCommon
{

    public class HelpCommonService : IHelpCommonService
    {
        private readonly ITxUbicacionColgadorService _txUbicacionColgadorService;
        // Constructor que toma el nombre de la impresora
        public HelpCommonService(ITxUbicacionColgadorService ITxUbicacionColgadorService)
        {
            _txUbicacionColgadorService = ITxUbicacionColgadorService;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class DOCINFO
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDataType;
        }

        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool OpenPrinter(string pPrinterName, ref IntPtr phPrinter, IntPtr pDefault);

        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, int Level, [In] DOCINFO pDocInfo);

        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        public static bool SendBytesToPrinter(string printerName, byte[] bytes)
        {
            IntPtr hPrinter = IntPtr.Zero;
            DOCINFO di = new DOCINFO
            {
                pDocName = "Raw Document",
                pDataType = "RAW"
            };

            bool success = false;

            if (OpenPrinter(printerName, ref hPrinter, IntPtr.Zero))
            {
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    if (StartPagePrinter(hPrinter))
                    {
                        IntPtr pUnmanagedBytes = Marshal.AllocCoTaskMem(bytes.Length);
                        Marshal.Copy(bytes, 0, pUnmanagedBytes, bytes.Length);

                        success = WritePrinter(hPrinter, pUnmanagedBytes, bytes.Length, out int bytesWritten);
                        Marshal.FreeCoTaskMem(pUnmanagedBytes);

                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }

            return success;
        }

        /// <summary>
        /// Descripcion: Imprime Tickets, con codigo de Barra  
        /// </summary>
        /// <param name="content"></param>
        /// <param name="PrintName"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<int>> PrintTicket(string content, string PrintName)
        {
            var result = new ServiceResponse<int>();
            try
            {
                byte[] command = await GenerateEscPosCommands(content);
                SendCommandToPrinter(command, PrintName);

                result.Message = "Impresión Realizado Correctamente.";
                result.Success = true;
                result.CodeTransacc = 1;

                return result;

            }
            catch (Exception ex)
            {
                result.Message = "Error al imprimir el ticket.";
                result.Success = true;
                result.CodeTransacc = 0;

                return result;
            }
        }

        #region FUNCIONES INTERNAS


        // Generar los comandos ESC/POS para configurar el tamaño y la impresión
        public async Task<byte[]> GenerateEscPosCommands(string content)
        {
            // Simulación de un retraso asíncrono (esto puede ser útil si tu función hace operaciones I/O en el futuro)
            await Task.Delay(1);  // Esto es solo un ejemplo. Remuévelo si no se requiere

            var escPosCommands = new System.Collections.Generic.List<byte>();

            // Configuración inicial de la impresora: Establecer tamaño de etiqueta (13 cm x 8 cm)
            escPosCommands.Add(27);  // ESC
            escPosCommands.Add(64);  // Inicialización de la impresora (establece formato predeterminado)

            // Configuración de tamaño de papel: Se ajusta a un tamaño personalizado de 13 cm de largo y 8 cm de alto.
            escPosCommands.Add(27);  // ESC
            escPosCommands.Add(84);  // Definir tamaño de etiqueta
            escPosCommands.Add(13);  // 13 cm de largo (tamaño de la etiqueta)
            escPosCommands.Add(8);   // 8 cm de alto (tamaño de la etiqueta)

            // Comandos para ajustar el tamaño de fuente, estilo, etc.
            escPosCommands.Add(27);  // ESC
            escPosCommands.Add(33);  // Tamaño de fuente (puede incluir negrita, subrayado, etc.)
            escPosCommands.Add(0);   // No aplicar formato especial (fuente normal)

            // Agregar el contenido del ticket
            byte[] contentBytes = Encoding.ASCII.GetBytes(content);
            escPosCommands.AddRange(contentBytes);

            // Finalizar la impresión (corte de papel)
            escPosCommands.Add(29);  // GS
            escPosCommands.Add(86);  // Cortar papel
            escPosCommands.Add(0);   // Activar corte de papel

            return escPosCommands.ToArray();
        }

        // Enviar los comandos a la impresora
        private void SendCommandToPrinter(byte[] command, string PrintName)
        {
            IntPtr printerHandle = IntPtr.Zero;
            IntPtr pd = IntPtr.Zero;  // Esta es una estructura que en algunos casos se usa para la configuración del dispositivo


            // Abrir la impresora (usando su nombre de red o IP)
            bool result = OpenPrinter(PrintName, ref printerHandle, pd);
            if (result)
            {
                bool success = SendBytesToPrinter(PrintName, command);
                Console.WriteLine("Impresora abierta correctamente.");
                // Aquí puedes hacer otras operaciones con la impresora...
            }
            else
            {
                Console.WriteLine("No se pudo acceder a la Ticketera.");
            }

            // Enviar comandos RAW a la impresora
            //WritePrinter(printerHandle, command, command.Length, out int bytesWritten);

            // Cerrar la conexión con la impresora
            //ClosePrinter(printerHandle);
        }




        /// <summary>
        /// Descripcion: Imprimir Sticker Version 2.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="PrintName"></param>
        /// <param name="tx_TelaEstructuraColgador"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<int>> PrintQRCode_v2(string content, string PrintName, Tx_TelaEstructuraColgador tx_TelaEstructuraColgador)
        {
            await Task.Delay(1);
            var result = new ServiceResponse<int>();

            var titulos = new List<string>
            {
                "Factory", "Fabric", "Composition", "Width B/W", "Weigth B/W",
                "Width A/W", "Weigth A/W", "Code", "Partida",
            };

            var descripciones = new List<string>
            {
                $"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.Nom_Cliente) ? "" : tx_TelaEstructuraColgador.Nom_Cliente)}",
                $"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.Fabric) ? "" : tx_TelaEstructuraColgador.Fabric)}",
                $"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.DesComposicion) ? "" : tx_TelaEstructuraColgador.DesComposicion)}",
                $"{tx_TelaEstructuraColgador.Width_BW?.ToString("0.00") ?? "-"} Mts",
                $"{tx_TelaEstructuraColgador.Weight_BW?.ToString("0.00") ?? "-"} Gr/m2",
                $"{tx_TelaEstructuraColgador.Width_AW?.ToString("0.00") ?? "-"} Mts",
                $"{tx_TelaEstructuraColgador.Weight_AW?.ToString("0.00") ?? "-"} Gr/m2",
                $"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.Cod_Tela) ? "" : tx_TelaEstructuraColgador.Cod_Tela)}",
                $"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.Cod_OrdTra) ? "" : tx_TelaEstructuraColgador.Cod_OrdTra)}",
            };

            try
            {
                var binstalled = PrinterSettings.InstalledPrinters.Cast<string>()
                    .Any(p => p.Equals(PrintName, StringComparison.OrdinalIgnoreCase));

                if (binstalled)
                {
                    // Crear QR
                    var qrGenerator = new QRCodeGenerator();
                    var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
                    var base64QRCode = new Base64QRCode(qrCodeData);
                    string base64String = base64QRCode.GetGraphic(20);
                    var qrCodeBitmap = Base64ToBitmap(base64String);

                    var pd = new PrintDocument
                    {
                        PrinterSettings = new PrinterSettings { PrinterName = PrintName }
                    };

                    // 11.5cm x 8cm a 100 DPI → 453 x 315 px
                    pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 453, 315);
                    pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                    pd.PrintPage += (sender, e) =>
                    {
                        var g = e.Graphics;
                        g.Clear(Color.White);

                        int pageWidth = e.PageBounds.Width;   // 453 px
                        int pageHeight = e.PageBounds.Height; // 315 px
                        int padding = 10;

                        using var fontTitle = new System.Drawing.Font("Arial", 10, FontStyle.Bold); // Títulos
                        using var fontNormal = new System.Drawing.Font("Arial", 10, FontStyle.Regular); // ":" y descripciones
                        int lineHeight = (int)Math.Ceiling(g.MeasureString("Test", fontTitle).Height); // ≈ 15 px

                        // Imprimir las 10 filas de texto
                        #region Cuerpo Etiqueta

                        // Column widths
                        float totalWidth = pageWidth - 2 * padding;
                        float col1X = padding;
                        float col2X = col1X + totalWidth * 0.20f; //+20 % 
                        float col3X = col2X + totalWidth * 0.05f; //+5  %

                        // FOR para títulos y los dos puntos
                        for (int i = 0; i < titulos.Count; i++)
                        {
                            float y = padding + i * lineHeight;
                            g.DrawString(titulos[i], fontTitle, Brushes.Black, new PointF(col1X, y));
                            g.DrawString(":", fontNormal, Brushes.Black, new PointF(col2X, y));
                        }

                        // FOR para descripciones
                        for (int i = 0; i < descripciones.Count; i++)
                        {
                            float y = padding + i * lineHeight;
                            g.DrawString(descripciones[i], fontNormal, Brushes.Black, new PointF(col3X, y));
                        }

                        #endregion

                        // QR: tamaño 3cm x 3cm
                        int qrSizePx = (int)(3.0 / 2.54 * 100); // ≈ 118 px

                        // Desplazamiento solicitado
                        int deltaX = -(int)(1.0 / 2.54 * 100);  // ≈ +39 px derecha
                        int deltaY = (int)(1.0 / 2.54 * 100); // ≈ -20 px arriba

                        // Posición base: fila 5
                        int qrX = pageWidth - qrSizePx - padding + deltaX;
                        int qrY = padding + (5 - 1) * lineHeight + deltaY;

                        using var scaledQR = new Bitmap(qrCodeBitmap, new Size(qrSizePx, qrSizePx));
                        g.DrawImage(scaledQR, new System.Drawing.Rectangle(qrX, qrY, qrSizePx, qrSizePx));
                    };

                    pd.Print();

                    result.Message = "Impresión realizada correctamente.";
                    result.Success = true;
                    result.CodeTransacc = 1;
                }
                else
                {
                    result.Message = "No se encontró la impresora.";
                    result.Success = true;
                    result.CodeTransacc = 0;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error al imprimir el sticker: " + ex.Message;
                result.Success = true;
                result.CodeTransacc = 0;
            }

            return result;

        }


        /// <summary>
        /// Descripcion: Imprimir Sticker Version 1.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="PrintName"></param>
        /// <param name="tx_TelaEstructuraColgador"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<int>> PrintQRCode_v1(string content, string PrintName, Tx_TelaEstructuraColgador tx_TelaEstructuraColgador, int? iCantidadPrint)
        {
            await Task.Delay(1);
            var result = new ServiceResponse<int>();
            string fechaActual = DateTime.Now.ToString("dd/MM/yyyy");
            string sTituloPreco = "PRECOTEX S.A.C.";
            var resultClient = await _txUbicacionColgadorService.ObtieneAbreviaturaCliente(tx_TelaEstructuraColgador.Cod_Tela!, tx_TelaEstructuraColgador.Cod_Ruta!, tx_TelaEstructuraColgador.Cod_OrdTra!);
            var titulos = new List<string>
    {
        "Factory", "Fabric Descr.", "Yarn", "Composition", "Color", "Shrinkage STD", "Width B/W", "Weigth B/W",
        "Yield Mt/Kg", "Fabric Finish", "Fabric Wash", "Code", "Partida", fechaActual
    };

            var descripciones = new List<string>
    {
        //$"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.Nom_Cliente) ? "" : tx_TelaEstructuraColgador.Nom_Cliente)}",
        $"{sTituloPreco}",
        $"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.Fabric) ? "" : tx_TelaEstructuraColgador.Fabric)}",
        $"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.Yarn) ? "" : tx_TelaEstructuraColgador.Yarn)}",
        $"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.DesComposicion) ? "" : tx_TelaEstructuraColgador.DesComposicion)}",
        $"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.Des_Color) ? "" : tx_TelaEstructuraColgador.Des_Color)}",

        //$"{tx_TelaEstructuraColgador.Encog_Lenght?.ToString("0") ?? "-"} % length   {tx_TelaEstructuraColgador.Encog_Width?.ToString("0") ?? "-"} % width",
        $"{(tx_TelaEstructuraColgador.Encog_Lenght == null || tx_TelaEstructuraColgador.Encog_Lenght == 0 ? "0": Convert.ToInt32(tx_TelaEstructuraColgador.Encog_Lenght))} % length " +
        $"{(tx_TelaEstructuraColgador.Encog_Width == null || tx_TelaEstructuraColgador.Encog_Width == 0 ? "0": Convert.ToInt32(tx_TelaEstructuraColgador.Encog_Width))} % width",

        //$"{tx_TelaEstructuraColgador.Width_BW?.ToString("0") ?? "-"} Mts  Width A/W {tx_TelaEstructuraColgador.Width_AW?.ToString("0") ?? "-"} Mts",
        $"{(tx_TelaEstructuraColgador.Width_BW == null || tx_TelaEstructuraColgador.Width_BW == 0 ? "0" : tx_TelaEstructuraColgador.Width_BW.Value.ToString("0.00"))} Mts Width A/W " +
        $"{(tx_TelaEstructuraColgador.Width_AW == null || tx_TelaEstructuraColgador.Width_AW == 0 ? "0" : tx_TelaEstructuraColgador.Width_AW.Value.ToString("0.00"))} Mts",

        $"{(tx_TelaEstructuraColgador.Weight_BW == null || tx_TelaEstructuraColgador.Weight_BW == 0 ? "0": Convert.ToInt32(tx_TelaEstructuraColgador.Weight_BW))} Gr/m2  Weight A/W " +
        $"{(tx_TelaEstructuraColgador.Weight_AW == null || tx_TelaEstructuraColgador.Weight_AW == 0 ? "0": Convert.ToInt32(tx_TelaEstructuraColgador.Weight_AW))} Gr/m2",


        $"{(tx_TelaEstructuraColgador.Yield == null || tx_TelaEstructuraColgador.Yield == 0 ? "0" : tx_TelaEstructuraColgador.Yield.Value.ToString("0.00"))} Mt/Kg",

        $"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.Des_Fabric_Finish) ? "" : tx_TelaEstructuraColgador.Des_Fabric_Finish)}",
        $"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.Des_Fabric_Wash) ? "" : tx_TelaEstructuraColgador.Des_Fabric_Wash)}",
        $"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.Cod_Tela) ? "" : tx_TelaEstructuraColgador.Cod_Tela)}",
        $"{(string.IsNullOrWhiteSpace(tx_TelaEstructuraColgador.Cod_OrdTra) ? "" : tx_TelaEstructuraColgador.Cod_OrdTra + " - " + resultClient.Elements.FirstOrDefault().Abr_Cliente!.ToString())}",
    };

            try
            {
                var binstalled = PrinterSettings.InstalledPrinters.Cast<string>()
                    .Any(p => p.Equals(PrintName, StringComparison.OrdinalIgnoreCase));

                if (binstalled)
                {
                    // Crear QR
                    var qrGenerator = new QRCodeGenerator();
                    var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
                    var base64QRCode = new Base64QRCode(qrCodeData);
                    string base64String = base64QRCode.GetGraphic(20);
                    var qrCodeBitmap = Base64ToBitmap(base64String);

                    var pd = new PrintDocument
                    {
                        PrinterSettings = new PrinterSettings { PrinterName = PrintName }
                    };

                    // 11.5cm x 7.5cm a 100 DPI → 453 x 295 px
                    pd.DefaultPageSettings.PaperSize = new PaperSize("USER", 453, 250);

                    // Márgenes de 0.5 cm (≈ 20 px)
                    int padding = (int)(0.5 / 2.54 * 100);
                    pd.DefaultPageSettings.Margins = new Margins(padding, padding, padding, padding);

                    pd.PrintPage += (sender, e) =>
                    {
                        var g = e.Graphics;
                        g.Clear(Color.White);

                        int pageWidth = e.PageBounds.Width;
                        int pageHeight = e.PageBounds.Height;

                        using var fontTitle = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
                        using var fontNormal = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
                        int lineHeight = (int)Math.Ceiling(g.MeasureString("Test", fontTitle).Height);

                        int dpi = 100;
                        float col1Width = 3.0f / 2.54f * dpi;
                        float col2Width = 0.5f / 2.54f * dpi;

                        float col1X = padding;
                        float col2X = col1X + col1Width;
                        float col3X = col2X + col2Width;

                        // Títulos y separador
                        for (int i = 0; i < titulos.Count; i++)
                        {
                            float y = padding + i * lineHeight;
                            g.DrawString(titulos[i], fontTitle, Brushes.Black, new PointF(col1X, y));
                            g.DrawString(":", fontNormal, Brushes.Black, new PointF(col2X, y));
                        }

                        // Descripciones
                        for (int i = 0; i < descripciones.Count; i++)
                        {
                            float y = padding + i * lineHeight;
                            g.DrawString(descripciones[i], fontNormal, Brushes.Black, new PointF(col3X, y));
                        }

                        // QR Code (3cm x 3cm ≈ 118 px)
                        //int qrSizePx = (int)(3.0 / 2.54 * dpi);
                        int qrSizePx = (int)(2.5 / 2.54 * dpi); // ≈ 98 px
                        int deltaX = -(int)(0.5 / 2.54 * dpi);  // -1 cm
                        int deltaY = (int)(1.5 / 2.54 * dpi);   // +1.5 cm

                        int qrX = pageWidth - qrSizePx - padding + deltaX;
                        int qrY = padding + (5 - 1) * lineHeight + deltaY;

                        using var scaledQR = new Bitmap(qrCodeBitmap, new Size(qrSizePx, qrSizePx));
                        g.DrawImage(scaledQR, new System.Drawing.Rectangle(qrX, qrY, qrSizePx, qrSizePx));
                    };

                    //Imprime cantidad de etiquetas
                    for (int i = 0; i < iCantidadPrint; i++) // IMPRIME N VECES
                    {
                        pd.Print();
                    }

                    result.Message = "Impresión realizada correctamente.";
                    result.Success = true;
                    result.CodeTransacc = 1;
                }
                else
                {
                    result.Message = "No se encontró la impresora.";
                    result.Success = true;
                    result.CodeTransacc = 0;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error al imprimir el sticker: " + ex.Message;
                result.Success = true;
                result.CodeTransacc = 0;
            }

            return result;
        }



        public async Task<ServiceResponse<int>> PrintQRCode(string content, string PrintName)
        {
            await Task.Delay(1);
            var result = new ServiceResponse<int>();
            try
            {
                // Validar existencia de impresora
                var binstalled = PrinterSettings.InstalledPrinters.Cast<string>().Any(p => p.Equals(PrintName, StringComparison.OrdinalIgnoreCase));

                if (binstalled)
                {
                    // Generar QR
                    var qrGenerator = new QRCodeGenerator();
                    var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);

                    // Usar Base64QRCode para generar la cadena Base64 del código QR
                    var base64QRCode = new Base64QRCode(qrCodeData);
                    string base64String = base64QRCode.GetGraphic(10);  // 10 es el tamaño del gráfico

                    // Convertir el Base64 a imagen para impresión
                    var qrCodeBitmap = Base64ToBitmap(base64String);

                    // Preparar documento para impresión
                    var pd = new PrintDocument
                    {
                        PrinterSettings = new PrinterSettings { PrinterName = PrintName }
                    };
                    // Convertir 8cm x 13cm a píxeles (100 DPI)
                    //pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 315, 512); // 100 DPI
                    //pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0); // Sin márgenes

                    pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 433, 256); // 100 DPI
                    pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

                    pd.PrintPage += (sender, e) =>
                    {
                        var g = e.Graphics;

                        int qrSizePx = 197; // 5 cm = 197 px
                        int topMarginPx = 20; // 0.5 cm = 20 px

                        int pageWidth = e.PageBounds.Width;   // 512 px (13 cm)
                        int pageHeight = e.PageBounds.Height; // 276 px (7 cm)

                        // Centrado horizontal, con espacio superior
                        float qrX = (pageWidth - qrSizePx) / 2f;
                        float qrY = topMarginPx;

                        var scaledQR = new Bitmap(qrCodeBitmap, new Size(qrSizePx, qrSizePx));
                        g.DrawImage(scaledQR, qrX, qrY);

                        using var font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
                        SizeF textSize = g.MeasureString(content, font);

                        // Posición del texto: justo debajo del QR
                        float textX = (pageWidth - textSize.Width) / 2f;
                        float textY = qrY + qrSizePx + 5; // 5 px debajo del QR

                        // Verificar que el texto no se salga del sticker
                        if (textY + textSize.Height <= pageHeight)
                        {
                            g.DrawString(content, font, Brushes.Black, new PointF(textX, textY));
                        }
                    };

                    // Imprimir el QR
                    pd.Print();

                    result.Message = "Impresión Realizado Correctamente.";
                    result.Success = true;
                    result.CodeTransacc = 1;
                }
                else
                {
                    result.Message = "No se encontro el Dispositivo (Ticketera), no fue encontrado";
                    result.Success = true;
                    result.CodeTransacc = 0;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error al imprimir el ticket.";
                result.Success = true;
                result.CodeTransacc = 0;
            }

            return result;
        }

        // Convertir Base64 a Bitmap para impresión
        private Bitmap Base64ToBitmap(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (var ms = new MemoryStream(imageBytes))
            {
                return new Bitmap(ms);
            }
        }
        #endregion

        //public async Task<ServiceResponse<int>> PrintA4(List<Tx_Memorandum?> memo, string PrintName, int iCantidad)
        //{
        //    await Task.Delay(1);
        //    var result = new ServiceResponse<int>();

        //    try
        //    {
        //        // Validar existencia de impresora
        //        var binstalled = PrinterSettings.InstalledPrinters.Cast<string>()
        //            .Any(p => p.Equals(PrintName, StringComparison.OrdinalIgnoreCase));

        //        if (!binstalled)
        //        {
        //            result.Message = "No se encontró la impresora especificada.";
        //            result.Success = false;
        //            result.CodeTransacc = 0;
        //            return result;
        //        }

        //        int currentCopy = 0;

        //        var pd = new PrintDocument
        //        {
        //            PrinterSettings = new PrinterSettings { PrinterName = PrintName }
        //        };

        //        // 🔹 Configurar A4
        //        pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
        //        pd.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

        //        pd.PrintPage += (sender, e) =>
        //        {
        //            var g = e.Graphics;

        //            // 📌 Dividir hoja en 2 mitades (superior e inferior)
        //            float halfPageHeight = e.PageBounds.Height / 2f;

        //            for (int half = 0; half < 2 && currentCopy < iCantidad; half++)
        //            {
        //                float startY = (half == 0) ? 50 : halfPageHeight + 20;
        //                float x = 50;
        //                float y = startY;
        //                float lineHeight = 22;

        //                using var fontHeader = new Font("Arial", 12, FontStyle.Bold);
        //                using var fontNormal = new Font("Arial", 10, FontStyle.Regular);
        //                using var fontBold = new Font("Arial", 10, FontStyle.Bold);
        //                using var fontDetail = new Font("Arial", 8, FontStyle.Regular);
        //                using var fontTableHeader = new Font("Arial", 11, FontStyle.Bold);

        //                // 🔹 Encabezado centrado
        //                string titulo = $"Memorandum # {memo[0]?.Num_Memo}";
        //                SizeF sizeTitulo = g.MeasureString(titulo, fontHeader);
        //                float xCenter = (e.PageBounds.Width - sizeTitulo.Width) / 2;
        //                g.DrawString(titulo, fontHeader, Brushes.Black, xCenter, y);
        //                y += lineHeight * 2;

        //                // ================== 🔹 CABECERAS ORDENADAS (TABLA INVISIBLE) ==================
        //                float col1Width = 120; // ancho fijo cabecera
        //                float sepWidth = 15;   // espacio para ":"
        //                float col2Start = x + col1Width + sepWidth;

        //                // Fecha (dd/MM/yyyy HH:mm)
        //                g.DrawString("Fecha", fontBold, Brushes.Black, x, y);
        //                g.DrawString(":", fontBold, Brushes.Black, x + col1Width, y);
        //                g.DrawString($"{memo[0]?.Fecha_Emisor:dd/MM/yyyy HH:mm}", fontNormal, Brushes.Black, col2Start, y);
        //                y += lineHeight - 5;

        //                // Emisor
        //                g.DrawString("Emisor", fontBold, Brushes.Black, x, y);
        //                g.DrawString(":", fontBold, Brushes.Black, x + col1Width, y);
        //                g.DrawString($"{memo[0]?.Emisor}", fontNormal, Brushes.Black, col2Start, y);
        //                y += lineHeight - 5;

        //                // Planta Origen
        //                g.DrawString("Planta Origen", fontBold, Brushes.Black, x, y);
        //                g.DrawString(":", fontBold, Brushes.Black, x + col1Width, y);
        //                g.DrawString($"{memo[0]?.Planta_Origen}", fontNormal, Brushes.Black, col2Start, y);
        //                y += lineHeight - 5;

        //                // Receptor
        //                g.DrawString("Receptor", fontBold, Brushes.Black, x, y);
        //                g.DrawString(":", fontBold, Brushes.Black, x + col1Width, y);
        //                g.DrawString($"{memo[0]?.Receptor}", fontNormal, Brushes.Black, col2Start, y);
        //                y += lineHeight - 5;

        //                // Planta Destino
        //                g.DrawString("Planta Destino", fontBold, Brushes.Black, x, y);
        //                g.DrawString(":", fontBold, Brushes.Black, x + col1Width, y);
        //                g.DrawString($"{memo[0]?.Planta_Destino}", fontNormal, Brushes.Black, col2Start, y);
        //                y += lineHeight - 5;

        //                // Tipo
        //                g.DrawString("Tipo", fontBold, Brushes.Black, x, y);
        //                g.DrawString(":", fontBold, Brushes.Black, x + col1Width, y);
        //                g.DrawString($"{memo[0]?.Descripcion_Tipo_Memorandum}", fontNormal, Brushes.Black, col2Start, y);
        //                y += lineHeight - 5;

        //                // Motivo
        //                g.DrawString("Motivo", fontBold, Brushes.Black, x, y);
        //                g.DrawString(":", fontBold, Brushes.Black, x + col1Width, y);
        //                g.DrawString($"{memo[0]?.Descripcion_Tipo_Motivo}", fontNormal, Brushes.Black, col2Start, y);
        //                y += lineHeight * 2;

        //                // ================== 🔹 TABLA DETALLE ==================
        //                int colItemWidth = 60;
        //                int colDescripcionWidth = 320;
        //                int colCantidadWidth = 100;
        //                int rowHeight = 25;

        //                int startX = (int)x;
        //                int startTableY = (int)y;

        //                Brush brushGray = Brushes.LightGray;
        //                Brush brushBlack = Brushes.Black;

        //                // Dibujar encabezado con fondo gris
        //                g.FillRectangle(brushGray, startX, startTableY, colItemWidth, rowHeight);
        //                g.FillRectangle(brushGray, startX + colItemWidth, startTableY, colDescripcionWidth, rowHeight);
        //                g.FillRectangle(brushGray, startX + colItemWidth + colDescripcionWidth, startTableY, colCantidadWidth, rowHeight);

        //                g.DrawRectangle(Pens.Black, startX, startTableY, colItemWidth, rowHeight);
        //                g.DrawRectangle(Pens.Black, startX + colItemWidth, startTableY, colDescripcionWidth, rowHeight);
        //                g.DrawRectangle(Pens.Black, startX + colItemWidth + colDescripcionWidth, startTableY, colCantidadWidth, rowHeight);

        //                // 🔹 Encabezados con tamaño 11
        //                g.DrawString("Item", fontTableHeader, brushBlack, startX + 5, startTableY + 5);
        //                g.DrawString("Descripción", fontTableHeader, brushBlack, startX + colItemWidth + 5, startTableY + 5);
        //                g.DrawString("Cantidad", fontTableHeader, brushBlack, startX + colItemWidth + colDescripcionWidth + 5, startTableY + 5);

        //                startTableY += rowHeight;

        //                // 🔹 Filas del detalle
        //                int item = 1;
        //                foreach (var det in memo)
        //                {
        //                    string descripcion = det?.Glosa ?? "";
        //                    string cantidad = det?.Cantidad.ToString() ?? "0";

        //                    // dividir descripción en líneas
        //                    List<string> lineas = new List<string>();
        //                    string[] palabras = descripcion.Split(' ');
        //                    string linea = "";
        //                    foreach (var palabra in palabras)
        //                    {
        //                        string test = (linea == "" ? palabra : linea + " " + palabra);
        //                        SizeF size = g.MeasureString(test, fontDetail);
        //                        if (size.Width > colDescripcionWidth - 10)
        //                        {
        //                            lineas.Add(linea);
        //                            linea = palabra;
        //                        }
        //                        else
        //                        {
        //                            linea = test;
        //                        }
        //                    }
        //                    if (!string.IsNullOrEmpty(linea))
        //                        lineas.Add(linea);

        //                    int rowHeightDynamic = lineas.Count * 15 + 10;

        //                    // ITEM
        //                    g.DrawRectangle(Pens.Black, startX, startTableY, colItemWidth, rowHeightDynamic);
        //                    g.DrawString(item.ToString(), fontDetail, Brushes.Black, startX + 5, startTableY + 5);

        //                    // DESCRIPCIÓN multilínea
        //                    g.DrawRectangle(Pens.Black, startX + colItemWidth, startTableY, colDescripcionWidth, rowHeightDynamic);
        //                    for (int i = 0; i < lineas.Count; i++)
        //                    {
        //                        g.DrawString(lineas[i], fontDetail, Brushes.Black, startX + colItemWidth + 5, startTableY + 5 + (i * 15));
        //                    }

        //                    // CANTIDAD
        //                    g.DrawRectangle(Pens.Black, startX + colItemWidth + colDescripcionWidth, startTableY, colCantidadWidth, rowHeightDynamic);
        //                    g.DrawString(cantidad, fontDetail, Brushes.Black, startX + colItemWidth + colDescripcionWidth + 5, startTableY + 5);

        //                    startTableY += rowHeightDynamic;
        //                    item++;
        //                }

        //                y = startTableY + 40;

        //                // 🔹 Pie
        //                g.DrawString("Garita", fontNormal, Brushes.Black, x, y);
        //                g.DrawString("Transportista", fontNormal, Brushes.Black, x + 200, y);

        //                currentCopy++;
        //            }

        //            // ¿Quedan más copias?
        //            e.HasMorePages = currentCopy < iCantidad;
        //        };

        //        pd.Print();

        //        result.Message = $"Se imprimieron {iCantidad} memorándums en media hoja A4.";
        //        result.Success = true;
        //        result.CodeTransacc = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = $"Error al imprimir: {ex.Message}";
        //        result.Success = false;
        //        result.CodeTransacc = 0;
        //    }

        //    return result;
        //}


        public async Task<ServiceResponse<string>> PrintA4ToPdf(List<Tx_Memorandum?> memo, int iCantidad)
        {
            await Task.Delay(1);
            var result = new ServiceResponse<string>();

            try
            {
                string fileName = $"Memorandum_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string filePath = Path.Combine(Path.GetTempPath(), fileName);

                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 40, 40, 40, 40))
                using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
                {
                    doc.Open();

                    // 🔹 Fuentes
                    var fontHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                    var fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    var fontBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                    var fontDetail = FontFactory.GetFont(FontFactory.HELVETICA, 8);

                    for (int copy = 0; copy < iCantidad; copy++)
                    {
                        // 🔹 Título
                        Paragraph titulo = new Paragraph($"Memorandum # {memo[0]?.Num_Memo}", fontHeader);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                        doc.Add(new Paragraph(" ", fontNormal));

                        // ================== 🔹 CABECERAS (tipo ficha alineada) ==================
                        PdfPTable headerTable = new PdfPTable(3);
                        headerTable.HorizontalAlignment = Element.ALIGN_LEFT; // 🔹 alinear toda la tabla a la izquierda
                        headerTable.WidthPercentage = 80; // opcional: que ocupe solo el 80% del ancho, no todo
                        headerTable.SetWidths(new float[] { 2, 0.3f, 5 });

                        void AddRow(string label, string value)
                        {
                            // Etiqueta alineada derecha
                            headerTable.AddCell(new PdfPCell(new Phrase(label, fontBold))
                            {
                                Border = iTextSharp.text.Rectangle.NO_BORDER,
                                HorizontalAlignment = Element.ALIGN_RIGHT,
                                PaddingBottom = 4f
                            });

                            // Dos puntos centrado
                            headerTable.AddCell(new PdfPCell(new Phrase(":", fontBold))
                            {
                                Border = iTextSharp.text.Rectangle.NO_BORDER,
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                PaddingBottom = 4f
                            });

                            // Valor alineado izquierda
                            headerTable.AddCell(new PdfPCell(new Phrase(value, fontNormal))
                            {
                                Border = iTextSharp.text.Rectangle.NO_BORDER,
                                HorizontalAlignment = Element.ALIGN_LEFT,
                                PaddingBottom = 4f
                            });
                        }

                        AddRow("Fecha", $"{memo[0]?.Fecha_Emisor:dd/MM/yyyy HH:mm}");
                        AddRow("Emisor", memo[0]?.Emisor ?? "");
                        AddRow("Planta Origen", memo[0]?.Planta_Origen ?? "");
                        AddRow("Receptor", memo[0]?.Receptor ?? "");
                        AddRow("Planta Destino", memo[0]?.Planta_Destino ?? "");
                        AddRow("Tipo", memo[0]?.Descripcion_Tipo_Memorandum ?? "");
                        AddRow("Motivo", memo[0]?.Descripcion_Tipo_Motivo ?? "");

                        doc.Add(headerTable);
                        doc.Add(new Paragraph(" ", fontNormal));

                        // ================== 🔹 TABLA DETALLE ==================
                        PdfPTable table = new PdfPTable(3);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 1, 5, 2 });

                        BaseColor gray = new BaseColor(220, 220, 220);
                        PdfPCell cell;

                        cell = new PdfPCell(new Phrase("Item", fontBold)) { BackgroundColor = gray, HorizontalAlignment = Element.ALIGN_CENTER };
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Descripción", fontBold)) { BackgroundColor = gray, HorizontalAlignment = Element.ALIGN_CENTER };
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase("Cantidad", fontBold)) { BackgroundColor = gray, HorizontalAlignment = Element.ALIGN_CENTER };
                        table.AddCell(cell);

                        int item = 1;
                        foreach (var det in memo)
                        {
                            table.AddCell(new PdfPCell(new Phrase(item.ToString(), fontDetail)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            table.AddCell(new PdfPCell(new Phrase(det?.Glosa ?? "", fontDetail)));
                            table.AddCell(new PdfPCell(new Phrase(det?.Cantidad.ToString() ?? "0", fontDetail)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            item++;
                        }

                        doc.Add(table);

                        // ================== 🔹 PIE (firmas) ==================
                        doc.Add(new Paragraph("\n\n\n")); // espacio antes de firmas
                        PdfPTable footerTable = new PdfPTable(2);
                        footerTable.WidthPercentage = 100;
                        footerTable.SetWidths(new float[] { 1, 1 });

                        PdfPCell garita = new PdfPCell(new Phrase("____________________________\nGarita", fontNormal))
                        {
                            Border = iTextSharp.text.Rectangle.NO_BORDER,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            PaddingTop = 20f
                        };
                        PdfPCell transportista = new PdfPCell(new Phrase("____________________________\nTransportista", fontNormal))
                        {
                            Border = iTextSharp.text.Rectangle.NO_BORDER,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            PaddingTop = 20f
                        };

                        footerTable.AddCell(garita);
                        footerTable.AddCell(transportista);

                        doc.Add(footerTable);

                        if (copy < iCantidad - 1)
                            doc.NewPage(); // cada copia en página nueva
                    }

                    doc.Close();
                }

                result.Message = "PDF generado correctamente.";
                result.Success = true;
                result.CodeTransacc = 1;
                result.Element = filePath; // o aquí podrías devolver Base64 si quieres
            }
            catch (Exception ex)
            {
                result.Message = $"Error al generar PDF: {ex.Message}";
                result.Success = false;
                result.CodeTransacc = 0;
            }

            return result;
        }
    }
}
