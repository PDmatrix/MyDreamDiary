import { Alert, Button, DatePicker, Input } from "antd";
import React, { useState } from "react";
import enxios from "../../lib/enxios";

const CreateDream: React.FunctionComponent<{
  handleInput: (newComment: IDreamInterface) => void;
}> = ({ handleInput }) => {
  const handleChange = (e: any) => {
    setInput(e.currentTarget.value);
  };

  const handleDateChange = ({}, dateString: string) => {
    setInputDate(dateString);
  };

  const handleClick = async () => {
    let res;
    try {
      res = await enxios.post(`http://localhost:5000/api/user/dream`, {
        content: input,
        dream_date: inputDate
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
    handleInput(await res.data);
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
      <h3>Добавить сон:</h3>
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
