import React from "react";
import Post from "./Post";

interface IPostListInterface {
  posts: IPostInterface[];
}

const PostList: React.FunctionComponent<IPostListInterface> = ({ posts }) => {
  return (
    <>
      {posts.map(post => (
        <Post key={post.id} post={post} />
      ))}
    </>
  );
};

export default PostList;
