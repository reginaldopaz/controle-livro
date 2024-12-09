@Code
    ViewBag.Title = "Relatório dos Livros"
End Code

<h2>@ViewBag.Title</h2>

<form action="@Url.Action("Index", "Report")" method="get">
    <button type="submit" class="btn btn-primary">Visualizar Relatório</button>
</form>
