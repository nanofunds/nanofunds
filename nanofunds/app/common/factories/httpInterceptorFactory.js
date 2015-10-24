angular.module('nano').factory('httpInterceptorFactory', ['$q', '$injector', '$location',
	function($q, $injector, $location) {
		var factory = {};
		var refreshRequest = null;

//		factory.request = function(config) {
//			//var token = authenticationFactory.getAccessToken();
//			//var tokenType = authenticationFactory.getTokenType();
//
//			config.headers.Authorization = tokenType + ' ' + token;
//			return config;
//		};

		factory.responseError = function(rejection) {
			switch(rejection.status) {
				case 401:
				    console.log(rejection.data);
					break;
				case 400:
					return $q.reject(rejection);
				case 404:
				    console.log(rejection.data);
					break;
				case 500:
				    console.log(rejection.data);
					break;
			}

			return $q.reject(rejection);
		};

		return factory;
	}
]);
