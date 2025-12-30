export interface IAPIResponse<T> {
    code: number;
  message: string;
  isValid: boolean;
  isSuccessStatusCode: boolean;
  data: T;
}

export interface PaginationResult<T> {
  result: T[];
  pageNumber: number;
  pageSize: number;
  totalPage: number;
  totalRecords: number;
}

export type PaginatedApiResponse<T> = IAPIResponse<PaginationResult<T>>;


export interface PageRequest {
  pageNumber: number;
  pageSize: number;
  searchKey?: string;
}
