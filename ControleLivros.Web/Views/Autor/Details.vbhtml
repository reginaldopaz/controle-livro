@ModelType ControleLivros.Models.ControleLivros.Models.Entities.Autor

@Code
    ViewData("Title") = "Detalhes do Autor"
End Code

<h2>Detalhes do Autor</h2>

<div>
    <h4>Autor</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>@Html.DisplayNameFor(Function(model) model.CodAu)</dt>
        <dd>@Html.DisplayFor(Function(model) model.CodAu)</dd>

        <dt>@Html.DisplayNameFor(Function(model) model.Nome)</dt>
        <dd>@Html.DisplayFor(Function(model) model.Nome)</dd>
    </dl>
</div>

<div>
    <a href="@Url.Action("Edit", New With {.CodAu = Model.CodAu})" class="btn btn-warning">
        <i class="fas fa-edit"></i> Editar
    </a>
    <a href="@Url.Action("Index")" class="btn btn-secondary">
        <i class="fas fa-arrow-left"></i> Voltar à Lista
    </a>
</div>
