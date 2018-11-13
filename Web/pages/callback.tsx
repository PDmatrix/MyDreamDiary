import { Spin } from "antd";
import Next from "next";
import { withRouter, WithRouterProps } from "next/router";
import React from "react";
import Auth from "../lib/Auth";

const Callback: Next.NextSFC<WithRouterProps> = (props) => {
	const auth = Auth.getInstance();
	const handleAuthentication = (path) => {
		if (/access_token|id_token|error/.test(path)) {
			auth.handleAuthentication();
		}
	};

	handleAuthentication(props.router.asPath);

	return (
		<div className={"callback"}>
			<Spin size={"large"} />
			<style jsx={true}>{`
				div.callback {
					position: absolute;
					display: flex;
					align-items: center;
					justify-content: center;
					height: 100vh;
					width: 100vw;
					top: 0;
					bottom: 0;
					left: 0;
					right: 0;
					background-color: white;
				}
			`}</style>
		</div>
	);
};

export default withRouter(Callback);
