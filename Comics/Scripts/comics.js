var url = "/api/Comics";
var urlEditoriales = "/api/Editoriales";

$(function () {
    console.clear();

    $('#cuadroerror').hide();

    $linea = $('#Comics li');

    $linea.detach();

    console.log($linea);

    $.getJSON(url, ComicOK).fail(fallo);
    $.getJSON(urlEditoriales, EditorialesOK).fail(fallo);

    $('#formComics').submit(formsubmit);
});

function EditorialesOK(editoriales) {
    $editoriales = $('#editoriales');

    $.each(editoriales, function (key, editorial) {
        $editoriales.append(new Option(editorial.Nombre, editorial.Id));
    });

}

function ComicOK(comics) {
    $comics = $('#Comics');

    $comics.empty();

    $.each(comics, function (key, comic) {
        $linea = $linea.clone();

        $linea.find('.NombreEditorial').text(comic.editorial.Nombre);
        $linea.find('.Titulo').text(comic.Titulo);
        $linea.find('.detalles').prop('href', url + "/" + comic.Id).click(comicdetalle);
        $linea.find('.borrar').prop('href', url + "/" + comic.Id).click(comicborrar);
        $linea.find('.actualizar').prop('href', url + "/" + comic.Id).click(comicactualizar);

        $comics.append($linea);

        console.log(key, comic);
    });
}

function formsubmit(e) {
    e.preventDefault();

    var comic = {
        "Titulo": $('#inputTitulo').val(),
        "Fecha": $('#inputFecha').val(),
        "Precio": $('#inputPrecio').val(),
        "Autor": $('#inputAutor').val(),
        "EditorialId": $('#editoriales').val(),
        "Calificacion": $('#inputStars').val(),
    };

    var comicId = $('#inputId').val();
    var esActualizacion = !!comicId;

    if (esActualizacion) {
        comic.Id = comicId;

        $.ajax({
            url: url + "/" + comicId,
            method: 'PUT',
            data: JSON.stringify(comic),
            dataType: 'json',
            contentType: 'application/json'
        }).done(function (comic) {
            console.log(comic);
            limpiar();
            $.getJSON(url, ComicOK).fail(fallo);
        }).fail(fallo);
    }
    else
        $.ajax({
            url: url,
            method: 'POST',
            data: JSON.stringify(comic),
            dataType: 'json',
            contentType: 'application/json'
        }).done(function (comic) {
            console.log(comic);
            limpiar();
            $.getJSON(url, ComicOK).fail(fallo);
            $('#editoriales').focus();
        }).fail(fallo);

    console.log(JSON.stringify(comic));
}

function limpiar() {
    $('#inputId').remove();
    $('#tituloform').text('Añadir Cómic');
    $('#formcomics')[0].reset();
}

function comicactualizar(e) {
    e.preventDefault();

    $.getJSON(this.href, function (comic) {
        $('#editoriales').val(comic.EditorialId);
        $('#inputTitulo').val(comic.Titulo);
        $('#inputFecha').val(comic.Fecha);
        $('#inputAutor').val(comic.Autor);
        $('#inputPrecio').val(comic.Precio);
        $('#inputStars').val(comic.Calificacion);

        $('#tituloform').text('UPDATE comic');

        if ($('#inputId').length)
            $('#inputId').val(comic.Id);
        else
            $('#formcomics').
                append('<input type="hidden" id="inputId" value="' + comic.Id + '" />');
    });
}


function comicborrar(e) {
    e.preventDefault();

    $.ajax({
        url: this.href,
        method: 'DELETE',
        //data: null,
        dataType: 'json',
        contentType: 'application/json'
    }).done(function (comic) {
        console.log(comic);
        $.getJSON(url, ComicOK).fail(fallo);
    }).fail(fallo);
}

function comicdetalle(e) {
    e.preventDefault();

    $.getJSON(this.href, function (comic) {
        $('#AuthorName').text(comic.Author.Name);
        $('#Titulo').text(comic.Titulo);
        $('#Fecha').text(comic.Fecha);
        $('#Autor').text(comic.Autor);
        $('#Precio').text(comic.Precio);
        $('#Stars').text(comic.Calificacion);
    });
}

function fallo(jqXHR, textStatus, errorThrown) {
    if (jqXHR.readyState === 0) {
        errorThrown = "ERROR DE CONEXIÓN";
    }

    console.log(jqXHR, textStatus, errorThrown);

    $('#cuadroerror').show();
    $('#textoerror').text(errorThrown);
}


