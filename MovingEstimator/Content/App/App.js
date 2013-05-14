var ballPark = angular.module("ballPark", []).config(function ($routeProvider) {
    $routeProvider.when("/", {
        templateUrl: "/Content/App/views/prices.html",
        controller: pricesController
    });
});

pricesController = function ($scope) {
    $scope.prices = [
        {
            ID: 1,
            FromLocationID: "1",
            FromLocation: "Whangerei",
            ToLocationID: 2,
            ToLocation: "Auckland",
            OneBdrm: "100",
            ThreeBdrm: "300",
            FiveBdrm: "500"
        },
        {
            ID: 2,
            FromLocationID: "3",
            FromLocation: "Snells beach",
            ToLocationID: 4,
            ToLocation: "Te Puke",
            OneBdrm: "500",
            ThreeBdrm: "600",
            FiveBdrm: "700"
        },
        {
            ID: 3,
            FromLocationID: "5",
            FromLocation: "Rotorua",
            ToLocationID: 6,
            ToLocation: "Warkworth",
            OneBdrm: "800",
            ThreeBdrm: "300",
            FiveBdrm: "100"
        }
    ];
}