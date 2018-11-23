import Next from "next";
import React from "react";
import CommentList from "../../components/Post/CommentList";
import Post from "../../components/Post/Post";
import query from "../../lib/query";

const loadPostDate = async (id: any) => {
	const res = await query.get(`http://localhost:5000/api/post/${id}`);
	return await res.data;
};

const PostEntry: Next.NextSFC<IPostInterface> = (props) => {
	return (
		<>
			<Post {...props} />
			<CommentList post_id={props.id} comments={props.comments} />
		</>
	);
};

PostEntry.getInitialProps = async ({ query }) => {
	return await loadPostDate(query.id);
};

export default PostEntry;
