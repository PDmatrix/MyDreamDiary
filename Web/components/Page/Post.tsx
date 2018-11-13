import { Divider, Tag } from "antd";
import moment from "moment";
import Next from "next";
import Link from "next/link";
import React, { useState } from "react";
import CommentIcon from "../Shared/CommentIcon";
import HeartIcon from "../Shared/HeartIcon";
import { Segment } from "../Shared/Segment";
import Auth from "../../lib/Auth";

interface IConcretePost {
	post: IPostInterface;
}

const Post: Next.NextSFC<IConcretePost> = (props) => {
	const auth = Auth.getInstance();
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
		<Segment>
			<Link
				prefetch={true}
				href={`/posts/entry?id=${post.id}`}
				as={`/posts/${post.id}`}
			>
				<a>
					<h3>{post.title}</h3>
				</a>
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
			{auth.isAuthenticated() && (
				<>
					<a onClick={likeClick} className={"no-selection"}>
						<HeartIcon liked={post.is_liked} />
						{post.likes_count}
					</a>
					<Divider type="vertical" />
				</>
			)}
			<Link
				prefetch={true}
				href={`/posts/entry?id=${post.id}#comments`}
				as={`/posts/${post.id}#comments`}
			>
				<a className={"no-selection"}>
					<CommentIcon />
					{post.comments_count}
				</a>
			</Link>
			<style jsx={true}>{`
				a.no-selection {
					-webkit-user-select: none;
					-moz-user-select: none;
					-khtml-user-select: none;
					-ms-user-select: none;
				}
			`}</style>
		</Segment>
	);
};

export default Post;
