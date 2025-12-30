import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";
import { createProduct, updateProduct } from "../../services/product";
import type { Category, Product } from "../../interfaces/product.interface";
import MultiSelectDropdown from "../common/MultiSelectDropDown";

interface Props {
  isOpen: boolean;
  onClose: () => void;
  onSuccess: () => void;
  product?: Product;
  categories: Category[];
}

interface ProductFormValues {
  name: string;
  description: string;
  price: number;
  stockQuantity: number;
  categoryIds: number[];
}

const validationSchema = Yup.object({
  name: Yup.string().required("Product name is required"),
  description: Yup.string().required("Description is required"),
  price: Yup.number().positive().required("Price is required"),
  stockQuantity: Yup.number().min(0).required("Stock is required"),
  categoryIds: Yup.array()
    .of(Yup.number())
    .min(1, "Select at least one category"),
});

const ProductFormDialog = ({
  isOpen,
  onClose,
  onSuccess,
  categories,
  product,
}: Props) => {
  if (!isOpen) return <></>;

  const initialValues: ProductFormValues = {
    name: product?.name || "",
    description: product?.description || "",
    price: product?.price || 0,
    stockQuantity: product?.stockQuantity || 0,
    categoryIds: product?.categories?.map((c) => c.id) || [],
  };

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
      <div className="bg-white rounded-xl shadow-lg w-full max-w-lg p-6">
        <h2 className="text-xl font-semibold mb-4">
          {product ? "Edit" : "Add"} Product
        </h2>

        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          enableReinitialize
          onSubmit={async (values, { setSubmitting, resetForm }) => {
            if (product) {
              await updateProduct(product.id, values); // edit
            } else {
              await createProduct(values); // create
            }
            resetForm();
            onSuccess();
            onClose();
            setSubmitting(false);
          }}
        >
          {({ values, setFieldValue, isSubmitting }) => (
            <Form className="space-y-4">
              {/* Name */}
              <div>
                <Field
                  name="name"
                  placeholder="Product name"
                  className="w-full border p-2 rounded"
                />
                <ErrorMessage
                  name="name"
                  component="p"
                  className="text-red-500 text-sm"
                />
              </div>

              {/* Description */}
              <div>
                <Field
                  name="description"
                  placeholder="Description"
                  className="w-full border p-2 rounded"
                />
                <ErrorMessage
                  name="description"
                  component="p"
                  className="text-red-500 text-sm"
                />
              </div>

              {/* Price */}
              <div>
                <Field
                  type="number"
                  name="price"
                  placeholder="Price"
                  className="w-full border p-2 rounded"
                />
                <ErrorMessage
                  name="price"
                  component="p"
                  className="text-red-500 text-sm"
                />
              </div>

              {/* Stock */}
              <div>
                <Field
                  type="number"
                  name="stockQuantity"
                  placeholder="Stock quantity"
                  className="w-full border p-2 rounded"
                />
                <ErrorMessage
                  name="stockQuantity"
                  component="p"
                  className="text-red-500 text-sm"
                />
              </div>

              {/* Categories */}
              <div>
                <MultiSelectDropdown
                  options={categories}
                  value={values.categoryIds}
                  onChange={(val) => setFieldValue("categoryIds", val)}
                />

                <ErrorMessage
                  name="categoryIds"
                  component="p"
                  className="text-red-500 text-sm"
                />
              </div>

              {/* Buttons */}
              <div className="flex justify-end gap-3 pt-4">
                <button
                  type="button"
                  onClick={onClose}
                  className="px-4 py-2 border rounded"
                >
                  Cancel
                </button>

                <button
                  type="submit"
                  disabled={isSubmitting}
                  className="px-4 py-2 !bg-blue-600 text-white rounded"
                >
                  {isSubmitting ? "Saving..." : "Save"}
                </button>
              </div>
            </Form>
          )}
        </Formik>
      </div>
    </div>
  );
};

export default ProductFormDialog;
