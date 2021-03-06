import React, { useState } from "react";
import Auth from "../../lib/Auth";
import { Segment } from "../Shared/Segment";
import Comment from "./Comment";
import CommentInput from "./CommentInput";

interface ICommentListInterface {
  post_id: number;
  comments: ICommentInterface[];
}

const CommentList: React.FunctionComponent<ICommentListInterface> = ({
  comments,
  post_id
}) => {
  const [commentsState, setCommentsState] = useState(comments);

  const handleInput = (newComment: ICommentInterface) => {
    setCommentsState([...commentsState, newComment]);
  };

  return (
    <Segment>
      {commentsState.length > 0
        ? commentsState.map(comment => (
            <Comment key={comment.id} {...comment} />
          ))
        : "Нет комментариев."}
      {Auth.isAuthenticated() && (
        <CommentInput handleInput={handleInput} post_id={post_id} />
      )}
    </Segment>
  );
};

export default CommentList;
