'use strict';
(function(){
    angular.module('angularServiceDashboard', []).
    value('backendServerUrl', 'http://localhost:5000')
    .factory('backendHubProxy', ['$rootScope', 'backendServerUrl', 
    function ($rootScope, backendServerUrl) {

        function backendFactory(serverUrl, hubName) {
        var connection = $.hubConnection(backendServerUrl);
        var proxy = connection.createHubProxy(hubName);

        connection.start().done(function () { });

        return {
            on: function (eventName, callback) {
                proxy.on(eventName, function (result) {
                    $rootScope.$apply(function () {
                    if (callback) {
                        callback(result);
                    }
                    });
                });
                },
            invoke: function (methodName, callback) {
                    proxy.invoke(methodName)
                    .done(function (result) {
                        $rootScope.$apply(function () {
                        if (callback) {
                            callback(result);
                        }
                        });
                    });
                    }
        };
        };

        return backendFactory;
    }])
    .controller('LogController', ['$scope', 'backendHubProxy',
        function ($scope, backendHubProxy) {
            console.log('trying to connect to service')
            var performanceDataHub = backendHubProxy(backendHubProxy.defaultServer, 'logHub');
            console.log('connected to service')
            $scope.logs = [];

            performanceDataHub.on('broadcastLogEvent', function (data) {
                $scope.logs.push(data); 
            });
        }
        ]);
})()




