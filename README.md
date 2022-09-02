# RTLMaze

## Intro

Scrapes information from the [TVmaze API](https://www.tvmaze.com/api) and make it availible trough our own API. Allowing us to change the datamodels, and combine additional information from other sources in the future.  

## Known issues

### OData REST Protocol

Preferably we would have uses the OData rest protocol. C#.NET has a default package which we can include to support a lot of the OData format with minimal effort ( such as paging, filtering and adding metadata to our requests ). Sadly I've only started working with this since yesterday and am still encountering a several issues; thus I've chosen to custom build some of the functionality for now trying to follow the OData patterns as close as possible. 

This is not preferred though, with a little bit more experience/time I would have used the OData package instead. 

### Entity Framework

I've encountered multiple issues with Entity Framework causing me to take some shortcuts ( As I wasn't able to fix them in foreseeable time ). 

* Model access ( private / protected ) is more open than preferred
* Model decoupeling is not as preferred ( Found out that I cannot make collections of interfaces )
* The *Cast* table has an own auto-generated ID to be able to store it properly. Normally I would have made this keyless, but trying with the model builder I've keep encountering issues.
* Lazy Loading; big hmm... I like the functionality but it can cause a lot of memory issues when requesting big datasets. Normally in your repository you would detatch your entities with *AsNoTracking* so your db doesn't no longer need to keep track of those entities as you wont plan on modifying them. Detatching them however also seemed to disable lazy loading, which in result has an impact on the chosen code structure. 

### Many more
....