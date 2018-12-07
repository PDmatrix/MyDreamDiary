import axios from "axios";
import React, { useState } from "react";
import { Segment } from "../Shared/Segment";
import Dream from "./Dream";

const DreamList: React.FunctionComponent<{
  dreams: IDreamInterface[];
}> = props => {
  const [dreams, setDreams] = useState(props.dreams);
  const handleDelete = async (id: number) => {
    const dreamArray = [...dreams]; // make a separate copy of the array
    const index = dreamArray.findIndex(r => r.id === id);
    if (index !== -1) {
      dreamArray.splice(index, 1);
      setDreams(dreamArray);
      await axios.delete(`/user/dream/${id}`);
    }
  };

  return (
    <Segment>
      {dreams.length > 0
        ? dreams.map(dream => (
            <Dream deleteDream={handleDelete} key={dream.id} dream={dream} />
          ))
        : ""}
    </Segment>
  );
};

export default DreamList;
