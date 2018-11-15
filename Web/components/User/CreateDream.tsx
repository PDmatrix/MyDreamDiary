import { Alert, Button, DatePicker, Input } from "antd";
import Next from "next";
import React, { useState } from "react";
import query from "../../lib/query";

const CreateDream: Next.NextSFC<{
	handleInput: (newComment: IDreamInterface) => void;
}> = ({ handleInput }) => {
	const handleChange = (e) => {
		setInput(e.currentTarget.value);
	};

	const handleDateChange = (date, dateString) => {
		setInputDate(dateString);
	};

	const handleClick = async () => {
		let res;
		try {
			res = await query.post(`http://localhost:5000/api/user/dream`, {
				content: input,
				dream_date: inputDate,
			});
		} catch (e) {
			setAlert(true);
			return;
		}
		if (res.status !== 201) {
			setAlert(true);
			return;
		}
		setInput("");
		handleInput(res.data);
	};

	const handleClose = () => {
		setAlert(false);
	};

	const [input, setInput] = useState("");
	const [inputDate, setInputDate] = useState("");
	const [alert, setAlert] = useState(false);
	return (
		<>
			{alert && (
				<Alert
					message="Ошибка."
					description="Ошибка при сохранении. Проверьте правильность заполнения всех полей."
					type="error"
					closable={true}
					onClose={handleClose}
				/>
			)}
			<DatePicker
				showTime={true}
				format="YYYY-MM-DD HH:mm:ss"
				placeholder={"Время сна"}
				onChange={handleDateChange}
			/>
			<br />
			<br />
			<Input.TextArea
				autosize={{ minRows: 6, maxRows: 10 }}
				onChange={handleChange}
				value={input}
			/>
			<br />
			<br />
			<Button onClick={handleClick} htmlType={"button"} type="primary">
				Добавить сон
			</Button>
		</>
	);
};

export default CreateDream;