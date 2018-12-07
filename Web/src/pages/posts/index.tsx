import { BackTop, Pagination, Spin } from "antd";
import axios from "axios";
import React, { useState } from "react";
import PostList from "../../components/Page/PostList";
import Auth from "../../lib/Auth";
import useFetch from "../../lib/useFetch";

const Post: React.FunctionComponent = () => {
  const [page, setPage] = useState(1);
  const { data, loading, setData } = useFetch<IPageInterface>(`/page/1`, "get");
  const changePage = async (newPage: number) => {
    setPage(newPage);
    const authorization = "Bearer " + Auth.getUserToken();
    const res = await axios.get(`${process.env.API_URL}/api/page/${newPage}`, {
      headers: { Authorization: authorization }
    });
    setData(await res.data);
  };

  return (
    <>
      {!loading ? (
        <>
          <BackTop />
          <PostList posts={data.records} />
          <Pagination
            onChange={changePage}
            current={page}
            total={data.total_pages * 10}
          />
        </>
      ) : (
        <Spin />
      )}
    </>
  );
};

export default Post;
