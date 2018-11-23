import { Button, Input } from "antd";
import Next from "next";
import React, { useState } from "react";
import css from "styled-jsx/css";
import query from "../../lib/query";

// language=CSS
const { className, styles } = css.resolve`
	button {
			margin-top: 10px;
	}
`;

const CommentInput: Next.NextSFC<{
	post_id: number;
	handleInput: (newComment: ICommentInterface) => void;
}> = ({ post_id, handleInput }) => {
	const handleChange = (e: any) => {
		setInput(e.currentTarget.value);
	};

	const handleClick = async () => {
		const res = await query.post(
			`http://localhost:5000/api/post/${post_id}/comment`,
			{
				content: input,
			}
		);
		setInput("");
		handleInput(res.data);
	};

	const [input, setInput] = useState("");
	return (
		<>
			<Input.TextArea
				autosize={{ minRows: 6, maxRows: 10 }}
				onChange={handleChange}
				value={input}
			/>
			<Button
				onClick={handleClick}
				className={className}
				htmlType={"button"}
				type="primary"
			>
				Добавить комментарий
			</Button>
			{styles}
		</>
	);
};

export default CommentInput;
