var siteListApp = angular.module('siteListApp', ['ngSanitize']);

siteListApp.controller('SiteListController', function ($scope) {
    'use strict';
    $scope.header = 'Local Legend Sites';
    $scope.sites = [
        {
            index: 'V1',
            description: 'Feature <a href="https://redmine.nexcel.vn/issues/20134" target="_blank">20134</a> - Support direct member for any agent level.',
            active: true
        },
        {
            index: 'V2',
            description: 'Feature <a href="https://redmine.nexcel.vn/issues/20134" target="_blank">20134</a> - Support direct member for any agent level.',
            active: false
        },
        {
            index: 'V3',
            description: 'Feature <a href="https://redmine.nexcel.vn/issues/20134" target="_blank">20134</a> - Support direct member for any agent level.',
            active: false
        },
        {
            index: 'V4',
            description: 'Feature <a href="https://redmine.nexcel.vn/issues/20134" target="_blank">20134</a> - Support direct member for any agent level.',
            active: false
        },
        {
            index: 'V5',
            description: 'Feature <a href="https://redmine.nexcel.vn/issues/20134" target="_blank">20134</a> - Support direct member for any agent level.',
            active: false
        }
    ];
});