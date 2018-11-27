import { Spin } from "antd";
import React from "react";
import { RouteComponentProps } from "react-router";
import withRouter from "umi/withRouter";
import Auth from "../lib/Auth";
import styles from "./callback.css";

const Callback: React.FunctionComponent<RouteComponentProps> = props => {
  const auth = Auth.getInstance();
  const handleAuthentication = (path: string) => {
    if (/access_token|id_token|error/.test(path)) {
      auth.handleAuthentication();
    }
  };

  handleAuthentication(props.location.hash);

  return (
    <div className={styles.callback}>
      <Spin size={"large"} />
    </div>
  );
};

export default withRouter(Callback);
