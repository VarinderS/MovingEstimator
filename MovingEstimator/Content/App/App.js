var ballPark = angular.module("ballPark", ["ngResource", "ngSanitize"]).config(function ($routeProvider) {
	$routeProvider.when("/", {
		templateUrl: "/Content/App/views/prices.html",
		controller: pricesController
	});
}).filter("highlight", function() {
	return function(text, query, caseSensitive) {
		text = text.toString();
		if (query || angular.isNumber(query)) {
			if (caseSensitive) {
				text = text.split(query).join("<mark>" + query + "</mark>");
			} else {
				text = text.replace(new RegExp(query, 'gi'), '<mark>$&</mark>');
			}
			//return "found some query"
		}
		return text;
	}
});

ballPark.factory("server", function($resource) {
	return $resource('/api/prices.json/:id', { id: '@id' }, { update: { method: 'PUT' } });
});

pricesController = function ($scope, $filter, server) {
	$scope.prices = server.query();

	$scope.reverse = false;
	$scope.predicate = "ID";
	//$scope.sortCol = "FromLocation";

	$scope.sortBy = function(field) {
		if ($scope.predicate == field) {
			$scope.reverse = !$scope.reverse;
		} else {
			$scope.predicate = field;
		}
	}

	$scope.display = function(content) {
		var highlight = $filter("highlight");
		return highlight(content, $scope.search);
	}
}