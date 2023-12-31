# How to run
Open the solution in Visual Studio and click the run button. This will open a new tab with the swagger doc displayed
There will be a single API endpoint `GET /HackerNews/{numberOfStories}`. Try this out by passing across the number of stories you want to retrieve.

# Assumptions:
- there is no upper limit to the number of stories requested, unless it is imposed by HackerNews. For example, if HackerNews limits to 500, and you request 1000, only 500 will be returned.

# Enhancements:
- Boundary check for an upper limit of stories that can be retrieved at once. Primarly this would be for testing so that we know more about the API we are integrating with.
- Introduce a retry policy on any stories that failed to be retrieved. This should be run after the initial story collection by collecting all failed IDs and attempting another GET with them.
	- This retry policy may have subsequent retries (typically 3), or it could log that n requests have failed.
- Try to identify a more performant API endpoint from HackerNews so that instead of sending n+1 requests, we could potentially make use of a list endpoint.
- Introduce observability and monitoring within the `HackerNewsService` so that there's telemetry on performance metrics
- Try/catch handling around integration points. As the test doesn't give insight into what exceptions could be raised by HN, this would require some research.
- As further development takes places, look to extract the interface and model layers into their own project to reduce the responsiblity within the API project