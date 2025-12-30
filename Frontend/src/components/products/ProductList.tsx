import { useEffect, useState } from "react";
import { getProducts, deleteProduct } from "../../services/product";
import Pagination from "../common/Pagination";
import Loader from "../common/Loader";
import ProductTable from "./ProductTable";
import { useDebounce } from "../../hooks/debounceHook";
import type { Category, Product } from "../../interfaces/product.interface";
import ProductFormDialog from "./ProductForm";
import ConfirmDialog from "../common/ConfirmDialog";
import { getCategories } from "../../services/category";

const ProductList = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [open, setOpen] = useState(false);
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [totalRecords, setTotalRecords] = useState(0);
  const [search, setSearch] = useState("");
  const [loading, setLoading] = useState(false);
  const [editProduct, setEditProduct] = useState<Product | undefined>(
    undefined
  );
  const [deleteId, setDeleteId] = useState<number | null>(null);

  const debouncedSearch = useDebounce(search, 500);

  const loadProducts = async () => {
    try {
      setLoading(true);

      const res = await getProducts({
        pageNumber: page,
        pageSize: 10,
        searchKey: debouncedSearch,
      });
      setEditProduct(undefined);
      setProducts(res.data.result);
      setTotalPages(res.data.totalPage);
      setTotalRecords(res.data.totalRecords);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadProducts();
  }, [page, debouncedSearch]);

  const loadCategories = async () => {
    try {
      setLoading(true);

      const res = await getCategories();
      setCategories(res.data);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadCategories();
  }, []);

  const handleDelete = async () => {
    if (!deleteId) return;

    await deleteProduct(deleteId);
    setDeleteId(null);
    loadProducts();
  };

  const handleEdit = (id: number) => {
    setEditProduct(products.find((p) => p.id === id) as Product);
    setOpen(true);
  };

  const handleAdd = () => {
    setEditProduct(undefined);
    setOpen(true);
  };

  const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
    setPage(1);
  };

  return (
    <>
      <div className="flex justify-between">
        <h2 className="font-bold text-4xl pl-4.5 py-2.5 pb-0">Products</h2>
        <button
          className="mt-2 mr-6 !px-4 !py-2 !bg-blue-600 text-white rounded"
          onClick={() => handleAdd()}
        >
          Add
        </button>
      </div>
      <div className="p-6">
        {/* Search */}
        <div className="mb-4">
          <input
            type="text"
            placeholder="Search product..."
            value={search}
            onChange={(e) => handleSearch(e)}
            className="w-full md:w-1/3 p-2 border rounded"
          />
        </div>

        {loading && <Loader />}

        <ProductTable
          products={products}
          onEdit={(id) => handleEdit(id)}
          onDelete={setDeleteId}
        />

        <Pagination
          pageNumber={page}
          totalPage={totalPages}
          totalRecords={totalRecords}
          onPageChange={setPage}
        />

        <ProductFormDialog
          isOpen={open}
          onClose={() => setOpen(false)}
          onSuccess={loadProducts}
          product={editProduct}
          categories={categories}
        />
      </div>
      <ConfirmDialog
        isOpen={!!deleteId}
        title="Delete Product"
        message="Are you sure you want to delete this product?"
        onCancel={() => setDeleteId(null)}
        onConfirm={handleDelete}
      />
    </>
  );
};

export default ProductList;
