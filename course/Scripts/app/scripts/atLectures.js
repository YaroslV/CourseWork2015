(function () {
    var app = angular.module("atLectures", ['ngRoute']);

    app.factory('lectureService',[ '$http', function ($http) {
        var getLectures = function (page, items, orderby) {
            return $http.get('/api/test/' + page + '/' + items + '/' + orderby);
        };

        var getAllDisciplines = function () {
            return $http.get('/api/lectures/disciplines');
        }

        var getTutorRequests = function () {
            return $http.get('/api/lectures/tutorrequests');
        }

        var activateTutor = function (id) {
            return $http.post('/api/lectures/tutoractivation', {"TutorId":id});            
        }

       

        return {
            getLectures: getLectures,
            getAllDisciplines: getAllDisciplines,
            getTutorRequests: getTutorRequests,
            activateTutor: activateTutor
           
        };
    }]);

}());