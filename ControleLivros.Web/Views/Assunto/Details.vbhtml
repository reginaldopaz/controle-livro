@ModelType ControleLivros.Models.ControleLivros.Models.Entities.Assunto

@Code
    ViewData("Title") = "Detalhes do Assunto"
End Code

<h2>Detalhes do Assunto</h2>

<div>
    <h4>Assunto</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>@Html.DisplayNameFor(Function(model) model.CodAs)</dt>
        <dd>@Html.DisplayFor(Function(model) model.CodAs)</dd>

        <dt>@Html.DisplayNameFor(Function(model) model.Descricao)</dt>
        <dd>@Html.DisplayFor(Function(model) model.Descricao)</dd>
    </dl>
</div>

<div>
    <a href="@Url.Action("Edit", New With {.CodAs = Model.CodAs})" class="btn btn-warning">
        <i class="fa fa-edit"></i> Editar
    </a>
    <a href="@Url.Action("Index")" class="btn btn-secondary">
        <i class="fa fa-arrow-left"></i> Voltar à Lista
    </a>
</div>
