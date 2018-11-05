import { Layout } from "antd";
import Next from "next";
import React from "react";
import css from "styled-jsx/css";
import { useMedia } from "the-platform";
import Navigation from "./Navigation";

// language=CSS
const { className, styles } = css.resolve`
	div {
		background-color: #fff;
		box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);
	}
`;

interface ICustomHeader {
	currentRoute: string;
}

const CustomHeader: Next.NextSFC<ICustomHeader> = ({ currentRoute }) => {
	const isWide = useMedia("(min-width: 720px)");
	return (
		<Layout.Header className={className}>
			{isWide && <span>Дневник снов</span>}
			<Navigation currentRoute={currentRoute} />
			{styles}
			<style jsx={true}>{`
				span {
					font-weight: bold;
				}
			`}</style>
		</Layout.Header>
	);
};

// @ts-ignore
export default React.memo(CustomHeader);
