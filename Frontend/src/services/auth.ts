import axios from "../api/axiosInstance";
import type { IAPIResponse } from "../interfaces/api-response.interface";

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  
    token: string;
    userName: string;
    userId: number;
  };


export const loginUser = async (
  payload: LoginRequest
): Promise<IAPIResponse<LoginResponse>> => {
  const response = await axios.post<IAPIResponse<LoginResponse>>(
    "/auth/login",
    payload
  );
  return response.data;
};
