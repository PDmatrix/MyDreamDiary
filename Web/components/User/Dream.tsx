import { Alert, Button, Divider, Input, Modal } from "antd";
import moment from "moment";
import Next from "next";
import React, { useState } from "react";
import query from "../../lib/query";

const Dream: Next.NextSFC<{
	dream: IDreamInterface;
	deleteDream: (id: number) => void;
}> = (props) => {
	const handleDelete = async () => {
		await props.deleteDream(props.dream.id);
	};

	const handlePublish = async () => {
		await query.post("http://localhost:5000/api/post", {
			dream_id: props.dream.id,
			title: input,
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
			{alert && (
				<Alert
					message="Успешно"
					description="Запись опубликована."
					type="success"
					closable={true}
					onClose={handleClose}
				/>
			)}
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
