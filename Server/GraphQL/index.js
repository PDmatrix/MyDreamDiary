const { ApolloServer, gql } = require("apollo-server");
require("isomorphic-unfetch");
require("es6-promise").polyfill();
require("dotenv").config();

const getPage = async (index) => {
	const url = `http://localhost:5000/api/page/get_page?index=${index}`; // TODO: Change localhost to actual url
	const response = await fetch(url);
	return await response.json();
};

const getUser = async (id) => {
	const url = `http://localhost:5000/api/user/get_user?id=${id}`; // TODO: Change localhost to actual url
	const response = await fetch(url);
	return await response.json();
};

const getPost = async (id) => {
	const url = `http://localhost:5000/api/post/get_post?id=${id}`; // TODO: Change localhost to actual url
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

      comment: [Comment]
      dream: Dream
      user: User
      postTag: [PostTag]
  }

  type Tag {
      id: ID!
      name: String!
      
      postTag: [PostTag]
  }

  type Comment {
      id: ID!
      content: String
      dateCreated: String

      post: Post
      user: User
  }

  type Dream {
      id: ID!
      content: String
      dreamDate: String

      post: Post
  }

  type User {
      id: ID!
      name: String!
      email: String!

      comment: [Comment]
      post: [Post]
  }

  type PostTag {
      id: ID!

      post: Post
      tag: Tag
  }

  type Query {
    getPage(index: Int = 0): Page!
    getUser(id: Int!): User!
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
