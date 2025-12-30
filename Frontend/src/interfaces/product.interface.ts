export interface Category {
  id: number;
  name: string;
}

export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  stockQuantity: number;
  categories: Category[];
}

export interface ProductCreateRequest {
  name: string;
  description: string;
  price: number;
  stockQuantity: number;
  categoryIds: number[];
}
