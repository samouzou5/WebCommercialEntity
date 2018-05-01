    $(document).ready(function () {
        $('#augmentation').on('click', function (e) {
            var valeur = $("#pourcentage").val();
            if (isNaN(parseInt(valeur)) || valeur.length === 0) {
                alert("Veuillez saisir une valeur valide");
                e.preventDefault();

            }
        });

        $("a[id ^=deleteCommand]").on('click', function (e) {
            if (confirm("Voulez-vous vraiment supprimer cette commande ?")) {
                console.log('Form is submitting');
            } else {
                alert('Suppression annulée');
                e.preventDefault();
            }
        });

        $('#DATE_CDE').datepicker({
            format: 'dd/mm/yyyy',
            language: 'fr'
        });

        $('#DATE_CMDE').datepicker({
            format: 'dd/mm/yyyy',
            language: 'fr'
        });

        $('#noCommand').on('change', function () {
            var numCommande = $(this).val().toString();
            var regex = /^[0-9]{6}$/;
            var result = regex.test(numCommande);
            if (result) {
                $("#affichage").html("<p style='color:green'>Votre id commande est valide</p>");
            } else {
                $("#affichage").html("<p style='color:red'>Votre id commande est invalide il faut 6 chiffres dans votre id</p>");
            }
        });

        $("#addCommand").on('click', function (e) {
            var idCommande = $("#noCommand").val();
            var regex = /^[0-9]{6}$/;
            var resultTest = regex.test(idCommande);
            if (!resultTest) {
                alert("L'id commande n'est pas valide, il faut 6 chiffres !");
                e.preventDefault();
            }

        });
    });