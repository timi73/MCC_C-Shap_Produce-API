let dataEmployee = [];
let dataUniversity = [];
$.ajax({
    type: 'GET',
    url: 'https://localhost:44365/api/Universities/UniversityCount',
    contentType: 'application/json;charset=utf-8',
    dataType: 'json',
    success: function (result) {
        $.each(result.result, function (key, val) {
            dataEmployee.push(val.count);
            dataUniversity.push(val.universityName);
        });
        console.log(dataEmployee);
        createBar();
    },
    error: function (jqXHR, textStatus, errorThrown) {
        console.log(jqXHR);
    }
})

function createBar() {
    var options = {
        series: [{
            data: dataEmployee
        }],
        chart: {
            type: 'bar',
            height: 350
        },
        plotOptions: {
            bar: {
                borderRadius: 4,
                horizontal: false,
            }
        },
        dataLabels: {
            enabled: false
        },
        xaxis: {
            categories: dataUniversity,
        }
    };

    var chart = new ApexCharts(document.querySelector("#univChart"), options);
    chart.render();
}


let dataGender = [];
let dataGenderName = ['Male', 'Female'];

$.ajax({
    url: "https://localhost:44365/api/Employees"
}).done((result) => {
    var male = result.filter((g) => {
        return g.gender == 0;
    });
    var female = result.filter((g) => {
        return g.gender == 1;
    });
    dataGender.push(male.length, female.length);
    console.log("dataGender ", dataGender);
    console.log("dataGenderName ", dataGenderName);
    createDonut();
}).fail((error) => {
    console.log(error);
});

function createDonut() {
    var options = {
        series: dataGender,
        chart: {
            type: 'donut',
        },
        labels: dataGenderName,
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    width: 200
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    };

    var chart = new ApexCharts(document.querySelector("#genderChart"), options);
    chart.render();
}

//function ChartPie(male, female) {
//    var options = {
//        series: [male, female],
//        chart: {
//            width: 380,
//            type: 'pie'
//        },
//        labels: ['Laki-laki', 'Perempuan'],
//        colors: ['#F44336', '#FFC0CB'],
//        responsive: [{
//            breakpoint: 480,
//            options: {
//                chart: {
//                    width: 200
//                },
//                legend: {
//                    position: 'bottom'
//                }
//            }
//        }]
//    };

//    var chart = new ApexCharts(document.querySelector("#genderChart"), options);
//    var render = chart.render();
//    //console.log(render);
//    document.getElementById("myPieTitle").innerHTML = "Statistik Karyawan Berdasarkan Gender";
//}