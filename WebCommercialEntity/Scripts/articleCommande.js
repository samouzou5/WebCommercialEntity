    $(document).ready(function () {
        $('#augmentation').on('click', function (e) {
            var valeur = $(this).val();
            if (isNan(parseInt(valeur)) || valeur.length === 0) {
                alert("Veuillez saisir une valeur valide");
                e.preventDefault();

            }
        });
    });