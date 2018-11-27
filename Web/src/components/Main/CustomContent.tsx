import { Layout } from "antd";
import React from "react";
import styles from "./styles.css";

const CustomContent: React.FunctionComponent = ({ children }) => {
  return (
    <Layout.Content className={styles.Content}>
      <div className={styles.ContentInner}>{children}</div>
    </Layout.Content>
  );
};

export default CustomContent;
