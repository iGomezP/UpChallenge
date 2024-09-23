/* eslint-disable @typescript-eslint/no-explicit-any */
import axios from 'axios';
import Cookies from 'js-cookie';

const API_URL = "https://localhost:7158/api";

const upBackApi = axios.create({
    baseURL: API_URL
});

const axiosAdditionalConfig = {
    'Content-Type': 'application/json;',
    "accept": "*",
};

const addJwtInterceptor = async (instance: any, additionalConfig = {}) => {
    instance.interceptors.request.use(
        async (config: any) => {
            try {
                const token = Cookies.get('jwt');
                if (token) {
                    config.headers.Authorization = `Bearer ${token}`;
                }
                config.headers = {
                    ...config.headers,
                    ...additionalConfig
                };
                return config;
            } catch (error) {
                console.log('Error en la configuración de la petición:', error);
                return Promise.reject(error);
            }
        },
        (error: any) => {
            console.log('Error en la petición interceptado por JwtHandler: ', error)
            window.location.replace("/");
            return Promise.reject(error);
        }
    );
};

addJwtInterceptor(upBackApi, axiosAdditionalConfig);

export default upBackApi;