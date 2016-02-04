'use strict';
(function() {
    angular.module('angularServiceDashboard', []).
        value('backendServerUrl', '../../')
        .factory('backendHubProxy', [
            '$rootScope', 'backendServerUrl',
            function($rootScope, backendServerUrl) {

                function backendFactory(serverUrl, hubName) {
                    var connection = $.hubConnection(backendServerUrl);
                    var proxy = connection.createHubProxy(hubName);

                    connection.start().done(function() {});

                    return {
                        on: function(eventName, callback) {
                            proxy.on(eventName, function(result) {
                                $rootScope.$apply(function() {
                                    if (callback) {
                                        callback(result);
                                    }
                                });
                            });
                        },
                        invoke: function(methodName, callback) {
                            proxy.invoke(methodName)
                                .done(function(result) {
                                    $rootScope.$apply(function() {
                                        if (callback) {
                                            callback(result);
                                        }
                                    });
                                });
                        }
                    };
                };

                return backendFactory;
            }
        ])
        .controller('LogController', [
            '$scope', 'backendHubProxy',
            function($scope, backendHubProxy) {
                console.log('trying to connect to service');
                var performanceDataHub = backendHubProxy(backendHubProxy.defaultServer, 'logHub');
                console.log('connected to service');
                $scope.logs = [];

                performanceDataHub.on('broadcastLogEvent', function(data) {
                    var maxLength = 100;
                    $scope.logs.push(data);
                    if ($scope.logs.length > maxLength)
                        $scope.logs.slice(Math.max($scope.logs.length - 5, maxLength));
                });
            }
        ]);
})();




