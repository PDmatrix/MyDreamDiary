import { Divider } from "antd";
import Next from "next";
import React, { useEffect, useState } from "react";
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

const loadUserData = async (id: string | string[]) => {
	const res = await query.get(`http://localhost:5000/api/user/${id}`);
	return await res.data;
};

const User: Next.NextSFC<IUserInterface> = (props) => {
	const auth = Auth.getInstance();

	const [dreams, setDreams] = useState(props!.dreams);
	const handleInput = (newDream: IDreamInterface) => {
		const newDreams = dreams;
		newDreams.push(newDream);
		setDreams(newDreams);
	};

	useEffect(() => {
		if (!auth.isAuthenticated()) {
			setTimeout(auth.login(), 1000);
		}
	});

	return (
		<div>
			{auth.isAuthenticated() && dreams !== undefined && (
				<>
					<UserInfo email={props!.email} name={props!.name} />
					<CreateDream handleInput={handleInput} />
					<Divider />
					{dreams!.length > 0 && (
						<>
							<h3>Список снов:</h3>
							<DreamList dreams={dreams} />
						</>
					)}
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
