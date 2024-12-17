Imports System.ComponentModel.DataAnnotations

Namespace ControleLivros.Models.Entities
    Public Class Livro
        <Key>
        Public Property Codl As Integer

        <Required(ErrorMessage:="O título é obrigatório")>
        <StringLength(40)>
        Public Property Titulo As String

        <Required(ErrorMessage:="A editora é obrigatória")>
        <StringLength(40)>
        Public Property Editora As String

        <Required>
        <Range(1, Integer.MaxValue, ErrorMessage:="A edição deve ser maior que zero")>
        Public Property Edicao As Integer

        <Required>
        <Range(1000, 9999, ErrorMessage:="Ano de publicação inválido")>
        <StringLength(4)>
        Public Property AnoPublicacao As String

        <DataType(DataType.Currency)>
        <DisplayFormat(DataFormatString:="{0:N2}", ApplyFormatInEditMode:=False)>
        Public Property Valor As Decimal


        '<Display(Name:="Data de Publicação")>
        <DataType(DataType.Date)>
        <DisplayFormat(DataFormatString:="{0:yyyy-MM-dd}", ApplyFormatInEditMode:=True)>
        Public Property DataPublicacao As DateTime?
        Public Overridable Property Autores As ICollection(Of Autor) = New HashSet(Of Autor)
        Public Overridable Property Assuntos As ICollection(Of Assunto) = New HashSet(Of Assunto)

    End Class
End Namespace