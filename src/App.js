import './styles/main-styles.css';
import { Navbar } from "./components/Navbar.jsx";
import { Container } from "./components/Container.jsx";


function App() {
  return (
    <div className="app">
      <Navbar></Navbar>
      <Container></Container>
      
    </div>
  );
}

export default App;
