import { Button, Divider } from "antd";
import Next from "next";
import React from "react";
import Auth from "../../lib/Auth";

interface IUserInfoInterface {
	name: string;
	email: string;
}

const UserInfo: Next.NextSFC<IUserInfoInterface> = (props) => {
	const handleClick = () => {
		Auth.getInstance().logout();
	};

	return (
		<>
			<h4>Имя пользователя:</h4>
			<span>{props.name}</span>
			<h4>Электронная почта:</h4>
			<span>{props.email}</span>
			<br />
			<Button type={"primary"} onClick={handleClick} htmlType={"button"}>
				Выйти
			</Button>
			<Divider />
		</>
	);
};

export default UserInfo;
