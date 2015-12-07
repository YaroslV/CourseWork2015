(function (app) {
    app.controller('aboutLectureController', ['$routeParams','$scope' ,function ($routeParams,$scope) {
        var id = $routeParams.id;
        $scope.bag = { 'id': id };
    }]);

}(angular.module('atLectures')));