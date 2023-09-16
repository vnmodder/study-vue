// service.js

import axios from 'axios';

const API_BASE_URL = 'https://dumy-api.keysee.click/api'; // Thay đổi URL theo ứng dụng của bạn

export default {
    async get(endpoint) {
        try {
            const response = await axios.get(`${API_BASE_URL}/${endpoint}`);
            return response;
        } catch (error) {
            console.log(error);
            return error.response;
        }
    },

    async post(endpoint, data) {
        try {
            const response = await axios.post(`${API_BASE_URL}/${endpoint}`, data);
            return response;
        } catch (error) {
            console.log(error);
            return error.response;
        }
    },

    async put(endpoint, data) {
        try {
            const response = await axios.put(`${API_BASE_URL}/${endpoint}`, data);
            return response;
        } catch (error) {
            console.log(error);
            return error.response;
        }
    },

    async delete(endpoint) {
        try {
            const response = await axios.delete(`${API_BASE_URL}/${endpoint}`);
            return response;
        } catch (error) {
            console.log(error);
            return error.response;
        }
    },
};
