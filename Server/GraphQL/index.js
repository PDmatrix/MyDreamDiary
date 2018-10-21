const { ApolloServer, gql } = require("apollo-server");
require("isomorphic-unfetch");
require("es6-promise").polyfill();
require("dotenv").config();

const getPage = async (index) => {
	const url = `http://localhost:5000/api/page/getpage?index=${index != undefined ? index : 0}`; // TODO: Change localhost to actual url
	const response = await fetch(url);
	return await response.json();
};

const getUser = async (id) => {
	const url = `http://localhost:5000/api/user/getuser?id=${id}`; // TODO: Change localhost to actual url
	const response = await fetch(url);
	return await response.json();
};

const getPost = async (id) => {
	const url = `http://localhost:5000/api/post/getpost?id=${id}`; // TODO: Change localhost to actual url
	const response = await fetch(url);
	return await response.json();
};

const typeDefs = gql`
  type Page {
      currentPage: Int!
      pageSize: Int!
      totalPages: Int!
      records: [Post]
  }

  type Post {
      id: ID!
      likesCount: Int
      dateCreated: String!
      title: String!
      comments: [Comment]
      tags: [Tag]
      dream: Dream
      identityUser: IdentityUser
  }

  type Tag {
      id: ID!
      name: String!
      postId: Int!
  }

  type Comment {
      id: ID!
      content: String
      dateCreated: String
      post: Post
      identityUser: IdentityUser
  }

  type Dream {
      id: ID!
      content: String
      dreamDate: String
      post: Post
  }

  type IdentityUser {
      id: ID!
      name: String!
      email: String!
      comments: [Comment]
      posts: [Post]
  }

  type Query {
    getPage(index: Int): Page!
    getUser(id: Int!): IdentityUser!
    getPost(id: Int!): Post!
  }
`;

// Resolvers define the technique for fetching the types in the
// schema.  We'll retrieve books from the "books" array above.
const resolvers = {
	Query: {
		getPage: async (_, { index }) => await getPage(index),
		getUser: async (_, { id }) => await getUser(id),
		getPost: async (_, { id }) => await getPost(id),
	},
};

// In the most basic sense, the ApolloServer can be started
// by passing type definitions (typeDefs) and the resolvers
// responsible for fetching the data for those types.
const server = new ApolloServer({
	typeDefs, 
	resolvers,
	engine: {
		apiKey: process.env.GRAPHQL_APIKEY
	},
	introspection: true
});

// This `listen` method launches a web-server.  Existing apps
// can utilize middleware options, which we'll discuss later.
server.listen().then(({ url }) => {
	console.log(`Server ready at ${url}`);
});