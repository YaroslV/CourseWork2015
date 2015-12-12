(function () {
    var app = angular.module('atTutorLectures', ['ngRoute']);

    app.directive('fileModel', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var model = $parse(attrs.fileModel);
                var modelSetter = model.assign;

                element.bind('change', function () {
                    scope.$apply(function() {
                        modelSetter(scope, element[0].files[0]);
                    });
                });
            }
        };        
    }]);

    app.factory('tutorService', ['$http', function ($http) {
        var uploadNewLecture = function (formData) {
            return $http({
                method: 'POST',
                url: '/api/lectures/add',
                data: formData,
                headers: {
                    'Content-Type': undefined
                },
                transformRequest: function (data) {
                    var fd = new FormData();
                    fd.append("subject", data.subject);
                    fd.append("name", data.name);
                    fd.append("file", data.file);

                    return fd;
                }
            });
        };

        var updateLecture = function (formData, lectureId) {
            return $http({
                method: 'PUT',
                url: '/api/lectures/update/' + lectureId,
                data: formData,
                headers: {
                    'Content-Type': undefined
                },
                transformRequest: function (data) {
                    var fd = new FormData();
                    fd.append("subject", data.subject);
                    fd.append("name", data.name);
                    fd.append("file", data.file);

                    return fd;
                }
            });
        };

        var getTutorLectures = function () {
            return $http.get('/api/lectures/all/tutor');
        };

        var deleteLecture = function (lectureId) {
            return $http.delete('/api/lectures/delete/'+ lectureId);
        };

        return {
            uploadLecture:    uploadNewLecture,
            getTutorLectures: getTutorLectures,
            deleteLecture:    deleteLecture,
            updateLecture:    updateLecture
        };

    }]);

    var config = function ($routeProvider) {
        $routeProvider
        .when('/list',
        { templateUrl: '/Scripts/app/views/tutorLectures.html' })
        .when('/add',
        { templateUrl: '/Scripts/app/views/addNewLecture.html' })
        .when('/update/:id',
        { templateUrl: '/Scripts/app/views/updateLecture.html' })
        .otherwise(
        { redirectTo: '/list'});
    };

    app.config(config);

    //*********************************
    //          CONTROLLER
    //*********************************
    app.controller('TutorController', ['$scope', '$location', 'tutorService', function ($scope, $location, tutorService) {
        function checkFile(file) {
            //TODO checkfile
        };

        $scope.publishLecture = function () {            
            
            var dataToUpload = 
            {
                file: $scope.ctrlBag.fileToUpload,
                name: $scope.ctrlBag.lectureName,
                subject: $scope.ctrlBag.lectureSubject
            };
            tutorService.uploadLecture(dataToUpload)
                .success(function (data) {
                    //TODO
                    //file upload congratulations
                })
                .error(function (data) {
                    //TODO
                    //uploading failure alert
                });
        
            $location.path('/list')
        };

        var removeById = function (lecId) {
            for (var i = 0; i < $scope.allTutorLectures.length; i++) {
                if ($scope.allTutorLectures[i].LectureId == lecId) {
                    $scope.allTutorLectures.splice(i, 1);
                    break;
                }
            }
        };

        

        $scope.deleteLecture = function (lectureId) {
            var answer = prompt("Ви дійсно бажаєте видалити лекцію", "Так", "Hі");
            if (answer === "Так") {
                tutorService.deleteLecture(lectureId)
                                .success(function (data) {
                                    //TODO
                                    //
                                    removeById(lectureId);
                                })
                                .error(function (data) {
                                    //TODO
                                    //
                                });
            }
        };

        tutorService.getTutorLectures()
                        .success(function (data) {
                            $scope.allTutorLectures = data;
                        })
                        .error(function (data) {
                            $scope.errormsg = "Проблеми із з'єднанням";
                        });
                        

    }]);

}());