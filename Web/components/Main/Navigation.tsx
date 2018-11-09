import { Menu } from "antd";
import Next from "next";
import Link from "next/link";
import React from "react";
import css from "styled-jsx/css";

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
				<Link prefetch={true} href={"/user"}>
					<a>Личная страница</a>
				</Link>
			</Menu.Item>
			{styles}
		</Menu>
	);
};

// @ts-ignore
export default React.memo(Navigation);
