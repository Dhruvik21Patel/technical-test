import axios from "../api/axiosInstance";
import type {
  IAPIResponse,
} from "../interfaces/api-response.interface";
import type {
    Category} from "../interfaces/product.interface";

const CATEGORY_API_BASE = "category";

export const getCategories = async (
): Promise<IAPIResponse<Category>> => {
  const response = await axios.get<IAPIResponse<Category>>(CATEGORY_API_BASE
  );
  return response.data;
};

