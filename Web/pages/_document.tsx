import Document, { Head, Main, NextScript } from "next/document";
import React from "react";

export default class CustomDocument extends Document {
	render() {
		return (
			<html>
				<Head>
					<meta name="viewport" content="width=device-width, initial-scale=1" />
					<meta charSet="utf-8" />
					<link rel="stylesheet" type="text/css" href="/static/nprogress.css" />
				</Head>
				<body>
					<Main />
					<NextScript />
				</body>
			</html>
		);
	}
}
