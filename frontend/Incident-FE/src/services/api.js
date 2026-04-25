import axios from "axios";

const API = axios.create({
    baseURL: "https://localhost:7136/api" // change later when deployed
});

// APIs
export const getStats = () => API.get("/dashboard");
export const getIncidents = () => API.get("/incidents");
export const retryIncident = (id) => API.post(`/orders/retry/${id}`);