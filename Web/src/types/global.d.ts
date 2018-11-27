declare module "*.css";

interface IPostInterface {
  title: string;
  content: string;
  username: string;
  comments?: ICommentInterface[];
  likes_count: number;
  date_created: string;
  tags: string[];
  id: number;
  is_liked: boolean;
  comments_count: number;
}

interface ICommentInterface {
  id: number;
  content: string;
  date_created: string;
  username: string;
}

interface IPageInterface {
  current_page: number;
  page_size: number;
  records: IPostInterface[];
  total_pages: number;
}

interface IDreamInterface {
  id: number;
  content: string;
  dream_date: string;
}
