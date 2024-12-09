@ModelType ControleLivros.Models.ControleLivros.Models.Entities.Livro

@Code
    ViewData("Title") = "Detalhes"
End Code

<h2>Detalhes do Livro</h2>

<div>
    <h4>Livro</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>@Html.DisplayNameFor(Function(model) model.Titulo)</dt>
        <dd>@Html.DisplayFor(Function(model) model.Titulo)</dd>

        <dt>@Html.DisplayNameFor(Function(model) model.Editora)</dt>
        <dd>@Html.DisplayFor(Function(model) model.Editora)</dd>

        <dt>@Html.DisplayNameFor(Function(model) model.Edicao)</dt>
        <dd>@Html.DisplayFor(Function(model) model.Edicao)</dd>

        <dt>@Html.DisplayNameFor(Function(model) model.AnoPublicacao)</dt>
        <dd>@Html.DisplayFor(Function(model) model.AnoPublicacao)</dd>

        <dt>@Html.DisplayNameFor(Function(model) model.Valor)</dt>
        <dd>@Html.DisplayFor(Function(model) model.Valor)</dd>
    </dl>
</div>

<div>
    <a href="@Url.Action("Edit", New With {.Codl = Model.Codl})" class="btn btn-warning">
        <i class="fas fa-edit"></i> Editar
    </a>
    |
    <a href="@Url.Action("Index")" class="btn btn-secondary">
        <i class="fas fa-list"></i> Voltar à Lista
    </a>
</div>

