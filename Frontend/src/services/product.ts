import axios from "../api/axiosInstance";
import type {
  IAPIResponse,
  PageRequest,
  PaginatedApiResponse,
} from "../interfaces/api-response.interface";
import type {
  Product,
  ProductCreateRequest,
} from "../interfaces/product.interface";

const PRODUCT_API_BASE = "products";

export const getProducts = async (
  payload: PageRequest
): Promise<PaginatedApiResponse<Product>> => {
  const response = await axios.post<PaginatedApiResponse<Product>>(
    `${PRODUCT_API_BASE}/get-all`,
    payload
  );
  return response.data;
};

export const getProductById = async (
  id: number
): Promise<IAPIResponse<Product>> => {
  const response = await axios.get<IAPIResponse<Product>>(
    `${PRODUCT_API_BASE}/${id}`
  );
  return response.data;
};

export const createProduct = async (
  data: ProductCreateRequest
): Promise<IAPIResponse<string>> => {
  const response = await axios.post<IAPIResponse<string>>(
    `${PRODUCT_API_BASE}`,
    data
  );
  return response.data;
};

export const updateProduct = async (
  id: number,
  data: ProductCreateRequest
): Promise<IAPIResponse<string>> => {
  const response = await axios.put<IAPIResponse<string>>(
    `${PRODUCT_API_BASE}/${id}`,
    data
  );
  return response.data;
};

export const deleteProduct = async (
  id: number
): Promise<IAPIResponse<string>> => {
  const response = await axios.delete<IAPIResponse<string>>(
    `${PRODUCT_API_BASE}/${id}`
  );
  return response.data;
};
