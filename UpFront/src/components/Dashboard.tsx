import { Box } from "@mui/material";

const Dashboard = () => {
    return (
        <>
            <Box className="flex flex-col justify-center items-center min-h-screen w-full gap-5">
                <Box sx={{ p: "24px", borderRadius: "16px", backgroundColor: "#F7F7FC", width: '100%', maxWidth: '600px' }}>
                    <div className="text-2xl text-center uppercase tracking-wide text-indigo-500 font-semibold mb-8">Welcome to the UpBack Banking App!</div>
                    <h2 className="text-center text-md font-bold mb-4">This is the home page of your banking application.</h2>
                    <h2 className="text-center text-md font-bold mb-4">From here, you can manage your accounts and view transactions.</h2>
                </Box>
            </Box>
        </>
    );
};

export default Dashboard;
