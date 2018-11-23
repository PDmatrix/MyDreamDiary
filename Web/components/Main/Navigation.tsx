import { Menu } from "antd";
import Next from "next";
import Link from "next/link";
import React from "react";
import css from "styled-jsx/css";
import Auth from "../../lib/Auth";

// language=CSS
const { className, styles } = css.resolve`
	ul {
		line-height: 64px;
	}
`;

interface INavigation {
	currentRoute: string;
}

const Navigation: Next.NextSFC<INavigation> = ({ currentRoute }) => {
	const auth = Auth.getInstance();

	return (
		<Menu
			theme="light"
			mode="horizontal"
			className={className}
			defaultSelectedKeys={[currentRoute]}
		>
			<Menu.Item key="/">
				<Link href="/">
					<a>Главная страница</a>
				</Link>
			</Menu.Item>
			<Menu.Item key="/posts">
				<Link prefetch={true} href={"/posts"}>
					<a>Публикации</a>
				</Link>
			</Menu.Item>
			<Menu.Item key="/user">
				<Link
					prefetch={true}
					href={`/user?id=${auth.getUserId()}`}
					as={"/user"}
				>
					<a>Личная страница</a>
				</Link>
			</Menu.Item>
			{styles}
		</Menu>
	);
};

// @ts-ignore
export default Navigation;
