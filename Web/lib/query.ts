import axios, { AxiosRequestConfig } from "axios";
import Auth from "./Auth";

const post = async (url: string, body: any, config?: AxiosRequestConfig) => {
	const authorization = "Bearer " + Auth.getInstance().getUserId();
	return await axios.post(url, body, {
		headers: { Authorization: authorization },
		...config,
	});
};

const get = async (url: string, config?: AxiosRequestConfig) => {
	const authorization = "Bearer " + Auth.getInstance().getUserId();
	return await axios.get(url, {
		headers: { Authorization: authorization },
		...config,
	});
};

export default { post, get };
