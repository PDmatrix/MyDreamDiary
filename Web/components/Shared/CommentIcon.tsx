import { Icon } from "antd";
import Next from "next";
import React from "react";
import css from "styled-jsx/css";

// language=CSS
const { className, styles } = css.resolve`
	i {
		color: #1890ff;
		padding: 0 5px;
		font-size: 16px;
    transition: all ease 0.4s;
	}
`;

const CommentIcon: Next.NextSFC = () => {
	return (
		<>
			<Icon type="message" theme={"outlined"} className={className} />
			{styles}
		</>
	);
};

export default CommentIcon;
