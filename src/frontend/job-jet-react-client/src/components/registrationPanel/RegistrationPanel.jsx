import "./registrationPanel-styles.css";

export const RegistrationPanel = (props) => {
  return (
    <div className="panel">
      <form className="form">
        <h2>Zarejestruj się</h2>
        <input
          className="custom-input"
          input
          type="text"
          placeholder="Enter Email"
          name="email"
          id="email"
          required
        ></input>
        <br />
        <input
          className="custom-input"
          type="password"
          placeholder="Enter Password"
          name="psw"
          id="psw"
          required
        ></input>
        <br />
        <button>Zarejestuj</button>
      </form>
    </div>
  );
};
