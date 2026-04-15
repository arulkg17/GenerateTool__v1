export class ProductService {
	apiurl = "api/Product";

	getAll()
	{
		return this.http.gey(this.apiurl);
	}
}