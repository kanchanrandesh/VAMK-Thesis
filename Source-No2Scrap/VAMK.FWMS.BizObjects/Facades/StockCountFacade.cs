using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Transactions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using VAMK.FWMS.Models.Util;
using System.Text;
using System.Globalization;
using System.Linq;

namespace VAMK.FWMS.BizObjects.Facades
{
    public class StockCountFacade
    {
        WebSettings websettings;

        public StockCountFacade() { }

        public StockCountFacade(WebSettings websettings)
        {
            this.websettings = websettings;
        }

        #region - Pdf Cells -

        private PdfPCell HeaderCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 16, 1, BaseColor.BLACK)));
            cell.BorderWidthTop = 1f;
            cell.BorderWidthLeft = 1f;
            cell.BorderWidthRight = 1f;
            cell.BorderWidthBottom = 0f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.MinimumHeight = 30F;
            return cell;
        }

        private PdfPCell SubHeaderCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 12)));
            cell.BorderWidthTop = 0f;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.MinimumHeight = 20F;
            return cell;
        }

        private PdfPCell SubHeaderCellBold(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 12, 1, BaseColor.BLACK)));
            cell.BorderWidthTop = 0f;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.MinimumHeight = 20F;
            return cell;
        }

        private PdfPCell EmptyCell()
        {
            PdfPCell cell = new PdfPCell(new Phrase("", FontFactory.GetFont("Helvetica", 9)));
            cell.BorderWidthTop = 0f;
            cell.BorderWidthLeft = 1f;
            cell.BorderWidthRight = 1f;
            cell.BorderWidthBottom = 0f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.MinimumHeight = 8f;
            return cell;
        }

        private PdfPCell DefaultCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 9)));
            cell.BorderWidthTop = 0f;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = 0f;
            return cell;
        }

        private PdfPCell DefaultCellBold(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 9, 1)));
            cell.BorderWidthTop = 0f;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = 0f;
            return cell;
        }

        private PdfPCell TableHeaderCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 9)));
            cell.BorderWidthTop = 1f;
            cell.BorderWidthLeft = 1f;
            cell.BorderWidthRight = 1f;
            cell.BorderWidthBottom = 1f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.MinimumHeight = 20f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            return cell;
        }

        private PdfPCell TableCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 9)));
            cell.BorderWidthTop = 0f;
            cell.BorderWidthLeft = 1f;
            cell.BorderWidthRight = 1f;
            cell.BorderWidthBottom = 0f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.MinimumHeight = 20f;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 5;
            return cell;
        }

        private PdfPCell BorderedCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 9)));
            cell.BorderWidthTop = 1f;
            cell.BorderWidthLeft = 1f;
            cell.BorderWidthRight = 1f;
            cell.BorderWidthBottom = 1f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.MinimumHeight = 18f;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            return cell;
        }

        private PdfPCell BorderedCellBold(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 9, 1)));
            cell.BorderWidthTop = 1f;
            cell.BorderWidthLeft = 1f;
            cell.BorderWidthRight = 1f;
            cell.BorderWidthBottom = 1f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.MinimumHeight = 18f;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            return cell;
        }

        private PdfPCell CountBoxCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 10, 1, BaseColor.WHITE)));
            cell.BorderWidthTop = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthLeft = 0f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = BaseColor.GRAY;
            cell.MinimumHeight = 15F;
            return cell;
        }

        private PdfPCell RowHeaderCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 10, 0, BaseColor.BLACK)));
            cell.BorderWidthTop = 0f;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = 0f;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.MinimumHeight = 20F;
            return cell;
        }

        private PdfPCell ParagraphCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Paragraph(content, FontFactory.GetFont("Helvetica", 9)));
            cell.BorderWidthTop = 0f;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            return cell;
        }

        private PdfPCell ParagraphCellBold(string content)
        {
            PdfPCell cell = new PdfPCell(new Paragraph(content, FontFactory.GetFont("Helvetica", 9, 1)));
            cell.BorderWidthTop = 0f;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            return cell;
        }

        private PdfPCell FooterCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 7)));
            cell.BorderWidthTop = 0f;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = 1f;
            cell.PaddingTop = 5;
            cell.PaddingBottom = 10;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            return cell;
        }

        #region - Detail Cells -

        private PdfPCell DetailHeaderCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 12, 1, BaseColor.BLACK)));
            cell.BorderWidthTop = 0f;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = 0f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.MinimumHeight = 30F;
            return cell;
        }

        private PdfPCell DetailTableHeaderCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 8, 1)));
            cell.BorderWidthTop = .5f;
            cell.BorderWidthLeft = .5f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = .5f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.MinimumHeight = 15f;
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 5;
            return cell;
        }

        private PdfPCell DetailTableCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 8)));
            cell.BorderWidthTop = .5f;
            cell.BorderWidthLeft = .5f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = .5f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.MinimumHeight = 15f;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 5;
            return cell;
        }

        private PdfPCell DetailTableCellRight(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 8)));
            cell.BorderWidthTop = .5f;
            cell.BorderWidthLeft = .5f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = .5f;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.MinimumHeight = 15f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 5;
            return cell;
        }

        private PdfPCell DetailFooterCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content, FontFactory.GetFont("Helvetica", 7)));
            cell.BorderWidthTop = 0f;
            cell.BorderWidthLeft = 0f;
            cell.BorderWidthRight = 0f;
            cell.BorderWidthBottom = 0f;
            cell.PaddingTop = 5;
            cell.PaddingBottom = 10;
            cell.BorderColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            return cell;
        }
        #endregion

        #endregion


        public Document GenerateStockCountReport(Models.ReportFilters.StockCountReportFilter reportFilter, Document document)
        {

            
            var list = BizObjectFactory.GetInventoryStockBO().GetAll();

            var loggedEmployee = BizObjectFactory.GetEmployeeBO().GetProxy(reportFilter.LoggedEmployeeID.Value);

            PdfPTable table = null;
            document.Open();

            #region - Header -

            // line 1
            table = new PdfPTable(1);
            table.WidthPercentage = 100;

            table.AddCell(DetailHeaderCell("Item Wise Stock Count Report"));

            document.Add(table);

            table = new PdfPTable(1);
            table.WidthPercentage = 100;
            table.AddCell(ParagraphCell(" "));
            document.Add(table);

            #endregion


            document = FillForReport(list, reportFilter, document);



            #region - Footer -

            var footerNote = new StringBuilder();
            footerNote.Append("PRINTED ON ")
                .Append(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .Append(" ")
                .Append(DateTime.Now.ToString("t"))
                .Append(" BY " + loggedEmployee.FirstName.ToUpper() + " " + loggedEmployee.LastName.ToUpper());

            table = new PdfPTable(1);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 1 });
            table.AddCell(DetailFooterCell(footerNote.ToString()));
            document.Add(table);

            #endregion

            return document;
        }

        private Document FillForReport(IList<InventoryStock> list, Models.ReportFilters.StockCountReportFilter reportFilter, Document document)
        {
            throw new NotImplementedException();

            PdfPTable table = null;
            PdfPCell cell = null;

            table = new PdfPTable(5);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { .7f, 2, 1.5f, 4.8f, 1 });
            table.AddCell(DetailTableHeaderCell("ITEM"));
            table.AddCell(DetailTableHeaderCell("QUANTITY"));
            //  cell = DetailTableHeaderCell("Amount");
            cell.BorderWidthRight = .5f;
            table.AddCell(cell);
            document.Add(table);

            Item foodItem = null;
            for (int i = 0; i < list.Count; i++)
            {


                table = new PdfPTable(1);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1 });

                cell.BorderWidthRight = .5f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);
                document.Add(table);            

                for (int j = i; j < reportFilter.ItemIDs.Count; j++)
                {
                    foodItem = null;
                    if (reportFilter.ItemIDs[j] == list[i].ItemID)
                    {
                        foodItem = BizObjectFactory.GetItemBO().GetProxy(list[i].ItemID.Value);                        
                       
                        table = new PdfPTable(5);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { .7f, 2, 1.5f, 4.8f, 1 });
                        table.AddCell(DetailTableCell(foodItem.Name));
                        table.AddCell(DetailTableCell(list[i].Quantity.ToString()));                        
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.BorderWidthRight = .5f;
                        table.AddCell(cell);
                        document.Add(table);
                        break;

                    }                   
                }
            }           

            return document;
        }

    }
}
