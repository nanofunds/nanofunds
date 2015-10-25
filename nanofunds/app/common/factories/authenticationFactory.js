angular.module('nano').factory('authenticationFactory', ['$localStorage',
	function($localStorage){

		var factory = {};

	    factory.setMerchantId = function(mId) {
	        $localStorage.mId = mId;
	    };

        factory.getMerchantId = function() {
            return $localStorage.mId;
        };

		factory.storeUser = function(user) {
		    $localStorage.user = user;
		};

		factory.getUser  = function() {
		    return $localStorage.user;
		};

		return factory;
	}
]);
