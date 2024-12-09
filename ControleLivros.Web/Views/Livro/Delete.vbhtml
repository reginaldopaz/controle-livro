@ModelType ControleLivros.Models.ControleLivros.Models.Entities.Livro

@Code
    ViewData("Title") = "Excluir"
End Code

<h2>Excluir Livro</h2>
@Html.ValidationSummary(True, "", New With {.class = "text-danger"})

<h3>Tem certeza de que deseja excluir este item?</h3>
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

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @<input type="submit" value="Excluir" class="btn btn-danger" />
End Using

<div>
    <a href="@Url.Action("Index", "Livro")" class="btn btn-secondary">
        <i class="fas fa-arrow-left"></i> Voltar à Lista
    </a>

</div>
