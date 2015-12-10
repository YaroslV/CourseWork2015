(function () {
    var app = angular.module("atLectures", ['ngRoute']);

    app.factory('lectureService',[ '$http', function ($http) {
        var getLectures = function (page, items, orderby) {
            return $http.get('/api/lectures/all');
        };

        var getAllDisciplines = function () {
            return $http.get('/api/lectures/disciplines');
        };

        var getTutorRequests = function () {
            return $http.get('/api/lectures/tutorrequests');
        };

        var activateTutor = function (id) {
            return $http.post('/api/lectures/tutoractivation', {"TutorId":id});            
        };

        var downloadFile = function (fileId) {
            return $http.get('/api/lectures/getfile/'+ fileId);
        };

       

        return {
            getLectures: getLectures,
            getAllDisciplines: getAllDisciplines,
            getTutorRequests: getTutorRequests,
            activateTutor: activateTutor,
            getFile: downloadFile
           
        };
    }]);

    var config = function ($routeProvider) {
        $routeProvider
        .when('/listall', {
            templateUrl: '/Scripts/app/views/studentLectures.html'
        })
        .when('/more/:id', {
            templateUrl: '/Scripts/app/views/moreAboutLecture.html'
        })
        .otherwise(
            {redirectTo: '/listall'}
        );
    };

    app.config(config);

}());