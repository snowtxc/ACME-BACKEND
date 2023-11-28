
using BusinessLayer.IBLs;
using DataAccessLayer.Models.Dtos.Factura;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Rotativa.AspNetCore;
using System.Net;
using System.Security.Policy;

namespace FACTURACION.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturacionController : Controller
    {

        private readonly IBL_Compra _blCompra;

        public FacturacionController(IBL_Compra blCompra)
        {
            _blCompra = blCompra;


        }

        private static byte[] urlImageToBytes(string url)
        {
            byte[] imageData;
            using (var webClient = new WebClient())
            {
                imageData = webClient.DownloadData(url);
            }
            return imageData;

        }


        [HttpPost]
        public async Task<IActionResult> imprimirFactura(FacturaDTO facturaData)
        {
            var data = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(30);

                    page.Header().ShowOnce().Row(row =>
                    {


                        row.ConstantItem(150).Width(100).Image(FacturacionController.urlImageToBytes(facturaData.logo));
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text(facturaData.nombreEmpresa).Bold().FontSize(14);
                            col.Item().AlignCenter().Text(facturaData.direccionEmpresa).FontSize(9);
                            col.Item().AlignCenter().Text(facturaData.telefonoEmpresa).FontSize(9);
                            col.Item().AlignCenter().Text(facturaData.correoEmpresa).FontSize(9);

                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Background("#1b2540").Padding(2).Text("Factura de Compra").Bold().FontColor("#fff").FontSize(14);

                            col.Item().AlignCenter().Text(text =>
                            {
                                text.Span("Fecha: ").SemiBold().FontSize(10);
                                text.Span(facturaData.fecha).FontSize(10);
                            });

                            col.Item().AlignCenter().Text(text =>
                            {
                                text.Span("Nro de Factura: ").SemiBold().FontSize(10);
                                text.Span(facturaData.nroFactura.ToString()).FontSize(10);
                            });



                        });


                    });

                    page.Content().Column(col1 =>
                    {
                        col1.Item().Text("Datos del Cliente").Underline().Bold();
                        col1.Item().Text(text =>
                        {
                            text.Span("Nombre: ").SemiBold().FontSize(10);
                            text.Span(facturaData.nombreCliente).FontSize(10);
                        });
                        col1.Item().Text(text =>
                        {
                            text.Span("Celular: ").SemiBold().FontSize(10);
                            text.Span(facturaData.celularCliente).FontSize(10);
                        });

                 
                        col1.Item().LineHorizontal(0.5f);

                        col1.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();

                                tabla.Header(header =>
                                {
                                    header.Cell().Background("#1b2540").Padding(2).Text("Foto Producto").FontColor("#ffff");
                                    header.Cell().Background("#1b2540").Padding(2).AlignCenter().Text("Nombre").FontColor("#ffff");
                                    header.Cell().Background("#1b2540").Padding(2).AlignCenter().Text("Precio Unitario").FontColor("#ffff");
                                    header.Cell().Background("#1b2540").Padding(2).AlignCenter().Text("Cantitad").FontColor("#ffff");
                                    header.Cell().Background("#1b2540").Padding(2).AlignRight().Text("Total").FontColor("#ffff");


                                    foreach(var item in facturaData.lineas)
                                    {
                                        tabla.Cell().Border(0.5f).BorderColor("#D9D9D9").Padding(2).Width(50).Image(FacturacionController.urlImageToBytes(item.fotoProducto)).FitWidth();
                                        tabla.Cell().Border(0.5f).BorderColor("#D9D9D9").Padding(2).AlignCenter().Placeholder(item.nombreProducto);
                                        tabla.Cell().Border(0.5f).BorderColor("#D9D9D9").Padding(2).AlignCenter().Placeholder(item.precioUnitario.ToString());
                                        tabla.Cell().Border(0.5f).BorderColor("#D9D9D9").Padding(2).AlignCenter().Placeholder(item.cantidad.ToString());
                                        tabla.Cell().Border(0.5f).BorderColor("#D9D9D9").Padding(2).AlignRight().Placeholder("$"+ item.subTotal.ToString()+"");

                                    }

                                });

                            });

                          

                        });

                        col1.Item().AlignRight().Text("Total: " + facturaData.total + "$").Bold().FontSize(12);

                    });

                });
            }).GeneratePdf();

            Stream stream = new MemoryStream(data);
            return File(stream, "application/pdf", "factura.pdf");
            

        }
        }
}
