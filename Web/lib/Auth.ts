import auth0 from "auth0-js";
import axios from "axios";
import jwt_decode from "jwt-decode";
import Router from "next/router";
import { AUTH_CONFIG } from "./auth0-variables";

export default class Auth {
	public static getInstance(): Auth {
		return Auth.instance;
	}

	protected static instance = new Auth();

	tokenRenewalTimeout: any;

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

	getUserId(): string | undefined | null {
		if (
			typeof window === "undefined" ||
			window === undefined ||
			!window.localStorage ||
			!localStorage
		) {
			return null;
		}

		const cookie = localStorage.getItem("id_token");
		if (cookie === null) {
			return undefined;
		}

		const token: { sub: string } = jwt_decode(cookie);
		return token.sub;
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
				const token: { sub: string } = jwt_decode(authResult.idToken);
				Router.push(`/user?id=${token.sub}`, `/user`);
			} else if (err) {
				Router.push("/user");
				alert(`Error: ${err.error}. Check the console for further details.`);
			}
		});
	}

	setSession(authResult: any) {
		if (
			typeof window === "undefined" ||
			window === undefined ||
			!window.localStorage ||
			!localStorage
		) {
			return;
		}
		// Set the time that the access token will expire at
		const expiresAt = JSON.stringify(
			authResult.expiresIn * 1000 + new Date().getTime()
		);

		localStorage.setItem("access_token", authResult.accessToken);
		localStorage.setItem("id_token", authResult.idToken);
		localStorage.setItem("expires_at", expiresAt);

		// schedule a token renewal
		this.scheduleRenewal();

		// navigate to the home route
		Router.push("/user");
	}

	getAccessToken() {
		if (
			typeof window === "undefined" ||
			window === undefined ||
			!window.localStorage ||
			!localStorage
		) {
			return;
		}

		const accessToken = localStorage.getItem("access_token");
		if (!accessToken) {
			throw new Error("No access token found");
		}
		return accessToken;
	}

	getUserToken() {
		if (
			typeof window === "undefined" ||
			window === undefined ||
			!window.localStorage ||
			!localStorage
		) {
			return;
		}

		return localStorage.getItem("id_token");
	}

	logout() {
		if (
			typeof window === "undefined" ||
			window === undefined ||
			!window.localStorage ||
			!localStorage
		) {
			return;
		}

		// Clear access token and ID token from local storage
		localStorage.removeItem("access_token");
		localStorage.removeItem("id_token");
		localStorage.removeItem("expires_at");
		localStorage.removeItem("scopes");
		clearTimeout(this.tokenRenewalTimeout);
		// navigate to the home route
		Router.push("/");
	}

	isAuthenticated() {
		if (
			typeof window === "undefined" ||
			window === undefined ||
			!window.localStorage ||
			!localStorage
		) {
			return false;
		}

		// Check whether the current time is past the
		// access token's expiry time
		const exp = localStorage.getItem("expires_at");
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
		if (
			typeof window === "undefined" ||
			window === undefined ||
			!window.localStorage ||
			!localStorage
		) {
			return;
		}

		const exp = localStorage.getItem("expires_at");
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
