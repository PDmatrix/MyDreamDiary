import { Layout, LocaleProvider } from "antd";
import ru_RU from "antd/lib/locale-provider/ru_RU";
import axios from "axios";
import React from "react";
import CustomContent from "../components/Main/CustomContent";
import CustomHeader from "../components/Main/CustomHeader";
import Auth from "../lib/Auth";
import styles from "./index.css";

const BaseLayout: React.FunctionComponent = props => {
  axios.defaults.baseURL = `${process.env.API_URL}/api`;
  axios.defaults.headers.common.Authorization = `Bearer ${Auth.getUserToken()}`;
  axios.defaults.headers.common["Content-Type"] = "application/json";

  return (
    <LocaleProvider locale={ru_RU}>
      <Layout>
        <CustomHeader />
        <div className={styles.content}>
          <CustomContent>{props.children}</CustomContent>
        </div>
      </Layout>
    </LocaleProvider>
  );
};

export default BaseLayout;
