import Next from "next";
import React from "react";
import Post from "./Post";

interface IPostListInterface {
	posts: IPostInterface[];
}

const PostList: Next.NextSFC<IPostListInterface> = ({ posts }) => {
	return (
		<>
			{posts.map((post) => (
				<Post key={post.id} post={post} />
			))}
		</>
	);
};

export default PostList;
