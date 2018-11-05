import LogRocket from "logrocket";
import setupLogRocketReact from "logrocket-react";
import App, { Container } from "next/app";
import React from "react";

import { Layout, LocaleProvider } from "antd";
import ru_RU from "antd/lib/locale-provider/ru_RU";
import Head from "next/head";
import CustomFooter from "../components/CustomFooter";
import CustomHeader from "../components/CustomHeader";

export default class MyApp extends App {
	componentDidMount() {
		LogRocket.init(process.env.LOGROCKET_APP_ID);
		setupLogRocketReact(LogRocket);
	}

	render() {
		const { Component, pageProps, router } = this.props;
		console.log(router);
		return (
			<Container>
				<Head>
					<title>Дневник снов</title>
				</Head>
				<LocaleProvider locale={ru_RU}>
					<Layout>
						<CustomHeader />
						<Layout.Content>
							<Component {...pageProps} />
						</Layout.Content>
						<CustomFooter text="Dmatrix, 2018. Все права защищены." />
					</Layout>
				</LocaleProvider>
			</Container>
		);
	}
}
