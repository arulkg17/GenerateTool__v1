export class Service {
	apiurl = "api/";

	getAll()
	{
		return this.http.gey(this.apiurl);
	}
}