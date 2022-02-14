var url = 'https://localhost:44357/Panel/GetRoles';

$.getJSON(url, function (response) {
    console.log(response);
    var labelDonutChart = response.map(item => item.name)
        .filter((value, index, self) => self.indexOf(value) === index);
    var dataDonutChart = response.map(item => item.user.length);
    console.log(dataDonutChart);
    console.log(labelDonutChart);
    var options = {
        series: dataDonutChart,
        chart: {
            type: 'donut',
            height: 350
        },
        labels: labelDonutChart
    };

    var chart = new ApexCharts(document.querySelector("#chart"), options);
    chart.render();
});

//var url = 'https://localhost:44381/api/statuscode';
//$.getJSON(url, function (response) {
//    console.log(response);
//    var labelDonutChart = response.map(item => item.name)
//        .filter((value, index, self) => self.indexOf(value) === index);
//    var dataDonutChart = response.map(item => item.role.length);
//    console.log(dataDonutChart);
//    console.log(labelDonutChart);
//    var options = {
//        series: dataDonutChart,
//        chart: {
//            type: 'donut',
//            height: 350
//        },
//        labels: labelDonutChart
//    };

//    var chart = new ApexCharts(document.querySelector("#chart"), options);
//    chart.render();
//});