# RTLMaze

## Intro

Scrapes information from the [TVmaze API](https://www.tvmaze.com/api) and make it availible trough our own API. Allowing us to change the datamodels, and combine additional information from other sources in the future.  

## Known issues

### OData REST Protocol

Preferably we would have uses the OData rest protocol. C#.NET has a default package which we can include to support a lot of the OData format with minimal effort ( such as paging, filtering and adding metadata to our requests ). Sadly I've only started working with this since yesterday and am still encountering a several issues; thus I've chosen to custom build some of the functionality for now trying to follow the OData patterns as close as possible. 

This is not preferred though, with a little bit more experience/time I would have used the OData package instead. 
