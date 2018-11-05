interface IPostInterface {
	content: string;
	date_created: string;
	id: number;
	is_liked: boolean;
	likes_count: number;
	tags: string[];
	title: string;
	username: string;
	comments_count: number;
}

interface IPageInterface {
	current_page: number;
	page_size: number;
	records: IPostInterface[];
	total_pages: number;
}
