import Next from "next";
import React, { useState } from "react";
import query from "../../lib/query";
import { Segment } from "../Shared/Segment";
import Dream from "./Dream";

const DreamList: Next.NextSFC<{ dreams: IDreamInterface[] }> = (props) => {
	const [dreams, setDreams] = useState(props.dreams);
	const handleDelete = async (id: number) => {
		const res = await query.del(`http://localhost:5000/api/user/dream/${id}`);
		const deletedDream: IDreamInterface = await res.data;
		const dreamArray = [...dreams]; // make a separate copy of the array
		const index = dreamArray.findIndex((r) => r.id === deletedDream.id);
		if (index !== -1) {
			dreamArray.splice(index, 1);
			setDreams(dreamArray);
		}
	};

	return (
		<Segment>
			{dreams.length > 0
				? dreams.map((dream) => (
						<Dream deleteDream={handleDelete} key={dream.id} dream={dream} />
				  ))
				: ""}
		</Segment>
	);
};

export default DreamList;
