import "./App.css";
import { BrowserRouter } from "react-router-dom";
import { AuthenticationProvider } from './common/AuthenticationContext';
import { AllRoutes } from './common/AllRoutes';

export default function App() {
  return (
    <BrowserRouter>
		<AuthenticationProvider>
			<AllRoutes />
		</AuthenticationProvider>
	</BrowserRouter>
  );
}