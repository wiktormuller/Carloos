import "./loginPanel-styles.css";

export const LoginPanel = (props) => {
  return (
    <div className="panel">
      <form className="form">
        <h2>Zaloguj się</h2>
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
        <button>Zaloguj</button>
      </form>
    </div>
  );
};
