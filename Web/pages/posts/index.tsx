import { BackTop, Pagination } from "antd";
import Next from "next";
import React, { useState } from "react";
import PostList from "../../components/Page/PostList";
import query from "../../lib/query";

const loadPageDate = async (index = 1) => {
	const res = await query.get(`http://localhost:5000/api/page/${index}`);
	return await res.data;
};

const Post: Next.NextSFC<IPageInterface> = (props) => {
	const [page, setPage] = useState(props.current_page);
	const [records, setRecords] = useState(props.records);

	const changePage = async (newPage: number) => {
		setPage(newPage);
		const newProps: IPageInterface = await loadPageDate(newPage);
		setRecords(newProps.records);
	};

	return (
		<>
			<BackTop />
			<PostList posts={records} />
			<Pagination
				onChange={changePage}
				current={page}
				total={props.total_pages * 10}
			/>
		</>
	);
};

Post.getInitialProps = async () => {
	return await loadPageDate();
};

export default Post;
