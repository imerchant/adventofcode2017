Param(
    [Parameter(Mandatory=$True)]
    [string]$projectName
)

mkdir -Path "src"
mkdir -Path "test"

$testName = ($projectName + ".Tests")
$projectLocation = ("src/" + $projectName)
$testLocation = ("test/" + $testName)
$projectCsProj = ($projectName + ".csproj")
$testCsProj = ($testName + ".csproj")
$projectSln = ($projectName + ".sln")

# Create projects
dotnet new classlib -n $projectName -o $projectLocation
dotnet new xunit -n $testName -o $testLocation

# Add references
dotnet add $testLocation/$testCsProj package FluentAssertions
dotnet add $testLocation/$testCsProj reference $projectLocation/$projectCsProj

# create solution
dotnet new sln -n $projectName
dotnet sln $projectSln add $projectLocation/$projectCsProj
dotnet sln $projectSln add $testLocation/$testCsProj

# restore and build
dotnet restore
dotnet build