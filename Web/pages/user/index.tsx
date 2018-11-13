import { Button, Divider } from "antd";
import Next from "next";
import React from "react";
import Auth from "../../lib/Auth";

const User: Next.NextSFC = () => {
	const auth = Auth.getInstance();
	if (!auth.isAuthenticated()) {
		auth.login();
	}
	return (
		<div>
			{!auth.isAuthenticated() && (
				<Button onClick={() => auth.login()} htmlType={"button"} type="primary">
					LogIn
				</Button>
			)}
			<Divider type={"vertical"} />
			{auth.isAuthenticated() && (
				<Button
					onClick={() => auth.logout()}
					htmlType={"button"}
					type="primary"
				>
					LogOut
				</Button>
			)}
		</div>
	);
};

export default User;
