import { Layout } from "antd";
import React from "react";
import Navigation from "./Navigation";
import styles from "./styles.css";

const CustomHeader: React.FunctionComponent = () => {
  return (
    <Layout.Header className={styles.Header}>
      <span className={styles.Title}>Дневник снов</span>
      <Navigation />
    </Layout.Header>
  );
};

export default CustomHeader;
