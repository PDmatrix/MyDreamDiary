import { Layout } from "antd";
import Next from "next";
import React from "react";
import css from "styled-jsx/css";

// language=CSS
const { className, styles } = css.resolve`
	div {
		padding: 20px 50px;
	}
`;

interface ICustomContent {
	children: object;
}

const CustomContent: Next.NextSFC<ICustomContent> = ({ children }) => {
	return (
		<Layout.Content className={className}>
			<div>
				{children}
				<style jsx={true}>{`
					div {
						padding: 24px;
						background: #fff;
						border-radius: 20px;
						min-height: 280px;
					}
				`}</style>
			</div>
			{styles}
		</Layout.Content>
	);
};

// @ts-ignore
export default React.memo(CustomContent);
