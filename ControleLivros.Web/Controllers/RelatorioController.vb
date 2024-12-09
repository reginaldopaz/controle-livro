Imports System.IO
Imports System.Web.Mvc
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Namespace Controllers
    Public Class RelatorioController
        Inherits Controller
        Function Index() As ActionResult

            Try
                Dim report As New ReportDocument()
                Dim reportPath As String = Server.MapPath("~/Relatorio/Relatorio.rpt")
                report.Load(reportPath)


                Dim stream As Stream = report.ExportToStream(ExportFormatType.PortableDocFormat)
                Return File(stream, "application/pdf", "Relatorio.pdf")
            Catch ex As Exception

                Return Content("Erro ao gerar o relatório: " & ex.Message)
            End Try

        End Function
    End Class
End Namespace