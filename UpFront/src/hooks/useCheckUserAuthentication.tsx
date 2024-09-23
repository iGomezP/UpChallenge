import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useMainState } from "../context/useMainStore";
import Cookies from 'js-cookie';

export const useCheckUserAuthentication = () => {
    const navigate = useNavigate();
    const { customerId, jwtToken } = useMainState();

    useEffect(() => {
        const storedCustomerId = Cookies.get('customerId');
        const storedJwtToken = Cookies.get('jwt');
        if (!storedCustomerId || !storedJwtToken) {
            navigate('/login');
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [customerId, jwtToken]);
};