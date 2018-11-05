import { Divider, Tag } from "antd";
import moment from "moment";
import Next from "next";
import Link from "next/link";
import React, { useState } from "react";
import CommentIcon from "./CommentIcon";
import HeartIcon from "./HeartIcon";
import { Segment } from "./Segment";

interface IConcretePost {
	post: IPostInterface;
}

const Post: Next.NextSFC<IConcretePost> = (props) => {
	const [post, setPost] = useState(props.post);

	const likeClick = (e) => {
		e.preventDefault();
		const isLike = !post.is_liked;
		setPost({
			...post,
			is_liked: isLike,
			likes_count: isLike ? post.likes_count + 1 : post.likes_count - 1,
		});
	};

	return (
		<Segment key={post.id}>
			<Link prefetch={true} href={`/posts/${post.id}`}>
				<a>
					<h3>{post.title}</h3>
				</a>
			</Link>
			<p>
				Опубликовано{" "}
				{moment(post.date_created)
					.locale("ru")
					.format("LLL")}{" "}
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
			<a onClick={likeClick}>
				<HeartIcon liked={post.is_liked} />
				{post.likes_count}
			</a>
			<Divider type="vertical" />
			<a onClick={() => console.log("comment")}>
				<CommentIcon />
				{post.comments_count}
			</a>
		</Segment>
	);
};

export default Post;
