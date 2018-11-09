import Next from "next";
import React from "react";
import { Segment } from "../Shared/Segment";
import Comment from "./Comment";
import CommentInput from "./CommentInput";

interface ICommentListInterface {
	comments: ICommentInterface[];
}

const CommentList: Next.NextSFC<ICommentListInterface> = ({ comments }) => {
	return (
		<Segment>
			{comments.length > 0
				? comments.map((comment) => <Comment key={comment.id} {...comment} />)
				: "Нет комментариев."}
			<CommentInput />
		</Segment>
	);
};

export default CommentList;
