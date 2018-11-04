module.exports = {
    server: {
        introspection: true,
        playground: true,
        engine: {
            apiKey: process.env.GRAPHQL_APIKEY
        }
    },
};