import axios, { AxiosRequestConfig } from "axios";
import Auth from "./Auth";

const post = async (url: string, body: any, config?: AxiosRequestConfig) => {
  const authorization = "Bearer " + Auth.getUserToken();
  return axios.post(url, body, {
    headers: { Authorization: authorization },
    ...config
  });
};

const get = async (url: string, config?: AxiosRequestConfig) => {
  const authorization = "Bearer " + Auth.getUserToken();
  return axios.get(url, {
    headers: { Authorization: authorization },
    ...config
  });
};

const del = async (url: string, config?: AxiosRequestConfig) => {
  const authorization = "Bearer " + Auth.getUserToken();
  return axios.delete(url, {
    headers: { Authorization: authorization },
    ...config
  });
};

export default { post, get, del };
