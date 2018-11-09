import Next from "next";
import React from "react";
import { Segment } from "../components/Shared/Segment";

const Index: Next.NextSFC = () => {
	return (
		<Segment>
			<p>Hello</p>
		</Segment>
	);
};

/*Index.getInitialProps = async () => {
	const res = await axios.get("https://api.github.com/repos/zeit/next.js");
	return { elems: res.data };
};*/

export default Index;
