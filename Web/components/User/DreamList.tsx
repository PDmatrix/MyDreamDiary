import Next from "next";
import React from "react";
import { Segment } from "../Shared/Segment";
import Dream from "./Dream";

const DreamList: Next.NextSFC<{ dreams: IDreamInterface[] }> = (props) => {
	return (
		<Segment>
			{props.dreams.length > 0
				? props.dreams.map((dream) => <Dream key={dream.id} {...dream} />)
				: ""}
		</Segment>
	);
};

export default DreamList;
