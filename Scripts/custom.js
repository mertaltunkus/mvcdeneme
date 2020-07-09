$(document).ready(function () {
    $('#tblTekDers').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        }
    });
});

//$('.datepicker').datepicker({

//    orientation: left,
//    clearBtn: true,
//    autoclose: true,
//    format: 'dd/MM/yyyy'
//})






$(function () {
    $("#tbl").on("click", ".btnOgrenciSil", function () {
        if (confirm("Silmek istediğinize emin misiniz?")) {
            var id = $(this).data("id");
            var btn = $(this);
            $.ajax({
                type: "GET",
                url: "/Ogrenci/Delete/" + id,
                success: function () {
                    btn.parent().parent().remove();
                }
            });
        }

    });
});

