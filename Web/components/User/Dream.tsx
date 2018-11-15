import { Divider } from "antd";
import Next from "next";
import React from "react";
import moment from "moment";

const PostList: Next.NextSFC<IDreamInterface> = (props) => {
	return (
		<>
			<h3>{props.content}</h3>
			<span>
				Дата:{" "}
				{moment(props.dream_date)
					.locale("ru")
					.format("L LT")}{" "}
			</span>
			<Divider />
		</>
	);
};

export default PostList;
