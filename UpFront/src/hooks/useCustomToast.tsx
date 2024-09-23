import React from 'react';
import { toast } from 'react-toastify';
import CancelIcon from "@mui/icons-material/Cancel";
import ErrorIcon from "@mui/icons-material/Error";
import InfoOutlinedIcon from "@mui/icons-material/InfoOutlined";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";

export interface ISeverity {
  error: string,
  warning: string,
  info: string,
  success: string
}

export interface IToastMessage {
  message: string | React.JSX.Element;
  severity: keyof ISeverity;
  autoHideDuration?: number;
}

export const showToast = ({ message, severity, autoHideDuration = 3000 }: IToastMessage) => {
  const toastOptions = {
    autoClose: autoHideDuration,
    icon: () => {
      const iconMapping = {
        error: <CancelIcon className="errorIcon" fontSize="inherit" />,
        warning: <ErrorIcon className="warningIcon" fontSize="inherit" />,
        info: <InfoOutlinedIcon className="infoIcon" fontSize="inherit" />,
        success: <CheckCircleIcon className="successIcon" fontSize="inherit" />,
      } as Record<string, JSX.Element>;
      return iconMapping[severity];
    },
  };

  // Utilizar el método de toast correspondiente a la severidad
  switch (severity) {
    case 'error':
      toast.error(message, toastOptions);
      break;
    case 'warning':
      toast.warning(message, toastOptions);
      break;
    case 'info':
      toast.info(message, toastOptions);
      break;
    case 'success':
      toast.success(message, toastOptions);
      break;
    default:
      break;
  }
};
