import axios from "axios";
import Next from "next";
import React from "react";
import CommentList from "../../components/Post/CommentList";
import Post from "../../components/Post/Post";

const loadPostDate = async (id) => {
	const res = await axios.get(`http://localhost:5000/api/post/${id}`);
	return await res.data;
};

const PostEntry: Next.NextSFC<IPostInterface> = (props) => {
	return (
		<>
			<Post {...props} />
			<CommentList comments={props.comments} />
		</>
	);
};

PostEntry.getInitialProps = async ({ query }) => {
	return await loadPostDate(query.id);
};

export default PostEntry;
