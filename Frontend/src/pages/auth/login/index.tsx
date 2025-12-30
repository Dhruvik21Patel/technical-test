import { Formik, Form } from "formik";
import * as Yup from "yup";
import { loginUser } from "../../../services/auth";
import { useNavigate } from "react-router-dom";
import TextField from "../../../components/common/Textfield";

interface LoginFormValues {
  email: string;
  password: string;
}

const Login = () => {
  const navigate = useNavigate();
  const initialValues: LoginFormValues = {
    email: "",
    password: "",
  };

  const validationSchema = Yup.object({
    email: Yup.string()
      .email("Invalid email format")
      .required("Email is required"),

    password: Yup.string()
      .required("Password is required")
      .min(8, "Password must be at least 8 characters")
      .matches(
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$/,
        "Password must contain at least 1 uppercase, 1 lowercase, 1 number, and 1 special character"
      ),
  });

  const handleSubmit = async (values: LoginFormValues) => {
    try {
      const response = await loginUser(values);

      if (response.isSuccessStatusCode) {
        localStorage.setItem("token", response.data.token);
        localStorage.setItem("userName", response.data.userName);

        navigate("/products");
      } else {
        console.log(response.message);
      }
    } catch (error: any) {
      console.log(
        error?.response?.data?.message || "Invalid email or password"
      );
    } finally {
      console.log("request completed");
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <div className="bg-white p-8 rounded-lg shadow-md w-full max-w-sm">
        <h2 className="text-2xl font-bold text-center mb-6">Login</h2>

        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          onSubmit={handleSubmit}
        >
          {({ isSubmitting, status }) => (
            <Form className="space-y-4">
              {status && (
                <div className="text-red-500 text-sm text-center">{status}</div>
              )}

              {/* Email */}
              <TextField
                name="email"
                label="Email"
                type="email"
                placeholder="Enter email"
              />

              {/* Password */}
              <TextField
                name="password"
                label="Password"
                type="password"
                placeholder="Enter password"
              />

              {/* Button */}
              <button
                type="submit"
                disabled={isSubmitting}
                className="w-full !bg-blue-600 text-white py-2 rounded !hover:bg-blue-700 transition"
              >
                {isSubmitting ? "Logging in..." : "Login"}
              </button>
            </Form>
          )}
        </Formik>
      </div>
    </div>
  );
};

export default Login;
