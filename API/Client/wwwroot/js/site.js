////const { error } = require("jquery");

//var menu1 = document.getElementById("menu1");
//var menu2 = document.getElementById("menu2");
//var data3 = document.querySelector("section#a p.p2");
//var data4 = document.querySelector("section#a p.p2");


//menu1.addEventListener("click", function () {
//    menu1.style.backgroundColor = "lightgrey";
//    menu2.style.backgroundColor = "white";
//    $(".judul").html("Mamalia");
//    data3.style.backgroundColor = "lightgrey";
//    data4.innerHTML = "Kata mamalia merupakan ciptaan dari Carl Linnaeus. Mamalia berasal dari bahasa latin ‘mamma’ yang memiliki arti puting. Kata ini pertama kali dikemukakan pada tahun 1758. Hewan mamalia dikenal dan disebut sebagai hewan menyusui karena mamalia menyusui anak-anaknya.";
//});

//function baseMenu2() {
//    menu2.style.backgroundColor = "lightgrey";
//    menu1.style.backgroundColor = "white";
//    $(".judul").html("Invertebrata");
//    data4.innerHTML = "Jika kamu gemar konsumsi hidangan laut, disadari atau tidak, mayoritas makanan tersebut kebanyakan berupa hewan invertebrata atau tidak bertulang belakang. Misalnya, kepiting, udang, kerang, juga cumi-cumi.";
//}

////const animals = [
////    { name: 'bimo', species: 'cat', kelas: { name: "mamalia" } },
////    { name: 'budi', species: 'cat', kelas: { name: "mamalia" } },
////    { name: 'nemo', species: 'snail', kelas: { name: "invertebrata" } },
////    { name: 'dori', species: 'cat', kelas: { name: "mamalia" } },
////    { name: 'simba', species: 'snail', kelas: { name: "invertebrata" } }
////]
////console.log("Ini animal awal ", animals);

////let onlyCat = [];
////for (var i = 0; i < animals.length; i++) {
////    if (animals[i].species == 'cat') {
////        onlyCat.push(animals[i]);
////    }
////}
////console.log("Ini animal cat ", onlyCat);

////for (var i = 0; i < animals.length; i++) {
////    if (animals[i].species == 'snail') {
////        animals[i].kelas.name = "non-mamalia";
////    }
////}
////console.log("Ini animal dengan kelas terbaru ", animals);


    $.ajax({
        url: "https://pokeapi.co/api/v2/pokemon"
    }).done((result) => {
        var text = "";
        $.each(result.results, function (key, val) {
            text += `<tr>
                        <th scope="row">${key + 1}</th>
                        <td><button type="button" class="btn btn-info" data-toggle="modal" data-target="#pokeModal" onclick="getDetail('${val.url}')">Info</button></td>
                        <td>${val.name}</td>
                    </tr>`;
        });
        $(".tabelPoke").html(text);
    }).fail((error) => {
        console.log(error);
    });


function getDetail(url) {
    $.ajax({
        url: url
    }).done((result) => {
        console.log(result);
        var detailAbilities = "";
        var detailType = "";
        $.each(result.abilities, function (key, val) {
            detailAbilities += `<span class="badge badge-pill badge-light">${val.ability.name}</span>`;
        });
        $.each(result.types, function (key, val) {
            detailType += typeColor(val.type.name);
        });

        var tbDetailStats1 = "";
        for (var i = 0; i < 3; i++) {
            tbDetailStats1 += `<tr>
                                <th>${result.stats[i].stat.name}</th>
                                <td>:</td>
                                <td>${result.stats[i].base_stat}</td>
                               </tr>`;
        }

        var tbDetailStats2 = "";
        for (var i = 3; i < 6; i++) {
            tbDetailStats2 += `<tr>
                                <th>${result.stats[i].stat.name}</th>
                                <td>:</td>
                                <td>${result.stats[i].base_stat}</td>
                               </tr>`;
        }

        var img = `<img src="${result.sprites.other.dream_world.front_default}" alt="" class="rounded-circle" width="304" height="236" />`;

        $(".detailName").html(result.name);
        $(".detailAbilities").html(detailAbilities);
        $(".detailHeight").html(result.height + " m");
        $(".detailWeight").html(result.weight + " kg");
        $(".detailType").html(detailType);
        $("#imgPoke").html(img);
        $(".tbDetailStats1").html(tbDetailStats1);
        $(".tbDetailStats2").html(tbDetailStats2);

    }).fail((error) => {
        console.log(error);
    });
}

function typeColor(val) {
    if (val == "normal") {
        var color = `<span class="badge badge-pill badge-light">${val}</span>`;
        return color;
    } else if (val == "fighting" || val == "electric") {
        var color = `<span class="badge badge-pill badge-warning">${val}</span>`;
        return color;
    } else if (val == "water" || val == "ice" || val == "flying") {
        var color = `<span class="badge badge-pill badge-primary">${val}</span>`;
        return color;
    } else if (val == "poison" || val == "grass" || val == "fairy") {
        var color = `<span class="badge badge-pill badge-success">${val}</span>`;
        return color;
    } else if (val == "ground" || val == "ghost" || val == "steel" || val == "unknown") {
        var color = `<span class="badge badge-pill badge-secondary">${val}</span>`;
        return color;
    } else if (val == "rock" || val == "dragon" || val == "fire") {
        var color = `<span class="badge badge-pill badge-danger">${val}</span>`;
        return color;
    } else if (val == "bug" || val == "psychic") {
        var color = `<span class="badge badge-pill badge-info">${val}</span>`;
        return color;
    } else if (val == "dark" || val == "shadow") {
        var color = `<span class="badge badge-pill badge-dark">${val}</span>`;
        return color;
    } else {
        var color = `<span class="badge badge-pill badge-light">${val}</span>`;
        return color;
    }
}
