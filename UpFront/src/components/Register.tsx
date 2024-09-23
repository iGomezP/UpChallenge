import { useState } from "react";
import { registerUserAsync } from "../services/userService";
import Box from "@mui/material/Box";
import { Button, CircularProgress, Tab, Tabs, TextField } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { TRegisterReq } from "../types/customer.type";
import { TError } from "../types/error.type";
import { showToast } from "../hooks/useCustomToast";

const Register = () => {
    const [formData, setFormData] = useState<TRegisterReq>({
        name: '',
        lastName: '',
        email: '',
        password: '',
        phoneNumber: '',
        birthDay: { year: 0, month: 0, day: 0 },
        street: '',
        city: '',
        state: '',
        zipCode: '',
        country: '',
        createdDate: new Date(),
        objectStatus: 'active'
    });

    const [loading, setLoading] = useState(false);
    const [currentTab, setCurrentTab] = useState(0);
    const navigate = useNavigate();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;

        if (name === 'birthYear' || name === 'birthMonth' || name === 'birthDay') {
            setFormData({
                ...formData,
                birthDay: {
                    ...formData.birthDay,
                    [name === 'birthYear' ? 'year' : name === 'birthMonth' ? 'month' : 'day']: parseInt(value),
                },
            });
        } else {
            setFormData({
                ...formData,
                [name]: value,
            });
        }
    };


    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            setLoading(true);
            const response = await registerUserAsync(formData);
            if (response && "status" in response) {
                throw {
                    type: response.type,
                    title: response.title,
                    status: response.status,
                    detail: response.detail,
                } as TError;
            } else {
                navigate("/login");
                showToast({
                    message: "Welcome! please use your credentials to log in.",
                    severity: "info"
                })
            }
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
        } catch (error: any) {
            showToast({
                message: `${error.title} - ${error.status} - ${error.detail}`,
                severity: "error"
            })
            console.error("Error logging in", error);
        } finally {
            setLoading(false);
        }
    };

    const handleLoginClick = () => {
        navigate('/login');
    };

    const handleTabChange = (_event: React.SyntheticEvent, newValue: number) => {
        setCurrentTab(newValue);
    };

    return (
        <Box position="relative">
            {loading && (
                <Box
                    position="absolute"
                    top={0}
                    left={0}
                    right={0}
                    bottom={0}
                    display="flex"
                    alignItems="center"
                    justifyContent="center"
                    bgcolor="rgba(255, 255, 255, 0.5)"
                    zIndex={2}
                >
                    <CircularProgress />
                </Box>
            )}
            <Box className="flex flex-col justify-center items-center min-h-screen w-full gap-5">
                <Box sx={{ p: "24px", borderRadius: "16px", backgroundColor: "#F7F7FC", width: '100%', maxWidth: '600px' }}>
                    <div className="text-center uppercase tracking-wide text-sm text-indigo-500 font-semibold mb-1">UpBank</div>
                    <h2 className="text-center text-2xl font-bold mb-4">Register</h2>

                    <Tabs value={currentTab} onChange={handleTabChange} centered>
                        <Tab label="Account Info" />
                        <Tab label="Personal Info" />
                    </Tabs>

                    {/* Tab 1: Email y Password */}
                    {currentTab === 0 && (
                        <Box component="form" onSubmit={handleSubmit} sx={{ mt: 2 }}>
                            <TextField
                                required
                                label="Email"
                                type="email"
                                variant="standard"
                                onChange={handleChange}
                                name="email"
                                fullWidth
                                value={formData.email}
                            />
                            <TextField
                                required
                                name="password"
                                label="Password"
                                type="password"
                                variant="standard"
                                onChange={handleChange}
                                fullWidth
                                value={formData.password}
                                sx={{ mt: 2 }}
                            />
                            <Button variant="contained" sx={{ mt: 5, mb: 5, ml: 'auto', display: 'block', width: "25%" }} onClick={() => setCurrentTab(1)}>
                                Next
                            </Button>
                        </Box>
                    )}

                    {/* Tab 2: Información personal */}
                    {currentTab === 1 && (
                        <Box component="form" onSubmit={handleSubmit} sx={{ mt: 2 }}>
                            <TextField
                                required
                                label="First Name"
                                name="name"
                                variant="standard"
                                onChange={handleChange}
                                fullWidth
                                value={formData.name}
                            />
                            <TextField
                                required
                                label="Last Name"
                                name="lastName"
                                variant="standard"
                                onChange={handleChange}
                                fullWidth
                                value={formData.lastName}
                                sx={{ mt: 2 }}
                            />
                            <TextField
                                required
                                label="Phone Number"
                                name="phoneNumber"
                                variant="standard"
                                onChange={handleChange}
                                fullWidth
                                value={formData.phoneNumber}
                                sx={{ mt: 2 }}
                            />
                            <TextField
                                required
                                label="Street"
                                name="street"
                                variant="standard"
                                onChange={handleChange}
                                fullWidth
                                value={formData.street}
                                sx={{ mt: 2 }}
                            />
                            <TextField
                                required
                                label="City"
                                name="city"
                                variant="standard"
                                onChange={handleChange}
                                fullWidth
                                value={formData.city}
                                sx={{ mt: 2 }}
                            />
                            <TextField
                                required
                                label="State"
                                name="state"
                                variant="standard"
                                onChange={handleChange}
                                fullWidth
                                value={formData.state}
                                sx={{ mt: 2 }}
                            />
                            <TextField
                                required
                                label="ZIP Code"
                                name="zipCode"
                                variant="standard"
                                onChange={handleChange}
                                fullWidth
                                value={formData.zipCode}
                                sx={{ mt: 2 }}
                            />
                            <TextField
                                required
                                label="Country"
                                name="country"
                                variant="standard"
                                onChange={handleChange}
                                fullWidth
                                value={formData.country}
                                sx={{ mt: 2 }}
                            />
                            <TextField
                                required
                                label="Year"
                                name="birthYear"
                                variant="standard"
                                onChange={handleChange}
                                fullWidth
                                value={formData.birthDay?.year || ''}
                                sx={{ mt: 2 }}
                            />
                            <TextField
                                required
                                label="Month"
                                name="birthMonth"
                                variant="standard"
                                onChange={handleChange}
                                fullWidth
                                value={formData.birthDay?.month || ''}
                                sx={{ mt: 2 }}
                            />
                            <TextField
                                required
                                label="Day"
                                name="birthDay"
                                variant="standard"
                                onChange={handleChange}
                                fullWidth
                                value={formData.birthDay?.day || ''}
                                sx={{ mt: 2 }}
                            />
                            <Box className="flex flex-row justify-between">
                                <Button variant="contained" sx={{ mt: 5, mb: 5, width: "25%" }} onClick={() => setCurrentTab(0)}>
                                    Back
                                </Button>
                                <Button variant="contained" type="submit" sx={{ mt: 5, mb: 5, width: "25%" }}>
                                    Register
                                </Button>
                            </Box>
                        </Box>
                    )}

                    <Box className="flex flex-col w-full max-w-sm mx-auto gap-1 mt-5">
                        <h3 className="text-center uppercase tracking-wide text-sm text-indigo-800 font-semibold mb-1">Already a User?</h3>
                        <Button variant="text" onClick={() => handleLoginClick()}>
                            Click to Login
                        </Button>
                    </Box>
                </Box>
            </Box>
        </Box>
    );
};

export default Register;
