var App = angular.module('App', ['ngRoute','ngFileUpload']).config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/Syllabus', {
        templateUrl: '/SPA/Syllabus/Syllabus.html',
        controller: 'SyllabusController'
    }).otherwise({ redirectTo: '/Syllabus' })
    $locationProvider.html5Mode(true);
}).controller('DefaultController', function ($scope, $http) {
    
});