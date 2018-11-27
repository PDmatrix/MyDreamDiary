import { Divider, Tag } from "antd";
import moment from "moment";
import React, { useState } from "react";
import Auth from "../../lib/Auth";
import HeartIcon from "../Shared/HeartIcon";
import { Segment } from "../Shared/Segment";
import styles from "./styles.css";

const Post: React.FunctionComponent<IPostInterface> = props => {
  const [post, setPost] = useState(props);

  const likeClick = (e: any) => {
    e.preventDefault();
    const isLike = !post.is_liked;
    setPost({
      ...post,
      is_liked: isLike,
      likes_count: isLike ? post.likes_count + 1 : post.likes_count - 1
    });
  };

  return (
    <Segment>
      <h3>{post.title}</h3>
      <p>
        Опубликовано{" "}
        {moment(post.date_created)
          .locale("ru")
          .format("L LT")}{" "}
        пользователем {post.username}
      </p>
      <Divider />
      <p>{post.content}</p>
      {post.tags.length !== 0 ? (
        <span>
          Теги:
          {post.tags.map((tag, id) => (
            <Tag key={id}>{tag}</Tag>
          ))}
        </span>
      ) : (
        ""
      )}
      {Auth.isAuthenticated() && (
        <>
          <Divider />
          <a onClick={likeClick} className={styles.noSelection}>
            <HeartIcon liked={post.is_liked} />
            {post.likes_count}
          </a>
        </>
      )}
    </Segment>
  );
};

export default Post;
