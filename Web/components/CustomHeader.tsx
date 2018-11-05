import { Layout } from "antd";
import Next from "next";
import React from "react";
import Navigation from "./Navigation";
import css from "styled-jsx/css";

const { className, styles } = css.resolve`
	div {
		background-color: #fff;
	}
`;

const CustomHeader: Next.NextSFC = () => {
	return (
		<Layout.Header className={className}>
			<Navigation />
			{styles}
		</Layout.Header>
	);
};

// @ts-ignore
export default React.memo(CustomHeader);
