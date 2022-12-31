import "../accessDenied-styles.css";
import { Link } from 'react-router-dom';

export default function AccessDeniedComponent()
{
  return (
    <div class="accessDenied">
            <div class="d-flex align-items-center justify-content-center vh-100">
                <div class="text-center">
                    <h1 class="display-1 fw-bold">403</h1>
                    <p class="fs-3"> <span class="text-danger">Opps!</span> Forbidden.</p>
                    <p class="lead">
                        Access Denied!
                    </p>
                    <Link className="btn btn-primary" to={`/`}>Go home</Link>
                </div>
            </div>
        </div>
  );
}