import { Alert, Button, Divider, Input, Modal } from "antd";
import axios from "axios";
import moment from "moment";
import React, { useState } from "react";

const Dream: React.FunctionComponent<{
  dream: IDreamInterface;
  deleteDream: (id: number) => void;
}> = props => {
  const handleDelete = async () => {
    await props.deleteDream(props.dream.id);
  };

  const handlePublish = async () => {
    await axios.post("/post", {
      dream_id: props.dream.id,
      title: input
    });
    setModal(false);
    setAlert(true);
    setInput("");
  };

  const handleKeyDown = (e: any) => {
    setInput(e.currentTarget.value);
  };

  const showModal = () => {
    setModal(true);
  };

  const handleClose = () => {
    setAlert(false);
  };

  const handleCancel = () => {
    setModal(false);
  };

  const [modal, setModal] = useState(false);
  const [alert, setAlert] = useState(false);
  const [input, setInput] = useState("");

  return (
    <>
      {alert && (
        <Alert
          message="Успешно"
          description="Запись опубликована."
          type="success"
          closable={true}
          onClose={handleClose}
        />
      )}
      <h3>{props.dream.content}</h3>
      <span>
        Дата:{" "}
        {moment(props.dream.dream_date)
          .locale("ru")
          .format("L LT")}{" "}
      </span>
      <br />
      <Button onClick={showModal} type={"primary"} htmlType={"button"}>
        Опубликовать сон
      </Button>
      <Divider type={"vertical"} />
      <Button onClick={handleDelete} type={"danger"} htmlType={"button"}>
        Удалить сон
      </Button>
      <Divider />
      <Modal
        title="Введите заголовок записи"
        visible={modal}
        onOk={handlePublish}
        onCancel={handleCancel}
      >
        <Input onKeyUp={handleKeyDown} />
      </Modal>
    </>
  );
};

export default Dream;
