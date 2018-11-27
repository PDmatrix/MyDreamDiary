import { Layout } from "antd";
import React from "react";
import styles from "./styles.css";

interface ICustomFooter {
  text: string;
}

const CustomFooter: React.FunctionComponent<ICustomFooter> = ({ text }) => {
  return (
    <Layout.Footer className={styles.Footer}>
      <span>{text}</span>
    </Layout.Footer>
  );
};

export default CustomFooter;
