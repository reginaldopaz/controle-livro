@ModelType ControleLivros.Models.ControleLivros.Models.Entities.Livro

@Code
    ViewData("Title") = "Detalhes"
End Code

<h2>Detalhes do Livro</h2>

<div>
    <h4>Livro</h4>
    <hr />
    <table class="table">
        <tr>
            <td>
                <strong>@Html.DisplayNameFor(Function(model) model.Titulo)</strong>
                <div>@Html.DisplayFor(Function(model) model.Titulo)</div>
            </td>
            <td>
                <strong>@Html.DisplayNameFor(Function(model) model.Editora)</strong>
                <div>@Html.DisplayFor(Function(model) model.Editora)</div>
            </td>
            <td>
                <strong>@Html.DisplayNameFor(Function(model) model.Edicao)</strong>
                <div>@Html.DisplayFor(Function(model) model.Edicao)</div>
            </td>
        </tr>
        <tr>
            <td>
                <strong>@Html.DisplayNameFor(Function(model) model.AnoPublicacao)</strong>
                <div>@Html.DisplayFor(Function(model) model.AnoPublicacao)</div>
            </td>
            <td>
                <strong>@Html.DisplayNameFor(Function(model) model.Valor)</strong>
                <div>@Html.DisplayFor(Function(model) model.Valor)</div>
            </td>
            <td>
                <strong>@Html.DisplayNameFor(Function(model) model.DataPublicacao)</strong>
                <div>@(If(Model.DataPublicacao.HasValue, Model.DataPublicacao.Value.ToString("dd/MM/yyyy"), String.Empty))</div>
            </td>
        </tr>
    </table>
</div>

<h4>Autores e Assuntos Associados</h4>
<table class="table">
    <thead>
        <tr>
            <th>Autores</th>
            <th>Assuntos</th>
        </tr>
    </thead>
    <tbody>
        @Code
            Dim autores = Model.Autores.Select(Function(a) a.Nome).ToList()
            Dim assuntos = Model.Assuntos.Select(Function(a) a.Descricao).ToList()
            Dim maxRows = Math.Max(autores.Count, assuntos.Count)
        End Code

        @For i As Integer = 0 To maxRows - 1
            @<tr>
                <td>@(If(i < autores.Count, autores(i), String.Empty))</td>
                <td>@(If(i < assuntos.Count, assuntos(i), String.Empty))</td>
            </tr>
        Next
    </tbody>
</table>

<div>
    <a href="@Url.Action("Edit", New With {.Codl = Model.Codl})" class="btn btn-warning">
        <i class="fas fa-edit"></i> Editar
    </a>
    |
    <a href="@Url.Action("Index")" class="btn btn-secondary">
        <i class="fas fa-list"></i> Voltar à Lista
    </a>
</div>
