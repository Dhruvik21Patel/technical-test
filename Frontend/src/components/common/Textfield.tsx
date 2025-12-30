import { Field, ErrorMessage } from "formik";

interface TextFieldProps {
  name: string;
  label: string;
  type?: string;
  placeholder?: string;
}

const TextField = ({
  name,
  label,
  type = "text",
  placeholder = "",
}: TextFieldProps) => {
  return (
    <div>
      <label id={name} className="block text-sm font-medium mb-1">
        {label}
      </label>

      <Field
        name={name}
        type={type}
        placeholder={placeholder}
        className="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
      />

      <ErrorMessage
        name={name}
        component="div"
        className="text-red-500 text-sm mt-1"
      />
    </div>
  );
};

export default TextField;
