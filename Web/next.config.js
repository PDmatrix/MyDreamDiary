const withTypescript = require("next-with-typescript");
const withCss = require("@zeit/next-css");

// fix: prevents error when .css files are required by node
if (typeof require !== "undefined") {
	// eslint-disable-next-line no-unused-vars
	require.extensions[".css"] = (file) => {};
}

require("dotenv").config();

const path = require("path");
const Dotenv = require("dotenv-webpack");

module.exports = withCss(
	withTypescript({
		webpack: (config) => {
			config.plugins = config.plugins || [];

			config.plugins = [
				...config.plugins,

				// Read the .env file
				new Dotenv({
					path: path.join(__dirname, ".env"),
					systemvars: true,
				}),
			];

			return config;
		},
	})
);
