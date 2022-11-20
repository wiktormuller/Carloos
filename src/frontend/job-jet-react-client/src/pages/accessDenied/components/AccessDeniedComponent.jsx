import "../accessDenied-styles.css";
import { Link } from "react-router-dom";

export default function AccessDeniedComponent(props)
{
  return (
    <div className="denial-page">
      Usługa dostępna dla <Link to="/login"> zalogowanych </Link> użytkowników.
      Jeśli nie posiadasz konta <Link to="/register"> zarejestruj się </Link>.
    </div>
  );
}