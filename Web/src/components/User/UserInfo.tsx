import { Button, Divider } from "antd";
import React from "react";
import Auth from "../../lib/Auth";
import styles from "./styles.css";

interface IUserInfoInterface {
  name: string;
  email: string;
}

const UserInfo: React.FunctionComponent<IUserInfoInterface> = props => {
  const handleClick = () => {
    Auth.getInstance().logout();
  };

  return (
    <>
      <h4>Имя пользователя:</h4>
      <span className={styles.userData}>{props.name}</span>
      <h4>Электронная почта:</h4>
      <span className={styles.userData}>{props.email}</span>
      <br />
      <Button type={"primary"} onClick={handleClick} htmlType={"button"}>
        Выйти
      </Button>
      <Divider />
    </>
  );
};

export default UserInfo;
