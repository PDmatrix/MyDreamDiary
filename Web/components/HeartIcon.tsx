import { Icon } from "antd";
import Next from "next";
import React from "react";
import css from "styled-jsx/css";

// language=CSS
const { className, styles } = css.resolve`
	i {
		color: red;
		padding: 0 5px;
		font-size: 16px;
	}
`;

interface IHeartIconInterface {
	liked: boolean;
}

const HeartIcon: Next.NextSFC<IHeartIconInterface> = ({ liked }) => {
	return (
		<>
			<Icon
				type="heart"
				theme={liked ? "filled" : "outlined"}
				className={className}
			/>
			{styles}
		</>
	);
};

export default HeartIcon;
