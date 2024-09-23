import { useState } from "react";
import { Box, Button, TextField } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { loginUserAsync } from "../services/userService";
import { useMainState } from "../context/useMainStore";
import { TLogin } from "../types/customer.type";
import { TError } from "../types/error.type";
import { showToast } from "../hooks/useCustomToast";

const Login = () => {
    const [formData, setFormData] = useState<TLogin>({
        email: '',
        password: '',
    });
    const navigate = useNavigate();
    const { setCustomerId, setJwtToken } = useMainState();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const response = await loginUserAsync(formData);

            if (response && "status" in response) {
                throw {
                    type: response.type,
                    title: response.title,
                    status: response.status,
                    detail: response.detail,
                } as TError;
            } else {
                const { customerId, token } = response;
                setCustomerId(customerId);
                setJwtToken(token);
                navigate("/index");
            }
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
        } catch (error: any) {
            showToast({
                message: `${error.title} - ${error.status} - ${error.detail}`,
                severity: "error"
            })
            console.error("Error logging in", error);
        }
    };

    const handleRegisterClick = () => {
        navigate('/register');
    };

    return (
        <Box className="flex flex-col justify-center items-center min-h-screen w-full gap-5">
            <Box sx={{ p: "24px", borderRadius: "16px", backgroundColor: "#F7F7FC", width: '100%', maxWidth: '600px' }} >
                <div className="text-center uppercase tracking-wide text-sm text-indigo-500 font-semibold mb-1">UpBank</div>
                <h2 className="text-center text-2xl font-bold mb-4">Login</h2>
                <Box
                    component="form"
                    onSubmit={handleSubmit}
                >
                    <Box className="flex flex-col w-full max-w-sm mx-auto gap-5">
                        <TextField
                            required
                            label="Email"
                            type="email"
                            variant="standard"
                            onChange={handleChange}
                            name="email"
                            fullWidth
                        >
                        </TextField>
                        <TextField
                            required
                            name="password"
                            label="Password"
                            type="password"
                            variant="standard"
                            onChange={handleChange}
                            fullWidth
                        >
                        </TextField>
                        <Button variant="contained" type="submit">
                            Login
                        </Button>
                    </Box>
                    <Box className="flex flex-col w-full max-w-sm mx-auto gap-1 mt-5">
                        <h3
                            className="text-center uppercase tracking-wide text-sm text-indigo-800 font-semibold mb-1">
                            New User?
                        </h3>
                        <Button variant="text" onClick={handleRegisterClick}>
                            Click to Register
                        </Button>
                    </Box>
                </Box>
            </Box>
        </Box>
    );
};

export default Login;
