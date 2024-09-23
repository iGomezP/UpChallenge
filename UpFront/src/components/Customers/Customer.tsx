import { Box, TextField, Button, Tab, Tabs } from '@mui/material';
import { useMainState } from '../../context/useMainStore';
import { useState } from 'react';

const Customer = () => {
    const { customerData } = useMainState();
    const [currentTab, setCurrentTab] = useState(0);

    const handleChange = async () => {

    }

    const handleSubmit = async () => {

    }

    const handleTabChange = (_event: React.SyntheticEvent, newValue: number) => {
        setCurrentTab(newValue);
    };

    if (!customerData) {
        return <div>Loading...</div>;
    }

    return (
        <Box className="flex flex-col justify-center items-center min-h-screen w-full gap-5">
            <Box sx={{ p: "24px", borderRadius: "16px", backgroundColor: "#F7F7FC", width: '100%', maxWidth: '600px' }}>
                <div className="text-center uppercase tracking-wide text-sm text-indigo-500 font-semibold mb-1">UpBank</div>
                <h2 className="text-center text-2xl font-bold mb-4">{customerData.name}'s Profile</h2>

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
                            value={customerData.email}
                            disabled
                        />
                        <TextField
                            required
                            name="password"
                            label="Password"
                            type="password"
                            variant="standard"
                            onChange={handleChange}
                            fullWidth
                            value={customerData.password}
                            sx={{ mt: 2 }}
                            disabled
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
                            value={customerData.name}
                            disabled
                        />
                        <TextField
                            required
                            label="Last Name"
                            name="lastName"
                            variant="standard"
                            onChange={handleChange}
                            fullWidth
                            value={customerData.lastName}
                            sx={{ mt: 2 }}
                            disabled
                        />
                        <TextField
                            required
                            label="Birthday"
                            name="birthYear"
                            variant="standard"
                            onChange={handleChange}
                            fullWidth
                            value={`${customerData.birthDay?.year} - ${customerData.birthDay?.month} - ${customerData.birthDay?.day}`}
                            sx={{ mt: 2 }}
                            disabled
                        />
                        <TextField
                            required
                            label="Phone Number"
                            name="phoneNumber"
                            variant="standard"
                            onChange={handleChange}
                            fullWidth
                            value={customerData.phoneNumber}
                            sx={{ mt: 2 }}
                        />
                        <TextField
                            required
                            label="Street"
                            name="street"
                            variant="standard"
                            onChange={handleChange}
                            fullWidth
                            value={customerData.address.street}
                            sx={{ mt: 2 }}
                        />
                        <TextField
                            required
                            label="City"
                            name="city"
                            variant="standard"
                            onChange={handleChange}
                            fullWidth
                            value={customerData.address.city}
                            sx={{ mt: 2 }}
                        />
                        <TextField
                            required
                            label="State"
                            name="state"
                            variant="standard"
                            onChange={handleChange}
                            fullWidth
                            value={customerData.address.state}
                            sx={{ mt: 2 }}
                        />
                        <TextField
                            required
                            label="ZIP Code"
                            name="zipCode"
                            variant="standard"
                            onChange={handleChange}
                            fullWidth
                            value={customerData.address.zipCode}
                            sx={{ mt: 2 }}
                        />
                        <TextField
                            required
                            label="Country"
                            name="country"
                            variant="standard"
                            onChange={handleChange}
                            fullWidth
                            value={customerData.address.country}
                            sx={{ mt: 2 }}
                        />

                        <Box className="flex flex-row justify-between">
                            <Button variant="contained" sx={{ mt: 5, mb: 5, width: "25%" }} onClick={() => setCurrentTab(0)}>
                                Back
                            </Button>
                            <Button variant="contained" type="submit" sx={{ mt: 5, mb: 5, width: "25%" }}>
                                Update
                            </Button>
                        </Box>
                    </Box>
                )}
            </Box>
        </Box>
    );
};

export default Customer;
