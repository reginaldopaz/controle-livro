@Imports PagedList.Mvc
@Imports PagedList
@ModelType PagedList.IPagedList(Of ControleLivros.Models.ControleLivros.Models.Entities.Livro)

@Code
    ViewData("Title") = "Index"
End Code

<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">

<h2>Livros</h2>

<div class="d-flex mb-3 align-items-center">
    @Using Html.BeginForm("Create", "Livro", FormMethod.Get, New With {.class = "mr-2"})
        @<button type="submit" class="btn btn-primary me-2">
            <i class="fas fa-plus"></i> Incluir Livro
        </button>
    End Using

    @Using Html.BeginForm("Index", "Livro", FormMethod.Get, New With {.class = "d-flex align-items-center me-2"})
        @<div class="form-group mb-0 me-2">
            <input type="text" id="codlFiltro" name="codlFiltro" class="form-control" placeholder="Código do Livro" value="@Request.QueryString("codlFiltro")" />
        </div>
        @<button type="submit" class="btn btn-secondary me-2">Filtrar</button>
    End Using

    <a href="@Url.Action("Index", "Visao")" class="btn btn-secondary">
        <i class="fas fa-chart-bar"></i> Ver Relatório
    </a>
</div>

<table class="table">
    <thead>
        <tr>
            <th>@Html.DisplayNameFor(Function(model) model.First().Codl)</th>
            <th>@Html.DisplayNameFor(Function(model) model.First().Titulo)</th>
            <th>@Html.DisplayNameFor(Function(model) model.First().Editora)</th>
            <th>@Html.DisplayNameFor(Function(model) model.First().AnoPublicacao)</th>
            <th>@Html.DisplayNameFor(Function(model) model.First().Valor)</th>
            <th>Ações</th>
        </tr>
    </thead>
    <tbody>
        @For Each item In Model
            @<tr>
                <td>@Html.DisplayFor(Function(modelItem) item.Codl)</td>
                <td>@Html.DisplayFor(Function(modelItem) item.Titulo)</td>
                <td>@Html.DisplayFor(Function(modelItem) item.Editora)</td>
                <td>@Html.DisplayFor(Function(modelItem) item.AnoPublicacao)</td>
                <td>@Html.DisplayFor(Function(modelItem) item.Valor)</td>
                <td>
                    <div class="btn-group" role="group">
                        <a href="@Url.Action("Details", "Livro", New With {.Codl = item.Codl})" class="btn btn-info btn-sm">
                            <i class="fas fa-info-circle"></i> Detalhes
                        </a>
                        <a href="@Url.Action("Edit", "Livro", New With {.Codl = item.Codl})" class="btn btn-warning btn-sm">
                            <i class="fas fa-edit"></i> Editar
                        </a>
                        <a href="@Url.Action("Delete", "Livro", New With {.Codl = item.Codl})" class="btn btn-danger btn-sm">
                            <i class="fas fa-trash"></i> Excluir
                        </a>
                    </div>
                </td>
            </tr>
        Next
    </tbody>
</table>

<div class="text-center">
    @Html.PagedListPager(Model, Function(page) Url.Action("Index", New With {.pageNumber = page}), PagedListRenderOptions.Classic)
</div>
