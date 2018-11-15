import { Divider } from "antd";
import Next from "next";
import React, { useState } from "react";
import CreateDream from "../../components/User/CreateDream";
import DreamList from "../../components/User/DreamList";
import UserInfo from "../../components/User/UserInfo";
import Auth from "../../lib/Auth";
import query from "../../lib/query";

interface IUserPostInterface {
	id: number;
	title: string;
}

interface IUserInterface {
	name: string;
	email: string;
	id: string;
	comments: ICommentInterface[];
	posts: IUserPostInterface[];
	dreams: IDreamInterface[];
}

const loadUserData = async (id) => {
	const res = await query.get(`http://localhost:5000/api/user/${id}`);
	return await res.data;
};

const User: Next.NextSFC<IUserInterface> = (props) => {
	const auth = Auth.getInstance();
	if (!auth.isAuthenticated()) {
		auth.login();
	}

	const [dreams, setDreams] = useState(props.dreams);
	const handleInput = (newDream: IDreamInterface) => {
		setDreams([...dreams, newDream]);
	};

	return (
		<div>
			{auth.isAuthenticated() && (
				<>
					<UserInfo email={props.email} name={props.name} />
					<CreateDream handleInput={handleInput} />
					<Divider />
					<h2>Список снов:</h2>
					<DreamList dreams={dreams} />
					{/*<CreateDream />
					<UserPost />*/}
				</>
			)}
		</div>
	);
};

User.getInitialProps = async ({ query }) => {
	if (query.id !== undefined && query.id !== "undefined") {
		return await loadUserData(query.id);
	} else {
		return {};
	}
};

export default User;
