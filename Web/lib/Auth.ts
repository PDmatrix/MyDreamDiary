import auth0 from "auth0-js";
import axios from "axios";
import Router from "next/router";
import Cookies from "universal-cookie";
import { AUTH_CONFIG } from "./auth0-variables";

export default class Auth {
	public static getInstance(): Auth {
		return Auth.instance;
	}

	protected static instance = new Auth();

	cookies = new Cookies();
	tokenRenewalTimeout;

	auth0 = new auth0.WebAuth({
		clientID: AUTH_CONFIG.clientId,
		domain: AUTH_CONFIG.domain,
		redirectUri: AUTH_CONFIG.callbackUrl,
		responseType: "token id_token",
		scope: "openid profile email",
	});

	private constructor() {
		this.login = this.login.bind(this);
		this.logout = this.logout.bind(this);
		this.handleAuthentication = this.handleAuthentication.bind(this);
		this.isAuthenticated = this.isAuthenticated.bind(this);
		this.getAccessToken = this.getAccessToken.bind(this);
	}

	setCookie(cookie) {
		this.cookies = cookie;
	}

	login() {
		this.auth0.authorize();
	}

	handleAuthentication() {
		this.auth0.parseHash((err, authResult) => {
			if (authResult && authResult.accessToken && authResult.idToken) {
				this.setSession(authResult);
				axios.post(
					`http://localhost:5000/api/user`,
					{},
					{
						headers: { Authorization: `Bearer ${authResult.idToken}` },
					}
				);
				Router.push("/user");
			} else if (err) {
				Router.push("/user");
				alert(`Error: ${err.error}. Check the console for further details.`);
			}
		});
	}

	setSession(authResult) {
		// Set the time that the access token will expire at
		const expiresAt = JSON.stringify(
			authResult.expiresIn * 1000 + new Date().getTime()
		);

		this.cookies.set("access_token", authResult.accessToken, { path: "/" });
		this.cookies.set("id_token", authResult.idToken, { path: "/" });
		this.cookies.set("expires_at", expiresAt, { path: "/" });

		// schedule a token renewal
		this.scheduleRenewal();

		// navigate to the home route
		Router.push("/user");
	}

	getAccessToken() {
		const accessToken = this.cookies.get("access_token");
		if (!accessToken) {
			throw new Error("No access token found");
		}
		return accessToken;
	}

	getUserId() {
		return this.cookies.get("id_token");
	}

	logout() {
		// Clear access token and ID token from local storage
		this.cookies.remove("access_token");
		this.cookies.remove("id_token");
		this.cookies.remove("expires_at");
		this.cookies.remove("scopes");
		clearTimeout(this.tokenRenewalTimeout);
		// navigate to the home route
		Router.push("/");
	}

	isAuthenticated() {
		// Check whether the current time is past the
		// access token's expiry time
		const exp = this.cookies.get("expires_at");
		if (exp === undefined) {
			return false;
		}
		const expiresAt = JSON.parse(exp);
		return new Date().getTime() < expiresAt;
	}

	renewToken() {
		this.auth0.checkSession({}, (err, result) => {
			if (err) {
				alert(
					`Could not get a new token (${err.error}: ${err.errorDescription}).`
				);
			} else {
				this.setSession(result);
				alert(`Successfully renewed auth!`);
			}
		});
	}

	scheduleRenewal() {
		const exp = this.cookies.get("expires_at");
		if (exp === undefined) {
			return;
		}
		const expiresAt = JSON.parse(exp);
		const delay = expiresAt - Date.now();
		if (delay > 0) {
			this.tokenRenewalTimeout = setTimeout(() => {
				this.renewToken();
			}, delay);
		}
	}
}
