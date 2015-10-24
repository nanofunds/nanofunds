angular.module('nano').factory('authenticationFactory', ['$localStorage',
	function($localStorage){

		function tokenExists(){
			if (typeof $localStorage.accessToken === 'undefined') { return false; }
			return true;
		}

		var factory = {};

		factory.storeToken = function(data) {
		 	$localStorage.$default({
				accessToken: data.access_token,
				refreshToken: data.refresh_token,
				tokenType: data.token_type,
			});
		};

		factory.deleteToken = function(){
			delete $localStorage.accessToken;
			delete $localStorage.refreshToken;
			delete $localStorage.tokenType;
			delete $localStorage.expirationDate;
			delete $localStorage.userProfile;
		};

		factory.logout = function() {
			this.deleteToken();
			this.deleteRouteTracking();
		};

		factory.isLoggedIn = function() {
			return tokenExists();
		};

		factory.setUserProfile = function(userProfile){
			$localStorage.userProfile = userProfile;
		};

		factory.getUserProfile  = function(){
			var userProfile = $localStorage.userProfile;
			return userProfile;
		};

		factory.getAccessToken = function() {
			if (!tokenExists()) return '';
			return $localStorage.accessToken;
		};

		factory.getRefreshToken = function() {
			if (!tokenExists()) return '';
			return $localStorage.refreshToken;
		};

		factory.getTokenType = function() {
			if (!tokenExists()) return '';
			return $localStorage.tokenType;
		};

		return factory;
	}
]);
