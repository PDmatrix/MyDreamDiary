import { Divider, Tag } from "antd";
import axios from "axios";
import moment from "moment";
import React, { useState } from "react";
import Link from "umi/link";
import Auth from "../../lib/Auth";
import CommentIcon from "../Shared/CommentIcon";
import HeartIcon from "../Shared/HeartIcon";
import { Segment } from "../Shared/Segment";
import styles from "./styles.css";

interface IConcretePost {
  post: IPostInterface;
}

const Post: React.FunctionComponent<IConcretePost> = props => {
  const [post, setPost] = useState(props.post);

  const likeClick = async (e: any) => {
    e.preventDefault();
    const isLike = !post.is_liked;
    setPost({
      ...post,
      is_liked: isLike,
      likes_count: isLike ? post.likes_count + 1 : post.likes_count - 1
    });
    await axios.post(`/post/${post.id}/like`, {});
  };

  return (
    <Segment>
      <Link to={`/posts/${post.id}`}>
        <h3>{post.title}</h3>
      </Link>
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
      <Divider />
      {Auth.isAuthenticated() && (
        <>
          <a onClick={likeClick} className={styles.noSelection}>
            <HeartIcon liked={post.is_liked} />
            {post.likes_count}
          </a>
          <Divider type="vertical" />
        </>
      )}
      <Link className={styles.noSelection} to={`/posts/${post.id}#comments`}>
        <CommentIcon />
        {post.comments_count}
      </Link>
    </Segment>
  );
};

export default Post;
