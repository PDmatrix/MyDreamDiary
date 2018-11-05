import { Layout } from "antd";
import Next from "next";
import React from "react";
import css from "styled-jsx/css";

const { className, styles } = css.resolve`
	div {
		text-align: center;
	}
`;

interface ICustomFooter {
	text: string;
}

const CustomFooter: Next.NextSFC<ICustomFooter> = ({ text }) => {
	return (
		<Layout.Footer className={className}>
			<span>{text}</span>
			{styles}
		</Layout.Footer>
	);
};

// @ts-ignore
export default React.memo(CustomFooter);
