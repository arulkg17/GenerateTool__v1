[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
	[HttpGet]
	public IActionResult GetAll()
	{
		return(Ok);
	}
}
