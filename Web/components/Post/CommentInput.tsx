import { Button, Input } from "antd";
import Next from "next";
import React, { useState } from "react";
import css from "styled-jsx/css";

// language=CSS
const { className, styles } = css.resolve`
	button {
			margin-top: 10px;
	}
`;

const CommentInput: Next.NextSFC = () => {
	const handleChange = (e) => {
		setInput(e.currentTarget.value);
	};

	const [input, setInput] = useState("");
	return (
		<>
			<Input.TextArea
				autosize={{ minRows: 6, maxRows: 10 }}
				onChange={handleChange}
				value={input}
			/>
			<Button className={className} htmlType={"button"} type="primary">
				Добавить комментарий
			</Button>
			{styles}
		</>
	);
};

export default CommentInput;
