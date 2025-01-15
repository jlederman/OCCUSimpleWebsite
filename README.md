## Set up instructions for this solution: 
1) Clone the repository on to a Windows machine via `git clone git@github.com:jlederman/OCCUSimpleWebsite.git`
2) Change directory so you're located in the solution folder: `cd OCCUSimpleWebsite`
3) Run the solution with `dotnet run --project CrudProject`
4) Navigate to http://localhost:5046/

## Notes

.NET 9 must be installed on the machine running the solution. This can be downloaded from https://dotnet.microsoft.com/en-us/download/dotnet/9.0

Part of the spec states "There are to be two sources of data, and each should be accessible". There is a separate branch for this titled `DiscreteStatusListTable`. When I initially set up the project, I visualized the editable CRUD page and the readonly Status list page mirroring data. When an item on the CRUD page is changed, its updated information is displayed on the readonly status page. However, this doesn't meet the spec. Therefore, I created a separate branch to seed the database with a separate, populated table specifically for the status page. Given the project architecture, this was a quick addition utilizing the generic function in the database seeder, which takes any enum and picks a constant at random until an iterator is finished. Running the `DiscreteStatusListTable` branch requires dropping the database before running the project. 