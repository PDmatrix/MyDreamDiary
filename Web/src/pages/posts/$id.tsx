import { Spin } from "antd";
import React from "react";
import { RouteComponentProps } from "react-router";
import CommentList from "../../components/Post/CommentList";
import Post from "../../components/Post/Post";
import useFetch from "../../lib/useFetch";

const PostEntry: React.FunctionComponent<
  RouteComponentProps<{ id: string }>
> = props => {
  const { data, loading } = useFetch<IPostInterface>(
    `/post/${props.match.params.id}`,
    "get"
  );
  return (
    <>
      {!loading ? (
        <>
          <Post {...data} />
          <CommentList post_id={data.id} comments={data.comments} />{" "}
        </>
      ) : (
        <Spin />
      )}
    </>
  );
};

export default PostEntry;
