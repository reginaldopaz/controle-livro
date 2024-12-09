<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - Controle de Livros</title>
    <link href="~/Content/Site.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    <style>
        body {
            display: flex;
            height: 100vh;
            overflow: hidden;
        }

        .sidebar {
            width: 250px;
            background-color: #343a40;
            color: white;
            flex-shrink: 0;
        }

            .sidebar .nav-link {
                color: white;
            }

        .content {
            flex-grow: 1;
            padding: 20px;
            overflow-y: auto;
        }
    </style>
</head>
<body>
    <div class="sidebar">
        <nav class="navbar navbar-dark">
            <div class="container-fluid">
                <a class="navbar-brand" href="#">Controle de Livros</a>
                <ul class="navbar-nav flex-column">

                    <li class="nav-item">
                        <a href="/Livro/Index" class="nav-link">
                            <img src="/Imagens/Livro.png" alt="Livros" style="width: 20px; height: 20px; margin-right: 5px;" />
                            Livros
                        </a>
                    </li>
                    <li class="nav-item">
                        <a href="/Autor/Index" class="nav-link">
                            <img src="/Imagens/Regi.jpg" alt="Autor" style="width: 20px; height: 20px; margin-right: 5px;" />
                            Autor
                        </a>
                    </li>
                    <li class="nav-item">
                        <a href="/Assunto/Index" class="nav-link">
                            <img src="/Imagens/Assuntos.png" alt="Assunto" style="width: 20px; height: 20px; margin-right: 5px;" />
                            Assunto
                        </a>
                    </li>
                        
                    <li class="nav-item">
                        <a href="/Relatorio/Index" class="nav-link">
                            <img src="/Imagens/Relatorio.png" alt="Relatorio" style="width: 20px; height: 20px; margin-right: 5px;" />
                            Relatorio
                        </a>
                    </li>
                                       

                </ul>
            </div>
        </nav>
    </div>
    <div class="content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - Controle de Livros</p>
        </footer>
    </div>

    <!-- Inclua o jQuery antes de qualquer plugin que dependa dele -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <!-- Inclua o jQuery Mask Plugin -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)
</body>
</html>
