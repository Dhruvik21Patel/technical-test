import axios from "axios";
import type {
  AxiosInstance,
  AxiosResponse,
  AxiosError,
  InternalAxiosRequestConfig,
} from "axios";
import { toast } from "react-toastify";

const axiosInstance: AxiosInstance = axios.create({
  baseURL: "https://localhost:7261/api/",
  headers: {
    "Content-Type": "application/json",
  },
  withCredentials: true,
});

/* =========================
   REQUEST INTERCEPTOR
========================= */
axiosInstance.interceptors.request.use(
  (config: InternalAxiosRequestConfig) => {
    const token = localStorage.getItem("token");

    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error: AxiosError) => Promise.reject(error)
);

/* =========================
   RESPONSE INTERCEPTOR
========================= */
axiosInstance.interceptors.response.use(
  (response: AxiosResponse) => {
    if (["post", "put", "delete"].includes(response.config.method || "")) {
      const msg = (response.data as any)?.message || "Operation successful";
      if (msg !== "Success") toast.success(msg);
    }

    return response;
  },
  (error: AxiosError) => {
    const message =
      (error.response?.data as any)?.messages || "Something went wrong";

    if (Array.isArray(message)) {
      toast.error(message.join(" "));
    } else if (typeof message === "string") {
      toast.error(message);
    } else {
      toast.error("Something went wrong");
    }

    if (error.response?.status === 401) {
      localStorage.clear();
      toast.warning("Session expired. Please login again.");

      setTimeout(() => {
        window.location.href = "/login";
      }, 1000);
    }

    return Promise.reject(error);
  }
);

export default axiosInstance;
