import type { Product } from "../../interfaces/product.interface";

interface Props {
  products: Product[];
  onEdit: (id: number) => void;
  onDelete: (id: number) => void;
}

const ProductTable = ({ products, onEdit, onDelete }: Props) => {
  return (
    <div className="overflow-x-auto">
      <table className="min-w-full border border-gray-300">
        <thead className="bg-gray-100">
          <tr>
            <th className="border p-2 text-left">Name</th>
            <th className="border p-2 text-left">Description</th>
            <th className="border p-2 text-left">Category</th>
            <th className="border p-2 text-left">Price</th>
            <th className="border p-2 text-left">Actions</th>
          </tr>
        </thead>

        <tbody>
          {products.length === 0 && (
            <tr>
              <td colSpan={5} className="text-center p-4">
                No products found
              </td>
            </tr>
          )}

          {products.map((p) => (
            <tr key={p.id} className="hover:bg-gray-50">
              <td className="border p-2">{p.name}</td>
              <td className="border p-2">{p.description}</td>
              <td className="border p-2">₹{p.price}</td>
              <td className="border p-2">
                {p.categories.length > 0
                  ? p.categories.map((c) => c.name).join(", ")
                  : "N/A"}
              </td>
              <td className="border p-2 space-x-2">
                <button
                  className="px-3 py-1 !bg-blue-600 text-white rounded"
                  onClick={() => onEdit(p.id)}
                >
                  Edit
                </button>

                <button
                  className="px-3 py-1 !bg-red-600 text-white rounded"
                  onClick={() => onDelete(p.id)}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ProductTable;
