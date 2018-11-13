import Next from "next";
import React from "react";
import { Segment } from "../Shared/Segment";
import Comment from "./Comment";
import CommentInput from "./CommentInput";
import Auth from "../../lib/Auth";

interface ICommentListInterface {
	comments: ICommentInterface[];
}

const CommentList: Next.NextSFC<ICommentListInterface> = ({ comments }) => {
	const auth = Auth.getInstance();
	return (
		<Segment>
			{comments.length > 0
				? comments.map((comment) => <Comment key={comment.id} {...comment} />)
				: "Нет комментариев."}
			{auth.isAuthenticated() && <CommentInput />}
		</Segment>
	);
};

export default CommentList;
