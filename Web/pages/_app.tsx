import LogRocket from "logrocket";
import setupLogRocketReact from "logrocket-react";
import App, { Container } from "next/app";
import Router from "next/router";
import NProgress from "nprogress";
import React from "react";

import { Layout, LocaleProvider } from "antd";
import "antd/dist/antd.css";
import ru_RU from "antd/lib/locale-provider/ru_RU";
import Head from "next/head";
import CustomContent from "../components/Main/CustomContent";
import CustomFooter from "../components/Main/CustomFooter";
import CustomHeader from "../components/Main/CustomHeader";

Router.events.on("routeChangeStart", () => {
	NProgress.start();
});
Router.events.on("routeChangeComplete", () => NProgress.done());
Router.events.on("routeChangeError", () => NProgress.done());

export default class MyApp extends App {
	componentDidMount() {
		LogRocket.init(process.env.LOGROCKET_APP_ID);
		setupLogRocketReact(LogRocket);
	}

	render() {
		const { Component, pageProps, router } = this.props;
		return (
			<Container>
				<Head>
					<title>Дневник снов</title>
				</Head>
				<LocaleProvider locale={ru_RU}>
					<Layout>
						<CustomHeader currentRoute={router.pathname} />
						<CustomContent>
							<Component {...pageProps} />
						</CustomContent>
						<CustomFooter text="Dmatrix, 2018. Все права защищены." />
					</Layout>
				</LocaleProvider>
			</Container>
		);
	}
}
