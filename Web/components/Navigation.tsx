import { Menu } from "antd";
import Next from "next";
import Link from "next/link";
import React from "react";

const Navigation: Next.NextSFC = () => {
	return (
		<Menu
			theme="light"
			mode="horizontal"
			// defaultSelectedKeys={[this.props.route]}
			style={{ lineHeight: "64px", float: "right" }}
		>
			<Menu.Item key="/">
				<Link href="/">
					<a>Главная страница</a>
				</Link>
			</Menu.Item>
			<Menu.Item key="/posts">
				<Link prefetch={true} href="/posts">
					<a>Публикации</a>
				</Link>
			</Menu.Item>
			<Menu.Item key="/user">
				<Link prefetch={true} href="/user">
					<a>Личная страница</a>
				</Link>
			</Menu.Item>
		</Menu>
	);
};

// @ts-ignore
export default React.memo(Navigation);
