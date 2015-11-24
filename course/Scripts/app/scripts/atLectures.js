(function () {
    var app = angular.module("atLectures", ['ngRoute']);

    app.factory('lectureService',[ '$http', function ($http) {
        var getLectures = function (page, items, orderby) {
            return $http.get('/api/test/' + page + '/' + items + '/' + orderby);
        };

        var getAllDisciplines = function () {
            return $http.get('/api/lectures/disciplines');
        }


        return {
            getLectures: getLectures,
            getAllDisciplines: getAllDisciplines
        };
    }]);

}());