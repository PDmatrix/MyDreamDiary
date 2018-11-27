import { Menu } from "antd";
import React from "react";
import Link from "umi/link";
import withRouter from "umi/withRouter";
import styles from "./styles.css";

const Navigation: React.FunctionComponent = props => {
  return (
    <Menu
      theme="light"
      mode="horizontal"
      className={styles.Navigation}
      defaultSelectedKeys={[props.location.pathname]}
    >
      <Menu.Item key="/">
        <Link to="/">Главная страница</Link>
      </Menu.Item>
      <Menu.Item key="/posts">
        <Link to={"/posts"}>Публикации</Link>
      </Menu.Item>
      <Menu.Item key="/user">
        <Link to={`/user`}>Личная страница</Link>
      </Menu.Item>
    </Menu>
  );
};

export default withRouter(Navigation);
