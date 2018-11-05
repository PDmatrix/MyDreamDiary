import Next from "next";
import React from "react";

interface ISegmentInterface {
	children?: any;
}

export const Segment: Next.NextSFC<ISegmentInterface> = ({ children }) => {
	return (
		<div>
			<style jsx={true}>
				{`
					div {
						border: 1px solid rgba(34, 36, 38, 0.15);
						box-shadow: 0 1px 2px 0 rgba(34, 36, 38, 0.15);
						border-radius: 0.28571429rem;
						padding: 1em 1em;
						margin: 1em 1em;
					}
				`}
			</style>
			{children}
		</div>
	);
};
