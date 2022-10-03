# GitHubActionsSample
An example solution with api &amp; NuGet package which shows the part of GitHub workflows functionality.

In the repo you can find a .NET solution with two projects:

- [**GitHubActionsSample.Api** project](https://github.com/blaze6950/GitHubActionsSample/tree/master/src/GitHubActionsSample.Api) - it contains a simple API implementation with the usage of Swagger. Also, it uses the Nuget package from GPR.
  - You can find [here](https://github.com/blaze6950/GitHubActionsSample/blob/f58de3856e71a747cb478c6065e372560e1d29b1/src/GitHubActionsSample.Api/GitHubActionsSample.Api.csproj#L12), that this projects has a dependency to the GitHubActionsSample.Package. There is no direct reference to the project in the solution. It is restored from the GPR.
  - The Package source is configured in the [nuget.config](https://github.com/blaze6950/GitHubActionsSample/blob/master/nuget.config) file. There are also default nuget sources for restoring other packages.
- [**GitHubActionsSample.Package** project](https://github.com/blaze6950/GitHubActionsSample/tree/master/packages/GitHubActionsSample.Package) - it contains a sample implementation with the Weather provider service. It is used for demonstrating how to build Nuget packages in GitHub Workflows and then use them in other projects.

> Note: it isn't a best practice to store package projects with the client projects. It was done for the simplicity to show the basic GitHub Actions/Workflows functionality.

---

# Workflows description
### [API Workflow](https://github.com/blaze6950/GitHubActionsSample/blob/master/.github/workflows/api.yml)
This workflow has two jobs:

- ["build-and-test"](https://github.com/blaze6950/GitHubActionsSample/blob/f58de3856e71a747cb478c6065e372560e1d29b1/.github/workflows/api.yml#L23) - accordingly to it's name its obviously that this job performs building and running tests for the GitHubActionsSample.Api project.
  - It uses the "actions/checkout@v3" action for downloading the repo code to the job machine for further actions with it.
  - It uses the "actions/cache@v3.0.8" action for caching the the downloaded & builded code. It helps to avoid downloading and building on the next job. This steps is executed in case, when the workflow is performed on the `master` branch, when we possibly will publish Docker image to the GDR. Otherwise, this step is omitted.
  - It uses the "actions/setup-dotnet@v2.1.1" action for installing a .Net SDK on to the job machine. It allows to use the "dotnet" command in further acions.
  - It uses the run step with name "Put github token for the auth into GPR". This command replaces a placeholder(`Token`) in the nuget.config file with the valid GITHUB_TOKEN for authenticating into GPR(This is required for restoring packages, namely GitHubActionsSample.Package).
  - Also, almost the same command is used after the building and testing steps - but the valid GITHUB_TOKEN is replaced back to the placeholder(`TOKEN`). This is required due to the lifetime of the GITHUB_TOKEN - it is valid only within a single job execution. And we Need to roll back it to placeholder because we need to restore packages again in the next job.
- ["publish"](https://github.com/blaze6950/GitHubActionsSample/blob/f58de3856e71a747cb478c6065e372560e1d29b1/.github/workflows/api.yml#L50) - accordingly to it's name this job builds a Docker image and publishes it to the Github Docker registry. This job is executed only when the workflow is executed on the `master` branch. Also, this job requires an approve from a user, which is specified in the `production` environment. Also, this job is performed only after the "build-and-test" job is successfully finished.
  - It uses the "actions/cache@v3.0.8" action for restoring the downloaded & built code from cache(It was cached during the previous job). It helps to avoid the performing of redundant work.
  - It uses the run command for replacing the placeholder in the nuget.config file to the valid GITHUB_TOKEN. As it was mentioned in the description of the previous job, the GITHUB_TOKEN has lifetime only within a single job. That's why we need to set it here again.
  - It uses the "VaultVulp/gp-docker-action@1.1.8" action which is community built(Anyone can build his own actions and share with the community). It is useful for building a Docker image using Dockerfile in the repo and then publish it to the GitHub Docker Registry.

### [Package Workflow](https://github.com/blaze6950/GitHubActionsSample/blob/master/.github/workflows/package.yml)
This workflow also has two jobs:

- ["build-and-test"](https://github.com/blaze6950/GitHubActionsSample/blob/f58de3856e71a747cb478c6065e372560e1d29b1/.github/workflows/package.yml#L19)- accordingly to it's name its obviously that this job performs building and running tests for the GitHubActionsSample.Package project.
  - It uses the "actions/checkout@v3" action for downloading the repo code to the job machine for further actions with it.
  - It uses the "actions/cache@v3.0.8" action for caching the the downloaded & builded code. It helps to avoid downloading and building on the next job. This steps is executed in case, when the workflow is performed on the `master` branch, when we possibly will publish a Nuget package to the GPR. Otherwise, this step is omitted.
  - It uses the "actions/setup-dotnet@v2.1.1" action for installing a .Net SDK on to the job machine. It allows to use the "dotnet" command in further acions.
  - Two run steps in this job performs building and running tests.
- ["publish"](https://github.com/blaze6950/GitHubActionsSample/blob/f58de3856e71a747cb478c6065e372560e1d29b1/.github/workflows/package.yml#L35) - accordingly to it's name this job builds a NuGet package and publishes it to the Github Package Registry. This job is executed only when the workflow is executed on the `master` branch. Also, this job requires an approve from a user, which is specified in the `production` environment. Also, this job is performed only after the "build-and-test" job is successfully finished.
  - It uses the "actions/cache@v3.0.8" action for restoring the downloaded & built code from cache(It was cached during the previous job). It helps to avoid the performing of redundant work.
  - It uses run commands for executing the `dotnet pack` & `dotnet nuget push` commands.

>You can find more info in comments in the api.yml & package.yml files.
