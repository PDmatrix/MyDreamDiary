import { Button, Input } from "antd";
import React, { useState } from "react";
import enxios from "../../lib/enxios";
import styles from "./styles.css";

const CommentInput: React.FunctionComponent<{
  post_id: number;
  handleInput: (newComment: ICommentInterface) => void;
}> = ({ post_id, handleInput }) => {
  const handleChange = (e: any) => {
    setInput(e.currentTarget.value);
  };

  const handleClick = async () => {
    const res = await enxios.post(
      `http://localhost:5000/api/post/${post_id}/comment`,
      {
        content: input
      }
    );
    setInput("");
    handleInput(res.data);
  };

  const [input, setInput] = useState("");
  return (
    <>
      <Input.TextArea
        autosize={{ minRows: 6, maxRows: 10 }}
        onChange={handleChange}
        value={input}
      />
      <Button
        onClick={handleClick}
        className={styles.button}
        htmlType={"button"}
        type="primary"
      >
        Добавить комментарий
      </Button>
    </>
  );
};

export default CommentInput;
