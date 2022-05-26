import "./loginPanel-styles.css";
import { useState, useEffect } from "react";

export const LoginPanel = (props) => {
  let [email, setEmail] = useState("");
  let [password, setPassword] = useState("");

  const authLogin = `https://jobjet.azurewebsites.net/api/v1/auth/login`;

  const handleClick = () => {
    let body = {
      email: `${email}`,
      password: `${password}`,
    };

    let strBody = JSON.stringify(body);

    console.log(body);
    console.log(strBody);

    fetch(authLogin, {
      method: "POST",
      body: strBody,
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
      });
  };

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
          onChange={(e) => {
            setEmail(e.target.value);
          }}
        ></input>
        <br />
        <input
          className="custom-input"
          type="password"
          placeholder="Enter Password"
          name="psw"
          id="psw"
          required
          onChange={(e) => {
            setPassword(e.target.value);
          }}
        ></input>
        <br />
        <button type="button" onClick={handleClick}>
          Zaloguj
        </button>
      </form>
    </div>
  );
};
