import { Divider } from "antd";
import moment from "moment";
import Next from "next";
import React from "react";

const Comment: Next.NextSFC<ICommentInterface> = (props) => {
	return (
		<div>
			<h4>{props.username}</h4>
			<span>
				{moment(props.date_created)
					.locale("ru")
					.format("L LT")}{" "}
			</span>
			<p>{props.content}</p>
			<Divider />
		</div>
	);
};

export default Comment;
